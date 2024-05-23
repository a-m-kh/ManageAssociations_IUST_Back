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
	public class SliderImageRepository :GeneralRepository<SliderImage>, ISliderImageRepository
	{
		public SliderImageRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public int Create(string ImageUrl)
		{
			var entity = new SliderImage() 
			{
				ImageUrl = ImageUrl,
			};
			var DbEntity =  TEntity.Add(entity);
			_uow.SaveChanges();
			return DbEntity.Entity.ID;
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

		public List<SliderImage> GetAll()
		{
			return TEntity.ToList();
		}

		public SliderImage Get(int Id)
		{
			return TEntity.Where(a => a.ID == Id).FirstOrDefault();
		}
	}
}
