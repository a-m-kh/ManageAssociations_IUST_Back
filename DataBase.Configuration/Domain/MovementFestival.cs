using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain;

public class MovementFestival : EntityWithTypedId<int>
{
	public string? Title { get; set; }

	[InverseProperty(nameof(CompetitiveField.MovementFestival))]
	public virtual ICollection<CompetitiveField> CompetitiveFields { get; set; }
}
