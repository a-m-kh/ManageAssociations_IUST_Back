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

			builder.Property(a => a.PeriodID).HasColumnName("PeriodID");
			builder.Property(a => a.TypeOfEventID).HasColumnName("TypeOfEventID");


			/*builder.HasOne(e => e.association)
				.WithMany(a => a.Events)
				.HasForeignKey(e => e.AssociationID);

			builder.HasOne(e => e.typeOfEvent)
				.WithMany(b => b.TypeOfEvents)
				.HasForeignKey(nameof(Event.TypeOfEventID));*/

			/*builder.HasOne(e => e.Issue)
				.WithMany(b => b.Issues)
				.HasForeignKey(e => e.IssueID);*/

			/*builder.HasOne(e => e.period)
				.WithMany(b => b.Periods)
				.HasForeignKey(e => e.PeriodID);*/
			/*builder.HasOne(e => e.period)
				.WithMany()
				.HasForeignKey(nameof(Event.PeriodID))
				.IsRequired();*/
		}
	}
}
