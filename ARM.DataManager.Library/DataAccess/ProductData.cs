using ARM.DataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess sql;

        public ProductData(ISqlDataAccess sql)
        {
            this.sql = sql;
        }
        public List<ProductModel> GetProducts()
        {
            var ouput = sql.LoadData<ProductModel, dynamic>("GetAllProducts", new { }, Constants.ARMDATA);

            return ouput;
        }

        public ProductModel GetProductById(int productId)
        {
            var ouput = sql.LoadData<ProductModel, dynamic>("GetProductById", new { Id = productId }, Constants.ARMDATA).FirstOrDefault();

            return ouput;
        }
    }
}
