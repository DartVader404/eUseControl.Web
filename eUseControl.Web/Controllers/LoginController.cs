using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Models;

namespace eUseControl.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                ULoginResp userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Recover()
        {
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recover(string mail )
        {
            if (ModelState.IsValid)
            {
                URecoverResp userRecover = _session.RecoverPassword(mail); 

                if (userRecover.Status)
                {
                    return RedirectToAction("EmailSend", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userRecover.StatusMsg);
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(MyAccountData data)
        {
            return View();
        }
    }
}