using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class CommunicationDto
	{
	}


	public class CommunicationDtoBase
	{
		public string Title { get; set; }
		public string Link { get; set; }
	}

	public class CreateCommunicationDto : CommunicationDtoBase
	{
		public int AssociationId { get; set; }
		public int TypeOfLinkId { get; set; }

	}

	public class UpdateCommunicationDto : CommunicationDtoBase
	{

		public int Id { get; set; }
		public int? TypeOfLinkId { get; set; }


	}

	public class GetCommunicationDto : CommunicationDtoBase
	{
		public int Id { get; set; }
		public string TypeOfLink { get; set; }
		public AssociationViewDto association { get; set; }

	}
}
