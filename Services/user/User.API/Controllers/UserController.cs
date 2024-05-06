using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Interfaces.Auth;
using User.API.Models;
using User.API.Utils;
using User.API.ViewModels;
using User.Domain.Entities;
using User.Domain.Exceptions;
using User.Services.Interfaces;

namespace User.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserController(IClientServices clientServices, IMapper mapper, IJwtService jwtService)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("/users/create")]
        public async Task<ActionResult> Create([FromBody] UserModel userModel)
        {
            try
            {
                var newClient = _mapper.Map<Client>(userModel);
                var newuser = await _clientServices.Create(newClient);

                return Ok(new BaseResultModel
                {
                    Success = true,
                    Message = "User Created",
                    MetaData = newuser
                });
            }
            catch (PersonalizeExceptions ex)
            {
                return BadRequest(GenericResponse.DomainError(ex.Message, ex.Err!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Message));
            }

        }


        [HttpPut]
        [Route("/users/update")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UserModel userModel)
        {
            try
            {
                var client = _mapper.Map<Client>(userModel);
                var user = await _clientServices.Update(client);
                return Ok(new BaseResultModel
                {
                    Success = true,
                    Message = "User updated",
                    MetaData = user
                });
            }
            catch (PersonalizeExceptions ex)
            {
                return BadRequest(GenericResponse.DomainError(ex.Message, ex.Err!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Message));
            }

        }


        [HttpDelete]
        [Route("/users/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(long id)
        {
            var loggedInUser = _jwtService.GetLoggedInUser();
            if (loggedInUser.Role != EnumRole.Admin)
            {
                Unauthorized(new BaseResultModel
                {
                    Success = false,
                    Message = "Login not authorized for this action",
                    MetaData = { }
                });
            }

            try
            {
                await _clientServices.Remove(id);
                return Ok(new BaseResultModel
                {
                    Success = true,
                    Message = "User deleted",
                    MetaData = { }
                });
            }
            catch (PersonalizeExceptions ex)
            {
                return BadRequest(GenericResponse.DomainError(ex.Message, ex.Err!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Message));
            }

        }



        [HttpGet]
        [Route("/users/{email}")]
        [Authorize]
        public async Task<ActionResult> Get(string email)
        {
            try
            {
                var client = await _clientServices.GetByEmail(email);
                return Ok(new BaseResultModel
                {
                    Success = true,
                    Message = "User localized",
                    MetaData = client
                });
            }
            catch (PersonalizeExceptions ex)
            {
                return BadRequest(GenericResponse.DomainError(ex.Message, ex.Err!));
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Message));
            }

        }

    }
}
