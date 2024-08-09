using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rent_core_api.Controllers;
using rent_core_api.Model;
using rent_core_api.Service;
using rent_core_api.ViewModel;

namespace rent_core_api_test
{
    public class LocacaoControllerTest
    {
        private readonly Mock<LocacaoService> _mockLocacaoService;
        private readonly LocacaoController _locacaoController;


        public LocacaoControllerTest()
        {
            _mockLocacaoService = new Mock<LocacaoService>();
            _locacaoController = new LocacaoController(_mockLocacaoService.Object);
        }

        [Fact]
        public void CriarLocacaoValidRequestReturnsOk()
        {
            var locacaoMock = new Locacao { Id = 1, IdMoto = 1, IdEntregador = 1, IdPlano = 1 };
            _mockLocacaoService.Setup(service => service.CriarLocacao(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(locacaoMock);

            var result = _locacaoController.CriarLocacao(1, 1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var locacao = Assert.IsType<Locacao>(okResult.Value);
            Assert.Equal(1, locacao.Id);
        }

        [Fact]
        public void CriarLocacaoExceptionThrownReturnsBadRequest()
        {
            _mockLocacaoService.Setup(service => service.CriarLocacao(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Erro ao criar locação"));

            var result = _locacaoController.CriarLocacao(1, 1, 1);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao criar locação", badRequestResult.Value);
        }

        [Fact]
        public void FinalizarLocacaoValidRequestReturnsOk()
        {
            var locacaoMock = new Locacao { Id = 1, IdMoto = 1, IdEntregador = 1, IdPlano = 1 };
            _mockLocacaoService.Setup(service => service.FinalizarLocacao(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(locacaoMock);

            var result = _locacaoController.FinalizarLocacao(1, DateTime.Now);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var locacao = Assert.IsType<Locacao>(okResult.Value);
            Assert.Equal(1, locacao.Id);
        }

        [Fact]
        public void FinalizarLocacao_ExceptionThrown_ReturnsBadRequest()
        {
            _mockLocacaoService.Setup(service => service.FinalizarLocacao(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new Exception("Erro ao finalizar locação"));

            var result = _locacaoController.FinalizarLocacao(1, DateTime.Now);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao finalizar locação", badRequestResult.Value);
        }
    }
}