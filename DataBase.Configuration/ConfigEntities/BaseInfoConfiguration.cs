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
		}
	}
}
