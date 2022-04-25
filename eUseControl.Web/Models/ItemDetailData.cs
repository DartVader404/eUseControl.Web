using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class ItemDetailData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public URole Level { get; set; }
        public int CartProducts { get; set; }
        public DbProduct Product { get; set; }
        public List<ImgPath> Paths { get; set; }
        public NewCartProduct Cart { get; set; }

    }
}