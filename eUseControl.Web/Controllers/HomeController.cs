using System;
using System.Web.Mvc;
using eUseControl.Web.Models;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinessLogic.DBModel;
using System.Linq;
using eUseControl.Web.Extension;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using System.Collections.Generic;

namespace eUseControl.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        private readonly ISession _session;
        private readonly IProduct _product;
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _product = bl.GetProductBL();
        }


        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View(new IndexData()
                {
                    Products = GetProduct()
                }
                );
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();     //obtain user data from session

            IndexData data = new IndexData()    //merge product and user data to send in index 
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts,
                Products = GetProduct()
            };
            return View(data);
        }

       

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Cart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            CartPageData u = new CartPageData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts,
                Products = _product.GetProductsInCart(user.Id)
            };

            if (u.Products == null) RedirectToAction("EmptyCart", "Home");

            return View(u);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult EditAddress()
        {
            return View();
        }

        public ActionResult EmptyCart()
        {
            return View();
        }

        public ActionResult ItemDetails(string productId)
        {
            if (productId != null)
            {
                SessionStatus();
                DbProduct product = GetProductById(Int32.Parse(productId));
                List<ImgPath> paths = GetImgPaths(product.ProductId);

                if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
                {
                    return View(new ItemDetailData()
                    {
                        Product = product,
                        Paths = paths
                    });
                }

                var user = System.Web.HttpContext.Current.GetMySessionObject();

                ItemDetailData data = new ItemDetailData()
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    Level = user.Level,
                    CartProducts = user.CartProducts,
                    Product = product,
                    Paths = paths,
                    Cart = new NewCartProduct
                    {
                        UserId = user.Id,
                        ProductId = product.ProductId
                    },
                };
                return View(data);
            }
            return RedirectToAction("Error", "Home");
        }

        public ActionResult MyAccount()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var pass = new UChangePassData() { UserId = user.Id };
            MyAccountData u = new MyAccountData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts,
                Password = pass
            };
            return View(u);
        }

        public ActionResult ThankYouPage()
        {
            return View();
        }
        public ActionResult EmailSend()
        {
            return View();
        }
        public ActionResult AddedToCart()
        {
            return View();
        }

        public ActionResult PassChanged()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}