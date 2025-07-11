﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.ProductDTOs;

public class ProductDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public string? Description { get; set; }
	public Guid CategoryId { get; set; }
	public string? CategoryName { get; set; }
}
