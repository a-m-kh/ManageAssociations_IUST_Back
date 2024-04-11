using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Report: EntityWithTypedId<int>
	{
		public string? ApplicationLevel { get; set; }
		public string? HoldingPeriod { get; set; }

		public string? WelcomeRate { get; set; }
		public string? ReflectionLink { get; set; }
		public string? AcademicLevelAndBackGround { get; set; }
		public string? RoleOfTeachers { get; set; }
		public string? AssociateCollections { get; set; }
		public string? ExecutiveColleagues { get; set; }
		public string? DetailsAndPanels { get; set; }

		[InverseProperty(nameof(ImageOfReport.Report))]
		public virtual ICollection<ImageOfReport> Images { get; set; }
	}
}
