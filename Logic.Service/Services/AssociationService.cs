using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;
using Utility;
using DataBase.Configuration.Dtos;
using Azure.Core;
using DataBase.Configuration.Domain;
using Microsoft.AspNetCore.Identity;
using Azure;

namespace Logic.Service.Services
{
	public class AssociationService : IAssociationService
	{
		private readonly IAssociationRepository _associationRepository;
		private readonly IMapper _mapper;
		private readonly ICommunicationRepository _communicationRepository;
		private readonly UserManager<User> _userManager;
		private readonly IAccountService _accountService;
		public AssociationService(
			IAssociationRepository associationRepository,
			IMapper mapper,
			ICommunicationRepository communicationRepository,
			UserManager<User> userManager,
			IAccountService accountService
			)
		{
			_associationRepository = associationRepository;
			_mapper = mapper;
			_communicationRepository = communicationRepository;	
			_userManager = userManager;
			_accountService= accountService;
		}
		public async Task<GeneralResponse<GetAssociationResponse>> GetByIdAsync(int id)
		{
			var res = new GeneralResponse<GetAssociationResponse>();
			var model = await _associationRepository.GetAsync(id);
			if (model == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین انجمنی وجود ندارد.";
				return res;
			}
			var resDate = _mapper.Map<GetAssociationResponse>(model);
			res.Data = resDate;
			return res;
		}



		public async Task<GeneralResponse<GetAssociationResponseForUser>> GetByIdAsyncForUser(int id)
		{
			var res = new GeneralResponse<GetAssociationResponseForUser>();
			var model = await _associationRepository.GetAsync(id);
			if (model == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین انجمنی وجود ندارد.";
				return res;
			}
			var resDate = _mapper.Map<GetAssociationResponseForUser>(model);
			res.Data = resDate;
			return res;
		}



		public async Task<GeneralResponse<bool>> Update(UpdateAssociationViewModel VModel, string WrPath, User user)
		{
			var res = new GeneralResponse<bool>();
			var findModel = await _associationRepository.GetAsync(VModel.Id);
			var statusOfUser = GeneralFunctions.CheckPermission(VModel.Id, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			if (findModel == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین انجمنی وجود ندارد.";
				return res;
			}
			
			var url = string.Empty;
			url = null;
			if(VModel.Logo != null)
			{
				var uploadImage = GeneralFunctions.Upload(VModel.Logo, "AssociationLogo", WrPath, "Image/Associations/Logo");
				if (uploadImage.Item2)
					url = uploadImage.Item1;
			}
			var model = _mapper.Map<AssociationUpdateDto>(VModel);
			model.LogoUrl = url;
			var IsUpdate = await _associationRepository.UpdateAsync(model);
			if(!IsUpdate)
			{
				res.IsSuccess = false;
				res.Message = "تغییرات ذخیره نشدند. دوباره تلاش نمایید.";
				return res;
			}
			res.Data = true;
			return res;
		}

		public async Task<GeneralResponse<int>> CreateAsync(CreateAssociationViewModel VModel, string WrPath)
		{
			var res = new GeneralResponse<int>();
			var findModel = await _associationRepository.GetByNameAsync(VModel.Name);
			if(findModel != null)
			{
				res.IsSuccess = false;
				res.Message = "در حال حاظر انجمنی با این نام وجود دارد. نام دیگری وارد نمایید.";
				return res;
			}

			var user = await _accountService.SignUp(new SignUpViewModel()
			{
				Password = VModel.Password,
				UserName = VModel.UserName
			});

			if (!user.IsSuccess)
			{
				res.IsSuccess= false;
				res.Message = user.Message;
				return res;
			}



			var url = string.Empty;
			url = null;
			if (VModel.Logo != null)
			{
				var uploadImage = GeneralFunctions.Upload(VModel.Logo, "AssociationLogo", WrPath, "Images/Associations/Logo");
				if (uploadImage.Item2)
					url = uploadImage.Item1;
			}
			try
			{
				var model = _mapper.Map<AssociationCreateDto>(VModel);
				model.LogoUrl = url;
				var id = await _associationRepository.CreateAsync(model);
				if (id == 0)
				{
					res.IsSuccess = false;
					res.Message = "انجمن ساخته نشد. دوباره تلاش نمایید";
					return res;
				}

				var assign = await _accountService.Assign(user.Data.userId, id);

				res.Data = id;
				return res;
			}catch(Exception ex)
			{
				var x = ex;
				return res;
			}
		}

		public async Task<GeneralResponse<bool>> DeleteByIdAsync(int id)
		{
			var res = new GeneralResponse<bool>();
			res.IsSuccess = false;
			var findModel  = await _associationRepository.GetAsync(id);
			if(findModel == null)
			{
				res.Message = "همچین انجمنی وجود ندارد.";
				return res;
			}
			var isDelete = await _associationRepository.DeleteAsync(id);
			if (isDelete)
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "حذف نشد. دوباره تلاش نمایید";
			return res;
		}

		public async Task<GeneralResponse<List<GetAssociationResponse>>> GetAll()
		{
			var res = new GeneralResponse<List<GetAssociationResponse>>(); 
			var lFindModel =await _associationRepository.GetAll();

			var model = _mapper.Map<List<GetAssociationResponse>>(lFindModel);
			res.Data = model;
			return res;
		}

		public async Task<GeneralResponse<int>> CreateCommunication(CreateCommunicationViewModel VModel, User user)
		{
			var response = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var statusOfUser = GeneralFunctions.CheckPermission(VModel.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				response.Message = statusOfUser.Item2;
				return response;
			}
			var modelDto = _mapper.Map<CreateCommunicationDto>(VModel);
			if (_communicationRepository.Create(modelDto) > 0)
			{
				response.IsSuccess = true;
				return response;
			}
			response.Message = "اضافه کردن لینک با مشکل مواجه شد. لطفا مجددا اقدام نمایید.";
			return response;

		}

		public async Task<GeneralResponse<bool>> UpdateCommunication(UpdateCommunicationViewModel VModel, User user)
		{
			var response = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var statusOfUser = GeneralFunctions.CheckPermission(VModel.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				response.Message = statusOfUser.Item2;
				return response;
			}
			var modelDto = _mapper.Map<UpdateCommunicationDto>(VModel);
			if (_communicationRepository.Update(modelDto))
			{
				response.IsSuccess = true;
				return response;
			}
			response.Message = "مجددا اقدام نمایید";
			return response;

		}

		public GeneralResponse<bool> DeleteCommunication(int Id, User user)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var entity= _communicationRepository.Get(Id);
			if(entity == null)
			{
				res.Message = "همچین لینکی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(entity.association.ID, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			if (!_communicationRepository.Delete(entity.Id))
			{
				res.Message = "مشکلی به وجود آمده است. لطفا مجددا اقدام کنید";
				return res;
			}
			res.IsSuccess = true;
			return res;
		}

		public GeneralResponse<GetCommunicationResponse> GetCommunication(int Id)
		{
			var res = new GeneralResponse<GetCommunicationResponse>()
			{
				IsSuccess = false
			};

			var entity = _communicationRepository.Get(Id);
			if (entity == null)
			{
				res.Message = "همچین لینکی وجود ندارد";
				return res;
			}

			var data  = _mapper.Map<GetCommunicationResponse>(entity);
			res.IsSuccess = true;
			res.Data = data;
			return res;
		}

		public GeneralResponse<List<GetCommunicationResponse>> GetAllCommunication(int associationId)
		{
			var res = new GeneralResponse<List<GetCommunicationResponse>>
			{
				IsSuccess = false
			};

			var asEntity = _associationRepository.Get(associationId);
			if(asEntity ==null)
			{
				res.Message = "همچین انجمنی وجود ندارد.";
				return res;
			}

			var entity = _communicationRepository.GetAll(associationId);
			if (entity == null)
			{
				res.Message = "همچین لینکی وجود ندارد";
				return res;
			}

			var data = _mapper.Map<List<GetCommunicationResponse>>(entity);
			res.IsSuccess = true;
			res.Data = data;
			return res;
		}
	}
}
