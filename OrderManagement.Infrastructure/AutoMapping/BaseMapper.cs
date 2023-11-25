using AutoMapper;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;

namespace OrderManagement.Infrastructure.AutoMapping
{
    public class BaseMapper : Profile
    {
        public BaseMapper()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap();

            CreateMap<OrderItemDto, OrderItemViewModel>()
                .ReverseMap();

            CreateMap<Provider, ProviderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();

            CreateMap<Order, OrderViewModel>()
                .ReverseMap();
        }
    }
}