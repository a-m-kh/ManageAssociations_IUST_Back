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
	public class ImageNewsRepository : GeneralRepository<NewsImage>, IImageNewsRepository
	{
		public ImageNewsRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public bool Create(List<NewsImage> Images)
		{
			TEntity.AddRange(Images);
			var res = _uow.SaveChanges();
			return res > 0;
			
		}

		public bool Create(NewsImage Images)
		{
			TEntity.Add(Images);
			var res = _uow.SaveChanges();
			return res > 0;
		}

		public bool DeleteAll(int NewsId)
		{
			var entities = TEntity.Where(a => a.NewsId == NewsId).ToList();
			TEntity.RemoveRange(entities);
			var res = _uow.SaveChanges();
			return res > 0;
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

		public NewsImage GetImage(int ImageId)
		{
			return TEntity.Where(a => a.ID == ImageId).FirstOrDefault();
		}
	}
}
