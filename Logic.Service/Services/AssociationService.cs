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

namespace Logic.Service.Services
{
	public class AssociationService : IAssociationService
	{
		private readonly IAssociationRepository _associationRepository;
		private readonly IMapper _mapper;

		public AssociationService(IAssociationRepository associationRepository, IMapper mapper)
		{
			_associationRepository = associationRepository;
			_mapper = mapper;
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

		public async Task<GeneralResponse<bool>> Update(UpdateAssociationViewModel VModel, string WrPath)
		{
			var res = new GeneralResponse<bool>();
			var findModel = await _associationRepository.GetAsync(VModel.Id);
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
				var uploadImage = GeneralFunctions.UploadImage(VModel.Logo, "AssociationLogo", WrPath, "Image/Associations/Logo");
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
			var url = string.Empty;
			url = null;
			if (VModel.Logo != null)
			{
				var uploadImage = GeneralFunctions.UploadImage(VModel.Logo, "AssociationLogo", WrPath, "Images/Associations/Logo");
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
	}
}
