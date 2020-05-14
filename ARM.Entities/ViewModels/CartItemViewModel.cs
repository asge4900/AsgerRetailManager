using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.Entities.ViewModels
{
    public class CartItemViewModel
    {
        public ProductModel Product { get; set; }

        public int QuantityInCart { get; set; }

        //public string DisplayText => $"{Product.ProductName} ({QuantityInCart})";
    }
}
