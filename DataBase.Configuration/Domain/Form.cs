using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Form : EntityWithTypedId<int>
	{
		public string Title { get; set; }
		public string Url { get; set; }

	}
}
