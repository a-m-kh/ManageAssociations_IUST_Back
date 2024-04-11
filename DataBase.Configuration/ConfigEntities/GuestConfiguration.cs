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
	public class GuestConfiguration : IEntityTypeConfiguration<Guest>
	{
		public void Configure(EntityTypeBuilder<Guest> builder)
		{
			builder.ToTable("Guests");
			builder.HasKey(a => a.ID);
		}
	}
}
