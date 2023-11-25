using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManagement.Models.ViewModels
{
    public class OrderViewModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Providers { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            for (int i = 0; i < OrderItems.Count; i++)
                if (OrderItems[i].Name == Number)
                {
                    yield return new ValidationResult("Должно отличаться от номера заказа", new[] { $"OrderItems[{i}].Name" });
                }
            yield return ValidationResult.Success;
        }
    }
}
