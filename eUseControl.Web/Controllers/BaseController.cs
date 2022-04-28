using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
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
        private readonly IOrder _order;
        private readonly IAdmin _admin;
        public BaseController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _product = bl.GetProductBL();
            _order = bl.GetOrderBL();
            _admin = bl.GetAdminBL();
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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DbCart, CartProducts>());
            var mapper = config.CreateMapper();

            DbProduct product;

            List<CartProducts> minCart = mapper.Map<List<CartProducts>>(cart);

            foreach (var c in minCart)                  //some logic to get max product quantity if user change number of products in cart
            {
                product = _product.GetProductById(c.ProductId);
                c.maxQuantity = product.Quantity;
            }

            return (minCart);
        }

        public ShipingAddress GetUserAddress(int userId)
        {
            DbAddress address = _order.GetAddress(userId);
            ShipingAddress minAddress = null;

            if (address != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DbAddress, ShipingAddress>());
                var mapper = config.CreateMapper();

                minAddress = mapper.Map<ShipingAddress>(address);
            }

            return minAddress;
        }

        public List<NewOrder> OrderFromCart(int userId)
        {
            List<DbCart> cart = _product.GetProductsInCart(userId);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DbCart, NewOrder>());
            var mapper = config.CreateMapper();

            List<NewOrder> minOrder = mapper.Map<List<NewOrder>>(cart);

            return minOrder;
        }

        public List<UserOrderData> GetUserOrders(int userId)
        {
            List<OrderMinimal> orders = _order.GetUserOrders(userId);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderMinimal, UserOrderData>());
            var mapper = config.CreateMapper();

            List<UserOrderData> viewOrder = mapper.Map<List<UserOrderData>>(orders);

            return viewOrder;
        }

        public List<AdminOrders> GetNewOrders()
        {
            List<OrderMinimal>  data = _admin.GetNewOrders();
            List<AdminOrders> orders = new List<AdminOrders>();

            var orderConfig = new MapperConfiguration(cfg => cfg.CreateMap<OrderMinimal, AdminOrders>());
            var orderMapper = orderConfig.CreateMapper();

            var addressConfig = new MapperConfiguration(cfg => cfg.CreateMap<DbAddress, ShipingAddress>());
            var addressMapper = addressConfig.CreateMapper();

            foreach (var item in data)
            {
                AdminOrders order = orderMapper.Map<AdminOrders>(item);

                order.Address = addressMapper.Map<ShipingAddress>(_order.GetAddress(item.UserId));

                orders.Add(order);
            }

            return orders;
        }
    }
}