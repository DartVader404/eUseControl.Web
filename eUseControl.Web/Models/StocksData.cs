using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class StocksData
    {
        public string UserName { get; set; }
        public URole Level { get; set; }
        public List<DbProduct> Products { get; set; }
        public NewProductData newProductData { get; set; }  
    }
}