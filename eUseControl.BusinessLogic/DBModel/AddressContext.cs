using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class AddressContext : DbContext
    {
        public AddressContext() :
            base("name=eUseControl")
        {

        }
        public virtual DbSet<UAddress> Addresses { get; set; }
    }
}

