using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSOPNSenseAPI.Tests
{
    [TestClass]
    public class ApiClientTests
    {
        private Mock<ILogger> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger>();
        }

        [TestMethod]
        public void Constructor_ValidParameters_CreatesClient()
        {
            // Arrange
            string baseUrl = "https://firewall.example.com";
            string apiKey = "key";
            string apiSecret = "secret";
            bool skipCertificateCheck = true;

            // Act
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, _loggerMock.Object);

            // Assert
            Assert.IsNotNull(client);
            Assert.AreEqual(baseUrl, client.BaseUrl);
            Assert.IsTrue(client.IsConnected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullBaseUrl_ThrowsArgumentNullException()
        {
            // Arrange
            string baseUrl = null;
            string apiKey = "key";
            string apiSecret = "secret";
            bool skipCertificateCheck = true;

            // Act
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, _loggerMock.Object);

            // Assert - Exception expected
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullApiKey_ThrowsArgumentNullException()
        {
            // Arrange
            string baseUrl = "https://firewall.example.com";
            string apiKey = null;
            string apiSecret = "secret";
            bool skipCertificateCheck = true;

            // Act
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, _loggerMock.Object);

            // Assert - Exception expected
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullApiSecret_ThrowsArgumentNullException()
        {
            // Arrange
            string baseUrl = "https://firewall.example.com";
            string apiKey = "key";
            string apiSecret = null;
            bool skipCertificateCheck = true;

            // Act
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, _loggerMock.Object);

            // Assert - Exception expected
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLogger_ThrowsArgumentNullException()
        {
            // Arrange
            string baseUrl = "https://firewall.example.com";
            string apiKey = "key";
            string apiSecret = "secret";
            bool skipCertificateCheck = true;

            // Act
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, null);

            // Assert - Exception expected
        }

        [TestMethod]
        public void Dispose_SetsIsConnectedToFalse()
        {
            // Arrange
            string baseUrl = "https://firewall.example.com";
            string apiKey = "key";
            string apiSecret = "secret";
            bool skipCertificateCheck = true;
            var client = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, _loggerMock.Object);

            // Act
            client.Dispose();

            // Assert
            Assert.IsFalse(client.IsConnected);
        }
    }
}
