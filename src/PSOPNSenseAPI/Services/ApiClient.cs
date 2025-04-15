using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Client for making API requests to OPNSense
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class
        /// </summary>
        /// <param name="baseUrl">The base URL of the OPNSense API</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiSecret">The API secret</param>
        /// <param name="skipCertificateCheck">Whether to skip certificate validation</param>
        public ApiClient(string baseUrl, string apiKey, string apiSecret, bool skipCertificateCheck = false)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _apiKey = apiKey;
            _apiSecret = apiSecret;

            var handler = new HttpClientHandler();
            if (skipCertificateCheck)
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            }

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Set basic authentication
            var authString = $"{apiKey}:{apiSecret}";
            var base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint
        /// </summary>
        /// <param name="endpoint">The API endpoint</param>
        /// <returns>The HTTP response</returns>
        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            var url = $"{_baseUrl}{endpoint}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint
        /// </summary>
        /// <param name="endpoint">The API endpoint</param>
        /// <param name="data">The data to send</param>
        /// <returns>The HTTP response</returns>
        public async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
        {
            var url = $"{_baseUrl}{endpoint}";
            var content = data != null
                ? new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
                : null;

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Sends a PUT request to the specified endpoint
        /// </summary>
        /// <param name="endpoint">The API endpoint</param>
        /// <param name="data">The data to send</param>
        /// <returns>The HTTP response</returns>
        public async Task<HttpResponseMessage> PutAsync(string endpoint, object data)
        {
            var url = $"{_baseUrl}{endpoint}";
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(url, content);
        }

        /// <summary>
        /// Sends a DELETE request to the specified endpoint
        /// </summary>
        /// <param name="endpoint">The API endpoint</param>
        /// <returns>The HTTP response</returns>
        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var url = $"{_baseUrl}{endpoint}";
            return await _httpClient.DeleteAsync(url);
        }
    }
}
