using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
    internal class OrderBL : OrderApi, IOrder
    {
        public DbAddress GetAddress(int userId)
        {
            return GetAddressAction(userId);
        }

        public int AddUserAddress(NewAddress address)
        {
            return AddUserAddressAction(address);
        }

        public PlaceOrderResp PlaceOrder(List<PlaceOrderData> data)
        {
            return PlaceOrderAction(data);
        }

        public UpdateAddressResp UpdateUserAddress(UpdateAddressData data)
        {
            return UpdateUserAddressAction(data);
        }

        public List<OrderMinimal> GetUserOrders(int userId)
        {
            return GetUserOrdersAction(userId);
        }

    }
}
