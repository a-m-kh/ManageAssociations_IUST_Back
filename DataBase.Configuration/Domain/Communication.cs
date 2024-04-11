using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace DataBase.Configuration.Domain
{
	public class Communication: EntityWithTypedId<int>
	{
		public string? Title { get; set; }
		public int TypeOfLinkId { get; set; }
		public string? Link { get; set; }
		public int AssociationId { get; set; }
		public bool IsDelete { get; set; }
		[ForeignKey(nameof(Communication.AssociationId))]
		public Association Association { get; set; }
		[ForeignKey(nameof(Communication.TypeOfLinkId))]
		public BaseInfo TypeOfLink { get; set; }


	}
}
