using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class UserOrderData
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime AddedDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}