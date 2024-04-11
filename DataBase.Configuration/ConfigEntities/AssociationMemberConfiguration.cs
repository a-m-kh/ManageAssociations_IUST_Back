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
	public class AssociationMemberConfiguration: IEntityTypeConfiguration<AssociationMember>
	{
		public void Configure(EntityTypeBuilder<AssociationMember> builder)
		{
			builder.ToTable("AssociationMembers");
			builder.HasKey(a => a.ID);
		}
	}
}
