using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain;

public class Position : EntityWithTypedId<int>
{
	public int? TitleId { get; set; }
	public string Owner { get; set; }

	public int CompetiveFieldId { get; set; }

	[ForeignKey(nameof(Position.CompetiveFieldId))]
	public CompetitiveField CompetitiveField { get; set; }

	[ForeignKey(nameof(Position.TitleId))]
	public BaseInfo Title { get; set; }

}
