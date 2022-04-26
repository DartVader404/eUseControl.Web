using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class OrderApi : BaseApi
    {
        internal UAddress GetAddressAction(int userId)
        {
            UAddress address;

            using (var db = new AddressContext())
            {
                address = db.Addresses.FirstOrDefault(m => m.UserId == userId);
            }

            return address;
        }
    }
}
