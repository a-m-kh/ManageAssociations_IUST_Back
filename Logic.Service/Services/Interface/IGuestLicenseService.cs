using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface IGuestLicenseService
	{
		GeneralResponse<int> Create(CreateGuestLicenseViewModel Model, User user);
		GeneralResponse<bool> Update(UpdateGuestLicenseViewModel Model, User user);
		GeneralResponse<bool> Delete(int id, User user);
		GeneralResponse<GetGuestLicenseResponse> Get(int id, User user);
		GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>> GetAll(int Page, User user, int AssociationId);
		GeneralResponse<bool> ChangeStatus(int GuestLicenseId, int StatusId);
	}
}
