using ARM.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetUsersAsync();

        Task<Dictionary<string, string>> GetRolesAsync();

        Task AddToRole(string userId, string roleName);

        Task RemoveRole(string userId, string roleName);        
    }
}