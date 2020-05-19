using ARM.Entities.ViewModels;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public interface ISaleRepository
    {
        Task<SaleModel> PostAsync(SaleModel sale);
        Task PostVoidAsync(SaleModel sale);
    }
}