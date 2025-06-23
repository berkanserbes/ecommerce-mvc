using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Contexts;

public class ECommerceDbContext : DbContext
{
	public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>()
			.HasOne(p => p.Category)
			.WithMany(c => c.Products)
			.HasForeignKey(p => p.CategoryId)
			.OnDelete(DeleteBehavior.Cascade);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);

		base.OnModelCreating(modelBuilder);
		
	}
}
