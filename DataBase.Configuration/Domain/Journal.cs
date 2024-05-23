using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Journal : EntityWithTypedId<int>
	{
		public string Name { get; set; }
		public string? NoAndDate { get; set; }
		public string? PdfUrl { get; set; }
		public bool IsDelete { get; set; } = false;
		public int AssociationId { get; set; }
		public string? ImageUrl { get; set; }

		[ForeignKey(nameof(Journal.AssociationId))]
		public Association Association { get; set; }
	}
}
