using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	public class GeneralPaginationModel<T> where T : class , new()
	{
		public int Total { get; set; }
		public List<T> Values { get; set; }

		public GeneralPaginationModel(int total, List<T> values)
		{
			Total = total;
			Values = values;
		}
		public GeneralPaginationModel() { }
	}
}
