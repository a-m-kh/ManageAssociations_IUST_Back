using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IImageOfReportRepository
	{
		bool Create(List<ImageOfReport> Images);
		bool Create(ImageOfReport Images);
		bool DeleteAll(int ReportId);
		bool Delete(int Id);
		ImageOfReport GetImage(int ImageId);
	}
}
