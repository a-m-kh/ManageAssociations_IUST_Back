using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class AssociationDto
	{
	}

	public class BaseAssociation
	{
		public string Name { get; set; }
		public string? LogoUrl { get; set; }
	}
	public class AssociationCreateDto : BaseAssociation 
	{
		
	}

	public class AssociationUpdateDto : BaseAssociation
	{
		public int Id { get; set; }
	}
	public class AssociationViewDto : BaseAssociation
	{
		public int ID { get; set; }
		public string AdminUserName { get; set; }
		public string AdminId { get; set; }
	}
}
