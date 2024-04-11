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
	public class GuestLicenseConfiguration: IEntityTypeConfiguration<GuestLicense>
	{
		public void Configure(EntityTypeBuilder<GuestLicense> builder)
		{
			builder.ToTable("GuestLicenses");
			builder.HasKey(a => a.ID);
		}
	}
}
