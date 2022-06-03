using Eccommerce.Api.Controllers;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using Xunit;

namespace Ecommerce.Testing
{
    public class ClientApiTesting
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly ClientController _controller;
        private readonly Mock<ILogger4Net> _logger;
        public ClientApiTesting() 
        {
            _clientRepository = new Mock<IClientRepository>();
            _logger = new Mock<ILogger4Net>();
            _controller = new ClientController(_clientRepository.Object, _logger.Object);
        }


        [Fact]
        public async void Create_Client_Sucess()
        {

            //Arrange
            var testViewModel = BuildClientViewModel();
            _clientRepository.Setup(cli => cli.CreateClient(It.IsAny<ClientDto>()).Result).Returns(1);
            
            //act
            var result = await _controller.CreateClient(testViewModel) as ObjectResult;


            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            
        }

        [Fact]
        public async void Create_Client_BadRequest()
        {
            //arange
            var testViewModel = BuildClientViewModel();
            _clientRepository.Setup(cli => cli.CreateClient(It.IsAny<ClientDto>()).Result).Returns(0);
            
            //act
            var result = await _controller.CreateClient(testViewModel) as BadRequestObjectResult;


            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);

        }

        [Fact]
        public async void Get_Client_ById_Ok() 
        {
            var testDto = new ClientDto
            {
                Id = 1,
                IdIdentityType = 1,
                IdentityTypeNumber = "123",
                IdentityTypeName = "Cedula ciudadanía",
                FirstName = "Test1",
                LastName = "TestLastName2",
                MobileNumber = "3203501085"
            };

            _clientRepository.Setup(cli => cli.GetClient(testDto.Id)).ReturnsAsync(testDto);

            var result = await _controller.Client(testDto.Id) as OkObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

        }

        [Fact]
        public async void Get_Client_ById_BadRequest()
        {
            var testDto = new ClientDto();
            testDto = null;

            _clientRepository.Setup(cli => cli.GetClient(1)).ReturnsAsync(testDto);

            var result = await _controller.Client(1) as NotFoundObjectResult;
            
            // assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);

        }


        private ClientViewModel BuildClientViewModel() 
        {
            return new ClientViewModel
            {
                IdIdentityType = 1,
                IdentityTypeNumber = "123",
                FirstName = "Test1",
                LastName = "TestLastName2",
                MobileNumber = "3203501085"
            };
        }
    }
}
