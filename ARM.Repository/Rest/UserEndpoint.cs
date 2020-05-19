using ARM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly HttpHelper _http;

        public UserEndpoint(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<List<UserModel>> GetUsersAsync() =>
            await _http.GetAsync<List<UserModel>>("User/Admin/GetAllUsers");

        public async Task<Dictionary<string, string>> GetRolesAsync() =>
            await _http.GetAsync<Dictionary<string, string>>("User/Admin/GetAllRoles");

        public async Task AddToRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            await _http.PostVoidAsync("User/Admin/AddRole", data);
        }

        public async Task RemoveRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            await _http.PostVoidAsync("User/Admin/RemoveRole", data);
        }
    }
}
