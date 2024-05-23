using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories;

public class CompetitiveFieldRepository : GeneralRepository<CompetitiveField>, ICompetitiveFieldRepository
{
	public CompetitiveFieldRepository(IUnitOfWork uow) : base(uow)
	{
	}

	public int Create(int movementFestivalId)
	{
		var enitty = new CompetitiveField()
		{
			MovementFestivalId = movementFestivalId
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

	public bool Exist(int Id)
	{
		return TEntity.Where(a => a.ID == Id).Count() > 0;
	}
}
