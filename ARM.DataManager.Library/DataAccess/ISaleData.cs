﻿using ARM.DataManager.Library.Models;
using System.Collections.Generic;

namespace ARM.DataManager.Library.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSaleReport();
        void SaveSale(SaleModel saleInfo, string cashierId);
    }
}