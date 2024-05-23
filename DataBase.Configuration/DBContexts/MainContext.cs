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
	public virtual DbSet<Event>Events { get; set; }
	public virtual DbSet<Association> Associations { get;set; }
	public virtual DbSet<BaseInfo> BaseInfos { get; set; }
	public virtual DbSet<MovementFestival> MovementFestivals { get; set; }
	//public virtual DbSet<Communication> Communications { get; set; }
	//public virtual DbSet<Guest> Guests { get; set; }

	public MainContext(DbContextOptions<MainContext> options):base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new BaseInfoConfiguration());
		modelBuilder.ApplyConfiguration(new EventConfiguration());
		modelBuilder.ApplyConfiguration(new AssociationConfiguration());
		modelBuilder.ApplyConfiguration(new CommunicationConfiguration());
		modelBuilder.ApplyConfiguration(new GuestConfiguration());
		modelBuilder.ApplyConfiguration(new AssociationMemberConfiguration());
		modelBuilder.ApplyConfiguration(new JournalConfiguration());
		modelBuilder.ApplyConfiguration(new CertificateOfAbsenceConfiguration());
		modelBuilder.ApplyConfiguration(new CertificationConfiguration());
		modelBuilder.ApplyConfiguration(new ParticipantsOfCertificateConfiguration());
		modelBuilder.ApplyConfiguration(new ImageOfReportConfiguration());
		modelBuilder.ApplyConfiguration(new ReportConfiguration());
		modelBuilder.ApplyConfiguration(new NewsConfiguration());
		modelBuilder.ApplyConfiguration(new NewsImagesConfiguration());
		modelBuilder.ApplyConfiguration(new ParticipantsOfCertificateOfAbsenceConfiguration());
		modelBuilder.ApplyConfiguration(new MovementFestivalConfiguration());
		modelBuilder.ApplyConfiguration(new CompetitiveFieldConfiguration());
		modelBuilder.ApplyConfiguration(new PositionConfiguration());
		modelBuilder.ApplyConfiguration(new SliderImageConfiguration());
	}
}
