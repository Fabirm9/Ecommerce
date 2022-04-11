using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class SaleViewModel
    {
        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "IdClient should be major that 0")]
        public int IdClient { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}
