using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;

namespace Logic.Service.Services.Interface
{
	public interface IEventService
	{
		Task<GeneralResponse<EventGetResponse>> GetEventsAsync(int EventId);
		Task<GeneralResponse<bool>> UpdateEventsAsync(EventUpdateViewModel VModel, User user, string WrPath);
		Task<GeneralResponse<bool>> DeleteEventsAsync(int EventId, User user);
		Task<GeneralResponse<int>> CreateEventsAsync(EventCreateViewModel VModel, User user, string WrPath);
		Task<GeneralResponse<EventGetForAdminResponse>> GetEventsForAdminAsync(EventGetViewModel VModel);
	}
}
