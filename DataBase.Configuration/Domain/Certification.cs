using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace DataBase.Configuration.Domain
{
	public class Certification : EntityWithTypedId<int>
	{
		public string Title { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public string? ExcelUrl { get; set; }
		public string? Organizer { get; set; }
		public bool IsDelete { get; set; } = false;
		public int Number { get; set; }
		public int StatusId { get; set; }
		public int AssociationId { get; set; }

		[ForeignKey(nameof(Certification.AssociationId))]
		public Association Association { get; set; }

		[ForeignKey(nameof(Certification.StatusId))]
		public BaseInfo Status { get; set; }

		[InverseProperty(nameof(ParticipantsOfCertificate.Certification))]
		public virtual ICollection<ParticipantsOfCertificate> People { get; set; }
	}
}
