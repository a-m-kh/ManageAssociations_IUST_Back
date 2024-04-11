using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	public class AssociationMemberResponse
	{
	}

	public class AssociationMemberResponseBase
	{
		public string Name { get; set; }
		public string? Role { get; set; }
		public string? ImageUrl { get; set; }
	}

	public class GetAssociationMemberResponse : AssociationMemberResponseBase
	{
		public int Id { get; set; }
	}
}
