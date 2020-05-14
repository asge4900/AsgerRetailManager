﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.Models
{
    public class SaleReportModel
    {
        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
