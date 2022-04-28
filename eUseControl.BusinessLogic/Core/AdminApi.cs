using AutoMapper;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class AdminApi : BaseApi
    {
        internal List<OrderMinimal> GetNewOrdersAction()
        {
            List<DbOrder> data;

            using (var db = new OrderContext())
            {
                data = db.Orders.Where(m => m.Status == OrderStatus.InProgres).ToList();
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DbOrder, OrderMinimal>());
            var mapper = config.CreateMapper();

            List<OrderMinimal> orders = mapper.Map<List<OrderMinimal>>(data);

            return orders;
        }
    }
}
