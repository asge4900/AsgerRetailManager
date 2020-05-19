using ARM.Entities;
using ARM.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAsync();
        Task<ProductModel> GetAsync(int id);
    }
}