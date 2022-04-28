using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
    internal class AdminBL : AdminApi, IAdmin
    {
        public List<OrderMinimal> GetNewOrders()
        {
            return GetNewOrdersAction();
        }
    }
}
