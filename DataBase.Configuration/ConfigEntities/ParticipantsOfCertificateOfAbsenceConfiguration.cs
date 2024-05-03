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
	public class ParticipantsOfCertificateOfAbsenceConfiguration : IEntityTypeConfiguration<ParticipantOfCertificateOfAbsence>
	{
		public void Configure(EntityTypeBuilder<ParticipantOfCertificateOfAbsence> builder)
		{
			builder.ToTable("ParticipantsOfCertificateOfAbsence");
			builder.HasKey(a => a.ID);
		}
	}
}
