using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface IGuestService
	{
		GeneralResponse<int> Create(CreateGuestViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateGuestViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int id, User user, string WrPath);
		GeneralResponse<GetGuestResponse> GetGuest(int id);
		GeneralResponse<List<GetGuestResponse>> GetAllGuest(int EventId);
		GeneralResponse<List<GetGuestResponse>> GetAllGuest_WithPublic(int EventId);
	}
}
