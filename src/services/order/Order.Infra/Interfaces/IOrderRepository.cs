
using Order.Domain.Entities;

namespace Order.Infra.Interfaces;
public interface IOrderRepository : IBaseRepository<OrderEntity> {
    Task<List<OrderEntity>> GetAll();
}