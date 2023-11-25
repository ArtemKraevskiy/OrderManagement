using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstractions
{
    public interface IOrderItemService
    {
        Task DeleteRangeAsync(int orderId);
        Task<List<OrderItemDto>> GetAsync(int orderId);
        void SetSerialNumbers(List<OrderItemViewModel> models);
    }
}
