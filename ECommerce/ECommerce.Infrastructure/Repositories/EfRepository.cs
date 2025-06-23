using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
	protected readonly DbContext _context;
	protected readonly DbSet<T> _dbSet;

	public EfRepository(DbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
	public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
	public virtual async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate) => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
	public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
	public virtual Task UpdateAsync(T entity)
	{
		_dbSet.Update(entity);
		return Task.CompletedTask;
	}
	public virtual Task DeleteAsync(T entity)
	{
		_dbSet.Remove(entity);
		return Task.CompletedTask;
	}
}
