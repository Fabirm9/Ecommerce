using Ecommerce.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ISaleRepository
    {
        Task<bool> CreateSale(IList<SaleDto> SaleViewModel);
        Task<IEnumerable<SaleDto>> GetSalesByUser(int id);
    }
}
