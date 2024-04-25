using AutoMapper;
using BusConnections.Core.Model;
using BusConnections.Events.Producer;
using Order.Infra.Interfaces;
using Order.Service.DTO;
using Order.Service.Interfaces;
using Order.Core.Exceptions;
using Order.Domain.Entities;

namespace Order.Service.Services;

public class OrderService : IOrderServices
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _OrderRepository;
    private readonly MbClient _mBClient;
    public OrderService(IMapper mapper, IOrderRepository OrderRepository, MbClient mBClient)
    {
        _mapper = mapper;
        _OrderRepository = OrderRepository;
        _mBClient = mBClient;
    }

    async Task<OrderDTO> IOrderServices.Create(OrderDTO orderDTO)
    {
        var _hasOrder = await _OrderRepository.Get(orderDTO.Id);
        if (_hasOrder != null) throw new DomainException("This Order is already registered");
        var order = _mapper.Map<OrderEntity>(orderDTO);
        order.Validate();
        var newOrder = await _OrderRepository.Create(order);
        return _mapper.Map<OrderDTO>(newOrder);
    }

    internal void CheckUserRole(long userId)
    {
        var user = _mBClient.Call(new Request
        {
            Method = "GetUser",
            Payload = new { Id = userId }
        });
        var userRole = user!.Payload.Role.ToString();
        if (userRole != "admin") throw new DomainException("The user must have administrator access.");
    }

    async Task<OrderDTO> IOrderServices.Get(long id)
    {
        var OrderDTO = await _OrderRepository.Get(id);
        return _mapper.Map<OrderDTO>(OrderDTO);
    }
    async Task<List<OrderDTO>> IOrderServices.GetAll()
    {
        var vehiclesDTO = await _OrderRepository.GetAll();
        return _mapper.Map<List<OrderDTO>>(vehiclesDTO);
    }

    async Task IOrderServices.Remove(long id)
    {
        await _OrderRepository.Delete(id);
    }

    async Task<OrderDTO> IOrderServices.Update(OrderDTO orderDTO)
    {
        var _hasOrder = await _OrderRepository.Get(orderDTO.Id);
        if (_hasOrder == null) throw new DomainException("Order not found");
        _hasOrder.Validate();
        var newOrder = await _OrderRepository.Update(_hasOrder);
        return _mapper.Map<OrderDTO>(newOrder);
    }
}
