using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public interface IGeneralRepository<T>: IDisposable where T : class , new()
{
	public void Add(T entity);
	public  int SaveChange();
	public  int SaveChanges(int UserID);
	public  Task<int> SaveChangesAsync(int? userID);
	public  Task<int> SaveChangesAsync();
	public  void Delete(T entity);
	public  T Find(Expression<Func<T, bool>> predicate);
	public  TResult Find<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectlist);
	public   Task<T> FindAsync(Expression<Func<T, bool>> predicate);
	public   Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectlist);
	public  IList<T> GetAll(Expression<Func<T, bool>> predicate);
	public IList<TResult> GetAll<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectList);
}
