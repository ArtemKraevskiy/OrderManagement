using OrderManagement.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Entities.OrderManagement
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
