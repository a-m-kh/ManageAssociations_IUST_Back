using Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class BaseDBContext : DbContext, IUnitOfWork
{


	public BaseDBContext() { }
	public BaseDBContext(DbContextOptions options)
		: base(options)
	{
	}

	public void RejectChanges()
	{
		foreach (var entry in this.ChangeTracker.Entries())
		{
			switch (entry.State)
			{
				case EntityState.Modified:
					{
						entry.State = EntityState.Unchanged;
						break;
					}
				case EntityState.Added:
					{
						entry.State = EntityState.Detached;
						break;
					}
			}
		}
	}
	public override int SaveChanges()
	{
		AuditFields();
		return base.SaveChanges();
	}
	public Task<int> SaveChangesAsync(int? userID)
	{
		if (null != userID)
		{
			AuditFields(userID.Value);
		}
		return base.SaveChangesAsync();
	}
	public int SaveChanges(int userID)
	{
		AuditFields(userID);
		return base.SaveChanges();
	}


	public Task<int> SaveChangesAsync(int userID)
	{
		AuditFields(userID);
		return base.SaveChangesAsync();

		
	}
	public new DbSet<TEntity> Set<TEntity>() where TEntity : class
	{
		return base.Set<TEntity>();
	}

	public void Reload()
	{
		foreach (var entry in this.ChangeTracker.Entries())
		{
			switch (entry.State)
			{
				case EntityState.Deleted:
					{
						entry.Reload();
						break;
					}
			}
		}
	}
	private void AuditFields(int userID = 0)
	{
		var auditDate = DateTime.Now;
		foreach (var entry in this.ChangeTracker.Entries<AuditEntityWithTypedId>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					{
						entry.Entity.CreateDateTime = auditDate;
						entry.Entity.LastModifiedDateTime = auditDate;
						if (userID != 0)
						{
							entry.Entity.CreatedBy = userID;
							entry.Entity.ModifiedBy = userID;
						}
						break;
					}
				case EntityState.Modified:
					{
						entry.Entity.LastModifiedDateTime = auditDate;
						if (userID != 0)
						{
							entry.Entity.ModifiedBy = userID;
						}
						break;
					}
			}
		}
	}
	
	public Task<int> SaveChangesAsync()
	{
		AuditFields();
		return base.SaveChangesAsync();
	}
}
