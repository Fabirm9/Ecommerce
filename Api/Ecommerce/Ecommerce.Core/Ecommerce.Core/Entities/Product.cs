using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Core.Entities
{
    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
