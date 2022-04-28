using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Web.CustomAttributes;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public readonly IProduct _product;
        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBL();
        }

        [ModeratorAdminMod]
        public ActionResult AddProduct(StocksData viewData)
        {
            if (ModelState.IsValid)
            {
                NewProductData inputData = viewData.newProductData;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<NewProductData, AddProductData>());
                var mapper = config.CreateMapper();

                AddProductData data = mapper.Map<AddProductData>(inputData);
            
                AddProductResp addProduct = _product.AddProduct(data);
                
                if (addProduct.Status)
                {
                    return RedirectToAction("Stocks", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", addProduct.StatusMsg);
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [ModeratorAdminMod]
        [HttpGet]
        public ActionResult ProductEdit(string productId)
        {
            if (productId != null)
            {
                DbProduct product = GetProductById(Int32.Parse(productId));
                List<ImgPath> paths = GetImgPaths(Int32.Parse(productId));

                var config = new MapperConfiguration(cfg => cfg.CreateMap<DbProduct, EditProductData>());
                var mapper = config.CreateMapper();

                EditProductData productData = mapper.Map<EditProductData>(product);
                productData.Paths = paths;

                var user = System.Web.HttpContext.Current.GetMySessionObject();

                productData.UserName = user.Username;
                productData.Level = user.Level;

                return View(productData);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(EditProductData productData)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EditProductData, UpdateProductData>());
                var mapper = config.CreateMapper();

                UpdateProductData data = mapper.Map<UpdateProductData>(productData);

                var productUpdate = _product.UpdateProduct(data);

                if (productUpdate.Status)
                {
                    return RedirectToAction("Stocks", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", productUpdate.StatusMsg);
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [AdminMod]
        public ActionResult ProductDelete(string productId)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(productId);
                var deleteProduct = _product.DeleteProduct(id);

                if (deleteProduct.Status)
                {
                    return RedirectToAction("Stocks", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", deleteProduct.StatusMsg);
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(ItemDetailData viewData)
        {
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<NewCartProduct, AddToCartData>());
                var mapper = config.CreateMapper();

                AddToCartData data = mapper.Map<AddToCartData>(viewData.Cart);

                var AddToCartResp = _product.AddProductToCart(data);

                if (AddToCartResp.Status)
                {
                    return RedirectToAction("AddedToCart", "Product");
                }
                else
                {
                    ModelState.AddModelError("", AddToCartResp.StatusMsg);
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        public ActionResult AddedToCart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View();
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData u = new UserData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts
            };

            return View(u);
        }

        public ActionResult RemoveFromCart(string cartId)
        {
            RemoveCartResp remove= _product.RemoveCartElement(Int32.Parse(cartId));

            if (remove.Status)
            {
                return RedirectToAction("Cart", "Home");
            }
            else
            {
                ModelState.AddModelError("", remove.StatusMsg);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult RemoveAllFromCart(string userId)
        {
            RemoveCartResp remove = _product.RemoveAllCartElement(Int32.Parse(userId));

            if (remove.Status)
            {
                return RedirectToAction("Cart", "Home");
            }
            else
            {
                ModelState.AddModelError("", remove.StatusMsg);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult UpdateCartQty(CartPageData viewData)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CartProducts, UpdateCartQtyData>());
            var mapper = config.CreateMapper();

            List<UpdateCartQtyData> data = mapper.Map<List<UpdateCartQtyData>>(viewData.Products);

            UpdateCartQtyResp update = _product.UpdateCartQty(data);

            if (update.Status)
            {
                return RedirectToAction("Cart", "Home");
            }
            else
            {
                ModelState.AddModelError("", update.StatusMsg);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EmptyCart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View();
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData u = new UserData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts
            };

            return View(u);
        }
    }
}