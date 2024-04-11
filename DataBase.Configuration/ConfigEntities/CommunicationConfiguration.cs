using DataBase.Configuration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.ConfigEntities
{
	public class CommunicationConfiguration : IEntityTypeConfiguration<Communication>
	{
		public void Configure(EntityTypeBuilder<Communication> builder)
		{
			builder.ToTable("Communications");
			builder.HasKey(a => a.ID);
		}
	}
}
