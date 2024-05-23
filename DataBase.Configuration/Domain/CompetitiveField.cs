using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class CompetitiveField : EntityWithTypedId<int>
	{
		public string Title { get; set; }
		public int MovementFestivalId { get; set; }

		[ForeignKey(nameof(CompetitiveField.MovementFestivalId))]
		public MovementFestival MovementFestival { get; set; }


		[InverseProperty(nameof(Position.CompetitiveField))]
		public virtual ICollection<Position> Positions { get; set; }

	}
}
