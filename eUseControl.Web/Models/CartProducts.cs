using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class CartProducts
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string PreImgPath { get; set; }

    }
}