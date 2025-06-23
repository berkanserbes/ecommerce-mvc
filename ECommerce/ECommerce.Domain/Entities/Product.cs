using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public class Product
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal UnitPrice { get; set; }
	public int Stock { get; set; }
	public bool IsActive { get; set; }
	public Guid CategoryId { get; set; }
	public Category? Category { get; set; }
}
