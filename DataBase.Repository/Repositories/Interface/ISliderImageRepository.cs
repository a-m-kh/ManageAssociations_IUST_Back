using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface ISliderImageRepository
	{
		int Create(string ImageUrl);
		bool Delete(int Id);
		List<SliderImage> GetAll();
		public SliderImage Get(int Id);
	}
}
