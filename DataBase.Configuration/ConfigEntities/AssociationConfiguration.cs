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
	public class AssociationConfiguration : IEntityTypeConfiguration<Association>
	{
		public void Configure(EntityTypeBuilder<Association> builder)
		{
			builder.ToTable("Associations");
			builder.HasKey(a => a.ID);
			builder.HasOne(a => a.Admin)
				.WithOne(u => u.Association)
				.HasForeignKey<Association>(a => a.AdminID);

			builder.HasMany(a => a.Events)
				.WithOne(e => e.association)
				.HasForeignKey(e => e.AssociationID);
		}
	}
}
