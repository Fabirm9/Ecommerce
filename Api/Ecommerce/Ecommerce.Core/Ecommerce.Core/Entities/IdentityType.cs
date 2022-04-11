using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Core.Entities
{
    public partial class IdentityType
    {
        public IdentityType()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string IdentityType1 { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
