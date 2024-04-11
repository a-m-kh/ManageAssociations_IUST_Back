using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IGuestRepository
	{
		int Create(CreateGuestDto Model);
		bool Update(UpdateGuestDto Model);
		bool Delete(int id);
		GetGuestDto Get(int id);
		List<GetGuestDto> GetAll(int EventId);
		List<GetGuestDto> GetAll_WithPublic(int EventId);
	}
}
