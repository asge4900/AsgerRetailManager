﻿using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using ARM.Entities.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ARM.DataManager.Controllers
{
    //[Authorize]
    public class SaleController : ApiController
    {
        //[Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = "1";
            data.SaveSale(sale, userId);
        }

        //[Authorize(Roles = "Admin, Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
