using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;
public interface IUnitOfWork
{

	DbSet<TEntity> Set<TEntity>() where TEntity : class;

	int SaveChanges(int UserID);
	Task<int> SaveChangesAsync(int? userID);

	int SaveChanges();
	Task<int> SaveChangesAsync();

	void RejectChanges();

	void Reload();
	

}