using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	public class CommunicationResponse
	{
	}

	public class CommunicationResponseBase
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
	}

	public class GetCommunicationResponse: CommunicationResponseBase
	{
		public string TypeOfLink { get; set; }
	}
}
