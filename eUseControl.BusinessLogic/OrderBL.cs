using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
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
        public UAddress GetAddress(int userId)
        {
            return GetAddressAction(userId);
        }
    }
}
