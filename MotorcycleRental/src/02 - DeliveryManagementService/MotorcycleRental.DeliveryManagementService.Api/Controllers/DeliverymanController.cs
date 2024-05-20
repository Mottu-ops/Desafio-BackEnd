using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.DeliveryManagementService.Service.Services.DeliverymanService;

namespace MotorcycleRental.DeliveryManagementService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DeliverymanController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDeliverymanService _deliverymanService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
                
        public DeliverymanController(IWebHostEnvironment webHostEnvironment, 
                                     IDeliverymanService deliverymanService)
        {
            _webHostEnvironment = webHostEnvironment;
            _deliverymanService = deliverymanService;
        }

        [ClaimsAuthorize("Deliveryman", "user")]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpGet("get-all-deliveryman-async")]
        public async Task<ActionResult> GetAllDeliverymanAsync()
        {
            List<DeliverymanFullDto> output = await _deliverymanService.GetAllAsync();

            return CustomResponse(output);
        }
    }
}
