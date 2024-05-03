using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IGuestLicenseRepository
	{
		int Create(CreateGuestLicenseDto Model);
		bool Update(UpdateGuestLicenseDto Model);
		bool Delete(int Id);
		GetGuestLicenseDto GetGuestLicense(int Id);
		bool ChangeStatus(int Id, int StatusId);
		GeneralPaginationModel<GetGuestLicenseDto> GetAll(int Page, int AssociationId);

	}
}
