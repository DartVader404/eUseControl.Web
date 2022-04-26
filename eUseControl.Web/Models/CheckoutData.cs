using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class CheckoutData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public URole Level { get; set; }
        public int CartProducts { get; set; }
        public List<CartProducts> Products { get; set; }
        public ShipingAddress Address { get; set; }
        public List<NewOrder> OrderList { get; set; }
        public string OrderNote { get; set; }
    }
}