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
    public class EntregadorControllerTest
    {
        private readonly Mock<EntregadorService> _entregadorServiceMock;
        private readonly EntregadorController _controller;

        public EntregadorControllerTest()
        {
            _entregadorServiceMock = new Mock<EntregadorService>();
            _controller = new EntregadorController(_entregadorServiceMock.Object);
        }

        [Fact]
        public void AddValidRequestReturnsOk()
        {
            var request = new EntregadorRequest
            {
                nome = "Teste",
                cnpj = "12345678000199",
                dataNascimento = new DateOnly(1990, 1, 1),
                numeroCnh = "12345678901",
                tipoCnh = "A",
                photo = null};

            var result = _controller.Add(request);

            result.Should().BeOfType<OkResult>();
            _entregadorServiceMock.Verify(x => x.Add(It.IsAny<Entregador>()), Times.Once);
        }

        [Fact]
        public void AddInvalidImageFormatReturnsBadRequest()
        {
            var request = new EntregadorRequest
            {
                nome = "Teste",
                cnpj = "12345678000199",
                dataNascimento = new DateOnly(1990, 1, 1),
                numeroCnh = "12345678901",
                tipoCnh = "A",
                photo = new FormFile(null, 0, 0, null, "foto.gif") 
            };

            var result = _controller.Add(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Formato de imagem inválido. Somente PNG e BMP são suportados.");
            _entregadorServiceMock.Verify(x => x.Add(It.IsAny<Entregador>()), Times.Never);
        }

        [Fact]
        public void UpdateEntregadorFail()
        {
            var mockEntregadorService = new Mock<EntregadorService>();

            var mockFile = new Mock<IFormFile>();
            var content = "Fake file content";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(ms.Length);
            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.CopyTo(It.IsAny<Stream>())).Callback<Stream>(s => ms.CopyTo(s));

            var entregadorRequest = new EntregadorPhotoRequest
            {
                Photo = mockFile.Object
            };

            var controller = new EntregadorController(mockEntregadorService.Object);

            var result = controller.UpdateCadastroEntregador(1, entregadorRequest);

            var failResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, failResult.StatusCode);
        }
    }
}