using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Web.Models;
using eUseControl.BusinessLogic.Core; 
using System.Data.Entity.Validation;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using AutoMapper;

namespace eUseControl.Web.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly ISession _session;

        public RegisterController()
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
        public ActionResult Index(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                if (register.Password != register.RepeatPassword)
                {
                    ModelState.AddModelError("", "Passwords not match!");
                    return View();
                }

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserRegister, URegisterData>());
                var mapper = config.CreateMapper();

                URegisterData data = mapper.Map<URegisterData>(register);
                data.LasIp = Request.UserHostAddress;
                data.LastLogin = DateTime.Now;
                data.Level = URole.User;
               
                var userRegister = _session.UserRegister(data);
                if (userRegister.Status)
                {
                    UserLogin login = new UserLogin()
                    {
                        Credential = register.UserName,
                        Password = register.Password
                    };
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", userRegister.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}