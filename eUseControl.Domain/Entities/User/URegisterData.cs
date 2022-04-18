using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
    public  class URegisterData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LasIp { get; set; }
        public DateTime LastLogin { get; set; }
        public URole Level { get; set; }
    }
}
