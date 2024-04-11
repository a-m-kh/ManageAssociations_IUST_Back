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
using AutoMapper;
using DataBase.Repository.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories;
using Utility;
using Microsoft.Extensions.Logging;

namespace Logic.Service.Services
{
	public class AssociationMemberService: IAssociationMemberService
	{

		private readonly IAssociationMemberRepository _associationMemberRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		public AssociationMemberService(
			IAssociationMemberRepository associationMemberRepository,
			UserManager<User> userManager,
			IAssociationRepository associationRepository,
			IMapper mapper
			)
		{
			_associationMemberRepository = associationMemberRepository;
			_associationRepository = associationRepository;
			_mapper = mapper;
			_userManager = userManager;
		}

		public GeneralResponse<int> Create(CreateAssociationMemberViewModel vm, User user, string WrPath)
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
			string imageUrl = null;
			if (vm.Image != null)
			{
				var uploadImage = GeneralFunctions.Upload(vm.Image, "Association_Members", WrPath, "Images/Associations/Members");
				if (uploadImage.Item2)
					imageUrl = uploadImage.Item1;
			}

			var entity = _mapper.Map<CreateAssociationMemberDto>(vm);
			entity.ImageUrl = imageUrl;
			var Id = _associationMemberRepository.Create(entity);
			if (Id > 0)
			{
				res.IsSuccess = true;
				res.Data = Id;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateAssociationMemberViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var associationMember = _associationMemberRepository.Get(vm.Id);
			if (associationMember == null)
			{
				res.Message = "همچین عضوی وجود ندارد";
				return res;
			}



			var statusOfUser = GeneralFunctions.CheckPermission(associationMember.AssoiciationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete Image
			if (associationMember.ImageUrl != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{associationMember.ImageUrl}");
			}
			///////////////////////////////////////////////////////////


			//////////////// Upload New Image
			string imageUrl = null;
			if (vm.Image != null)
			{
				var uploadImage = GeneralFunctions.Upload(vm.Image, "Association_Members", WrPath, "Images/Associations/Members");
				if (uploadImage.Item2)
					imageUrl = uploadImage.Item1;
			}

			/////////////////////////////////



			var entity = _mapper.Map<UpdateAssociationMemberDto>(vm);
			entity.ImageUrl = imageUrl;
			var status = _associationMemberRepository.Update(entity);
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

			var associationMember = _associationMemberRepository.Get(Id);
			if (associationMember == null)
			{
				res.Message = "همچین عضوی وجود ندارد";
				return res;
			}



			var statusOfUser = GeneralFunctions.CheckPermission(associationMember.AssoiciationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete Image
			if (associationMember.ImageUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{associationMember.ImageUrl}");
			}
			///////////////////////////////////////////////////////////
			if (_associationMemberRepository.Delete(Id))
			{
				res.IsSuccess = true;
				res.Data = true;
				return res;
			}
			res.Message = "مشکلی پیش امده است؛ لطفا مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GetAssociationMemberResponse> Get(int id)
		{
			var res = new GeneralResponse<GetAssociationMemberResponse>();
			var entity = _associationMemberRepository.Get(id);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین عضوی وجود ندارد";
				return res;
			}
			res.Data = _mapper.Map<GetAssociationMemberResponse>(entity);
			return res;
		}

		public GeneralResponse<List<GetAssociationMemberResponse>> GetAll(int AssociationId)
		{
			var res = new GeneralResponse<List<GetAssociationMemberResponse>>();

			var entity = _associationMemberRepository.GetAll(AssociationId);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین عضوی وجود ندارد";
				return res;
			}
			res.Data = _mapper.Map<List<GetAssociationMemberResponse>>(entity);
			return res;
		}
	}
}
