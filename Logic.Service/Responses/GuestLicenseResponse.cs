using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class GuestLicenseResponse
	{
	}


	public class GetListGuestLicenseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string Status { get; set; }

	}

	public class GetGuestLicenseResponse
	{
		public string Name { get; set; }
		public string NationalCode { get; set; }
		public string JobTitle { get; set; }
		public bool IsGuest { get; set; }
		public bool IsFromIust { get; set; }

	}
}
