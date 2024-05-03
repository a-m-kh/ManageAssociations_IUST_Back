using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class ParticipantOfCertificateOfAbsence : EntityWithTypedId<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int CertificationId { get; set; }
		public string StudentId { get; set; }
		[ForeignKey(nameof(ParticipantOfCertificateOfAbsence.CertificationId))]
		public CertificateOfAbsence Certification { get; set; }
	}
}
