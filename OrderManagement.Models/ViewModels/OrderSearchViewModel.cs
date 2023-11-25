using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Models.ViewModels
{
    public class OrderSearchViewModel
    {
        public int? ProviderId { get; set; }
        public List<SelectListItem> Providers { get; set; }
        public string? Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SelectListItem> Units { get; set; }
        public string? Unit { get; set; }
    }
}
