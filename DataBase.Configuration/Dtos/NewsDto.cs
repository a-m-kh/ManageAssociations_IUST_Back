using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class NewsDto
	{
	}

	public class NewsDtoBase
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? RegistrationDate { get; set; }
	}

	public class CreateNewsDto: NewsDtoBase
	{
		public int StatusId { get; set; }
		public int AssociationId { get; set; }
	}

	public class GetNewsDto: NewsDtoBase
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int AssociationId { get; set; }
		public int Views { get; set; }
		public string Status { get; set; }
		public bool IsPublic { get; set; }
		public DateTime RegistrationDate { get; set; }
		public List<ImageDto>? Images { get; set; }
		public string AssociationName { get; set; }

	}

	public class UpdateNewsDto : NewsDtoBase
	{
		public int Id { get; set; }
	}
}
