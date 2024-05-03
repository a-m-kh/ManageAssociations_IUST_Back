using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class GuestLicense: EntityWithTypedId<int>
	{
		public string Name { get; set; }
		public string NationalCode { get; set; }
		public string JobTitle { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public int StatusId { get; set; } 
		public bool IsGuest { get; set; }
		public bool IsDelete { get; set; } = false;
		public bool IsFromIust { get; set; }
		public int AssociationId { get; set; }

		[ForeignKey(nameof(GuestLicense.StatusId))]
		public BaseInfo Status { get; set; }

		[ForeignKey(nameof(GuestLicense.AssociationId))]
		public Association Association { get; set; }

	}
}
