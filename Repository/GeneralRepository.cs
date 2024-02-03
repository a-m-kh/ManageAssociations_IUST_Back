using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class GeneralRepository<T>: IGeneralRepository<T> where T : class, new()
{

	protected IUnitOfWork _uow;
	protected DbSet<T> TEntity;

	public GeneralRepository(IUnitOfWork uow){
			
		this._uow = uow;	
		this.TEntity =  _uow.Set<T>();
	}

	public virtual void Add(T entity)
	{
		TEntity.Add(entity);
	}

	public virtual int SaveChange()
	{
		return _uow.SaveChanges();
	}
	public virtual int SaveChanges(int UserID)
	{
		return _uow.SaveChanges(UserID);
	}
	public virtual Task<int> SaveChangesAsync(int? userID)
	{
		return _uow.SaveChangesAsync(userID);
	}
	public virtual Task<int> SaveChangesAsync()
	{
		return _uow.SaveChangesAsync();
	}
	public void RejectChanges()
	{
		_uow.RejectChanges();
	}
	public void Reaload()
	{
		_uow.Reload();
	}
	public virtual void Delete(T entity)
	{
		var x =  TEntity.Remove(entity);
	}

	public virtual T Find(Expression<Func<T, bool>> predicate)
	{
		var x = TEntity.Where(predicate).ToList();
		if(null != x && x.Count() == 1)
		{
			return x.First();
		}
		return null;
	}
	public virtual TResult Find<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectlist)
	{
		var x = TEntity.Where(predicate).Select(selectlist).ToList();
		if (null != x && x.Count() == 1)
		{
			return x.First();
		}
		return default(TResult);
	}
	public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
	{
		var x = await TEntity.Where(predicate).ToListAsync();
		if (null != x && x.Count() == 1)
		{
			return x.First();
		}
		return null;
	}
	public virtual async Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectlist)
	{
		var x = await TEntity.Where(predicate).Select(selectlist).ToListAsync();
		if (null != x && x.Count() == 1)
		{
			return x.First();
		}
		return default(TResult);
	}
	public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate)
	{
		return TEntity.Where(predicate).ToList();
	}

	public virtual IList<TResult> GetAll<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectList)
	{
		return TEntity.Where(predicate).Select(selectList).ToList();
	}

	public void Dispose()
	{
	}
}
