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
    [Route("api/ecommercesale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger4Net _logger;
        public SaleController(ISaleRepository saleRepository, ILogger4Net logger) 
        {
            _logger = logger;
            _saleRepository = saleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Sales(SalesViewModel salesViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdSales = await _saleRepository.CreateSale(BuildSalesDto(salesViewModel));
                    if(createdSales)
                        return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error - {ex.Message} - {ex.StackTrace}");
                return BadRequest("Error");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataByUSer(int id)
        {
            var salesByUser = await _saleRepository.GetSalesByUser(id);
            if (salesByUser != null)
            {
                var response = new ApiResponse<IEnumerable<SaleDto>>(salesByUser);
                return Ok(response);
            }
            else
                return NotFound(new ResponseMessage { Message = "Data no found", Status = 1 });
        }

        private IList<SaleDto> BuildSalesDto(SalesViewModel saleViewModel) 
        {
            var salesDto = new List<SaleDto>();
            foreach (var sale in saleViewModel.SaleViewModel)
            {
                salesDto.Add(new SaleDto
                {
                    IdClient = sale.IdClient,
                    IdProduct = sale.IdProduct,
                    Quantity = sale.Quantity,
                    UnitPrice = sale.UnitPrice,
                    Total = sale.Total
                });
            }
            return salesDto;
        }
    }
}
