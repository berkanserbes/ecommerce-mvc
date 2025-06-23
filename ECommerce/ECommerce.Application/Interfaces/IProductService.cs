using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> GetAllProductsAsync();
	Task<Product?> GetProductByIdAsync(Guid id);
	Task AddProductAsync(Product product);
	Task UpdateProductAsync(Product product);
	Task DeleteProductAsync(Guid id);
	Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
	Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
}
