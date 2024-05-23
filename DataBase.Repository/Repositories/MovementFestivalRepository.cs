using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories
{
	public class MovementFestivalRepository : GeneralRepository<MovementFestival>, IMovementFestivalRepository
	{
		public MovementFestivalRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public int Create()
		{
			var enitty = new MovementFestival();
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

		public GetForAdminMovementFestivalDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id)
				.Select(a => new GetForAdminMovementFestivalDto()
				{
					Id = a.ID,
					Title = a.Title,
					competitiveFields = a.CompetitiveFields != null ? (a.CompetitiveFields.Select(c => new CompetitiveFieldDto()
					{
						positions = c.Positions != null ? (c.Positions.Select(p => new PositionDto()
						{
							Owner = p.Owner,
							Title = new BaseInfoDto()
							{
								ID = p.Title.ID,
								Title = p.Title.Title
							}
						}).ToList()) : (null)
					}).ToList()) : (null)
				})
				.FirstOrDefault();
			
			return entity;
		}

		public List<GetForAdminMovementFestivalDto> GetAll()
		{

			var x = TEntity.ToList();


			var entities = TEntity
				.Select(a => new GetForAdminMovementFestivalDto()
				{
					Id = a.ID,
					Title = a.Title,
					competitiveFields =a.CompetitiveFields != null ?( a.CompetitiveFields.Select(c => new CompetitiveFieldDto()
					{
						positions = c.Positions != null ? (c.Positions.Select(p => new PositionDto()
						{
							Owner = p.Owner,
							Title = new BaseInfoDto()
							{
								ID = p.Title.ID,
								Title = p.Title.Title
							}
						}).ToList()) : (null)
					}).ToList()) : (null)
				})
				.ToList();
		return entities;
		}

		public bool Exist(int Id)
		{
			return TEntity.Where(a => a.ID == Id).Count() > 0;
		}
	}
}
