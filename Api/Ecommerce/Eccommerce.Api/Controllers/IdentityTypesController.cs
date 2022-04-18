using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eccommerce.Api.Controllers
{
    [Route("api/ecommerceidentitytypes")]
    [ApiController]
    public class IdentityTypesController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger4Net _logger;

        public IdentityTypesController(IClientRepository clientRepository, ILogger4Net logger) 
        {
            _clientRepository=clientRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes() 
        {
            try
            {
                var identityTypes = await _clientRepository.GetIdentityTypes();
                if (identityTypes != null) 
                {
                    var response = new ApiResponse<IEnumerable<IdentityTypeDto>>(identityTypes);
                    return Ok(response);
                }
                return NotFound(new ResponseMessage { Message = "Data no found", Status = 1 });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error{ ex.Message} {ex.StackTrace }");
                return BadRequest(new ResponseMessage { Message = "Something ocurried on server", Status = 2 });
            }
        }
    }
}
