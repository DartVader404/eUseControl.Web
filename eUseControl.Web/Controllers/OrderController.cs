using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Enums;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrder _order;
        public OrderController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _order = bl.GetOrderBL();
        }

        // GET: Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(CheckoutData viewData)
        {
            int addressId;
            if (viewData.Address.AddressId == 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ShipingAddress, NewAddress>());
                var mapper = config.CreateMapper();

                NewAddress newAddress = mapper.Map<NewAddress>(viewData.Address);
                newAddress.UserId = viewData.UserId;

                addressId = _order.AddUserAddress(newAddress);
            }
            else
            {
                addressId = viewData.Address.AddressId;
            }

            List<PlaceOrderData> newOrderData = new List<PlaceOrderData>();
            foreach (var order in viewData.OrderList)
            {
                PlaceOrderData newOrder = new PlaceOrderData()
                {
                    AddressId = addressId,
                    UserId = viewData.UserId,
                    ProductId = order.ProductId,
                    CartId = order.CartId,
                    Quantity = order.Quantity,
                    AddedDate = DateTime.Now,
                    Status = OrderStatus.InProgres,
                    OrderNote = viewData.OrderNote
                };

                newOrderData.Add(newOrder);
            }

            PlaceOrderResp placeOrder = _order.PlaceOrder(newOrderData);
            if (placeOrder.Status)
            {
                return RedirectToAction("ThankYouPage", "Order");
            }
            else
            {
                ModelState.AddModelError("", placeOrder.StatusMsg);
                return RedirectToAction("PlaceError", "Order", new { StatusMsg = placeOrder.StatusMsg});
            }
        }

        public ActionResult PlaceError(string StatusMsg)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Home");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            UserData u = new UserData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts,
                MsgStatus = StatusMsg
            };
            return View(u);
        }

        public ActionResult ThankYouPage()
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
                CartProducts = user.CartProducts,
            };
            return View(u);
        }

        public ActionResult EditAddress()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Error", "Home");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            MyAccountData u = new MyAccountData
            {
                UserName = user.Username,
                Level = user.Level,
                CartProducts = user.CartProducts,
                Address = GetUserAddress(user.Id)
            };
            
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddress(MyAccountData viewData)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ShipingAddress, UpdateAddressData>());
            var mapper = config.CreateMapper();

            UpdateAddressData address = mapper.Map<UpdateAddressData>(viewData.Address);

            UpdateAddressResp update = _order.UpdateUserAddress(address);


            if (update.Status)
            {
                return RedirectToAction("MyAccount", "Home");
            }
            else
            {
                ModelState.AddModelError("", update.StatusMsg);
                return RedirectToAction("Error", "Home");
            }

        }
    }
}