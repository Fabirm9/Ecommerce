using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Core.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly EcommerceContext _context;

        public SaleRepository(EcommerceContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateSale(IList<SaleDto> SalesDto) 
        {
            if (SalesDto.Count > 0) 
            { 
                foreach (var item in SalesDto) 
                {
                    var Sale = new Sale
                    {
                        IdClient = item.IdClient,
                        IdProduct = item.IdProduct,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Total = item.Total
                    };
                    _context.Sales.Add(Sale);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<SaleDto>> GetSalesByUser(int id)
        {
            var salesByUser = await (from s in _context.Sales
                                     join c in _context.Clients on s.IdClient equals c.Id
                                     join p in _context.Products on s.IdProduct equals p.Id
                                     where s.IdClient == id
                                     select new SaleDto
                                     {
                                         IdClient= s.IdClient,
                                         FullName = $"{c.FirstName} {c.LastName}",
                                         IdProduct = s.IdProduct,
                                         ProductName = p.ProductName,
                                         UnitPrice = s.UnitPrice,
                                         Quantity = s.Quantity,
                                         Total = s.Total
                                     }).ToListAsync();

            var salesByUserDto = new List<SaleDto>();
            foreach (var item in salesByUser)
            {
                salesByUserDto.Add(new SaleDto
                {
                    
                    IdClient=item.IdClient,
                    FullName = item.FullName,
                    IdProduct = item.IdProduct,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total
                });
            }
            return salesByUserDto;
        }
    }    
}
