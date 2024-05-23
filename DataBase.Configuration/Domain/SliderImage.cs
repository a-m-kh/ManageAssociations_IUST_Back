using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class SliderImage : EntityWithTypedId<int>
	{
		public string ImageUrl { get; set; }
	}
}
