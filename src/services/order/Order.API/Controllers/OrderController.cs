
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.API.ViewModels;
using Order.Service.DTO;
using Order.Service.Interfaces;
using Order.API.Utilities;
using Order.Core.Exceptions;

namespace Order.API.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _OrdersServices;
        private readonly IMapper _mapper;

        public OrderController(IOrderServices OrderServices, IMapper mapper)
        {
            _mapper = mapper;
            _OrdersServices = OrderServices;
        }

        [HttpPost]
        [Route("/api/v1/Orders/create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderViewModel order)
        {
            try
            {
                var orderDTO = _mapper.Map<OrderDTO>(order);
                var newOrder = await _OrdersServices.Create(orderDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Order added successfully",
                    Data = newOrder
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
        [Route("/api/v1/Orders/update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderViewModel order)
        {
            try
            {
                var orderDTO = _mapper.Map<OrderDTO>(order);
                var orderUpdated = await _OrdersServices.Update(orderDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Order updated successfully",
                    Data = orderUpdated
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
        [Route("/api/v1/Orders/delete/{id}")]
        public async Task<IActionResult> Delete(long id, long userId)
        {
            try
            {
                await _OrdersServices.Remove(id);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Order deleted successfully",
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
        [Route("/api/v1/Orders/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var order = await _OrdersServices.Get(id);
                if (order == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Order not found",
                        Data = { }
                    });
                }
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Order returned successfully",
                    Data = order
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
        [Route("/api/v1/Orders/user")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Orders = await _OrdersServices.GetAll();
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = Orders.Count().Equals(0) ? "Not Orders at all..." : "Orders listed successfully",
                    Data = Orders
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