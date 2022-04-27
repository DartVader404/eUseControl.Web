using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class UserData
    {
        public string UserName { get; set; }
        public URole Level { get; set; }
        public int CartProducts { get; set; }
        public string MsgStatus { get; set; }
    }
}