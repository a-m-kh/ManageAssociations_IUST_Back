using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface ICertificationRepository
	{
		int Create(CreateCertificationDto Model);
		bool Update(UpdateCertificationDto Model);
		bool Delete(int Id);
		GetCertificationDto Get(int Id);
		Task<GeneralPaginationModel<GetCertificationDto>> GetAllAsync(int AssociationId,int Page = 1);
		bool UpdateStatus(int Id, int StatusId);
		Task<GeneralPaginationModel<GetCertificationDto>> GetAllForAdminAsync(int Page = 1);
	}
}
