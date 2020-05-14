using ARM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.Entities.ViewModels
{
    public class SaleModel
    {
        public List<SaleDetailViewModel> SaleDetails { get; set; } = new List<SaleDetailViewModel>();
    }
}
