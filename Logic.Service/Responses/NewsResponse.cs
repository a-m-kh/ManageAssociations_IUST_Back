using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class NewsResponse
	{
	}

	public class GetNewsResponse
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public List<ImageDto>? Images { get; set; }
	}

	public class GetNewsForAdminResponse
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int Views { get; set; }
		public string Status { get; set; }
		public bool IsPublic { get; set; }
		public DateTime RegistrationDate { get; set; }
	}
}
