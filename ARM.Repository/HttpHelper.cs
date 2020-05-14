using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ARM.Repository
{
    /// <summary>
    /// Wrapper for making strongly-typed REST calls. 
    /// </summary>
    internal class HttpHelper
    {
        /// <summary>           
        /// The Base URL for the API.
        /// /// </summary>
        private readonly string _baseUrl;

        public HttpHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Makes an HTTP GET request to the given controller and returns the deserialized response content.
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string controller, string bearerToken = null)
        {
            using (var client = BaseClient())
            {
                if (bearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await client.GetAsync(controller);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<TResult>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Makes an HTTP POST request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        public async Task<TResult> PostAsync<TRequest, TResult>(string controller, TRequest body, string bearerToken = null)
        {
            using (var client = BaseClient())
            {
                if (bearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await client.PostAsJsonAsync(controller, body);

                //string json = await response.Content.ReadAsStringAsync();
                //TResult obj = JsonConvert.DeserializeObject<TResult>(json);            

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<TResult>();
                    return result; 
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task PostVoidAsync<TRequest>(string controller, TRequest body, string bearerToken = null)
        {
            using (var client = BaseClient())
            {
                if (bearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await client.PostAsJsonAsync(controller, body);            

                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsAsync<TResult>();
                    //return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Makes an HTTP PUT request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        public async Task<TResult> UpdateAsync<TRequest, TResult>(string controller, int id, TRequest body)
        {
            using (var client = BaseClient())
            {
                var response = await client.PutAsJsonAsync($"{controller}/{id}", body);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<TResult>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Makes an HTTP DELETE request to the given controller and includes all the given
        /// object's properties as URL parameters. Returns the deserialized response content.
        /// </summary>
        public async Task DeleteAsync(string controller, int Id)
        {
            using (var client = BaseClient())
            {
                var response = await client.DeleteAsync($"{controller}/{Id}");                
            }
        }

        /// <summary>
        /// Makes an HTTP POST request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        //public async Task<TResult> PostAsync<TResult>(string controller, FormUrlEncodedContent content)
        //{
        //    using (var client = BaseClient())
        //    {
        //        var response = await client.PostAsync(controller, content);                

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<TResult>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}

        /// <summary>
        /// Constructs the base HTTP client, including correct authorization and API version headers.
        /// </summary>
        private HttpClient BaseClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        /// <summary>
        /// Helper class for formatting <see cref="StringContent"/> as UTF8 application/json. 
        /// </summary>
        private class JsonStringContent : StringContent
        {
            /// <summary>
            /// Creates <see cref="StringContent"/> formatted as UTF8 application/json.
            /// </summary>
            public JsonStringContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
    }
}
