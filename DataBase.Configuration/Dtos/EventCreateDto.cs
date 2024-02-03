using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{

	public class EventBase
	{
		public string Title { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string? Description { get; set; }
		public int? Price { get; set; }
		public string? ImageUrl { get; set; }
	}

	public class EventCreateDto : EventBase
	{
		public int? Period { get; set; }
		public int? TypeOfEvent { get; set; }
		public int? Issue { get;set; }
	}

	public class EventUpdateDto : EventCreateDto
	{
		public int ID { get; set; }
	}

	public class EventViewDto : EventBase
	{
		public int ID { get; set; }
		public string? Period { get; set; }
		public string? TypeOfEvent { get; set; }
		public string? Issue { get; set; }
	}
}
