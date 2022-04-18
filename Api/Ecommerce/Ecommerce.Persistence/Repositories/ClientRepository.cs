using Ecommerce.Core.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly EcommerceContext _context;

        public ClientRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientDto>> GetClients()
        {
            var clients = await _context.Clients.Include(idt =>idt.IdIdentityTypeNavigation).ToListAsync();
            var clientsDto = new List<ClientDto>();
            foreach (var item in clients)
            {
                clientsDto.Add(new ClientDto
                {
                    Id = item.Id,
                    IdIdentityType = item.IdIdentityType,
                    IdentityTypeNumber = item.IdentityTypeNumber,
                    IdentityTypeName = item.IdIdentityTypeNavigation.IdentityType1,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MobileNumber = item.MobileNumber
                });
            }

            return clientsDto;
        }

        public async Task<ClientDto> GetClient(int idClient)
        {
            var client = await _context.Clients.Where(x => x.Id == idClient).SingleOrDefaultAsync();
            if (client != null)
            {
                var clientDto = new ClientDto
                {
                    Id = client.Id,
                    IdIdentityType = client.IdIdentityType,
                    IdentityTypeNumber = client.IdentityTypeNumber,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    MobileNumber = client.MobileNumber
                };

                return clientDto;
            }
            return null;
        }

        public async Task<int> CreateClient(ClientDto clientDto)
        {
            var client = new Client
            {
                IdIdentityType = clientDto.IdIdentityType,
                IdentityTypeNumber = clientDto.IdentityTypeNumber,
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                MobileNumber = clientDto.MobileNumber
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client.Id > 0 ? client.Id : 0;
        }

        public async Task<bool> UpdateClient(int id, ClientDto clientDto)
        {
            var existClient = _context.Clients.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
            if (existClient != null)
            {
                var entry = new Client
                {
                    Id= id,
                    IdIdentityType = clientDto.IdIdentityType,
                    IdentityTypeNumber = clientDto.IdentityTypeNumber,
                    FirstName = clientDto.FirstName,
                    LastName = clientDto.LastName,
                    MobileNumber = clientDto.MobileNumber
                };
                _context.Entry(entry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<IdentityTypeDto>> GetIdentityTypes() 
        {
            var identityTypes = await _context.IdentityTypes.ToListAsync();
            var identityTypesList = new List<IdentityTypeDto>();

            foreach (var item in identityTypes) 
            {
                identityTypesList.Add(new IdentityTypeDto
                {
                    Id = item.Id,
                    IdentityTypeName = item.IdentityType1
                });
            }
            return identityTypesList;
        }
    }
}
