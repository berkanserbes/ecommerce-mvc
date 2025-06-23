﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ECommerce.Domain.Interfaces;

public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(Guid id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
