using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase;
using MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetAllMotorcycle;
using MotorcycleRental.Api.Core.Identity;

namespace MotorcycleRental.AdminManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MotorcycleController : BaseController
    {
        private readonly ILogger<MotorcycleController> _logger;

        public MotorcycleController(ILogger<MotorcycleController> logger)
        {
            _logger = logger;
        }
        [ClaimsAuthorize("Deliveryman", "user")]
        [HttpGet("get-all-motorcycle")]
        public async Task<ActionResult> GetAllMotorcycle([FromServices] IGetAllMotorcycleUseCase _getAllMotorcycle)
        {
            List<MotorcycleInputOutput> output = await _getAllMotorcycle.Execute();

            return CustomResponse(output);
        }

        [ClaimsAuthorize("Deliveryman", "user")]
        [HttpGet("get-motorcycle-by-place/{place}")]
        public async Task<ActionResult> GetMotorcycleByPlace(
            [FromServices] IGetMotorcycleByPlateUseCase _getMotorcycleByPlateUseCase, string place)

        {
            if (string.IsNullOrEmpty(place))
            {
                AddError("The plate parameter must be informed!");
                return CustomResponse();
            }

            MotorcycleInputOutput output = await _getMotorcycleByPlateUseCase.Execute(new GetMotorcycleByPlateInput(place));

            if (output == null)
            {
                AddError("License plate reported, not found!");
                return CustomResponse();
            }

            return CustomResponse(output);
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpPost("add-motorcycle")]
        public async Task<ActionResult> AddMotorcycle(
            [FromServices] IAddMotorcycleUseCase _addMotorcycleUseCase,
            [FromBody] AddMotorcycleInput input,
            IValidator<AddMotorcycleInput> validator)
        {
            var validation = await validator.ValidateAsync(input);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            AddMotorcycleOutput output = await _addMotorcycleUseCase.Execute(input);

            return CustomResponse(output);
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpPut("update-motorcycle")]
        public async Task<ActionResult> UpdateMotorcycle(
            [FromServices] IUpdateMotorcycleUseCase _updateMotorcycleUseCase,
            [FromBody] MotorcycleInputOutput input,
            IValidator<MotorcycleInputOutput> validator)
        {
            var validation = await validator.ValidateAsync(input);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            MotorcycleInputOutput result = await _updateMotorcycleUseCase.Execute(input);

            return CustomResponse(result);
        }
        [ClaimsAuthorize("Admin", "admin")]
        [HttpDelete("delete-motorcycle")]
        public async Task<ActionResult> DeleteMotorcycle(
            [FromServices] IDeleteMotorcycleUseCase _deleteMotorcycleUseCase, DeleteMotorcycleInput input)
        {
            await _deleteMotorcycleUseCase.Execute(input);

            return CustomResponse();
        }


    }
}
