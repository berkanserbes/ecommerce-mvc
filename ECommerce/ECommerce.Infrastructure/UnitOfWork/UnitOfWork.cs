using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Contexts;

namespace ECommerce.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly ECommerceDbContext _context;

	public UnitOfWork(ECommerceDbContext context, IProductRepository productRepository)
	{
		_context = context;
		ProductRepository = productRepository;
	}

	public IProductRepository ProductRepository { get; }

	public void Dispose()
	{
		_context.Dispose();
	}

	public async Task<int> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync();
	}
}
