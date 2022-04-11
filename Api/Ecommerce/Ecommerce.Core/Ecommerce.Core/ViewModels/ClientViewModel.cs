using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class ClientViewModel
    {
        [Required]
        public int IdIdentityType { get; set; }
        [Required]
        public string IdentityTypeNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
    }
}
