using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Models.ViewModels
{
    public class OrderItemViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ValidateNever]
        public string Name { get; set; }
        [ValidateNever]
        public decimal? Quantity { get; set; }
        [ValidateNever]
        public string Unit { get; set; }
        [ValidateNever]
        public int SerialNumber { get; set; }
        public bool IsDeleted { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsDeleted)
            { 
                yield return ValidationResult.Success;
            }
            else
            {
                if(string.IsNullOrEmpty(Name))
                    yield return new ValidationResult("Обязательное поле", new[] { nameof(Name) });
                if (string.IsNullOrEmpty(Unit))
                    yield return new ValidationResult("Обязательное поле", new[] { nameof(Unit) });
                if (string.IsNullOrEmpty(Quantity.ToString()))
                    yield return new ValidationResult("Обязательное поле", new[] { nameof(Quantity) });
            }
        }
    }
}
