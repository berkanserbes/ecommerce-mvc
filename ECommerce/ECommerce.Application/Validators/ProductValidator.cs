using ECommerce.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Validators;

public class ProductValidator : AbstractValidator<Product>
{
	public ProductValidator()
	{
		RuleFor(p => p.Name)
			.NotEmpty().WithMessage("Product name cannot be empty.")
			.Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");

		RuleFor(p => p.UnitPrice)
			.GreaterThan(0).WithMessage("Unit price must be greater than zero.");

		RuleFor(p => p.Stock)
			.GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
	}
}
