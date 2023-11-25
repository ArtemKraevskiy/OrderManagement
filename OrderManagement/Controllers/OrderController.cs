using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models;
using OrderManagement.Models.ViewModels;
using OrderManagement.Services.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrderManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        private string _viewTable = "~/Views/Order/Partial/_orderTable.cshtml";
        private string _viewOrderDetails = "~/Views/Order/Partial/_orderDetailsForm.cshtml";
        private string _viewOrderSearch = "~/Views/Order/Partial/_orderSearchForm.cshtml";
        private string _viewError = "~/Views/Shared/_errorForm.cshtml";

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> SearchModel()
        {
            try
            {
                //throw new Exception();
                return PartialView(_viewOrderSearch, await _orderService.GetSearchModelAsync());
            }
            catch (Exception ex)
            {
                return PartialView(_viewError, new ErrorViewModel("Ошибка! Не удалось получить данные для поиска заказов!"));
            }
        }

        public async Task<IActionResult> List(OrderSearchViewModel SearchModel)
        {
            try
            {
                //throw new Exception();
                var list = await _orderService.GetAsync(SearchModel);
                return PartialView(_viewTable, list);
            }
            catch (Exception ex)
            {
                return PartialView(_viewError, new ErrorViewModel("Ошибка! Не удалось получить список заказов!"));
            }
        }

        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                //throw new Exception();
                var result = await _orderService.GetByIdAsync(Id);
                return PartialView(_viewOrderDetails, result);
            }
            catch (Exception ex)
            {
                return PartialView(_viewError, new ErrorViewModel("Ошибка! Не удалось получить детали заказа!"));
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                //throw new Exception();
                await _orderService.DeleteAsync(Id);
                return Ok("Заказ удалён!");
            }
            catch (Exception ex)
            {
                return Ok("Ошибка! Не удалось удалить заказ!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                //throw new Exception();
                var resultModel = await _orderService.CreateEditModelAsync(Id);
                return View(resultModel);
            }
            catch (Exception ex)
            {
                return View(_viewError, new ErrorViewModel("Ошибка! Не удалось получить детали заказа!"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel Model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //throw new Exception();
                    if (Model.Id == 0)
                        await _orderService.CreateAsync(Model);
                    else
                        await _orderService.UpdateAsync(Model);
                    return RedirectToAction(nameof(Index), new { Id = Model.Id });
                }
                catch (Exception ex)
                {
                    return View(_viewError, new ErrorViewModel("Ошибка! Не удалось добавить или отредактировать заказ!"));
                }
            }
            return View(Model);
        }

        public async Task<IActionResult> NumberValidation(NumberValidationViewModel Model)
        {
            //throw new Exception();
            var result = await _orderService.NumberValidationAsync(Model);
            return Ok(result);
        }
    }
}