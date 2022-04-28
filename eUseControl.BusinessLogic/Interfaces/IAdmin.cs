using eUseControl.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        List<OrderMinimal> GetNewOrders();
    }
}
