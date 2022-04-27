using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Order
{
    public class PlaceOrderData
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public OrderStatus Status { get; set; }
        public string OrderNote { get; set; }
    }
}
