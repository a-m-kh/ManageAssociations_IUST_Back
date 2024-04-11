using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class News : EntityWithTypedId<int>
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; } = false;
		public int StatusId { get; set; }
		public int Views { get; set; }
		public DateTime RegistrationDate { get; set; }
		public bool IsDelete { get; set; } = false;

		[ForeignKey(nameof(News.StatusId))]
		public BaseInfo Status { get; set; }

		[InverseProperty(nameof(NewsImage.News))]
		public virtual ICollection<NewsImage> Images { get; set; }
	}
}
