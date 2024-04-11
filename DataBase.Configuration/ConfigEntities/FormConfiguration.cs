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
	public class FormConfiguration : IEntityTypeConfiguration<Form>
	{
		public void Configure(EntityTypeBuilder<Form> builder)
		{
			builder.ToTable("Forms");
			builder.HasKey(a => a.ID);
		}
	}
}
