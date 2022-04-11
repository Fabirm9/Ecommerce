using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Responses;
using Ecommerce.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eccommerce.Api.Controllers
{
    [Route("api/ecommerceproducts")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger4Net _logger;

        public ProductController(IProductRepository productRepository, ILogger4Net logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var idProduct = await _productRepository.CreateProduct(BuildProductDto(model));
                    if (idProduct > 0)
                        return Ok(idProduct);
                    else
                        return BadRequest(new ResponseMessage { Message = "created product", Status = 0 });
                }
                return BadRequest(new ResponseMessage { Message = "Problem with Model", Status = 2 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error{ ex.Message} {ex.StackTrace }");
                return BadRequest(new ResponseMessage { Message = "Something ocurried on server", Status = 2 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = await _productRepository.GetProducts();
            if (products != null)
            {
                var response = new ApiResponse<IEnumerable<ProductDto>>(products);
                return Ok(response);
            }
            else
                return NotFound(new ResponseMessage { Message = "Data no found", Status = 1 });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product != null)
                    return Ok(product);
                else
                    return NotFound(new ResponseMessage { Message = "Data no found", Status = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error - {ex.Message} - {ex.StackTrace}");
                return BadRequest("Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductViewModel model)
        {
            try
            {
                var wasUpdate = await _productRepository.UpdateProduct(id, BuildProductDto(model));
                if (wasUpdate)
                    return Ok(new ResponseMessage { Message = "updated register", Status = 0 });
                else
                    return NotFound(new ResponseMessage { Message = "No found register", Status = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error{ ex.Message} {ex.StackTrace }");
                return BadRequest(new ResponseMessage { Message = "Something ocurried on server", Status = 2 });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var wasDeleted = await _productRepository.DeleteProduct(id);
                if (wasDeleted)
                    return Ok(new ResponseMessage { Message = "deleted register", Status = 0 });
                else
                    return NotFound(new ResponseMessage { Message = "No found register", Status = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error{ ex.Message} {ex.StackTrace }");
                return BadRequest(new ResponseMessage { Message = "Something ocurried on server", Status = 2 });
            }
        }

        private ProductDto BuildProductDto(ProductViewModel model)
        {
            return new ProductDto
            {
                ProductName = model.ProductName,
                Price = model.Price
            };
        }
    }
}
