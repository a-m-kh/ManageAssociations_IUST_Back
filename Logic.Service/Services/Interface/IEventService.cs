using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface IEventService
	{
		Task<GeneralResponse<EventGetResponse>> GetEventsAsync(int EventId);
		Task<GeneralResponse<bool>> UpdateEventsAsync(EventUpdateViewModel VModel, string userId);
		Task<GeneralResponse<bool>> DeleteEventsAsync(int EventId, string userId);
		Task<GeneralResponse<int>> CreateEventsAsync(EventCreateViewModel VModel, string userId);
		Task<GeneralResponse<EventGetForAdminResponse>> GetEventsForAdminAsync(EventGetViewModel VModel);
	}
}
