using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.DeliveryManagementService.Api.Config.Extensions;
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

        [ClaimsAuthorize("Delivery", "user")]
        [HttpPost("add-deliveryman-async")]
        public async Task<ActionResult> AddPhotoCnh([FromBody] DeliverymanAddPhotoDto deliveryman)
        {
            if (string.IsNullOrEmpty(deliveryman.Id.ToString()))
            {
                AddError("The deliverymanId parameter must be informed!");
                return CustomResponse();
            }

            deliveryman.SetUrlImage(GetUrlLocalStorage());
            await _deliverymanService.AddPhotoCnh(deliveryman);
            return CustomResponse();
        }

        [ClaimsAuthorize("Delivery", "user")]
        [HttpPut("update-deliveryman-async")]
        public async Task<ActionResult> UpdateDeliverymanAsync(
            [FromBody] DeliverymanFullDto deliveryman,
            IValidator<DeliverymanFullDto> validator)
        {
            var validation = await validator.ValidateAsync(deliveryman);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            await _deliverymanService.UpdateAsync(deliveryman);

            return CustomResponse();
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpDelete("delete-deliveryman-async")]
        public async Task<ActionResult> DeleteDeliverymanAsync(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                AddError("The id parameter must be informed!");
                return CustomResponse();
            }

            await _deliverymanService.DeleteAsync(id);

            return CustomResponse();
        }

        private string GetUrlLocalStorage()
        {
            return Path.Combine(_webHostEnvironment.ContentRootPath, $"App_Data\\Images\\");
        }
    }
}
