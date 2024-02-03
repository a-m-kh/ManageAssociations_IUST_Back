using DataBase.Configuration.Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IAssociationRepository : IGeneralRepository<Association>
	{
		Task<bool> CreateAsync(AssociationCreateDto Model);
		Task<bool> UpdateAsync(AssociationUpdateDto Model);
		Task<bool> DeleteAsync(int Id);
		Task<AssociationViewDto> GetAsync(int Id);
		Task<GeneralPaginationModel<AssociationViewDto>>GetAllAsync(int Page = 1);
		Task<bool> AssignAdminAsync(User user, int AssociationID);

	}
}
