using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SaleController : ControllerBase
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