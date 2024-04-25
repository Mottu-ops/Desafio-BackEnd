using AutoMapper;
using Order.Domain.Entities;
using Order.Service.DTO;

namespace Order.Service.Profiles;
public class OrderAutoMapperProfile : Profile {
    public OrderAutoMapperProfile() {
        CreateMap<OrderDTO, OrderEntity>();
        CreateMap<OrderEntity, OrderDTO>();
    }
}