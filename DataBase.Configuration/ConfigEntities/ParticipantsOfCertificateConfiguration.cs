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
	public class ParticipantsOfCertificateConfiguration: IEntityTypeConfiguration<ParticipantsOfCertificate>
	{
		public void Configure(EntityTypeBuilder<ParticipantsOfCertificate> builder)
		{
			builder.ToTable("ParticipantsOfCertificate");
			builder.HasKey(a => a.ID);
		}
	}
}
