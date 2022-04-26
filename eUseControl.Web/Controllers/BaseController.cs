using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eUseControl.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISession _session;
        private readonly IProduct _product;
        public BaseController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _product = bl.GetProductBL();
        }

        public void SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null)
                {
                    System.Web.HttpContext.Current.SetMySessionObject(profile);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }

        public List<DbProduct> GetProduct()
        {
            List<DbProduct> Products = _product.GetProducts();
            return Products;
        }

        public List<ImgPath> GetImgPaths(int ProductId)
        {
            List<ImgPath> paths = _product.GetImgPaths(ProductId);
            return paths;
        }

        public DbProduct GetProductById(int productId)
        {
            DbProduct product = _product.GetProductById(productId);
            return product;
        }

        public List<CartProducts> ProductsInCart(int userId)
        {
            List<DbCart> cart = _product.GetProductsInCart(userId);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<List<DbCart>, List<CartProducts>>());
            var mapper = config.CreateMapper();

            List<CartProducts> minCart = mapper.Map<List<CartProducts>>(cart);

            return (minCart);
        }
    }
}