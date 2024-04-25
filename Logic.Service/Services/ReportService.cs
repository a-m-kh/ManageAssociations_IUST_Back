using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using DataBase.Configuration.Dtos;
using Microsoft.AspNetCore.Identity;
using Utility;
using AutoMapper;
using DataBase.Repository.Repositories.Interface;
using DataBase.Repository.Repositories;

namespace Logic.Service.Services
{
	public class ReportService: GeneralRepository<Report>, IReportService
	{

		private readonly IAssociationRepository _associationRepository;
		private readonly IEventRepository _eventRepository;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IReportRepository _reportRepository;
		private readonly IImageOfReportRepository _imageOfReportRepository;
		public ReportService(
			IUnitOfWork uow,
			IReportRepository reportRepository,
			IEventRepository eventRepository,
			IImageOfReportRepository imageOfReportRepository,
			IAssociationRepository associationRepository,
			UserManager<User> userManager,
			IMapper mapper
			) : base(uow)
		{
			_reportRepository = reportRepository;
			_imageOfReportRepository = imageOfReportRepository;
			_associationRepository = associationRepository;
			_userManager = userManager;
			_mapper = mapper;
			_eventRepository = eventRepository;
		}

		public GeneralResponse<int> Create(CreateReportViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var eventDto = _eventRepository.GetById(vm.EventId);
			if (eventDto == null)
			{
				res.Message = "همچین رویدادی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(eventDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			if(eventDto.ReportDto.Id != 0)
			{
				res.Message = "هر رویداد فقط یک گزارش میتواند داشته باشد.";
				return res;
			}
			var entity = _mapper.Map<CreateReportDto>(vm);
			var entityId = _reportRepository.Create(entity);
			if(entityId == 0)
			{
				res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید";
				return res;
			}
			var uploadImages = GeneralFunctions.UploadImage_Report(vm.Images, "Report", WrPath, "Images/Events/Reports", entityId);
			if (uploadImages.Item2)
			{
				var imagesStatus = _imageOfReportRepository.Create(uploadImages.Item3);
				if (imagesStatus)
				{
					res.IsSuccess = true;
					return res;
				}
				res.Message = "عکس ها ذخیره نشدند. عکس ها را دوباره آپلود کنید ";
				return res;
			}
			res.Message = "عکس ها ذخیره نشدند. عکس ها را دوباره آپلود کنید ";
			return res;
		}

		public GeneralResponse<bool>Update(UpdateReportViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var reportDto = _reportRepository.Get(vm.Id);
			if(reportDto == null)
			{
				res.Message = "همچین گزارشی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(reportDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var entity = _mapper.Map<UpdateReportDto>(vm);
			var isUpdate = _reportRepository.Update(entity);
			if(isUpdate)
			{
				res.IsSuccess = true;
				return res;
			}
			return res;
		}

		public GeneralResponse<bool>AddImage(AddImageViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var reportDto = _reportRepository.Get(vm.Id);
			if (reportDto == null)
			{
				res.Message = "همچین گزارشی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(reportDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var url = GeneralFunctions.Upload(vm.File, "Report", WrPath, "Images/Events/Reports");
			if (url.Item2)
			{
				var status = _imageOfReportRepository.Create(new ImageOfReport()
				{
					ReportId = reportDto.Id,
					Url = url.Item1
				});
				if (status)
				{
					res.IsSuccess = true;
					return res;
				}
			}
			res.Message = "مشکلی پیش آمده لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool>Delete(int ReportId,User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var reportDto  = _reportRepository.Get(ReportId);
		
			if (reportDto == null)
			{
				res.Message = "همچین گزارشی وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(reportDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			var imageStatus = _imageOfReportRepository.DeleteAll(ReportId);
			foreach(var url in reportDto.ImagesUrl)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{url}");
			}
			
			//GeneralFunctions.DeleteImage("")
			if (imageStatus)
			{
				var reportStatus = _reportRepository.Delete(ReportId);
				if (reportStatus)
				{
					res.IsSuccess = true;
					return res;
				}
			}
			res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<bool> DeleteImage(int ImageId, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var imageDto = _imageOfReportRepository.GetImage(ImageId);
			if (imageDto == null)
			{
				res.Message = "همچین عکسی وجود ندارد";
				return res;
			}
			var reportDto = _reportRepository.Get(imageDto.ReportId);
			var statusOfUser = GeneralFunctions.CheckPermission(reportDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			GeneralFunctions.DeleteImage($@"{WrPath}\{imageDto.Url}");
			if (_imageOfReportRepository.Delete(ImageId))
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}
		public GeneralResponse<GetReportDto> GetReport(int ReportId, User user)
		{
			var res = new GeneralResponse<GetReportDto>()
			{
				IsSuccess = false
			};
			var reportDto = _reportRepository.Get(ReportId);

			if (reportDto == null)
			{
				res.Message = "همچین گزارشی وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(reportDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			res.IsSuccess = true;
			res.Data = reportDto;
			return res;

		}
	}
}
