using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using ARM.Entities.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ARM.DataManager.Controllers
{
    //[Authorize]
    public class SaleController : ApiController
    {
        // POST api/sale
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();

            data.SaveSale(sale, "1");
        }

        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();

            return data.GetSaleReport();
        }
    }
}
