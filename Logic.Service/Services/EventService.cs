using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Utility;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using DataBase.Configuration.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Logic.Service.Services
{
	public class EventService :IEventService
	{

		private readonly IEventRepository _eventRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public EventService(
			IEventRepository eventRepository,
			IMapper mapper,
			UserManager<User> userManager,
			IAssociationRepository associationRepository)
		{
			_eventRepository = eventRepository;
			_mapper = mapper;
			_userManager = userManager;
			_associationRepository = associationRepository;
		}


		public async Task<GeneralResponse<EventGetResponse>> GetEventsAsync(int EventId)
		{
			var res = new GeneralResponse<EventGetResponse>();
			if(EventId <1 || EventId == null)
			{
				res.IsSuccess = false;
				res.Message = "آیدی رویداد را درست وارد نمایید.";
				return res;
			}
			var result = await _eventRepository.GetByIdAsync(EventId);
			if(result == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین رویدادی یافت نشد";
				return res;
			}
			var date = _mapper.Map<EventGetResponse>(result);
			res.Data = date;
			return res;

		}

		public async Task<GeneralResponse<bool>> UpdateEventsAsync(EventUpdateViewModel VModel, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>();
			res.IsSuccess = false;
			var roles = _userManager.GetRolesAsync(user).Result.ToList();
			var association = await  _associationRepository.GetAsync(VModel.AssociationId);

			if ((association == null || user.Id!= association.AdminId) 
				&& roles.Find(a=>a == "SuperAdmin") == null)
			{
				res.Message = "شما به این انجمن دسترسی ندارید.";
				return res;
			}
			var mapEvent = _mapper.Map<EventUpdateDto>(VModel);
			if(mapEvent != null)
			{
				var imageUrl = (string.Empty, false);
				if (VModel.Image != null)
				{
					imageUrl = GeneralFunctions.UploadImage(VModel.Image, "Event", WrPath, "Images/Events");
				}
				if (imageUrl.Item2)
				{
					mapEvent.ImageUrl = imageUrl.Item1;
				}
				var isUpdate = await _eventRepository.UpdateAsync(mapEvent);
				if (isUpdate)
				{
					res.IsSuccess = true;
					res.Data = true;
					return res;
				}	
			}
			res.Message = "مشکلی به وجود امده است.";
			return res;

		}

		public async Task<GeneralResponse<bool>> DeleteEventsAsync(int EventId, User user)
		{
			var res = new GeneralResponse<bool>();
			res.IsSuccess = false;
			var entity  = await _eventRepository.GetByIdAsync(EventId);
			if(entity == null)
			{
				res.Message = "همچین رویدادی وجود ندارد.";
				return res;
			}
			var roles = _userManager.GetRolesAsync(user).Result.ToList();
			var association = await _associationRepository.GetAsync(entity.AssociationId);

			if ((association == null || user.Id != association.AdminId) && 
			    roles.Find(a=>a == "SuperAdmin") == null)
			{
				res.Message = "شما به این انجمن دسترسی ندارید.";
				return res;
			}
			var isDelete = await _eventRepository.DeleteAsync(EventId);
			if (!isDelete)
			{
				res.Message = "حذف نشد. دوباره تلاش نمایید.";
			}
			res.IsSuccess = true;
			return res;
		}

		public async Task<GeneralResponse<int>> CreateEventsAsync(EventCreateViewModel VModel, User user, string WrPath)
		{
			var res = new GeneralResponse<int>();
			res.IsSuccess = false;
			var roles = _userManager.GetRolesAsync(user).Result.ToList();
			var association = await _associationRepository.GetAsync(VModel.AssociationId);

			if ((association == null || user.Id != association.AdminId) &&
			    roles.Find(a => a == "SuperAdmin") == null)
			{
				res.Message = "شما به این انجمن دسترسی ندارید.";
				return res;
			}

			var url = (string)null;

			if(VModel.Image!= null)
			{
				var uploadImage = GeneralFunctions.UploadImage(VModel.Image, "Event", WrPath, "Images/Events");
				if (uploadImage.Item2)
					url = uploadImage.Item1;
			}

			var entity = _mapper.Map<EventCreateDto>(VModel);
			var isUpdate = await _eventRepository.CreateAsync(entity);
			if(isUpdate == 0)
			{
				res.Message = "رویداد اضافه نشد. لطفا دوباره تلاش نمایید.";
				return res;
			}
			res.IsSuccess = true;
			res.Data = isUpdate;
			return res;
		}

		public Task<GeneralResponse<EventGetForAdminResponse>> GetEventsForAdminAsync(EventGetViewModel VModel)
		{
			throw new NotImplementedException();
		}
	}
}
