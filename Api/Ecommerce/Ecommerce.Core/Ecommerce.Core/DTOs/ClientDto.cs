namespace Ecommerce.Core.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public int IdIdentityType { get; set; }
        public string IdentityTypeNumber { get; set; }
        public string IdentityTypeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
    }
}
