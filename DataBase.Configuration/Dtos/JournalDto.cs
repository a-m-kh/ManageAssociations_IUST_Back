using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class JournalDto
	{
	}

	public class JournalDtoBase
	{
		public string Name { get; set; }
		public string? NoAndDate { get; set; }
		public string? PdfUrl { get; set; }
	}

	public class GetJournalDto : JournalDtoBase
	{
		public int Id { get; set; }
		public int AssoiciationId { get; set; }
	}

	public class CreateJournalDto : JournalDtoBase
	{
		public int AssociationId { get; set; }
	}

	public class UpdateJournalDto : JournalDtoBase
	{
		public int Id { get; set; }
	}
}
