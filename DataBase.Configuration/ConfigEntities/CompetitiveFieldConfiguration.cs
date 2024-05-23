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
	public class CompetitiveFieldConfiguration : IEntityTypeConfiguration<CompetitiveField>
	{
		public void Configure(EntityTypeBuilder<CompetitiveField> builder)
		{
			builder.ToTable("CompetitiveFields");
			builder.HasKey(a => a.ID);
		}
	}
}
