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
	public class CertificateOfAbsenceConfiguration : IEntityTypeConfiguration<CertificateOfAbsence>
	{
		public void Configure(EntityTypeBuilder<CertificateOfAbsence> builder)
		{
			builder.ToTable("CertificatesOfAbsence");
			builder.HasKey(a => a.ID);
		}
	}
}
