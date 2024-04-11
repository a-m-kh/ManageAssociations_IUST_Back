using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class AssociationMemberDto
	{
	}

	public class AssociationMemberDtoBase
	{
		public string Name { get; set; }
		public string? Role { get; set; }
		public string? ImageUrl { get; set; }
	}

	public class GetAssociationMemberDto : AssociationMemberDtoBase
	{
		public int Id { get; set; }
		public int AssoiciationId { get; set; }
	}

	public class CreateAssociationMemberDto : AssociationMemberDtoBase
	{
		public int AssociationId { get; set; }
	}

	public class UpdateAssociationMemberDto : AssociationMemberDtoBase
	{
		public int Id { get; set; }
	}
}
