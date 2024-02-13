using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Event: EntityWithTypedId<int>
	{
		//public string Name { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool IsConfirm { get; set; } = false;
		public string? Description { get; set; }
		public int AssociationID { get; set; }
		public int? Price { get;set; }
		public string? Title { get; set; }
		public int? TypeOfEventID { get; set; }
		public int? IssueID { get; set; }
		public string? ImageUrl { get; set; }
		public int? PeriodID { get; set; }
		public bool IsDelete { get; set; }=false;
		public bool IsPublic { get; set; } = false;
		public Association association { get; set; }


		[ForeignKey(nameof(Event.PeriodID))]
		public BaseInfo Period { get; set; }
		[ForeignKey(nameof(Event.TypeOfEventID))]
		public BaseInfo TypeOfEvent { get; set; }
		[ForeignKey(nameof(Event.IssueID))]
		public BaseInfo Issue { get; set; }
	}
}
