using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class CertificationResponse
	{
	}

	public class GetCertificationResponse
	{
		public string Title { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string? ExcelUrl { get; set; }
		public string? Organizer { get; set; }
		public int Number { get; set; }
		public string Status { get; set; }
		public int AssociationId { get; set; }
	}

	
}
