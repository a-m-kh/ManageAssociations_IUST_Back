using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	public class MovementFestivalDto
	{
	}

	public class GetForAdminMovementFestivalDto
	{
		public List<CompetitiveFieldDto>? competitiveFields { get; set; }
		/*public List<Position> positions { get; set; }*/
		public int Id { get; set; }
		public string Title { get; set; }
	}
	
	public class CompetitiveFieldDto
	{
		public string Title { get; set; }
		public List<PositionDto>? positions { get; set; }
	}

	public class PositionDto
	{
		public BaseInfoDto Title { get; set; }
		public string Owner { get; set; }
	}
}
