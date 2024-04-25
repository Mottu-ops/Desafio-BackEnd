using AutoMapper;
using Order.API.ViewModels;
using Order.Service.DTO;

namespace Order.API.Profiles;
 
public class OrderProfile : Profile {
    public OrderProfile() {
        CreateMap<CreateOrderViewModel, OrderDTO>();
        CreateMap<UpdateOrderViewModel, OrderDTO>();
    }
}