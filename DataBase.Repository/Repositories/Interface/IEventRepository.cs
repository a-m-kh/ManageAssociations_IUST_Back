using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Repository;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IEventRepository: IGeneralRepository<Event>
	{
		Task<int> CreateAsync(EventCreateDto Model);
		Task<bool> UpdateAsync(EventUpdateDto Model);
		Task<bool> DeleteAsync(int Id);
		Task<EventViewDto> GetByIdAsync(int Id);
		Task<GeneralPaginationModel<EventViewDto>> GetByAssociationIdAsync(int Id, int Page = 1);
		Task<GeneralPaginationModel<EventViewDto>> GetLastEvent(int Page = 1);
		bool ConfirmEvent(int Id);

	}
}
