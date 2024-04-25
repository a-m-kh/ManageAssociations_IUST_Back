using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IReportRepository
	{
		int Create(CreateReportDto Model);
		bool Update(UpdateReportDto Model);
		bool Delete(int Id);
		GetReportDto Get(int Id);
	}
}
