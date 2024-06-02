using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;
using DataBase.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Utility;
using DataBase.Configuration.Dtos;
using Utility.Enums;

namespace Logic.Service.Services
{
	public class GuestLicenseService :IGuestLicenseService
	{
		private readonly IGuestLicenseRepository _guestLicenseRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IAssociationRepository _associationRepository;

		public GuestLicenseService(IGuestLicenseRepository guestLicenseRepository, IMapper mapper, UserManager<User> userManager, IAssociationRepository associationRepository)
		{
			_guestLicenseRepository = guestLicenseRepository;
			_mapper = mapper;
			_userManager = userManager;
			_associationRepository = associationRepository;
		}

		public GeneralResponse<int> Create(CreateGuestLicenseViewModel Model, User user)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var statusOfUser = GeneralFunctions.CheckPermission(Model.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			
			var entity = _mapper.Map<CreateGuestLicenseDto>(Model);
			entity.StatusId =(int)BaseInfoEnum.Waiting;
			var entityId = _guestLicenseRepository.Create(entity);
			if (entityId == 0)
			{
				res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید";
				return res;
			}
			res.IsSuccess = true;
			//res.Message = "عکس ها ذخیره نشدند. عکس ها را دوباره آپلود کنید ";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateGuestLicenseViewModel Model, User user)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var GuestLicenseDto = _guestLicenseRepository.GetGuestLicense(Model.Id);
			if (GuestLicenseDto == null)
			{
				res.Message = "همچین مجوز مدعویی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(GuestLicenseDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			if(GuestLicenseDto.StatusId != (int) BaseInfoEnum.Waiting )
			{
				res.Message = "فقط در وضعیت انتظار میتوانید مجوز مدعو را تغییر دهید";
				return res;
			}

			var entity = _mapper.Map<UpdateGuestLicenseDto>(Model);
			var isUpdate = _guestLicenseRepository.Update(entity);
			if (isUpdate)
			{
				res.IsSuccess = true;
				return res;
			}
			return res;
		}

		public GeneralResponse<bool> Delete(int id, User user)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var guestLicenseDto = _guestLicenseRepository.GetGuestLicense(id);

			if (guestLicenseDto == null)
			{
				res.Message = "همچین مجوز مدعویی وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(guestLicenseDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			
			var guestLicenseStatus = _guestLicenseRepository.Delete(id);
			if (guestLicenseStatus)
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GetGuestLicenseResponse> Get(int id, User user)
		{
			var res = new GeneralResponse<GetGuestLicenseResponse>()
			{
				IsSuccess = false
			};
			var guestLicenseDto = _guestLicenseRepository.GetGuestLicense(id);

			if (guestLicenseDto == null)
			{
				res.Message = "همچین گزارشی وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(guestLicenseDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			res.Data = _mapper.Map<GetGuestLicenseResponse>(guestLicenseDto);
			res.IsSuccess = true;
			//res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>> GetAll(int Page, User user, int AssociationId)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>>()
			{
				IsSuccess = false
			};
			var statusOfUser = GeneralFunctions.CheckPermission(AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var guestLicenseDto = _guestLicenseRepository.GetAll(Page, AssociationId);

			if (guestLicenseDto == null)
			{
				res.Message = "همچین مجوز مدعویی وجود ندارد";
				return res;
			}

			
			res.Data = _mapper.Map<GeneralPaginationModel<GetListGuestLicenseResponse>> (guestLicenseDto);
			res.IsSuccess = true;
			//res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}


		public GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>> GetAllForAdmin(int Page)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>>()
			{
				IsSuccess = false
			};

			var guestLicenseDto = _guestLicenseRepository.GetAllForSuperAdmin(Page);

			if (guestLicenseDto == null)
			{
				res.Message = "همچین مجوز مدعویی وجود ندارد";
				return res;
			}


			res.Data = _mapper.Map<GeneralPaginationModel<GetListGuestLicenseResponse>>(guestLicenseDto);
			res.IsSuccess = true;
			//res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}




		public GeneralResponse<bool> ChangeStatus(int GuestLicenseId, int StatusId)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var guestLicenseDto = _guestLicenseRepository.GetGuestLicense(GuestLicenseId);

			if (guestLicenseDto == null)
			{
				res.Message = "همچین مجوز مدعویی وجود ندارد";
				return res;
			}

			var changeStatus = _guestLicenseRepository.ChangeStatus(GuestLicenseId, StatusId);
			if(changeStatus)
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "مشکلی پیش آمده است لطفا مجددا اقدام نمایید";
			return res;

		}
	}
}
