using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models.ViewModels;
using OrderManagement.Services.Abstractions;
using OrderManagement.Services.Services;

namespace OrderManagement.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IProviderService _providerService;

        private string _viewOrderItemTable = "~/Views/Provider/Partial/_providerTable.cshtml";
        private string _viewError = "~/Views/Shared/_errorForm.cshtml";

        public ProviderController(ILogger<ProviderController> logger, IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> List(ProviderSearchViewModel SearchModel)
        {
            try
            {
                //throw new Exception();
                var list = await _providerService.GetAsync(SearchModel);
                return PartialView(_viewOrderItemTable, list);
            }
            catch (Exception ex)
            {
                return PartialView(_viewError, new ErrorViewModel("Ошибка! Не удалось получить список поставщиков!"));
            }
        }
    }
}
