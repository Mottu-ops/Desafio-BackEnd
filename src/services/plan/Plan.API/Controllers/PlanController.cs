using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Utilities;
using Plan.API.ViewModels;
using Plan.Core.Execeptions;
using Plan.Service.DTO;
using Plan.Service.Interfaces;


namespace Plan.API.Controllers
{
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanServices _planServices;
        private readonly IMapper _mapper;

        public PlanController(IPlanServices planServices, IMapper mapper)
        {
            _mapper = mapper;
            _planServices = planServices;
        }

        [HttpPost]
        [Route("/api/v1/plans/create")]
        public async Task<IActionResult> Create([FromBody] CreatePlanViewModel planViewModel)
        {
            try
            {
                var planDto = _mapper.Map<PlanDto>(planViewModel);
                var newPlan = await _planServices.Create(planDto);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Plan added successfully",
                    Data = newPlan
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
        [Route("/api/v1/plans/update")]
        public async Task<IActionResult> Update([FromBody] UpdatePlanViewModel planViewModel)
        {
            try
            {
                var planDto = _mapper.Map<PlanDto>(planViewModel);
                var plan = await _planServices.Update(planDto);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Plan updated successfully",
                    Data = plan
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
        [Route("/api/v1/plans/delete/{id}/user/{userId}")]
        public async Task<IActionResult> Delete(long id, long userId)
        {

            try
            {
                await _planServices.Remove(id, userId);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Plan deleted successfully",
                    Data = {}
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
        [Route("/api/v1/plans/{id}/user/{userId}")]
        public async Task<IActionResult> Get(long id, long userId)
        {
            try
            {
                var planDto = await _planServices.Get(id, userId);
                if (planDto == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Plan not found",
                        Data = {}
                    });
                }
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Plan returned successfully",
                    Data = planDto
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
        [Route("/api/v1/plans/user/{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            try
            {
                var plans = await _planServices.GetAll(userId);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = plans.Count().Equals(0) ? "Not Plans at all..." : "Plans listed successfully",
                    Data = plans
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