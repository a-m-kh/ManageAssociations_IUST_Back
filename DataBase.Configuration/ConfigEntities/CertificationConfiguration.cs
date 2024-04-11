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
	internal class CertificationConfiguration: IEntityTypeConfiguration<Certification>
	{
		public void Configure(EntityTypeBuilder<Certification> builder)
		{
			builder.ToTable("Certifications");
			builder.HasKey(a => a.ID);
		}
	}
}
