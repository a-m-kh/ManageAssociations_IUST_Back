using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class GuestDto
	{
	}

	public class GuestDtoBase
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string ImageUrl { get; set; }
	}

	public class GetGuestDto : GuestDtoBase
	{
		public int Id { get; set; }
		public int AssociationId { get; set; }
	}

	public class UpdateGuestDto : GuestDtoBase
	{
		public int Id { get; set; }
	}
	public class CreateGuestDto : GuestDtoBase
	{
		public int EventId { get; set; }
	}
}
