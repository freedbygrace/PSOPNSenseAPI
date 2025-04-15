using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSOPNSenseAPI.Tests.Services
{
    [TestClass]
    public class AliasServiceTests
    {
        private Mock<OPNSenseApiClient> _apiClientMock;
        private Mock<ILogger> _loggerMock;
        private AliasService _aliasService;

        [TestInitialize]
        public void Initialize()
        {
            _apiClientMock = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _loggerMock = new Mock<ILogger>();
            _aliasService = new AliasService(_apiClientMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetAliasesAsync_ReturnsAliases()
        {
            // Arrange
            var expectedResponse = new AliasListResponse
            {
                Total = 2,
                RowCount = 2,
                Current = 1,
                Rows = new List<Alias>
                {
                    new Alias
                    {
                        Uuid = "uuid1",
                        Name = "WebServers",
                        Type = "host",
                        Content = "192.168.1.10,192.168.1.11",
                        Description = "Web Servers",
                        Enabled = "1"
                    },
                    new Alias
                    {
                        Uuid = "uuid2",
                        Name = "WebPorts",
                        Type = "port",
                        Content = "80,443",
                        Description = "Web Ports",
                        Enabled = "1"
                    }
                }
            };

            _apiClientMock.Setup(x => x.GetAsync<AliasListResponse>("firewall/alias/searchItem"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.GetAliasesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Total);
            Assert.AreEqual(2, result.Rows.Count);
            Assert.AreEqual("WebServers", result.Rows[0].Name);
            Assert.AreEqual("WebPorts", result.Rows[1].Name);
            _loggerMock.Verify(x => x.Information("Getting aliases"), Times.Once);
        }

        [TestMethod]
        public async Task GetAliasAsync_ReturnsAlias()
        {
            // Arrange
            string uuid = "uuid1";
            var expectedResponse = new AliasResponse
            {
                Alias = new AliasDetail
                {
                    Name = "WebServers",
                    Type = "host",
                    Content = "192.168.1.10,192.168.1.11",
                    Description = "Web Servers",
                    Protocol = "",
                    UpdateFrequency = "",
                    Counters = "0",
                    Enabled = "1"
                }
            };

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.GetAliasAsync(uuid);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("WebServers", result.Alias.Name);
            Assert.AreEqual("host", result.Alias.Type);
            _loggerMock.Verify(x => x.Information($"Getting alias with UUID {uuid}"), Times.Once);
        }

        [TestMethod]
        public async Task CreateAliasAsync_ReturnsUuid()
        {
            // Arrange
            var alias = new AliasConfig
            {
                Name = "WebServers",
                Type = "host",
                Content = "192.168.1.10,192.168.1.11",
                Description = "Web Servers",
                Enabled = "1"
            };

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.CreateAliasAsync(alias);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("new-uuid", result.Uuid);
            _loggerMock.Verify(x => x.Information("Creating alias"), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAliasAsync_ReturnsSuccess()
        {
            // Arrange
            string uuid = "uuid1";
            var alias = new AliasConfig
            {
                Name = "WebServers",
                Type = "host",
                Content = "192.168.1.10,192.168.1.11,192.168.1.12",
                Description = "Updated Web Servers",
                Enabled = "1"
            };

            var expectedResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.UpdateAliasAsync(uuid, alias);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("saved", result.Result);
            _loggerMock.Verify(x => x.Information($"Updating alias with UUID {uuid}"), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAliasAsync_ReturnsSuccess()
        {
            // Arrange
            string uuid = "uuid1";
            var expectedResponse = new AliasDeleteResponse
            {
                Result = "deleted"
            };

            _apiClientMock.Setup(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.DeleteAliasAsync(uuid);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("deleted", result.Result);
            _loggerMock.Verify(x => x.Information($"Deleting alias with UUID {uuid}"), Times.Once);
        }

        [TestMethod]
        public async Task ReconfigureAliasesAsync_ReturnsSuccess()
        {
            // Arrange
            var expectedResponse = new AliasReconfigureResponse
            {
                Status = "ok"
            };

            _apiClientMock.Setup(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _aliasService.ReconfigureAliasesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.Status);
            _loggerMock.Verify(x => x.Information("Reconfiguring aliases"), Times.Once);
        }
    }
}
