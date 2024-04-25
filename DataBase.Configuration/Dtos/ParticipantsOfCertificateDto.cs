using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class ParticipantsOfCertificateDto
	{
	}

	public class ParticipantsOfCertificateDtoBase
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int CertificationId { get; set; }
	}

	public class CreateParticipantsOfCertificateDto : ParticipantsOfCertificateDtoBase
	{

	}
}
