using ARM.Entities;
using ARM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly HttpHelper _http;

        public SaleRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        //public async Task<List<Product>> GetAsync() =>
        //    await _http.GetAsync<List<Product>>("product");

        //public async Task<Product> GetAsync(int id) =>
        //    await _http.GetAsync<Product>($"product/{id}");

        public async Task<SaleModel> PostAsync(SaleModel sale) =>
            await _http.PostAsync<SaleModel, SaleModel>("sale", sale);

        public async Task PostVoidAsync(SaleModel sale) =>
            await _http.PostVoidAsync("sale", sale);
    }
}
