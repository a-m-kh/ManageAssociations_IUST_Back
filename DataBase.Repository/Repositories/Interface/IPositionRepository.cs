using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IPositionRepository
	{
		public int Create(int competiveFieldId, string owner, int titleId);
		public bool Delete(int Id);
		public bool Update(string? owner, int? titleId, int? Id);
	}
}
