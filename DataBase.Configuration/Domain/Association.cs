using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Association : EntityWithTypedId<int>
	{
		public string Name { get;set; }
		public int AdminID { get; set; }
		public User Admin { get; set; }
		public List<Event> Events { get;set;}
	}
}
