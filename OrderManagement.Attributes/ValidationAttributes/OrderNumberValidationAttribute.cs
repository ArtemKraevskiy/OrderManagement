using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderNumberValidationAttribute : ValidationAttribute
    {
        public OrderNumberValidationAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            // Пример: Проверяем, что значение является числом и больше 0
            if (int.TryParse(value.ToString(), out int number))
            {
                return number > 0;
            }

            return false;
        }
    }
}
