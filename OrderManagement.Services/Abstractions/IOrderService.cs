using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstractions
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAsync(OrderSearchViewModel searchModel);
        Task<OrderDto> GetByIdAsync(int Id);
        Task CreateAsync(OrderViewModel model);
        Task UpdateAsync(OrderViewModel model);
        Task DeleteAsync(int Id);
        Task<OrderViewModel> CreateEditModelAsync(int Id);
        Task<bool> NumberValidationAsync(NumberValidationViewModel numberValidationModel);
        Task<OrderSearchViewModel> GetSearchModelAsync();
    }
}
