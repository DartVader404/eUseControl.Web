using eUseControl.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() :
            base("name=eUseControl")
        {

        }
        public virtual DbSet<DbOrder> Orders { get; set; }
    }
}
