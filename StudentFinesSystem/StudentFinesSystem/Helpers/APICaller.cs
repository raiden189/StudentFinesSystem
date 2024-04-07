using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Helpers
{
    public class APICaller
    {
        private HttpClient ApiClient { get; set; }

        private readonly IConfiguration _configuration;

        public APICaller(IConfiguration configuration)
        {
            this._configuration = configuration;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string baseAddress = DeviceInfo.Platform == DevicePlatform.Android ?
                this._configuration.GetValue<string>("AndroidAPI") :
                this._configuration.GetValue<string>("API");

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(baseAddress);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void AddAuthorization(string token)
        {
            if (string.IsNullOrEmpty(token))
                return;
            
            IEnumerable<string> values;
            var apiHeader = ApiClient.DefaultRequestHeaders;
            if (!apiHeader.TryGetValues("Authorization", out values))
            {
                ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }

        public async Task<T> GetRequest<T>(string method)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync(method))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PostRequest<T>(string method, HttpContent parameter)
        {
            using (HttpResponseMessage response = await ApiClient.PostAsync(method, parameter))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                else
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<T> PostJsonRequest<T, U>(string method, U parameter)
        {
            using (HttpResponseMessage response = await ApiClient.PostAsync(method, new StringContent(
                    JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
