using ARM.DesktopUI.Library.Api;
using ARM.DesktopUI.Library.Helpers;
using ARM.DesktopUI.Models;
using ARM.Entities;
using ARM.Entities.ViewModels;
using AutoMapper;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ARM.DesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        #region Fields

        private IApiHelper apiHelper;

        private IConfigHelper configHelper;

        private IMapper mapper;

        private BindingList<ProductDisplayModel> products;

        private int itemQuantity = 1;

        private ProductDisplayModel selectedProduct;

        private CartItemDisplayModel selectedCartItem;

        private BindingList<CartItemDisplayModel> cart = new BindingList<CartItemDisplayModel>();

        #endregion

        public SalesViewModel(IApiHelper apiHelper, IConfigHelper configHelper, IMapper mapper)
        {
            this.apiHelper = apiHelper;
            this.configHelper = configHelper;
            this.mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            var productList = await apiHelper.Repository.Product.GetAsync();

            var displayProducts = mapper.Map<List<ProductDisplayModel>>(productList);

            Products = new BindingList<ProductDisplayModel>(displayProducts);
        }

        #region Properties

        public BindingList<ProductDisplayModel> Products
        {
            get => products;
            set
            {
                products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public int ItemQuantity
        {
            get => itemQuantity;
            set
            {
                itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public ProductDisplayModel SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public CartItemDisplayModel SelectedCartItem
        {
            get => selectedCartItem;
            set
            {
                selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

        public BindingList<CartItemDisplayModel> Cart
        {
            get => cart;
            set
            {
                cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal => CalculateSubTotal().ToString("C");        

        public string Tax => CalculateTax().ToString("C");

        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                if (SelectedCartItem != null && SelectedCartItem?.QuantityInCart > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        #endregion

        #region Methods

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subTotal;
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = configHelper.GetTaxRate()/100;

            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            return taxAmount;
        }

        public void AddToCart()
        {
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;                
            }
            else
            {
                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }


        public void RemoveFromCart()
        {
            SelectedCartItem.Product.QuantityInStock += 1;

            if (SelectedCartItem.QuantityInCart > 1)
            {
                SelectedCartItem.QuantityInCart -= 1;                
            }
            else
            {                
                Cart.Remove(SelectedCartItem);
            }

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
        }

        public async Task CheckOut()
        {
            SaleModel sale = new SaleModel();

            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailViewModel
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityInCart
                });
            }

            await apiHelper.Repository.Sale.PostVoidAsync(sale);

            await ResetSalesViewModelAsync();
        }
        
        private async Task ResetSalesViewModelAsync()
        {
            Cart = new BindingList<CartItemDisplayModel>();

            await LoadProductsAsync();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);           
        }

        #endregion
    }
}
