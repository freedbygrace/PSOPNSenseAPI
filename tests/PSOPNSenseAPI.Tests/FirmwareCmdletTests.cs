using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PSOPNSenseAPI.Cmdlets;
using PSOPNSenseAPI.Services;
using PSOPNSenseAPI.Logging;
using System;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

namespace PSOPNSenseAPI.Tests
{
    [TestClass]
    public class FirmwareCmdletTests
    {
        private Mock<OPNSenseApiClient> _mockApiClient;
        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _mockLogger = new Mock<ILogger>();
        }

        [TestMethod]
        public void UpdateOPNSenseFirmwareCmdlet_ProcessRecord_CallsUpdateAsync()
        {
            // Arrange
            _mockApiClient.Setup(c => c.PostAsync<dynamic>("core/firmware/update", It.IsAny<object>()))
                .ReturnsAsync(new { status = "ok" });

            // Create and configure the cmdlet
            var cmdlet = new UpdateOPNSenseFirmwareCmdlet();

            // Use reflection to set the protected properties
            SetProtectedProperty(cmdlet, "ApiClient", _mockApiClient.Object);
            SetProtectedProperty(cmdlet, "Logger", _mockLogger.Object);

            // Act
            InvokeMethod(cmdlet, "ProcessRecord");

            // Assert
            _mockApiClient.Verify(c => c.PostAsync<dynamic>("core/firmware/update", It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void StartOPNSenseFirmwareUpgradeCmdlet_ProcessRecord_CallsUpgradeAsync()
        {
            // Arrange
            _mockApiClient.Setup(c => c.PostAsync<dynamic>("core/firmware/upgrade", It.IsAny<object>()))
                .ReturnsAsync(new { status = "ok" });

            // Create and configure the cmdlet
            var cmdlet = new UpgradeOPNSenseFirmwareCmdlet();

            // Use reflection to set the protected properties
            SetProtectedProperty(cmdlet, "ApiClient", _mockApiClient.Object);
            SetProtectedProperty(cmdlet, "Logger", _mockLogger.Object);

            // Set Force parameter to skip confirmation
            typeof(UpgradeOPNSenseFirmwareCmdlet).GetProperty("Force").SetValue(cmdlet, new SwitchParameter(true));

            // Act
            InvokeMethod(cmdlet, "ProcessRecord");

            // Assert
            _mockApiClient.Verify(c => c.PostAsync<dynamic>("core/firmware/upgrade", It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void GetOPNSenseFirmwareCmdlet_ProcessRecord_CallsGetStatusAsync()
        {
            // Arrange
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/status"))
                .ReturnsAsync(new { status = "ok" });

            // Create and configure the cmdlet
            var cmdlet = new GetOPNSenseFirmwareCmdlet();

            // Use reflection to set the protected properties
            SetProtectedProperty(cmdlet, "ApiClient", _mockApiClient.Object);
            SetProtectedProperty(cmdlet, "Logger", _mockLogger.Object);

            // Act
            InvokeMethod(cmdlet, "ProcessRecord");

            // Assert
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/status"), Times.Once);
        }

        [TestMethod]
        public void GetOPNSenseFirmwareCmdlet_WithChangelog_CallsGetChangelogAsync()
        {
            // Arrange
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/changelog"))
                .ReturnsAsync(new { changelog = "Changes in version X.Y.Z" });

            // Create and configure the cmdlet
            var cmdlet = new GetOPNSenseFirmwareCmdlet();

            // Use reflection to set the protected properties
            SetProtectedProperty(cmdlet, "ApiClient", _mockApiClient.Object);
            SetProtectedProperty(cmdlet, "Logger", _mockLogger.Object);

            // Set Changelog parameter
            typeof(GetOPNSenseFirmwareCmdlet).GetProperty("Changelog").SetValue(cmdlet, new SwitchParameter(true));

            // Act
            InvokeMethod(cmdlet, "ProcessRecord");

            // Assert
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/changelog"), Times.Once);
        }

        [TestMethod]
        public void GetOPNSenseFirmwareCmdlet_WithAudit_CallsGetAuditAsync()
        {
            // Arrange
            _mockApiClient.Setup(c => c.GetAsync<dynamic>("core/firmware/audit"))
                .ReturnsAsync(new { audit = new[] { new { name = "package1", version = "1.0.0" } } });

            // Create and configure the cmdlet
            var cmdlet = new GetOPNSenseFirmwareCmdlet();

            // Use reflection to set the protected properties
            SetProtectedProperty(cmdlet, "ApiClient", _mockApiClient.Object);
            SetProtectedProperty(cmdlet, "Logger", _mockLogger.Object);

            // Set Audit parameter
            typeof(GetOPNSenseFirmwareCmdlet).GetProperty("Audit").SetValue(cmdlet, new SwitchParameter(true));

            // Act
            InvokeMethod(cmdlet, "ProcessRecord");

            // Assert
            _mockApiClient.Verify(c => c.GetAsync<dynamic>("core/firmware/audit"), Times.Once);
        }

        private void SetProtectedProperty(object obj, string propertyName, object value)
        {
            var propertyInfo = obj.GetType().BaseType.GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property {propertyName} not found on {obj.GetType().FullName}");
            }

            var backingField = obj.GetType().BaseType.GetField(
                $"<{propertyName}>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic);

            if (backingField != null)
            {
                backingField.SetValue(obj, value);
            }
            else if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, value);
            }
            else
            {
                throw new ArgumentException($"Cannot set property {propertyName} on {obj.GetType().FullName}");
            }
        }

        private void InvokeMethod(object obj, string methodName)
        {
            var methodInfo = obj.GetType().GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methodInfo == null)
            {
                throw new ArgumentException($"Method {methodName} not found on {obj.GetType().FullName}");
            }

            methodInfo.Invoke(obj, null);
        }
    }
}
