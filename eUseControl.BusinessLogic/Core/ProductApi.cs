using AutoMapper;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace eUseControl.BusinessLogic.Core
{
    public class ProductApi : BaseApi
    {
        internal AddProductResp AddProductAction(AddProductData data)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AddProductData, DbProduct>());
                var mapper = config.CreateMapper();

                DbProduct newProduct = mapper.Map<DbProduct>(data);

                var InputFileName = Path.GetFileName(DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + data.PreviewImg.FileName);     //make file name
                InputFileName = InputFileName.Replace(" ", String.Empty);                                             //remove all spaces from path string 
                var ServerSavePath = Path.Combine(HostingEnvironment.MapPath("~/Images/products/preview/") + InputFileName);     //obtain path for save file

                newProduct.PreImgPath = ("/Images/products/preview/" + InputFileName);                         //save preview image path to store this later in database
                data.PreviewImg.SaveAs(ServerSavePath);                                         //save preview image on server
                newProduct.AddedDate = DateTime.Now;

                using (var db = new ProductContext())
                {
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }

                using (var db = new ImgPathContext())
                {
                    foreach (HttpPostedFileBase file in data.ProductImg)
                    {
                        ImgPath path = new ImgPath();

                        InputFileName = Path.GetFileName(DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + file.FileName);
                        InputFileName = InputFileName.Replace(" ", String.Empty);
                        ServerSavePath = Path.Combine(HostingEnvironment.MapPath("~/Images/products/detail/") + InputFileName);

                        path.Path = ("/Images/products/detail/" + InputFileName);
                        path.ProductId = newProduct.ProductId;

                        file.SaveAs(ServerSavePath);
                        db.ImgPaths.Add(path);
                    }
                    db.SaveChanges();
                }

                return new AddProductResp() { Status = true };
            }
            catch (Exception ex)
            {
                return new AddProductResp() { Status = false, StatusMsg = ex.ToString() };
            }
        }

        internal List<DbProduct> GetProductsAction()
        {
            List<DbProduct> Products;
            try
            {
                using (var db = new ProductContext())
                {
                    Products = db.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }


            return Products;
        }

        internal List<ImgPath> GetImgPathsAction(int productId)
        {
            //TO DO: obtain list of img path from database where row contains ProductId
            List<ImgPath> paths;
            using (var db = new ImgPathContext())
            {
                paths = db.ImgPaths.Where(x => x.ProductId == productId).ToList();
            }
            return paths;
        }

        internal DbProduct GetProductByIdAction(int productId)
        {
            return ProductById(productId);
        }

        internal UpdateProductResp UpdateProductAction(UpdateProductData data)
        {
            if (data.PreviewImg != null)
            {
                var InputFileName = Path.GetFileName(DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + data.PreviewImg.FileName);     //make file name
                InputFileName = InputFileName.Replace(" ", String.Empty);                                             //remove all spaces from path string 
                var ServerSavePath = Path.Combine(HostingEnvironment.MapPath("~/Images/products/preview/") + InputFileName);     //obtain path for save file

                data.PreImgPath = ("/Images/products/preview/" + InputFileName);                         //save preview image path to store this later in database
                data.PreviewImg.SaveAs(ServerSavePath);                                         //save preview image on server
            } 

            DbProduct product;

            using (var db = new ProductContext())
            {
                product = db.Products.FirstOrDefault(m => m.ProductId == data.ProductId);

                product.ProductName = data.ProductName;
                product.Category = data.Category;
                product.Brand = data.Brand;
                product.Price = data.Price;
                product.Quantity = data.Quantity;
                product.Description = data.Description;
                if (data.PreImgPath != null)
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("~/") + product.PreImgPath));
                    product.PreImgPath = data.PreImgPath;
                }

                db.SaveChanges();
            }

            List<ImgPath> Paths = GetImgPathsAction(data.ProductId);
            int i = 0;
            foreach (var path in Paths)
            {
                if (data.status[i] == true)
                {
                    using (var db = new ImgPathContext())
                    {
                        System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("~/") + path.Path));
                        db.ImgPaths.Remove(db.ImgPaths.FirstOrDefault(m => m.Path == path.Path));
                        db.SaveChanges();
                    }
                }
                i++;
            }

            if (data.ProductImg != null)
            {
                using (var db = new ImgPathContext())
                {
                    foreach (HttpPostedFileBase file in data.ProductImg)
                    {
                        if (file == null) continue;
                        ImgPath path = new ImgPath();

                        var InputFileName = Path.GetFileName(DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + file.FileName);
                        InputFileName = InputFileName.Replace(" ", String.Empty);
                        var ServerSavePath = Path.Combine(HostingEnvironment.MapPath("~/Images/products/detail/") + InputFileName);

                        path.Path = ("/Images/products/detail/" + InputFileName);
                        path.ProductId = data.ProductId;

                        file.SaveAs(ServerSavePath);
                        db.ImgPaths.Add(path);
                    }
                    db.SaveChanges();
                }
            }
            
            return new UpdateProductResp() { Status = true };
        }

        internal DeleteProductResp DeleteProductAction(int productId)
        {
            DbProduct product;

            using (var db = new ProductContext())    //delete product data from Product table in database
            {
                product = db.Products.FirstOrDefault(m => m.ProductId == productId);
                System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("~/") + product.PreImgPath));
                db.Products.Remove(product);
                db.SaveChanges();
            }

            using (var db = new ImgPathContext())
            {
                List<ImgPath> Paths = db.ImgPaths.Where(m => m.ProductId == productId).ToList();

                foreach (var path in Paths)
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("~/") + path.Path));
                    db.ImgPaths.Remove(path);
                    db.SaveChanges();
                }
            }

            return new DeleteProductResp() { Status = true};
        }

        internal AddToCartResp AddProductToCartAction(AddToCartData data)
        {
            DbProduct productData;

            using (var db = new ProductContext())
            {
                productData = db.Products.FirstOrDefault(m => m.ProductId == data.ProductId);
                if (productData == null) return new AddToCartResp { Status = false, StatusMsg = "Product not found!" };
            }

            DbCart cart = new DbCart
            {
                UserId = data.UserId,
                ProductId = productData.ProductId,
                ProductName = productData.ProductName,
                Price = productData.Price,
                Quantity = data.Quantity,
                PreImgPath = productData.PreImgPath,
                AddedDate = DateTime.Now
            };

            using (var db = new CartContext())
            {
                db.Cart.Add(cart);
                db.SaveChanges();
            }

            using (var db = new UserContext())
            {
                UDbTable userData = db.Users.FirstOrDefault(m => m.Id == data.UserId);
                if (userData == null) return new AddToCartResp { Status = false, StatusMsg="User not found!" };

                userData.CartProducts += 1;
                db.SaveChanges();
            }

            return new AddToCartResp { Status = true };
        }

        internal List<DbCart> GetProductsInCartAction(int userId)
        {
            List <DbCart> cart;

            using (var db = new CartContext())
            {
                cart = db.Cart.Where(m => m.UserId == userId).ToList();
            }

            return cart;
        }

        internal RemoveCartResp RemoveCartElementAction(int cartId)
        {
            return RemoveCartElementById(cartId);
        }

        internal RemoveCartResp RemoveAllCartElementAction(int userId)
        {
            using (var db = new CartContext())
            {
                List<DbCart> carts = db.Cart.Where(m => m.UserId == userId).ToList();
                if (!carts.Any())
                {
                    return new RemoveCartResp() { Status = false, StatusMsg = "Element not found!" };
                }

                foreach(var cart in carts)
                {
                    db.Cart.Remove(cart);
                }

                db.SaveChanges();
            }

            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(m => m.Id == userId);
                if (user == null)
                {
                    return new RemoveCartResp() { Status = false, StatusMsg = "User not found!" };
                }

                user.CartProducts = 0;

                db.SaveChanges();
            }

            return new RemoveCartResp() { Status = true };
        }

        internal UpdateCartQtyResp UpdateCartQtyAction(List<UpdateCartQtyData> data)
        {
            //update qty code
            DbCart cart;
            foreach (var item in data)
            {
                using (var db = new CartContext())
                {
                    cart = db.Cart.FirstOrDefault(m => m.CartId == item.CartId);
                    if (cart == null)
                    {
                        return new UpdateCartQtyResp() { Status = false, StatusMsg = "Element not found!" };
                    }

                    if (item.Quantity == 0)
                    {
                        RemoveCartElementAction(item.CartId);
                    }
                    else
                    {
                        cart.Quantity = item.Quantity;
                        db.SaveChanges();
                    }
                }
            }

            return new UpdateCartQtyResp() { Status = true };
        }


    }
}
