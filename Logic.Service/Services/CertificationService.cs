using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Enums;

namespace Logic.Service.Services
{


	public class CertificationService : ICertificationService
	{
		private readonly ICertificationRepository _certificationRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly IParticipantsOfCertificateRepository _participantsOfCertificateRepository;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		public CertificationService(
			ICertificationRepository certificationRepository,
			IAssociationRepository associationRepository,
			IParticipantsOfCertificateRepository participantsOfCertificateRepository,
			UserManager<User> userManager,
			IMapper mapper)
		{
			_certificationRepository = certificationRepository;
			_associationRepository = associationRepository;
			_userManager = userManager;
			_mapper = mapper;
			_participantsOfCertificateRepository = participantsOfCertificateRepository;
		}

		public GeneralResponse<int> Create(CreateCertificationViewModel vm, User user, string WrPath)
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

			var excelStatus = GeneralFunctions.CheckExcel(vm.File);
			if(!excelStatus.Item2)
			{
				res.Message = excelStatus.Item1;
				return res;
			}

			string excelUrl = null;
			if (vm.File != null)
			{
				var upload = GeneralFunctions.Upload(vm.File, "Association_Certificate", WrPath, "Excels/Certifications");
				if (upload.Item2)
					excelUrl = upload.Item1;
			}

			var entity = _mapper.Map<CreateCertificationDto>(vm);
			if(vm.Organizer == null)
			{
				entity.Organizer = association.Name;
			}
			entity.RegistrationDate = DateTime.UtcNow;
			entity.Number = excelStatus.Item3;
			entity.StatusId =(int)BaseInfoEnum.Waiting;
			entity.ExcelUrl = excelUrl;
			var Id = _certificationRepository.Create(entity);
			if (Id > 0)
			{
				res.IsSuccess = true;
				res.Data = Id;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}



		public GeneralResponse<bool>Update(UpdateCertificationViewModel vm, User user,string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};


			var certificateDto = _certificationRepository.Get(vm.Id);
			if (certificateDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}
			
			if(certificateDto.StatusId !=(int)BaseInfoEnum.Waiting)
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
			var excelStatus = GeneralFunctions.CheckExcel(vm.File);
			if (vm.File != null)
			{
				if (!excelStatus.Item2)
				{
					res.Message = excelStatus.Item1;
					return res;
				}
			}
						
			////////////////// delete Image
			if (certificateDto.ExcelUrl!= null && vm.File != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{certificateDto.ExcelUrl}");
			}
			///////////////////////////////////////////////////////////

			var entity = _mapper.Map<UpdateCertificationDto>(vm);
			//////////////// Upload New Image
			string pdfUrl = null;
			if (vm.File != null)
			{
				var upload = GeneralFunctions.Upload(vm.File, "Association_Certificate", WrPath, "Excels/Certifications");
				if (upload.Item2)
					entity.ExcelUrl = upload.Item1;
			}

			/////////////////////////////////

			if(vm.File != null)
			{
				entity.Number = excelStatus.Item3;
			}

			var status = _certificationRepository.Update(entity);
			if (status)
			{
				res.IsSuccess = true;
				res.Data = status;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}


		public GeneralResponse<bool>Delete(int Id, User user, string WrPath)
		{

			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificationRepository.Get(Id);
			if (certificationDto == null)
			{
				res.Message = "همچین گواهی ای وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(certificationDto.AssociationId , user, _userManager, _associationRepository);
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
			if (_certificationRepository.Delete(Id))
			{
				res.IsSuccess = true;
				res.Data = true;
				return res;
			}
			res.Message = "مشکلی پیش امده است؛ لطفا مجددا اقدام نمایید.";
			return res;
		}


		public GeneralResponse<GetCertificationResponse> Get(int Id,User user)
		{
			var res = new GeneralResponse<GetCertificationResponse>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificationRepository.Get(Id);
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
			res.Data = _mapper.Map<GetCertificationResponse>(certificationDto);
			return res;
		}


		public async Task<GeneralResponse<GeneralPaginationModel<GetCertificationDto>>>GetAll(int AssociationId,User user, int page = 1)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetCertificationDto>>()
			{
				IsSuccess = false
			};

			var statusOfUser = GeneralFunctions.CheckPermission(AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			res.Data = await _certificationRepository.GetAllAsync(AssociationId, page);
			res.IsSuccess = true;
			return res;
		}


		public async Task<GeneralResponse<GeneralPaginationModel<GetCertificationDto>>> GetAllForAdmin(User user, int page = 1)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetCertificationDto>>()
			{
				IsSuccess = false
			};

			/*var statusOfUser = GeneralFunctions.CheckPermission(AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}*/

			res.Data = await _certificationRepository.GetAllForAdminAsync(page);
			res.IsSuccess = true;
			return res;
		}



		public GeneralResponse<bool> ChangeState(int CertificateId,int StatusId,string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var certificationDto = _certificationRepository.Get(CertificateId);
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
				
				var data = GeneralFunctions.ReadExcel_Certificate($@"{WrPath}\{certificationDto.ExcelUrl}", CertificateId);
				var insertStatus = _participantsOfCertificateRepository.Create(data);
				var updateStatus = _certificationRepository.UpdateStatus(CertificateId, StatusId);
				if (insertStatus && updateStatus)
				{
					
					res.IsSuccess = true;
					return res;
				}
				return res;
			}
			var update = _certificationRepository.UpdateStatus(CertificateId, StatusId);
			if (update)
			{
				res.IsSuccess = true;
				return res;
			}
			return res;
		}
		
	}
}
