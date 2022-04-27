using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        DbAddress GetAddress(int userId);
        int AddUserAddress(NewAddress address);
        PlaceOrderResp PlaceOrder(List<PlaceOrderData> data);
        UpdateAddressResp UpdateUserAddress(UpdateAddressData data);
        List<OrderMinimal> GetUserOrders(int userId);
    }
}
