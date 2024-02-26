using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;

namespace DataBase.Repository.Repositories
{
	public class BaseInfoRepository : GeneralRepository<BaseInfo>, IBaseInfoRepository
	{
		public BaseInfoRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public List<BaseInfoDto> Get(int parrentId)
		{
			return(TEntity.Where(a => a.ParrentID == parrentId && !a.IsDelete).
				Select(a => new BaseInfoDto
				{
					Title = a.Title,
					ID = a.ID
				}).ToList());
		}
	}
}
