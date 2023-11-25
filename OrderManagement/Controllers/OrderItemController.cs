using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models.ViewModels;
using OrderManagement.Services.Abstractions;

namespace OrderManagement.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IOrderItemService _orderItemService;

        private string _viewOrderItemCreate = "~/Views/OrderItem/Partial/_orderItemEditForm.cshtml";
        private string _viewOrderItemTable = "~/Views/OrderItem/Partial/_orderItemTable.cshtml";
        private string _viewError = "~/Views/Shared/_errorForm.cshtml";

        public OrderItemController(ILogger<OrderItemController> logger, IOrderItemService orderItemService)
        {
            _logger = logger;
            _orderItemService = orderItemService;
        }

        //public async Task<IActionResult> List(int OrderId)
        //{
        //    try
        //    {
        //        throw new Exception();
        //        var list = await _orderItemService.GetAsync(OrderId);
        //        return PartialView(_viewOrderItemTable, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        return PartialView(_viewError, new ErrorViewModel("Ошибка! Не удалось получить список элементов заказа!"));
        //    }
        //}

        public IActionResult Create(int SerialNumber)
        {
            return PartialView(_viewOrderItemCreate, new OrderItemViewModel { SerialNumber = SerialNumber });
        }
    }
}
