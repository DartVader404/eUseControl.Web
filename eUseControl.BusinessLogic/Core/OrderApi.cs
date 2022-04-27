using AutoMapper;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class OrderApi : BaseApi
    {
        internal DbAddress GetAddressAction(int userId)
        {
            DbAddress address;

            using (var db = new AddressContext())
            {
                address = db.Addresses.FirstOrDefault(m => m.UserId == userId);
            }

            return address;
        }

        internal int AddUserAddressAction(NewAddress address)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewAddress, DbAddress>());
            var mapper = config.CreateMapper();
            DbAddress newAddress = mapper.Map<DbAddress>(address);

            newAddress.AddedDate = DateTime.Now;
            newAddress.LastEdit = DateTime.Now;

            using (var db = new AddressContext())
            {
                db.Addresses.Add(newAddress);
                db.SaveChanges();
            }

            return newAddress.AddressId;
        }

        internal PlaceOrderResp PlaceOrderAction(List<PlaceOrderData> data)
        {
            DbOrder newOrder;
            PlaceOrderResp resp = new PlaceOrderResp() { Status = true , StatusMsg = "Not enought quantity of "};
            bool addOrder = true;

            foreach (PlaceOrderData dataItem in data)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PlaceOrderData, DbOrder>());
                var mapper = config.CreateMapper();
                newOrder = mapper.Map<DbOrder>(dataItem);

                DbProduct product;

                using (var db = new ProductContext())
                {
                    product = db.Products.FirstOrDefault(x => x.ProductId == dataItem.ProductId);

                    newOrder.ProductName = product.ProductName;
                    newOrder.Price = product.Price;

                    if (newOrder.Quantity <= product.Quantity)
                    {
                        product.Quantity -= newOrder.Quantity;
                    }
                    else
                    {
                        resp.Status = false;
                        resp.StatusMsg += "[" + product.ProductName + " only " + product.Quantity + "] ";

                        addOrder = false;
                    }

                    db.SaveChanges();
                }

                if (addOrder)
                {
                    using (var db = new OrderContext())
                    {
                        db.Orders.Add(newOrder);
                        db.SaveChanges();
                    }
                    RemoveCartElementById(dataItem.CartId);
                }
                else
                {
                    addOrder = true;
                }
            }

            return resp;
        }

        internal UpdateAddressResp UpdateUserAddressAction(UpdateAddressData data)
        {
            using (var db = new AddressContext())
            {
                DbAddress address = db.Addresses.FirstOrDefault(m => m.AddressId == data.AddressId);
                if (address == null)
                {
                    return new UpdateAddressResp() { Status = false, StatusMsg = "AddressID not found!"};
                }

                address.FirstName = data.FirstName;
                address.LastName = data.LastName;
                address.Email = data.Email;
                address.City = data.City;
                address.State = data.State;
                address.StreetAddress = data.StreetAddress;
                address.PostCode = data.PostCode;
                address.Country = data.Country;
                address.Phone = data.Phone;
                address.LastEdit = DateTime.Now;

                db.SaveChanges();
            }

            return new UpdateAddressResp() { Status = true };
        }

        internal List<OrderMinimal> GetUserOrdersAction(int userId)
        {
            List<DbOrder> dbData;
            
            using (var db = new OrderContext())
            {
                dbData = db.Orders.Where(m => m.UserId == userId).ToList();
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DbOrder, OrderMinimal>());
            var mapper = config.CreateMapper();

            List<OrderMinimal> minOrder = mapper.Map<List<OrderMinimal>>(dbData);

            return minOrder;
        }
    }
}
