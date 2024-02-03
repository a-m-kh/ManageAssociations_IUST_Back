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
	public class BaseInfoConfiguration : IEntityTypeConfiguration<BaseInfo>
	{
		public void Configure(EntityTypeBuilder<BaseInfo> builder)
		{
			builder.ToTable("BaseInfo");
			builder.HasKey(a => a.ID);

			builder.HasMany(b => b.TypeOfEvents)
				.WithOne(e => e.typeOfEvent)
				.HasForeignKey(e => e.TypeOfEventID);

			builder.HasMany(b => b.Periods)
				.WithOne(e => e.period)
				.HasForeignKey(e => e.PeriodID);

			builder.HasMany(b => b.issues)
				.WithOne(e => e.issue)
				.HasForeignKey(e => e.IssueID);
		}
	}
}
