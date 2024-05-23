using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IMovementFestivalRepository
	{
		int Create();
		bool Delete(int Id);
		GetForAdminMovementFestivalDto Get(int Id);
		List<GetForAdminMovementFestivalDto> GetAll();
		bool Exist(int Id);
	}
}
