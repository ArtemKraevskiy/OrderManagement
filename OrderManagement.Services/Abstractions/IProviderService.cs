using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstractions
{
    public interface IProviderService
    {
        Task<List<ProviderDto>> GetAsync(ProviderSearchViewModel searchModel);
        Task<ProviderDto> GetByIdAsync(int Id);
        Task<List<SelectListItem>> GetSelectListAsync();
    }
}
