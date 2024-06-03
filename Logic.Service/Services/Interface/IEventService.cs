using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;

namespace Logic.Service.Services.Interface
{
	public interface IEventService
	{
		Task<GeneralResponse<EventGetResponse>> GetEventsAsync(int EventId);
		Task<GeneralResponse<bool>> UpdateEventsAsync(EventUpdateViewModel VModel, User user, string WrPath);
		Task<GeneralResponse<bool>> DeleteEventsAsync(int EventId, User user);
		Task<GeneralResponse<int>> CreateEventsAsync(EventCreateViewModel VModel, User user, string WrPath);
		Task<GeneralResponse<EventGetForAdminResponse>> GetEventsForAdminAsync(EventGetViewModel VModel);
		Task<GeneralResponse<GeneralPaginationModel<GetEventDto>>> GetAllForAdmin(int AssociationId, User user, int Page = 1);
		Task<GeneralResponse<List<GetForUserEventDto>>> GetAllForUser(int AssociationId);
		GeneralResponse<GeneralPaginationModel<GetEventDto>> GetAllForSuperAdmin(int Page = 1);
		Task<GeneralResponse<bool>> ChangePublic(int Id, User user, int AssociationId);
		GeneralResponse<bool> ChangeConfirm(EventChangeConfirmViewModel Vm);
		Task<GeneralResponse<List<GetForUserEventDto>>> GetAllForUser();

	}
}
