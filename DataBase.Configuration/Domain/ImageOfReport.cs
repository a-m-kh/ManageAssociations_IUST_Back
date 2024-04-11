using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class ImageOfReport : EntityWithTypedId<int>
	{
		public string Url { get; set; }
		public int ReportId { get; set; }
		[ForeignKey(nameof(ImageOfReport.ReportId))]
		public Report Report { get; set; }
	}
}
