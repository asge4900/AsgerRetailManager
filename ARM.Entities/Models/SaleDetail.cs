using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.Entities.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Tax { get; set; }
    }
}
