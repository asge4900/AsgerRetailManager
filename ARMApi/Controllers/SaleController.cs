using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleData saleData;

        public SaleController(ISaleData saleData)
        {
            this.saleData = saleData;
        }

        //[Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            string userId = "1";
            saleData.SaveSale(sale, userId);
        }

        //[Authorize(Roles = "Admin, Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSalesReport()
        {
            return saleData.GetSaleReport();
        }
    }
}