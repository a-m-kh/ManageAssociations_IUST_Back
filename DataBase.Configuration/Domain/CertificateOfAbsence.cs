using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class CertificateOfAbsence : EntityWithTypedId<int>
	{
		public string Title { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int StatusId { get; set; }
		public string CourseName { get; set; }
		public string ProfessorName { get; set; }
		public string Reason { get; set; }
		public string ExcelUrl { get; set; }
		public DateTime AbsenceDate { get; set; }
		public bool IsDelete { get; set; }
		public int AssociationId { get; set; }

		[ForeignKey(nameof(CertificateOfAbsence.StatusId))]
		public BaseInfo Status { get; set; }

		[ForeignKey(nameof(CertificateOfAbsence.AssociationId))]
		public Association Association { get; set; }

		[InverseProperty(nameof(ParticipantOfCertificateOfAbsence.Certification))]
		public virtual ICollection<ParticipantOfCertificateOfAbsence> People { get; set; }
	}
}
