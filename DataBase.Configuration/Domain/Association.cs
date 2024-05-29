using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Association : EntityWithTypedId<int>
	{
		public string? Name { get;set; }
		public string? LogoUrl { get; set; }
		public bool IsDelete { get; set; } = false;
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public string? AdminID { get; set; }
		[ForeignKey(nameof(Association.AdminID))]
		public User? Admin { get; set; }
		public List<Event>? Events { get;set;}

		[InverseProperty(nameof(Communication.Association))]
		public virtual ICollection<Communication> Communications { get; set; }

		[InverseProperty(nameof(AssociationMember.Association))]
		public virtual ICollection<AssociationMember> AssociationMembers { get; set; }

		[InverseProperty(nameof(Journal.Association))]
		public virtual ICollection<Journal> Journals { get; set; }

		[InverseProperty(nameof(DataBase.Configuration.Domain.News.Association))]
		public virtual ICollection<News> News { get; set; }

		[InverseProperty(nameof(DataBase.Configuration.Domain.GuestLicense.Association))]
		public virtual ICollection<GuestLicense> GuestLicenses { get; set; }

		[InverseProperty(nameof(DataBase.Configuration.Domain.CertificateOfAbsence.Association))]
		public virtual ICollection<CertificateOfAbsence> CertificateOfAbsences { get; set; }
	}
}
