using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using AutoMapper;
using DataBase.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Utility.Enums;
using Utility;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Colors;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;


namespace Logic.Service.Services
{
	public class CertificateOfAbsenceService : ICertificateOfAbsenceService
	{
		private readonly ICertificateOfAbsenceRepository _certificateOfAbsenceRepository;
		private readonly IMapper _mapper;
		private readonly IAssociationRepository _associationRepository;
		private readonly UserManager<User> _userManager;
		private readonly IParticipantsOfCertificateOfAbsenceRepository _participantsOfCertificateOfAbsenceRepository;

		public CertificateOfAbsenceService(
			ICertificateOfAbsenceRepository certificateOfAbsenceRepository,
			IMapper mapper,
			IAssociationRepository associationRepository,
			UserManager<User> userManager,
			IParticipantsOfCertificateOfAbsenceRepository participantsOfCertificateOfAbsenceRepository)
		{
			_certificateOfAbsenceRepository = certificateOfAbsenceRepository;
			_mapper = mapper;
			_associationRepository = associationRepository;
			_userManager = userManager;
			_participantsOfCertificateOfAbsenceRepository = participantsOfCertificateOfAbsenceRepository;
		}

		public GeneralResponse<int> Create(CreateCertificateOfAbsenceViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var association = _associationRepository.Get(vm.AssociationId);
			if (association == null)
			{
				res.Message = "همچین انجمنی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(association.ID, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var excelStatus = GeneralFunctions.CheckExcel(vm.ExcelFile);
			if (!excelStatus.Item2)
			{
				res.Message = excelStatus.Item1;
				return res;
			}

			string excelUrl = null;
			if (vm.ExcelFile != null)
			{
				var upload = GeneralFunctions.Upload(vm.ExcelFile, "Association_CertificatesOfAbsence", WrPath, "Excels/CertificatesOfAbsence");
				if (upload.Item2)
					excelUrl = upload.Item1;
			}

			var entity = _mapper.Map<CreateCertificateOfAbsenceDto>(vm);
			entity.RegistrationDate = DateTime.Now;
			entity.StatusId = (int)BaseInfoEnum.Waiting;
			entity.ExcelUrl = excelUrl;
			var Id = _certificateOfAbsenceRepository.Create(entity);
			if (Id > 0)
			{
				res.IsSuccess = true;
				res.Data = Id;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateCertificateOfAbsenceViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};


			var certificateDto = _certificateOfAbsenceRepository.Get(vm.Id);
			if (certificateDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}

			if (certificateDto.StatusId != (int)BaseInfoEnum.Waiting)
			{
				res.Message = "گواهی فقط در وضعیت انتظار میتواند تغییر کند";
				return res;
			}


			var statusOfUser = GeneralFunctions.CheckPermission(certificateDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			var excelStatus = GeneralFunctions.CheckExcel(vm.ExcelFile);
			if (vm.ExcelFile != null)
			{
				if (!excelStatus.Item2)
				{
					res.Message = excelStatus.Item1;
					return res;
				}
			}

			////////////////// delete Image
			if (certificateDto.ExcelUrl != null && vm.ExcelFile != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{certificateDto.ExcelUrl}");
			}
			///////////////////////////////////////////////////////////

			var entity = _mapper.Map<UpdateCertificateOfAbsenceDto>(vm);
			//////////////// Upload New Image
			string pdfUrl = null;
			if (vm.ExcelFile != null)
			{
				var upload = GeneralFunctions.Upload(vm.ExcelFile, "Association_CertificatesOfAbsence", WrPath, "Excels/CertificatesOfAbsence");
				if (upload.Item2)
					entity.ExcelUrl = upload.Item1;
			}

			/////////////////////////////////

			
			var status = _certificateOfAbsenceRepository.Update(entity);
			if (status)
			{
				res.IsSuccess = true;
				res.Data = status;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Delete(int Id, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificateOfAbsenceRepository.Get(Id);
			if (certificationDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(certificationDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete Image
			if (certificationDto.ExcelUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{certificationDto.ExcelUrl}");
			}
			///////////////////////////////////////////////////////////
			if (_certificateOfAbsenceRepository.Delete(Id))
			{
				res.IsSuccess = true;
				res.Data = true;
				return res;
			}
			res.Message = "مشکلی پیش امده است؛ لطفا مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GetCertificateOfAbsenceResponse> Get(int Id, User user)
		{
			var res = new GeneralResponse<GetCertificateOfAbsenceResponse>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificateOfAbsenceRepository.Get(Id);
			if (certificationDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(certificationDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			res.IsSuccess = true;
			res.Data = _mapper.Map<GetCertificateOfAbsenceResponse>(certificationDto);
			return res;
		}

		public GeneralResponse<GeneralPaginationModel<GetCertificateOfAbsenceDto>> GetAll(int AssociationId, User user, int page = 1)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetCertificateOfAbsenceDto>>()
			{
				IsSuccess = false
			};

			var statusOfUser = GeneralFunctions.CheckPermission(AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			var date = _certificateOfAbsenceRepository.GetAll(AssociationId, page);
			res.Data = _mapper.Map<GeneralPaginationModel<GetCertificateOfAbsenceDto>>(date);
			res.IsSuccess = true;
			return res;
		}

		public GeneralResponse<bool> ChangeState(int CertificateId, int StatusId, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificateOfAbsenceRepository.Get(CertificateId);
			if (certificationDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}
			if (certificationDto.StatusId != (int)BaseInfoEnum.Waiting)
			{
				res.Message = "گواهی فقط در وضعیت انتظار میتواند تغییر کند";
				return res;
			}


			if (StatusId == (int)BaseInfoEnum.Accept)
			{

				var data = GeneralFunctions.ReadExcel_CertificateOfAbsence($@"{WrPath}\{certificationDto.ExcelUrl}", CertificateId);
				var insertStatus = _participantsOfCertificateOfAbsenceRepository.Create(data);
				var updateStatus = _certificateOfAbsenceRepository.ChangeStatus(CertificateId, StatusId);
				if (insertStatus && updateStatus)
				{

					res.IsSuccess = true;
					return res;
				}
				return res;
			}
			var update = _certificateOfAbsenceRepository.ChangeStatus(CertificateId, StatusId);
			if (update)
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید";
			return res;
		}
		public GeneralResponse<GeneralPaginationModel<GetCertificateOfAbsenceDto>> GetAllForAdmin(int page = 1)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetCertificateOfAbsenceDto>>()
			{
				IsSuccess = false
			};
			var date = _certificateOfAbsenceRepository.GetAllForAdmin(page);
			res.Data = date;
			res.IsSuccess = true;
			return res;
		}



		public async Task<GeneralResponse<byte[]>> Downlaod(int CertificateId, string WrPath, User user)
		{
			var res = new GeneralResponse<byte[]>()
			{
				IsSuccess = false
			};


			var certificationDto = _certificateOfAbsenceRepository.Get(CertificateId);
			if (certificationDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(certificationDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var participants = _participantsOfCertificateOfAbsenceRepository.Get(CertificateId);

			var pdfPath = $@"{WrPath}\Certificate\Untitled.pdf";
			var newfile = $@"{WrPath}\Certificate";
			//var outputPdfPath =System.IO.Path.Combine(newfile, $"Amir_Khorshidi.pdf");


			using (var memoryStream = new MemoryStream())
			{

				using (var writer = new PdfWriter(memoryStream))
				{
					using (var pdfDoc = new PdfDocument(writer))
					{
						var document = new Document(pdfDoc);

						foreach (var participant in participants)
						{
							var reader = new PdfReader(pdfPath);
							var tempPdf = new PdfDocument(reader);
							var tempPage = tempPdf.GetFirstPage();
							pdfDoc.AddPage(tempPage.CopyTo(pdfDoc));

							var canvas = new PdfCanvas(pdfDoc.GetLastPage());

							// تنظیم مکان و فونت متن (موقعیت مکانی و مشخصات فونت باید بر اساس نیاز شما تغییر کند)
							var x = 50;
							var y = 40;
							canvas.BeginText()
							.SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 5)
							.MoveText(x, y)
							.SetColor(ColorConstants.BLACK, true)
							.ShowText($"{participant.FirstName}  {participant.LastName}")
							.EndText();

							tempPdf.Close();
						}

						document.Close();
					}
				}

				var byteArray = memoryStream.ToArray();

				// memoryStream.Flush();
				// تنظیم موقعیت جریان حافظه به ابتدا
				// memoryStream.Position = 0;


				//var editedPdf = 
				res.IsSuccess = true;
				res.Data = byteArray;
				return (res);
			}
		}




	}
}
