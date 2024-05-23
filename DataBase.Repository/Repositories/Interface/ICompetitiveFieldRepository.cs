using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface ICompetitiveFieldRepository
	{
		int Create(int movementFestivalId);
		bool Delete(int Id);
		bool Exist(int Id);
	}
}
