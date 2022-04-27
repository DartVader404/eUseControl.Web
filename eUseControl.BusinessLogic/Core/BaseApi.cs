using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class BaseApi
    {

        internal DbProduct ProductById(int productId)
        {
            DbProduct product;

            using (var db = new ProductContext())
            {
                product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            }

            return product;
        }

        internal RemoveCartResp RemoveCartElementById(int cartId)
        {
            int userId;
            using (var db = new CartContext())
            {
                DbCart cart = db.Cart.FirstOrDefault(m => m.CartId == cartId);
                if (cart == null)
                {
                    return new RemoveCartResp() { Status = false, StatusMsg = "Element not found!" };
                }

                userId = cart.UserId;

                db.Cart.Remove(cart);
                db.SaveChanges();
            }

            using (var db = new UserContext())
            {
                UDbTable user = db.Users.FirstOrDefault(m => m.Id == userId);
                if (user == null)
                {
                    return new RemoveCartResp() { Status = false, StatusMsg = "User not found!" };
                }

                user.CartProducts -= 1;             //update number of products in the cart for display in the header of the page

                db.SaveChanges();
            }

            return new RemoveCartResp() { Status = true };
        }
    }
}
