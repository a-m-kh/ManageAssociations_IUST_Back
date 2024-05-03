using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface ICertificateOfAbsenceRepository
	{
		int Create(CreateCertificateOfAbsenceDto Model);
		bool Update(UpdateCertificateOfAbsenceDto Model);
		bool Delete(int Id);
		GetCertificateOfAbsenceDto Get(int Id);
		GeneralPaginationModel<GetCertificateOfAbsenceDto> GetAll(int AssociationId, int Page = 1);
		bool ChangeStatus(int Id, int StatusId);
	}
}
