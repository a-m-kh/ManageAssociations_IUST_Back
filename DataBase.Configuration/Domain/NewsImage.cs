using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class NewsImage : EntityWithTypedId<int>
	{
		public string Url { get; set; }
		public int NewsId { get; set; }

		[ForeignKey(nameof(NewsImage.NewsId))]
		public News News { get; set; }
	}
}
