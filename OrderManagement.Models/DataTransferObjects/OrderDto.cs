using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Models.ViewModels;

namespace OrderManagement.Models.DataTransferObjects
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public ProviderDto Provider { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
