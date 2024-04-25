using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Utility;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using DataBase.Configuration.Dtos;
using System.IO;
using Repository.Models;

namespace Logic.Service.Services
{
	public class GuestService : IGuestService
	{
		private readonly IGuestRepository _guestRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly UserManager<User> _userManager;
		private readonly IEventRepository _eventRepository;
		private readonly IMapper _mapper;

		public GuestService(
			IGuestRepository guestRepository,
			UserManager<User> userManager,
			IAssociationRepository associationRepository,
			IEventRepository eventRepository,
			IMapper mapper
			)
		{
			_guestRepository = guestRepository;
			_userManager = userManager;
			_associationRepository = associationRepository;
			_eventRepository = eventRepository;
			_mapper = mapper;
		}
		public GeneralResponse<int> Create(CreateGuestViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var Event =  _eventRepository.GetById(vm.EventId);
			if(Event == null )
			{
				res.Message = "همچین رویدادی وجود ندارد";
				return res;
			}
			var statusOfUser= GeneralFunctions.CheckPermission(Event.AssociationId , user,_userManager,_associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			string imageUrl = null;
			if(vm.Image != null)
			{
				var uploadImage = GeneralFunctions.Upload(vm.Image, "Event_Guest", WrPath, "Images/Events/Guests");
				if (uploadImage.Item2)
					imageUrl = uploadImage.Item1;
			}

			var entity = _mapper.Map<CreateGuestDto>(vm);
			entity.ImageUrl = imageUrl;
			var Id = _guestRepository.Create(entity);
			if (Id > 0)
			{
				res.IsSuccess = true;
				res.Data = Id;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateGuestViewModel vm,User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var guest = _guestRepository.Get(vm.Id);
			if(guest == null)
			{
				res.Message = "همچین سخنرانی وجود ندارد";
				return res;
			}


			
			var statusOfUser = GeneralFunctions.CheckPermission(guest.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			
			////////////////// delete Image
			if(guest.ImageUrl != null && vm.Image != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{guest.ImageUrl}");
			}
			///////////////////////////////////////////////////////////


			//////////////// Upload New Image
			string imageUrl = null;
			if (vm.Image != null)
			{
				var uploadImage = GeneralFunctions.Upload(vm.Image, "Event_Guest", WrPath, "Images/Events/Guests");
				if (uploadImage.Item2)
					imageUrl = uploadImage.Item1;
			}
			
			/////////////////////////////////
			


			var entity = _mapper.Map<UpdateGuestDto>(vm);
			entity.ImageUrl = imageUrl;
			var status = _guestRepository.Update(entity);
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

			var guest = _guestRepository.Get(Id);
			if (guest == null)
			{
				res.Message = "همچین سخنرانی وجود ندارد";
			}



			var statusOfUser = GeneralFunctions.CheckPermission(guest.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete Image
			if (guest.ImageUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{guest.ImageUrl}");
			}
			///////////////////////////////////////////////////////////
			if (_guestRepository.Delete(Id))
			{
				res.IsSuccess = true;
				res.Data = true;
				return res;
			}
			res.Message = "مشکلی پیش آمده است؛ لطفا مجددا اقدام نمایید.";
			return res;

		}

		public GeneralResponse<GetGuestResponse> GetGuest(int id)
		{
			var res = new GeneralResponse<GetGuestResponse>();
			var entity = _guestRepository.Get(id);
			if(entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین سخنرانی وجود ندارد";
				return res;
			}
			res.Data = _mapper.Map<GetGuestResponse>(entity);
			return res;
		}

		public GeneralResponse<List<GetGuestResponse>> GetAllGuest(int EventId)
		{
			var res = new GeneralResponse<List<GetGuestResponse>>();

			var entity = _guestRepository.GetAll(EventId);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین سخنرانی وجو ندارد";
				return res;
			}
			res.Data = _mapper.Map<List<GetGuestResponse>>(entity);
			return res;
		}

		public GeneralResponse<List<GetGuestResponse>> GetAllGuest_WithPublic(int EventId)
		{
			var res = new GeneralResponse<List<GetGuestResponse>>();

			var entity = _guestRepository.GetAll_WithPublic(EventId);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین سخنرانی وجو ندارد";
				return res;
			}
			res.Data = _mapper.Map<List<GetGuestResponse>>(entity);
			return res;
		}
	}
}
