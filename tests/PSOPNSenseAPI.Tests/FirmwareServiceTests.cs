using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PSOPNSenseAPI.Cmdlets;
using PSOPNSenseAPI.Services;
using PSOPNSenseAPI.Logging;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace PSOPNSenseAPI.Tests
{
    [TestClass]
    public class FirmwareServiceTests
    {
        private Mock<OPNSenseApiClient> _mockApiClient;
        private Mock<ILogger> _mockLogger;
        private FirmwareService _firmwareService;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _mockLogger = new Mock<ILogger>();
            _firmwareService = new FirmwareService(_mockApiClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetStatusAsync_ReturnsStatus()
        {
            // Arrange
            var expectedResponse = new { status = "ok" };
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/status"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.GetStatusAsync();

            // Assert
            Assert.AreEqual("ok", result.Status);
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/status"), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_CallsCorrectEndpoint()
        {
            // Arrange
            var expectedResponse = new { status = "ok" };
            _mockApiClient.Setup(c => c.PostAsync<dynamic>("core/firmware/update", It.IsAny<object>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.UpdateAsync();

            // Assert
            Assert.AreEqual("ok", result.Status);
            _mockApiClient.Verify(c => c.PostAsync<dynamic>("core/firmware/update", It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public async Task UpgradeAsync_CallsCorrectEndpoint()
        {
            // Arrange
            var expectedResponse = new { status = "ok" };
            _mockApiClient.Setup(c => c.PostAsync<dynamic>("core/firmware/upgrade", It.IsAny<object>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.UpgradeAsync();

            // Assert
            Assert.AreEqual("ok", result.Status);
            _mockApiClient.Verify(c => c.PostAsync<dynamic>("core/firmware/upgrade", It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public async Task GetChangelogAsync_ReturnsChangelog()
        {
            // Arrange
            var expectedResponse = new { changelog = "Changes in version X.Y.Z" };
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/changelog"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.GetChangelogAsync();

            // Assert
            Assert.AreEqual("Changes in version X.Y.Z", result.Changelog);
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/changelog"), Times.Once);
        }

        [TestMethod]
        public async Task GetHealthAsync_ReturnsHealth()
        {
            // Arrange
            var expectedResponse = new { health = "healthy" };
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/health"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.GetHealthAsync();

            // Assert
            Assert.AreEqual("healthy", result.Health);
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/health"), Times.Once);
        }

        [TestMethod]
        public async Task GetAuditAsync_ReturnsAudit()
        {
            // Arrange
            var expectedResponse = new { audit = new[] { new { name = "package1", version = "1.0.0" } } };
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/audit"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _firmwareService.GetAuditAsync();

            // Assert
            Assert.IsNotNull(result.Audit);
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/audit"), Times.Once);
        }
    }
}
