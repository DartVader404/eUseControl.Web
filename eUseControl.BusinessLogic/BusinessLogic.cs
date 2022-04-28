using eUseControl.BusinessLogic.Interfaces;

namespace eUseControl.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IProduct GetProductBL()
        {
            return new ProductBL();
        }

        public IOrder GetOrderBL()
        {
            return new OrderBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }
    }
}
