﻿using ARM.DataManager.Library;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using ARM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ARM.DataManager.Controllers
{
    //[Authorize]
    public class ProductController : ApiController
    {
        // GET api/Product
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }
    }
}
