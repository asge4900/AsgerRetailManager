﻿using ARM.DataManager.Library.Models;
using ARM.Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class SaleData
    {
        private readonly IConfiguration configuration;

        public SaleData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetail> details = new List<SaleDetail>();
            ProductData product = new ProductData(configuration);
            var TaxRate = ConfigHelper.GetTaxRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = product.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;

                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * TaxRate;
                }

                details.Add(detail);
            }            

            Sale sale = new Sale
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;            

            using (SqlDataAccess sql = new SqlDataAccess(configuration))
            {
                try
                {
                    sql.StartTransaction(Constants.ARMDATA);

                    sale.Id = sql.SaveAndLoadDataInTransaction<int, dynamic>("CreateSale", sale).FirstOrDefault();

                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataInTransaction("CreateSaleDetail", item);
                    }

                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            var output = sql.LoadData<SaleReportModel, dynamic>("SaleReport", new { }, Constants.ARMDATA);

            return output;
        }
    }
}
