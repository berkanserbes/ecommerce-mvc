﻿using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
	Task<IEnumerable<Product>> GetAllWithCategoryAsync();
}
