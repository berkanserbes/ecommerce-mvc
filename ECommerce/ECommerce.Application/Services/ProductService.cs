using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<ProductService> _logger;

	public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger)
	{
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	public async Task<IEnumerable<Product>> GetAllProductsAsync()
	{
		try
		{
			return await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while retrieving all products.");
			throw new Exception("An error occurred while retrieving all products.", ex);
		}
	}

	public async Task<Product?> GetProductByIdAsync(Guid id)
	{
		try
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if(product == null)
			{
				_logger.LogWarning($"Product with ID {id} not found.");
				return null;
			}

			return product;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while retrieving the product by ID.");
			throw new Exception($"An error occurred while retrieving the product with ID {id}.", ex);
		}
	}

	public async Task AddProductAsync(Product product)
	{
		try
		{
			await _unitOfWork.ProductRepository.AddAsync(product);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while adding a new product.");
			throw new Exception("An error occurred while adding a new product.", ex);
		}
	}

	public async Task UpdateProductAsync(Product product)
	{
		try
		{
			var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(product.Id);
			if(existingProduct == null)
			{
				_logger.LogWarning($"Product with ID {product.Id} not found for update.");
				throw new Exception($"Product with ID {product.Id} not found.");
			}

			await _unitOfWork.ProductRepository.UpdateAsync(product);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while updating the product.");
			throw new Exception($"An error occurred while updating the product with ID {product.Id}.", ex);
		}
		
	}

	public async Task DeleteProductAsync(Guid id)
	{
		try
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null)
			{
				_logger.LogWarning($"Product with ID {id} not found for deletion.");
				throw new Exception($"Product with ID {id} not found.");
			}

			await _unitOfWork.ProductRepository.DeleteAsync(product);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			
			_logger.LogError($"An error occurred while deleting the product with ID {id}.", ex);
			throw new Exception($"An error occurred while deleting the product with ID {id}.", ex);
		}	
	}

	public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
	{
		try
		{
			return await _unitOfWork.ProductRepository.WhereAsync(p => p.CategoryId == categoryId);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while retrieving products by category.");
			throw new Exception($"An error occurred while retrieving products for category ID {categoryId}.", ex);
		}
		
	}

	public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
	{
		try
		{
			return await _unitOfWork.ProductRepository.WhereAsync(p => p.Name.Contains(searchTerm));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, "An error occurred while searching for products.");
			throw new Exception($"An error occurred while searching for products with term '{searchTerm}'.", ex);
		}
	}
}
