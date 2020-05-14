using ARM.DataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var ouput = sql.LoadData<ProductModel, dynamic>("GetAllProducts", new { }, Constants.ARMDATA);

            return ouput;
        }

        public ProductModel GetProductById( int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var ouput = sql.LoadData<ProductModel, dynamic>("GetProductById", new { Id = productId }, Constants.ARMDATA).FirstOrDefault();

            return ouput;
        }
    }
}
