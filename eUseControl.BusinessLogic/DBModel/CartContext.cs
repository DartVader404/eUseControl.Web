using eUseControl.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class CartContext : DbContext
    {
        public CartContext() :
            base("name=eUseControl")
        {

        }
        public virtual DbSet<DbCart> Cart { get; set; }
    }
}
