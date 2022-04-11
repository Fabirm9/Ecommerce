using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
