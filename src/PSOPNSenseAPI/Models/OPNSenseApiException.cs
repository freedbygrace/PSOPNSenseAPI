using System;
using System.Net;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Exception thrown when an OPNSense API request fails
    /// </summary>
    public class OPNSenseApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code of the failed request
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the response content of the failed request
        /// </summary>
        public string ResponseContent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OPNSenseApiException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="statusCode">The HTTP status code</param>
        /// <param name="responseContent">The response content</param>
        public OPNSenseApiException(string message, HttpStatusCode statusCode, string responseContent)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OPNSenseApiException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="statusCode">The HTTP status code</param>
        /// <param name="responseContent">The response content</param>
        /// <param name="innerException">The inner exception</param>
        public OPNSenseApiException(string message, HttpStatusCode statusCode, string responseContent, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }
    }
}
