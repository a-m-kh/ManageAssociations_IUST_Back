using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class ParticipantsOfCertificate : EntityWithTypedId<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int CertificationId { get; set; }

		[ForeignKey(nameof(ParticipantsOfCertificate.CertificationId))]
		public Certification Certification { get; set; }
	}
}
