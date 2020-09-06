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
        private readonly IConfiguration configuration;

        public SaleController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //[Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(configuration);
            string userId = "1";
            data.SaveSale(sale, userId);
        }

        //[Authorize(Roles = "Admin, Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData(configuration);
            return data.GetSaleReport();
        }
    }
}