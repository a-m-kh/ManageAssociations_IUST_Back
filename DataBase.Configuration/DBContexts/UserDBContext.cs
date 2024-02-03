using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;

namespace DataBase.Configuration.DBContexts;

public class UserDBContext : IdentityDbContext<User, IdentityRole<string>, string>
{
	public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
	{

	}
		//public virtual DbSet<UserClaim> UserClaims { get; set; }
}
