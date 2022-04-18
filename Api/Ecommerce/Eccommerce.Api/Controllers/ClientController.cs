using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Responses;
using Ecommerce.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eccommerce.Api.Controllers
{
    [Route("api/ecommerceclients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger4Net _logger;

        public ClientController(IClientRepository clientRepository, ILogger4Net logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var idClient = await _clientRepository.CreateClient(BuildProductDto(model));
                    if (idClient>0)
                        return Ok(idClient);
                    else
                        return BadRequest(new ResponseMessage { Message = "problem with client", Status = 1 });
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
        public async Task<IActionResult> Clients()
        {
            var clients = await _clientRepository.GetClients();
            if (clients.Count() > 0) 
            {            
                var response = new ApiResponse<IEnumerable<ClientDto>>(clients);
                return Ok(response);
            }
            else
                return NotFound(new ResponseMessage { Message = "Data no found", Status = 1 });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Client(int id)
        {
            try
            {
                var product = await _clientRepository.GetClient(id);
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
        public async Task<IActionResult> UpdateClient(int id, ClientViewModel model)
        {
            try
            {
                var wasUpdate = await _clientRepository.UpdateClient(id, BuildProductDto(model));
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
        public async Task<IActionResult> RemoveClient(int id)
        {
            try
            {
                var wasDeleted = await _clientRepository.DeleteClient(id);
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

        private ClientDto BuildProductDto(ClientViewModel model)
        {
            return new ClientDto
            {
                IdIdentityType = model.IdIdentityType,
                IdentityTypeNumber = model.IdentityTypeNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber
            };
        }
    }
}
