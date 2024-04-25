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
	public class ImageOfReportRepository:GeneralRepository<ImageOfReport>,IImageOfReportRepository
	{
		public ImageOfReportRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public bool Create(ImageOfReport Images)
		{
			TEntity.Add(Images);
			var res = _uow.SaveChanges();
			return res > 0;
		}

		public bool Create(List<ImageOfReport> Images)
		{
			TEntity.AddRange(Images);
			var res = _uow.SaveChanges();
			return res > 0;
		}

		public bool DeleteAll( int ReportId)
		{
			var entities = TEntity.Where(a => a.ReportId == ReportId).ToList();
			TEntity.RemoveRange(entities);
			var res = _uow.SaveChanges();
			return res > 0;
		}

		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id).FirstOrDefault();
			if(entity != null)
			{
				TEntity.Remove(entity);
				var res = _uow.SaveChanges();
				return res > 0;
			}
			return false;
		}

		public ImageOfReport GetImage(int ImageId)
		{
			return TEntity.Where(a=>a.ID == ImageId).FirstOrDefault();
		}
	}
}
