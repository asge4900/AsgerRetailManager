using ARM.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpHelper _http;

        public LoginRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<AuthenticatedUser> AuthenticateAsync(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            //return await _http.PostAsync<AuthenticatedUser>("token", data);

            return await _http.PostAsync<dynamic, AuthenticatedUser>("token", data);
        }

    }
}
