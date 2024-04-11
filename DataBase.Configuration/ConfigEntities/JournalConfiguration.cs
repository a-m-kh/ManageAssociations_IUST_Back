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
	public class JournalConfiguration : IEntityTypeConfiguration<Journal>
	{
		public void Configure(EntityTypeBuilder<Journal> builder)
		{
			builder.ToTable("Journals");
			builder.HasKey(a => a.ID);
		}
	}
}
