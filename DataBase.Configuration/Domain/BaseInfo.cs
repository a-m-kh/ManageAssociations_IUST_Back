using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class BaseInfo : EntityWithTypedId<int>
	{
		public int? ParrentID { get; set; }
		public string Title { get; set; }
		public ICollection<Event> issues { get; set; }
		public ICollection<Event> Periods { get; set; }
		public ICollection<Event> TypeOfEvents { get; set; }
	}
}
