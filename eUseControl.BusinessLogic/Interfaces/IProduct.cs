using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        AddProductResp AddProduct(AddProductData data);
        List<DbProduct> GetProducts();
        List<ImgPath> GetImgPaths(int productId);
        DbProduct GetProductById(int productId);
        UpdateProductResp UpdateProduct(UpdateProductData data);
        DeleteProductResp DeleteProduct(int productId);
        AddToCartResp AddProductToCart(AddToCartData data);
        List<DbCart> GetProductsInCart(int userId);
        RemoveCartResp RemoveCartElement(int cartId);
        RemoveCartResp RemoveAllCartElement(int userId);
        UpdateCartQtyResp UpdateCartQty(List<UpdateCartQtyData> data);
    }
}
