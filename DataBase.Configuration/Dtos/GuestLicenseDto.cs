using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class GuestLicenseDto
	{
	}

	public class GuestLicenseDtoBase
	{
		public string Name { get; set; }
		public string NationalCode { get; set; }
		public string JobTitle { get; set; }

	}

	public class CreateGuestLicenseDto : GuestLicenseDtoBase
	{
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public int StatusId { get; set; }
		public bool? IsGuest { get; set; }
		public bool? IsFromIust { get; set; }
		public int AssociationId { get; set; }
	}

	public class UpdateGuestLicenseDto : GuestLicenseDtoBase
	{
		public int Id { get; set; }
		public bool? IsGuest { get; set; }
		public bool? IsFromIust { get; set; }
	}

	public class GetGuestLicenseDto: GuestLicenseDtoBase
	{
		public int Id { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public string? Status { get; set; }
		public bool? IsGuest { get; set; }
		public bool? IsFromIust { get; set; }
		public int AssociationId { get; set; }
		public int StatusId { get; set; }
	}
}
