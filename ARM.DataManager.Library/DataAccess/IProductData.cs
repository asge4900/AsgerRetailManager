using ARM.DataManager.Library.Models;
using System.Collections.Generic;

namespace ARM.DataManager.Library.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int productId);
        List<ProductModel> GetProducts();
    }
}