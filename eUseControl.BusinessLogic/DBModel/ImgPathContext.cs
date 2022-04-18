using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class ImgPathContext : DbContext
    {
        public ImgPathContext() : base("name=eUseControl")
        {
        }

        public virtual DbSet<ImgPath> ImgPaths { get; set; }
    }
}
