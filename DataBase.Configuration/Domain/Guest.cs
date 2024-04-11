using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Guest : EntityWithTypedId<int>
	{
		public string Name { get;set; }
		public string Title { get;set; }
		public int EventId { get;set; }
		public string? ImageUrl { get;set; }
		public bool IsDelete { get; set; } = false;
		[ForeignKey(nameof(Guest.EventId))]
		public Event Event { get;set; }
	}
}
