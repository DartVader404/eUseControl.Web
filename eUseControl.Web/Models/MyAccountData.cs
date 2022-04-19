using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class MyAccountData
    {
        public string UserName { get; set; }
        public URole Level { get; set; }
        public List<Order> Orders { get; set; }
        public UAddress Address { get; set; }
        public UChangePass Password { get; set; }
    }
}