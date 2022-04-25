using AutoMapper;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Web.CustomAttributes;
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
        public ActionResult AddProduct(NewProductData inputData)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult AddToCart(ItemDetailData data)
        {
            return RedirectToAction("AddedToCart", "Home");
        }
    }
}