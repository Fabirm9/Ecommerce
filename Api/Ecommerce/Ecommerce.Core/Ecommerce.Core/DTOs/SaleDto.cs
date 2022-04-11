namespace Ecommerce.Core.DTOs
{
    public class SaleDto
    {
        public int IdClient { get; set; }
        public string FullName { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
