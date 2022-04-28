using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Web.CustomAttributes;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdmin _admin;
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBL();
        }

        // GET: Admin
        [ModeratorAdminMod]
        public ActionResult Stocks()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();     //obtain user data from session

            StocksData data = new StocksData()    //merge product and user data to send in index 
            {
                UserName = user.Username,
                Level = user.Level,
                Products = GetProduct()
            };

            return View(data);
        }

        [AdminMod]
        public ActionResult OrdersHistory()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();     //obtain user data from session

            IndexData data = new IndexData()    //merge product and user data to send in index 
            {
                UserName = user.Username,
                Level = user.Level,
                Products = GetProduct()
            };
            ViewBag.Data = data;
            return View();
        }

        [ModeratorAdminMod]
        public ActionResult NewOrders()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();     //obtain user data from session

            ANewOrdersData data = new ANewOrdersData()    //merge product and user data to send in index 
            {
                UserName = user.Username,
                Level = user.Level,
                Orders = GetNewOrders()
            };

            return View(data);
        }

        [AdminMod]
        public ActionResult CustomersList()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();     //obtain user data from session

            IndexData data = new IndexData()    //merge product and user data to send in index 
            {
                UserName = user.Username,
                Level = user.Level,
                Products = GetProduct()
            };
            ViewBag.Data = data;
            return View();
        }
    }
}