using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	public class EventResponse
	{
	}

	public class EventCreateResponse
	{
		public int Id { get; set; }
	}

	public class EventGetResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string? AssociationName { get; set; }
		public int? Price { get; set; }
		public string Title { get; set; }
		public int? TypeOfEventID { get; set; }
		public int? IssueID { get; set; }
		public string? ImageUrl { get; set; }
		public int? PeriodID { get; set; }
	}

	public class EventGetForAdminResponse : EventGetResponse
	{
		public bool IsConfirm { get; set; }
	}
}
