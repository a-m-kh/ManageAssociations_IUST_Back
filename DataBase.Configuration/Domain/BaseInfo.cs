using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class BaseInfo : EntityWithTypedId<int>
	{
		public int? ParrentID { get; set; }
		public string? Title { get; set; }
		public bool IsDelete { get; set; } = false;
		[InverseProperty(nameof(Event.Issue))]
		public ICollection<Event>? Issues { get; set; }
		[InverseProperty(nameof(Event.Period))]
		public virtual ICollection<Event> Periods { get; set; }
		[InverseProperty(nameof(Event.TypeOfEvent))]
		public virtual ICollection<Event> TypeOfEvents { get; set; }
		[InverseProperty(nameof(Communication.TypeOfLink))]
		public virtual ICollection<Communication> TypeOfLinks { get; set; }

		[InverseProperty(nameof(Certification.Status))]
		public virtual ICollection<Certification> Statuses { get; set; }

		[InverseProperty(nameof(CertificateOfAbsence.Status))]
		public virtual ICollection<CertificateOfAbsence> CertificateOfAbsenceStatuses { get; set; }
		[InverseProperty(nameof(GuestLicense.Status))]
		public virtual ICollection<GuestLicense> GuestLicenseStatuses { get; set; }
		[InverseProperty(nameof(News.Status))]
		public virtual ICollection<News> NewsStatuses { get; set; }
	}
}
