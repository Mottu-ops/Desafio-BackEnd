using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Utilities;
using User.API.ViewModels;
using User.Core.Execeptions;
using User.Service.DTO;
using User.Service.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPartnerServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IPartnerServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<PartnerDto>(userViewModel);
                var newUser = await _userServices.Create(userDTO);

                return Ok(new ResultViewModel  
                {
                    Success = true,
                    Message = "User Created Successfully",
                    Data = newUser
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
        [Route("/api/v1/users/update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<PartnerDto>(userViewModel);
                var user = await _userServices.Update(userDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User updated successfully",
                    Data = user
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
        [Route("/api/v1/users/delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _userServices.Remove(id);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User deleted successfully",
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
        [Route("/api/v1/users/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var user = await _userServices.Get(id);
                if (user == null) {
                    return Ok(new ResultViewModel{
                        Success = true,
                        Message = "User not found",
                        Data = { }
                    });
                }
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User returned successfully",
                    Data = user
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
        [Route("/api/v1/users")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userServices.GetAll();
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = users.Count().Equals(0) ? "Not users at all..." : "Users listed successfully",
                    Data = users
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

