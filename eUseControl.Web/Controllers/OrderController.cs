using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Enums;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class OrderController : Controller
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
            if (viewData.Address == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ShipingAddress, NewAddress>());
                var mapper = config.CreateMapper();

                NewAddress newAddress = mapper.Map<NewAddress>(viewData.Address);
                newAddress.AddedDate = DateTime.Now;

                addressId = _order.AddUserAddress(newAddress);
            }
            else
            {
                addressId = _order.GetUserAddressId(viewData.UserId);
            }

            List<PlaceOrderData> newOrderData = new List<PlaceOrderData>();
            foreach (var order in viewData.OrderList)
            {
                PlaceOrderData newOrder = new PlaceOrderData()
                {
                    AddressId = addressId,
                    ProductId = order.ProductId,
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
                return RedirectToAction("Stocks", "Admin");
            }
            else
            {
                ModelState.AddModelError("", placeOrder.StatusMsg);
                return RedirectToAction("Error", "Home");
            }

            return View();
        }
    }
}