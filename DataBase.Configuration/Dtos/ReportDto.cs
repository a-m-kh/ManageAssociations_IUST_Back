using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class ReportDto
	{
	}

	public class ReportDtoBase
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
		///public int EventId { get; set; }
	}

	public class CreateReportDto : ReportDtoBase
	{
		public int EventId { get; set; }
	}

	public class UpdateReportDto : ReportDtoBase
	{
		public int Id { get; set; }
	}

	public class GetReportDto : ReportDtoBase
	{
		public int Id { get; set; }
		public List<string> ImagesUrl { get; set; }
		public int AssociationId { get; set; }
	}
}
