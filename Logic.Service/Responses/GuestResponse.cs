using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class GuestResponse
	{
	}

	public class GuestResponseBase
	{
		public string? Name { get; set; }
		public string? Title { get; set; }
		public string? ImageUrl { get; set; }
	}

	public class GetGuestResponse : GuestResponseBase
	{

	}
}
