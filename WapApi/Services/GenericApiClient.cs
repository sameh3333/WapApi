using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace WapApi.Services
{
    public class GenericApiClient
    {
   
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GenericApiClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            {
                _httpClient = clientFactory.CreateClient("ApiClient");
            // Get the base API URL from appsettings.json
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl);  // Set the base URL for the HTTP client
            _httpContextAccessor = httpContextAccessor;
            }

        public async Task<T> GetAsync<T>(string url)
        {
            AddAccessTokenToHeader();

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {response.StatusCode}, Content: {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        public async Task<T> PostAsync<T>(string url, object data)
            {
                AddAccessTokenToHeader();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                // Log or inspect the error response content
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Response: {errorContent}");

                // Throw an exception or return default (can be customized based on your needs)
                throw new HttpRequestException($"Error {response.StatusCode}: {errorContent}");
            }
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
            //    var json = JsonConvert.SerializeObject(data);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");

            //    var response = await _httpClient.PostAsync(url, content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    throw new ApplicationException($"API Error: {response.StatusCode}, Content: {errorContent}");
            //}
            //var responseData = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<T>(responseData);
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            AddAccessTokenToHeader();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {response.StatusCode}, Content: {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        public async Task<T> DeleteAsync<T>(string url)
        {
            AddAccessTokenToHeader();

            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        private void AddAccessTokenToHeader()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
        }
    }
}
