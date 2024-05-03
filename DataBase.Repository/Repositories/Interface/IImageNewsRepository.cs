using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IImageNewsRepository
	{
		bool Create(List<NewsImage> Images);
		bool Create(NewsImage Images);
		bool DeleteAll(int NewsId);
		bool Delete(int Id);
		NewsImage GetImage(int ImageId);
	}
}
