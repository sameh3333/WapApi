using Newtonsoft.Json;
using System.Text;

namespace Ui.Services
{
    public class GenericApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericApiClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<T> PostAsync<T>(string url, object data)
        {
            AddAccessTokenToHeader();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {response.StatusCode}, Content: {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
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
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {response.StatusCode}, Content: {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }
    }
}

