using Ecommerce.Core.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productsDto = new List<ProductDto>();
            foreach (var item in products)
            {
                productsDto.Add(new ProductDto
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Price = item.Price
                });
            }

            return productsDto;
        }

        public async Task<ProductDto> GetProduct(int idProduct)
        {
            var product = await _context.Products.Where(x => x.Id == idProduct).SingleOrDefaultAsync();
            if (product != null)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price
                };

                return productDto;
            }
            return null;
        }


        public async Task<int> CreateProduct(ProductDto productDto) 
        {            
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> UpdateProduct(int id, ProductDto productDto)
        {
            var existProduct = _context.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
            if (existProduct != null)
            {
                var entry = new Product
                {
                    Id = id,
                    ProductName = productDto.ProductName,
                    Price = productDto.Price
                };
                _context.Entry(entry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

        public async Task<bool> DeleteProduct(int id) 
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null) { 
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
