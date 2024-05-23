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
	public class SliderImageConfiguration : IEntityTypeConfiguration<SliderImage>
	{
		public void Configure(EntityTypeBuilder<SliderImage> builder)
		{
			builder.ToTable("SliderImages");
			builder.HasKey(a => a.ID);
		}
	}
}
