using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public ErrorViewModel(string message)
        {
            Message = message;
        }
    }
}
