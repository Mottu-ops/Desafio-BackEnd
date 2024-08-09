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
    public class MotoControllerTest
    {
        private readonly Mock<MotoService> _mockMotoService;
        private readonly MotoController _motoController;


        public MotoControllerTest()
        {
            _mockMotoService = new Mock<MotoService>();
            _motoController = new MotoController(_mockMotoService.Object);
        }

        [Fact]
        public void AddValidRequestReturnsOk()
        {
            var motoRequest = new MotoRequest { ano = 2020, modelo = "ModelX", placa = "ABC1234" };

            var result = _motoController.Add(motoRequest);

            Assert.IsType<OkResult>(result);
            _mockMotoService.Verify(service => service.Add(It.IsAny<Moto>()), Times.Once);
        }

        [Fact]
        public void GetReturnsListOfMotos()
        {
            var motosMock = new List<Moto> { new Moto(2020, "ModelX", "ABC1234"), new Moto(2021, "ModelY", "DEF5678") };
            _mockMotoService.Setup(service => service.GetAll()).Returns(motosMock);

            var result = _motoController.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var motos = Assert.IsType<List<Moto>>(okResult.Value);
            Assert.Equal(2, motos.Count);
        }

        [Fact]
        public void GetByPlacaValidPlaca_ReturnsMoto()
        {
            var motoMock = new Moto(2020, "ModelX", "ABC1234");
            _mockMotoService.Setup(service => service.GetByPlaca("ABC1234")).Returns(motoMock);

            var result = _motoController.GetByPlaca("ABC1234");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var moto = Assert.IsType<Moto>(okResult.Value);
            Assert.Equal("ABC1234", moto.placa);
        }

        [Fact]
        public void GetByPlacaInvalidPlacaReturnsNotFound()
        {
            _mockMotoService.Setup(service => service.GetByPlaca("ZZZ9999")).Returns((Moto)null);

            var result = _motoController.GetByPlaca("ZZZ9999");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdatePlacaValidRequestReturnsOk()
        {
            var motoMock = new Moto(2020, "ModelX", "ABC1234");
            _mockMotoService.Setup(service => service.GetById(1)).Returns(motoMock);

            var result = _motoController.UpdatePlaca(1, "XYZ5678");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedMoto = Assert.IsType<Moto>(okResult.Value);
            Assert.Equal("XYZ5678", updatedMoto.placa);
        }

        [Fact]
        public void RemoverMotoValidIdReturnsNoContent()
        {
            var result = _motoController.RemoverMoto(1);

            Assert.IsType<NoContentResult>(result);
            _mockMotoService.Verify(service => service.Delete(1), Times.Once);
        }

        [Fact]
        public void RemoverMotoExceptionThrownReturnsBadRequest()
        {
            _mockMotoService.Setup(service => service.Delete(1)).Throws(new Exception("Erro ao remover moto"));

            var result = _motoController.RemoverMoto(1);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao remover moto", badRequestResult.Value);
        }

    }
}