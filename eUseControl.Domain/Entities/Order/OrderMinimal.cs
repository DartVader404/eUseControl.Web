using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Order
{
    public class OrderMinimal
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime AddedDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
