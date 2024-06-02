using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class JournalResponse
	{
	}

	public class JournalResponseBase
	{
		public string Name { get; set; }
		public string? NoAndDate { get; set; }
		public string? PdfUrl { get; set; }
	}

	public class GetJournalResponse : JournalResponseBase
	{
		public int Id { get; set; }
		public string? ImageUrl { get; set; }

	}
}
