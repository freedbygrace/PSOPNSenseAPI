using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Client for interacting with the OPNSense API
    /// </summary>
    public class OPNSenseApiClient : IOPNSenseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private bool _disposed = false;

        /// <summary>
        /// Gets the base URL of the OPNSense API
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Gets the API key
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Gets the API secret
        /// </summary>
        public string ApiSecret { get; }

        /// <summary>
        /// Gets a value indicating whether to skip certificate validation
        /// </summary>
        public bool SkipCertificateCheck { get; }

        /// <summary>
        /// Gets a value indicating whether the client is connected
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets the full URL for the given endpoint
        /// </summary>
        /// <param name="endpoint">The endpoint</param>
        /// <returns>The full URL</returns>
        public string GetFullUrl(string endpoint)
        {
            return $"{BaseUrl}/api/{endpoint.TrimStart('/')}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OPNSenseApiClient"/> class
        /// </summary>
        /// <param name="baseUrl">The base URL of the OPNSense API</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiSecret">The API secret</param>
        /// <param name="skipCertificateCheck">Whether to skip certificate validation</param>
        /// <param name="logger">The logger to use</param>
        public OPNSenseApiClient(string baseUrl, string apiKey, string apiSecret, bool skipCertificateCheck, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            if (string.IsNullOrWhiteSpace(apiSecret))
                throw new ArgumentNullException(nameof(apiSecret));

            BaseUrl = baseUrl.TrimEnd('/');
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            SkipCertificateCheck = skipCertificateCheck;

            var handler = new HttpClientHandler();
            if (skipCertificateCheck)
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            }

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set up basic authentication
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{apiSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            _logger.Debug($"Initialized OPNSense API client for {baseUrl}");
            IsConnected = true;
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response to</typeparam>
        /// <param name="endpoint">The API endpoint</param>
        /// <returns>The deserialized response</returns>
        public T Get<T>(string endpoint)
        {
            var url = $"{BaseUrl}/api/{endpoint.TrimStart('/')}";
            _logger.Debug($"GET {url}");

            try
            {
                var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();
                return ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GET request to {url}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response to</typeparam>
        /// <param name="endpoint">The API endpoint</param>
        /// <param name="data">The data to send</param>
        /// <returns>The deserialized response</returns>
        public T Post<T>(string endpoint, object data = null)
        {
            var url = $"{BaseUrl}/api/{endpoint.TrimStart('/')}";
            _logger.Debug($"POST {url}");

            try
            {
                HttpContent content = null;
                if (data != null)
                {
                    var json = JsonConvert.SerializeObject(data);
                    _logger.Debug($"Request body: {json}");
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var response = _httpClient.PostAsync(url, content).GetAwaiter().GetResult();
                return ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in POST request to {url}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Processes the HTTP response
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response to</typeparam>
        /// <param name="response">The HTTP response</param>
        /// <returns>The deserialized response</returns>
        private T ProcessResponse<T>(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            _logger.Debug($"Response status: {(int)response.StatusCode} {response.StatusCode}");
            _logger.Debug($"Response body: {content}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error($"API request failed: {(int)response.StatusCode} {response.StatusCode}");
                _logger.Error($"Response content: {content}");
                throw new OPNSenseApiException($"API request failed with status code {(int)response.StatusCode}", response.StatusCode, content);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonException ex)
            {
                _logger.Error($"Failed to deserialize response: {ex.Message}");
                _logger.Error($"Response content: {content}");
                throw new OPNSenseApiException($"Failed to deserialize response: {ex.Message}", response.StatusCode, content, ex);
            }
        }

        /// <summary>
        /// Disposes the HTTP client
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the HTTP client
        /// </summary>
        /// <param name="disposing">Whether to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _httpClient?.Dispose();
                IsConnected = false;
            }

            _disposed = true;
        }
    }
}

