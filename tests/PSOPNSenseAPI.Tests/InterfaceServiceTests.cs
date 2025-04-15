using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSOPNSenseAPI.Tests
{
    [TestClass]
    public class InterfaceServiceTests
    {
        private Mock<IOPNSenseApiClient>? _mockApiClient;
        private Mock<ILogger>? _mockLogger;
        private InterfaceService? _interfaceService;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<IOPNSenseApiClient>();
            _mockLogger = new Mock<ILogger>();
            _interfaceService = new InterfaceService(_mockApiClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetInterfacesAsync_ReturnsInterfaces()
        {
            // Arrange
            var response = new InterfaceListResponse
            {
                Interfaces = new List<InterfaceInfo>
                {
                    new InterfaceInfo
                    {
                        Name = "wan",
                        Description = "WAN",
                        IpAddress = "192.168.1.1",
                        SubnetMask = "255.255.255.0",
                        Gateway = "192.168.1.254",
                        Status = "up"
                    }
                }
            };

            _mockApiClient!.Setup(x => x.GetAsync<InterfaceListResponse>("interfaces/overview/searchInterfaces"))
                .ReturnsAsync(response);

            // Act
            var result = await _interfaceService!.GetInterfacesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Interfaces.Count);
            Assert.AreEqual("wan", result.Interfaces[0].Name);
            Assert.AreEqual("WAN", result.Interfaces[0].Description);
            Assert.AreEqual("192.168.1.1", result.Interfaces[0].IpAddress);
            Assert.AreEqual("255.255.255.0", result.Interfaces[0].SubnetMask);
            Assert.AreEqual("192.168.1.254", result.Interfaces[0].Gateway);
            Assert.AreEqual("up", result.Interfaces[0].Status);
        }

        [TestMethod]
        public async Task GetInterfaceDetailAsync_ReturnsInterfaceDetail()
        {
            // Arrange
            var interfaceName = "wan";
            var response = new InterfaceDetailResponse
            {
                Interface = new InterfaceDetail
                {
                    Name = "wan",
                    Description = "WAN",
                    IpAddress = "192.168.1.1",
                    SubnetMask = "255.255.255.0",
                    Gateway = "192.168.1.254",
                    Enabled = "1"
                }
            };

            _mockApiClient!.Setup(x => x.GetAsync<InterfaceDetailResponse>($"interfaces/overview/getInterface/{interfaceName}"))
                .ReturnsAsync(response);

            // Act
            var result = await _interfaceService!.GetInterfaceDetailAsync(interfaceName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Interface);
            Assert.AreEqual("wan", result.Interface.Name);
            Assert.AreEqual("WAN", result.Interface.Description);
            Assert.AreEqual("192.168.1.1", result.Interface.IpAddress);
            Assert.AreEqual("255.255.255.0", result.Interface.SubnetMask);
            Assert.AreEqual("192.168.1.254", result.Interface.Gateway);
            Assert.AreEqual("1", result.Interface.Enabled);
        }

        [TestMethod]
        public async Task UpdateInterfaceAsync_ReturnsSuccess()
        {
            // Arrange
            var interfaceName = "wan";
            var interfaceConfig = new InterfaceConfig
            {
                Description = "WAN",
                IpAddress = "192.168.1.1",
                SubnetMask = "255.255.255.0",
                Gateway = "192.168.1.254",
                Enabled = "1"
            };

            var response = new InterfaceUpdateResponse
            {
                Result = "saved"
            };

            _mockApiClient!.Setup(x => x.PostAsync<InterfaceUpdateResponse>($"interfaces/overview/setInterface/{interfaceName}", It.IsAny<object>()))
                .ReturnsAsync(response);

            // Act
            var result = await _interfaceService!.UpdateInterfaceAsync(interfaceName, interfaceConfig);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("saved", result.Result);
        }
    }
}
