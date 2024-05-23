using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories
{
	public class PositionRepository : GeneralRepository<Position>, IPositionRepository
	{
		public PositionRepository(IUnitOfWork uow) : base(uow)
		{
		}
		public int Create(int competiveFieldId, string owner, int titleId)
		{
			var enitty = new Position()
			{
				Owner = owner,
				CompetiveFieldId = competiveFieldId,
				TitleId = titleId,
			};
			var dbEntity = TEntity.Add(enitty);
			_uow.SaveChanges();
			if (dbEntity == null || dbEntity.Entity == null || dbEntity.Entity.ID == 0)
				return 0;

			return dbEntity.Entity.ID;
		}
		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id).FirstOrDefault();
			if (entity != null)
			{
				TEntity.Remove(entity);
				var res = _uow.SaveChanges();
				return res > 0;
			}
			return false;
		}


		public bool Update(string? owner, int? titleId, int? Id)
		{
			var entity = TEntity.Where(a => a.ID == Id).FirstOrDefault();
			if(entity == null)
				return false;

			entity.TitleId = titleId == null ? (entity.TitleId) : (titleId);
			entity.Owner = owner == null ? (entity.Owner) : (owner);
			var isUpdate = _uow.SaveChanges();
			return (isUpdate > 0);
		}
	}
}
