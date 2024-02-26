using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class BaseInfo : EntityWithTypedId<int>
	{
		public int? ParrentID { get; set; }
		public string? Title { get; set; }
		public bool IsDelete { get; set; } = false;
		[InverseProperty(nameof(Event.Issue))]
		public ICollection<Event>? Issues { get; set; }
		[InverseProperty(nameof(Event.Period))]
		public virtual ICollection<Event> Periods { get; set; }
		[InverseProperty(nameof(Event.TypeOfEvent))]
		public virtual ICollection<Event> TypeOfEvents { get; set; }


	}
}
