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
	public class EventConfiguration: IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			builder.ToTable("Events");
			builder.HasKey(a => a.ID);
			builder.HasOne(e => e.association)
				.WithMany(a => a.Events)
				.HasForeignKey(e => e.AssociationID);
		}
	}
}
