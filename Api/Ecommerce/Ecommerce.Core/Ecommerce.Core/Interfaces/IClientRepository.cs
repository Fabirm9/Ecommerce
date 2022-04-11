using Ecommerce.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientDto>> GetClients();
        Task<ClientDto> GetClient(int idProduct);
        Task<bool> UpdateClient(int id, ClientDto productDto);
        Task<bool> CreateClient(ClientDto productDto);
        Task<bool> DeleteClient(int id);
    }
}
