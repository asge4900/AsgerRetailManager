using System.Collections.Generic;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize (Roles = ("Cashier")]
    public class ProductController : ControllerBase
    {
        private readonly IProductData productData;

        public ProductController(IProductData productData)
        {
            this.productData = productData;
        }

        // GET api/Product
        [HttpGet]
        public List<ProductModel> Get()
        {
            return productData.GetProducts();
        }
    }
}