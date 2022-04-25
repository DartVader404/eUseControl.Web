using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
    public class ProductBL : ProductApi, IProduct 
    {
        public AddProductResp AddProduct(AddProductData data)
        {
            return AddProductAction(data);
        }

        public List<DbProduct> GetProducts()
        {
            return GetProductsAction();
        }

        public List<ImgPath> GetImgPaths(int productId)
        {
            return GetImgPathsAction(productId);
        }

        public DbProduct GetProductById(int productId)
        {
            return GetProductByIdAction(productId);
        }

        public UpdateProductResp UpdateProduct(UpdateProductData data)
        {
            return UpdateProductAction(data);
        }

        public DeleteProductResp DeleteProduct(int productId)
        {
            return DeleteProductAction(productId);
        }

        public AddToCartResp AddProductToCart(AddToCartData data)
        {
            return AddProductToCartAction(data);
        }
    }
}
