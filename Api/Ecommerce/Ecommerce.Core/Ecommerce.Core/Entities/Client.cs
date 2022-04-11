using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Core.Entities
{
    public partial class Client
    {
        public Client()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public int IdIdentityType { get; set; }
        public string IdentityTypeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }

        public virtual IdentityType IdIdentityTypeNavigation { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
