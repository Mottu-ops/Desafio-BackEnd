using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.AddRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.DeleteRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetAllRentalPlan;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetRentalPlanById;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan;
using MotorcycleRental.Api.Core.Identity;

namespace MotorcycleRental.AdminManagementService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RentalPlanController : BaseController
    {
        private readonly ILogger<RentalPlanController> _logger;

        public RentalPlanController(ILogger<RentalPlanController> logger)
        {
            _logger = logger;
        }
        [ClaimsAuthorize("Deliveryman", "user")]
        [HttpGet("get-all-rental-plan")]
        public async Task<ActionResult> GetAllRentalPlan([FromServices] IGetAllRentalPlanUseCase _getAllRentalPlan)
        {
            List<RentalPlanInputOutput> output = await _getAllRentalPlan.Execute();

            return CustomResponse(output);
        }
        [ClaimsAuthorize("Deliveryman", "user")]
        [HttpGet("get-rental-plan-by-id/{id}")]
        public async Task<ActionResult> GetRentalPlanByid(
            [FromServices] IGetRentalPlanByIdUseCase _getRentalPlanById, Guid id)

        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                AddError("The id parameter must be informed!");
                return CustomResponse();
            }

            RentalPlanInputOutput output = await _getRentalPlanById.Execute(id);

            if (output == null)
            {
                AddError("Rental Plan reported, not found!");
                return CustomResponse();
            }

            return CustomResponse(output);
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpPost("add-rental-plan")]
        public async Task<ActionResult> AddRentalPlan(
            [FromServices] IAddRentalPlanUseCase _addRentalPlan,
            [FromBody] AddRentalPlanInput input,
            IValidator<AddRentalPlanInput> validator)
        {
            var validation = await validator.ValidateAsync(input);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            AddRentalPlanOutput output = await _addRentalPlan.Execute(input);

            return CustomResponse(output);
        }
        [ClaimsAuthorize("Admin", "admin")]
        [HttpPut("update-rental-plan")]
        public async Task<ActionResult> UpdateRentalPlan(
            [FromServices] IUpdateRentalPlanUseCase _updateRentalPlan,
            [FromBody] UpdateRentalPlanIntput input,
            IValidator<UpdateRentalPlanIntput> validator)
        {
            var validation = await validator.ValidateAsync(input);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            RentalPlanInputOutput result = await _updateRentalPlan.Execute(input);

            return CustomResponse(result);
        }
        [ClaimsAuthorize("Admin", "admin")]
        [HttpDelete("delete-rental-plan")]
        public async Task<ActionResult> DeleteRentalPlan(
            [FromServices] IDeleteRentalPlanUseCase _deleteRentalPlan, Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                AddError("The id parameter must be informed!");
                return CustomResponse();
            }

            await _deleteRentalPlan.Execute(id);

            return CustomResponse();
        }


    }
}
