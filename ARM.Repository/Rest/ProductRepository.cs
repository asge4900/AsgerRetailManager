using ARM.Entities;
using ARM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpHelper _http;

        public ProductRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<List<ProductModel>> GetAsync() =>
            await _http.GetAsync<List<ProductModel>>("product");

        public async Task<ProductModel> GetAsync(int id) =>
            await _http.GetAsync<ProductModel>($"product/{id}");
    }
}
