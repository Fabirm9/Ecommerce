#nullable disable

namespace Ecommerce.Core.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
