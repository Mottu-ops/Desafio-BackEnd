using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle.API.Utilities;
using Motorcycle.API.ViewModels;
using Motorcycle.Core.Exceptions;
using Motorcycle.Service.DTO;
using Motorcycle.Service.Interfaces;


namespace Motorcycle.API.Controllers
{
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IMotorcycleServices _motorcyclesServices;
        private readonly IMapper _mapper;

        public MotorcyclesController(IMotorcycleServices motorcycleServices, IMapper mapper)
        {
            _mapper = mapper;
            _motorcyclesServices = motorcycleServices;
        }

        [HttpPost]
        [Route("/api/v1/motorcycles/create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleViewModel vehicleViewModel)
        {
            try
            {
                var vehicleDTO = _mapper.Map<VehicleDto>(vehicleViewModel);
                var newVehicle = await _motorcyclesServices.Create(vehicleDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Motorcycle added successfully",
                    Data = newVehicle
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }
        }

        [HttpPut]
        [Route("/api/v1/motorcycles/update")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleViewModel vehicleViewModel)
        {
            try
            {
                var vehicleDto = _mapper.Map<VehicleDto>(vehicleViewModel);
                var motorcycle = await _motorcyclesServices.Update(vehicleDto);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Motorcycle updated successfully",
                    Data = motorcycle
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }
        }
        [HttpDelete]
        [Route("/api/v1/motorcycles/delete/{id}/user/{userId}")]
        public async Task<IActionResult> Delete(long id, long userId)
        {
            try
            {
                await _motorcyclesServices.Remove(id, userId);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Motorcycle deleted successfully",
                    Data = { }
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }
        }
        [HttpGet]
        [Route("/api/v1/motorcycles/{id}/user/{userId}")]
        public async Task<IActionResult> Get(long id, long userId)
        {
            try
            {
                var motorcycleDto = await _motorcyclesServices.Get(id, userId);
                if (motorcycleDto == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Motorcycle not found",
                        Data = { }
                    });
                }
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Motorcycle returned successfully",
                    Data = motorcycleDto
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }

        }
        [HttpGet]
        [Route("/api/v1/motorcycles/{plateCode}/user/{userId}")]
        public async Task<IActionResult> Get(string plateCode, long userId)
        {
            try
            {
                var motorcycleDto = await _motorcyclesServices.Get(plateCode, userId);
                if (motorcycleDto == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Motorcycle not found",
                        Data = { }
                    });
                }
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Motorcycle returned successfully",
                    Data = motorcycleDto
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }

        }

        [HttpGet]
        [Route("/api/v1/motorcycles/user/{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            try
            {
                var motorcycles = await _motorcyclesServices.GetAll(userId);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = motorcycles.Count().Equals(0) ? "Not motorcycles at all..." : "Motorcycles listed successfully",
                    Data = motorcycles
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
            }

        }
    }
}