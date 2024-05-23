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
	public class MovementFestivalConfiguration : IEntityTypeConfiguration<MovementFestival>
	{
		public void Configure(EntityTypeBuilder<MovementFestival> builder)
		{
			builder.ToTable("MovementFestivals");
			builder.HasKey(a => a.ID);
		}
	}
}
