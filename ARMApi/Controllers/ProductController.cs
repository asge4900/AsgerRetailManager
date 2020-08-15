using System.Collections.Generic;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize (Roles = ("Cashier")]
    public class ProductController : ControllerBase
    {
        // GET api/Product
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();
            return data.GetProducts();
        }
    }
}