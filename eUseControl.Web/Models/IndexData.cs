using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class IndexData
    {
        public string UserName { get; set; }
        public URole Level { get; set; }
        public int CartProducts { get; set; }    //number of products in card to display in header
        public List<DbProduct> Products { get; set; }
        public int? SortBy { get; set; }
    }
}