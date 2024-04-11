using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class AssociationMember: EntityWithTypedId<int>
	{
		public string Name { get; set; }
		public string? Role { get; set; }
		public string? ImageUrl { get; set; }
		public bool IsDelete { get; set; }
		public int AssociationId { get; set; }

		[ForeignKey(nameof(AssociationMember.AssociationId))]
		public Association Association { get; set; }
	}
}
