using DataBase.Configuration.ConfigEntities;
using DataBase.Configuration.DBContexts.Interface;
using DataBase.Configuration.Domain;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.DBContexts;

public class MainContext : BaseDBContext , IMainUnitOfWork
{
	public DbSet<Event>Events { get; set; }	
	public DbSet<Association> Associations { get;set; }


	public MainContext(DbContextOptions<MainContext> options):base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new EventConfiguration());
		modelBuilder.ApplyConfiguration(new AssociationConfiguration());
	}
}
