using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IBaseInfoRepository : IGeneralRepository<BaseInfo>
	{

		public List<BaseInfoDto> Get(int parrentId);
	}
}
