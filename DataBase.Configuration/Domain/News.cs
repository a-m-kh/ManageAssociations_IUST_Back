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
		public int Views { get; set; } = 0;
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public bool IsDelete { get; set; } = false;
		public int AssociationId { get; set; }

		[ForeignKey(nameof(News.AssociationId))]
		public Association Association { get; set; }

		[ForeignKey(nameof(News.StatusId))]
		public BaseInfo Status { get; set; }

		[InverseProperty(nameof(NewsImage.News))]
		public virtual ICollection<NewsImage> Images { get; set; }
	}
}
