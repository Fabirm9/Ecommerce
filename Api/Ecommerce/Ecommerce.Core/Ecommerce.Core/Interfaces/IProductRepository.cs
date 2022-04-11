using Ecommerce.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int idProduct);
        Task<bool> UpdateProduct(int id,ProductDto productDto);
        Task<int> CreateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int id);
    }
}
