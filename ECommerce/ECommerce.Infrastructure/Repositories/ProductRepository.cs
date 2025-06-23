using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
	
	public ProductRepository(ECommerceDbContext context) : base(context)
	{
		
	}

	public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
	{
		return await _dbSet
			.Include(p => p.Category)
			.AsNoTracking()
			.ToListAsync();
	}
}
