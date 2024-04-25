
using Order.Service.DTO;

namespace Order.Service.Interfaces;

public interface IOrderServices {
    Task<OrderDTO> Create(OrderDTO orderDTO);
    Task<OrderDTO> Update(OrderDTO orderDTO);
    Task Remove(long id);
    Task<OrderDTO> Get(long id);
    Task<List<OrderDTO>> GetAll();
}