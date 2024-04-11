using DataBase.Configuration.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuration.ConfigEntities
{
	public class NewsImagesConfiguration : IEntityTypeConfiguration<NewsImage>
	{
		public void Configure(EntityTypeBuilder<NewsImage> builder)
		{
			builder.ToTable("NewsImages");
			builder.HasKey(a => a.ID);
		}
	}
}
