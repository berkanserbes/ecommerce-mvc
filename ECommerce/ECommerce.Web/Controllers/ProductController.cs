using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Web.Models;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ECommerceDbContext _dbContext;

    public ProductController(IProductService productService, IUnitOfWork unitOfWork, ECommerceDbContext dbContext)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
        var viewModel = products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            UnitPrice = p.UnitPrice,
            Stock = p.Stock,
            IsActive = p.IsActive,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name
        });
        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        var viewModel = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            Stock = product.Stock,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await GetCategoriesSelectList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                UnitPrice = model.UnitPrice,
                Stock = model.Stock,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId
            };
            await _productService.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = await GetCategoriesSelectList();
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        var model = new ProductCreateEditViewModel
        {
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            Stock = product.Stock,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId
        };
        ViewBag.Categories = await GetCategoriesSelectList();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductCreateEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            product.Name = model.Name;
            product.UnitPrice = model.UnitPrice;
            product.Stock = model.Stock;
            product.IsActive = model.IsActive;
            product.CategoryId = model.CategoryId;
            await _productService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = await GetCategoriesSelectList();
        return View(model);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        var viewModel = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            Stock = product.Stock,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name
        };
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<SelectListItem>> GetCategoriesSelectList()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        return categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
    }
}
