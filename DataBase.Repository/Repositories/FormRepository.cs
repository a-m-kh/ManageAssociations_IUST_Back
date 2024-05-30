using DataBase.Configuration.Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;

namespace DataBase.Repository.Repositories
{
	public class FormRepository : GeneralRepository<Form>, IFormRepository
	{
		public FormRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public int Create(string Url, string Title)
		{
			var entity = new Form { Url = Url,Title = Title };
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(FormUpdateDto EDto)
		{
			var entity = TEntity.Where(a => a.ID == EDto.Id).FirstOrDefault();
			if (entity == null)
				return false;

			entity.Url = EDto.Url != null ? (EDto.Url):(entity.Url) ;
			entity.Title = EDto.Title != null ? (EDto.Title) : (entity.Title);
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id).FirstOrDefault();
			if (entity == null) return false;
			TEntity.Remove(entity);
			var isDelete = _uow.SaveChanges();
			return isDelete > 0;
		}

		public List<Form> GetAll()
		{
			return TEntity.ToList();
		}
	}
}
