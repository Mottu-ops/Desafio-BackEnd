using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rent.API.Utilities;
using Rent.API.ViewModels;
using Rent.Core.Exceptions;
using Rent.Service.DTO;
using Rent.Service.Interfaces;

namespace Rent.API.Controllers;

[ApiController]
public class RentController : ControllerBase {
    private readonly IRentService _rentService;
    private readonly IMapper _mapper;

    public RentController(IRentService rentService, IMapper mapper)
    {
        _rentService = rentService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/api/v1/rents/{id}")] 
    public async Task<IActionResult> Get(long id) {
        try{
            var rentDto = await _rentService.Get(id);
            if (rentDto == null) return StatusCode(404, "Rent not found");
            return Ok(new ResultViewModel
            {
                Message = "User found successfully",
                Data = rentDto,
                Success = true
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
    [Route("/api/v1/rents")] 
    public async Task<IActionResult> GetAll() {
        try{
            var rentsDto = await _rentService.GetAll();
            return Ok(new ResultViewModel
            {
                Message = "User found successfully",
                Data = rentsDto,
                Success = true
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

    [HttpPost]
    [Route("/api/v1/rents/create")] 
    public async Task<IActionResult> Create([FromBody] RentCreationViewModel rent) {
        try{
            var rentDto = _mapper.Map<TransactionDTO>(rent);
            var newRent = await _rentService.Create(rentDto);
            return Ok(new ResultViewModel
            {
                Message = "Rent created successfully",
                Success = true,
                Data = newRent
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
    [Route("/api/v1/rents/update")] 
    public async Task<IActionResult> Update([FromBody] RentUpdatingViewModel rent) {
        try{
            var rentDto = _mapper.Map<TransactionDTO>(rent);
            var updatedRent = await _rentService.Create(rentDto);
            return Ok(new ResultViewModel
            {
                Message = "Rent updated successfully",
                Success = true,
                Data = updatedRent
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
    [Route("/api/v1/rents/delete/{id}")] 
    public async Task<IActionResult> Delete(long id) {
        try{
            await _rentService.Remove(id);
            return Ok(new ResultViewModel
            {
                Message = "Rent deleted successfully",
                Success = true,
                Data = new {}
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
    [Route("/api/v1/rents/{id}/end-date/{endDate}/user-id")] 
    public async Task<IActionResult> SetEndDate(long id, string endDate, long userId) {
        try{
            var rent = await _rentService.SetEndDate(id, endDate, userId);
            return Ok(new ResultViewModel
            {
                Message = "Rent updated successfully",
                Success = true,
                Data = rent
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