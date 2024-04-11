using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IJournalRepository
	{
		int Create(CreateJournalDto Model);
		bool Update(UpdateJournalDto Model);
		bool Delete(int id);
		GetJournalDto Get(int id);
		List<GetJournalDto> GetAll(int AssociationId);
	}
}
