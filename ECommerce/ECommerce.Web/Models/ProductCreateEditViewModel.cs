using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;

public class ProductCreateEditViewModel
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public bool IsActive { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}