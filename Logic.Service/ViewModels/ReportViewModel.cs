using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels
{
	internal class ReportViewModel
	{
	}

	public class ReportViewModelBase
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
	}

	public class CreateReportViewModel : ReportViewModelBase
	{
		public List<IFormFile>? Images { get; set; }
		[Required(ErrorMessage ="آیدی رویداد مدنظر را وارد نمایید")]
		public int EventId { get; set; }


	}

	public class UpdateReportViewModel : ReportViewModelBase
	{
		[Required(ErrorMessage ="آیدی گزارش مد نظر را وارد نمایید")]
		public int Id { get; set; }
	}

	public class AddImageViewModel
	{
		[Required(ErrorMessage = "آیدی گزارش مد نظر را وارد نمایید")]
		public int Id { get; set; }

		public IFormFile File {get;set;}
	}

}
