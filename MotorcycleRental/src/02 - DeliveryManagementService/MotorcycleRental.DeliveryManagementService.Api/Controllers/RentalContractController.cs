using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.DeliveryManagementService.Api.Config.Extensions;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.DeliveryManagementService.Service.Services.RentalContractService;

namespace MotorcycleRental.DeliveryManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalContractController : BaseController
    {
        private readonly IRentalContractService _rentalContractService;

        public RentalContractController(IRentalContractService rentalContractService)
        {
            _rentalContractService = rentalContractService;
        }

        [ClaimsAuthorize("Admin", "admin")]
        [HttpGet("get-all-rental-contract-async")]
        public async Task<ActionResult> GetAllRentalContractAsync()
        {

            var rentalContracts = await _rentalContractService.GetAllAsync();

            return CustomResponse(rentalContracts);
        }

        [ClaimsAuthorize("Delivery", "user"),]
        [HttpGet("get-rental-contract-by-id-async/{id}")]
        public async Task<ActionResult> GetRentalContractByIdAsync(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                AddError("The id parameter must be informed!");
                return CustomResponse();
            }

            var rentalContracts = await _rentalContractService.GetByIdAsync(id);
            return CustomResponse(rentalContracts);
        }

        [ClaimsAuthorize("Delivery", "user")]
        [HttpPost("add-rental-contract-async")]
        public async Task<ActionResult> AddRentaContractAsync(
            [FromBody] RentalContractAddDto rentalContract,
            IValidator<RentalContractAddDto> validator)
        {
            var validation = await validator.ValidateAsync(rentalContract);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            await _rentalContractService.AddAsync(rentalContract);
            return CustomResponse();
        }
        [ClaimsAuthorize("Delivery", "user")]
        [HttpPut("update-rental-contract-async")]
        public async Task<ActionResult> UpdateDeliverymanAsync(
            [FromBody] RentalContractFullDto rentalContract,
            IValidator<RentalContractFullDto> validator)
        {
            var validation = await validator.ValidateAsync(rentalContract);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.ToCustomErrorResponse());

            await _rentalContractService.UpdateAsync(rentalContract);

            return CustomResponse();
        }


        [HttpDelete("delete-rental-contract-async")]
        public async Task<ActionResult> DeleteRentalContractAsync(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                AddError("The id parameter must be informed!");
                return CustomResponse();
            }

            await _rentalContractService.DeleteAsync(id);

            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpPost("check-total-rental-price-async")]
        public async Task<ActionResult> CheckTotalRentalPrice([FromBody] CheckRentalPriceInputDto checkRental,
                                                                IValidator<CheckRentalPriceInputDto> validator)
        {
            var validation = await validator.ValidateAsync(checkRental);
            if (!validation.IsValid)

                return BadRequest(validation.Errors.ToCustomErrorResponse());
            CheckTotalRentalPriceDto check = await _rentalContractService.CheckTotalRentalPrice(checkRental.ContractId, checkRental.EndDate);

            return CustomResponse(check);
        }
        [AllowAnonymous]
        [HttpPost("generate-simulation-plans-async")]
        public async Task<ActionResult> GenerateSimulationPlansAsync([FromBody] SimulationPeriodDto periodDto,
                                                                IValidator<SimulationPeriodDto> validator)
        {
            var validation = await validator.ValidateAsync(periodDto);
            if (!validation.IsValid)

                return BadRequest(validation.Errors.ToCustomErrorResponse());
            SimulationDto simulation = await _rentalContractService.SimulateValuesContract(periodDto.StartDate, periodDto.EndDate);

            return CustomResponse(simulation);
        }


    }
}
