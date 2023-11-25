using AutoMapper;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using OrderManagement.Repositories.Abstractions;
using OrderManagement.Services.Abstractions;

namespace OrderManagement.Services.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<OrderItem> _orderItemRepository;

        public OrderItemService(IMapper mapper, IBaseRepository<OrderItem> orderItemRepository)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderItemDto>> GetAsync(int orderId)
        {
            var entities = await _orderItemRepository.GetAsync(_ => _.OrderId == orderId);
            var models = _mapper.Map<List<OrderItemDto>>(entities);
            return models;
        }

        public void SetSerialNumbers(List<OrderItemViewModel> models)
        {
            models = models.Select((model, index) =>
            {
                model.SerialNumber = index;
                return model;
            }).ToList();
        }

        public async Task DeleteRangeAsync(int orderId)
        {
            await _orderItemRepository.DeleteRangeAsync(_ => _.OrderId == orderId);
            await _orderItemRepository.CommitAsync();
        }
    }
}
