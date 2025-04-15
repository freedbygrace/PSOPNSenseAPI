using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PSOPNSenseAPI.Cmdlets;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Services;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PSOPNSenseAPI.Tests.Cmdlets
{
    [TestClass]
    public class NewOPNSenseAliasCmdletTests
    {
        private Mock<OPNSenseApiClient> _apiClientMock;
        private Mock<ILogger> _loggerMock;
        private NewOPNSenseAliasCmdlet _cmdlet;

        [TestInitialize]
        public void Initialize()
        {
            _apiClientMock = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _loggerMock = new Mock<ILogger>();
            
            _cmdlet = new NewOPNSenseAliasCmdlet();
            
            // Use reflection to set private fields
            typeof(OPNSenseBaseCmdlet).GetProperty("ApiClient").SetValue(_cmdlet, _apiClientMock.Object);
            typeof(OPNSenseBaseCmdlet).GetProperty("Logger").SetValue(_cmdlet, _loggerMock.Object);
        }

        [TestMethod]
        public void ProcessRecord_CreateHostAlias_ReturnsUuid()
        {
            // Arrange
            _cmdlet.Name = "WebServers";
            _cmdlet.Type = "host";
            _cmdlet.Content = "192.168.1.10,192.168.1.11";
            _cmdlet.Description = "Web Servers";

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            // Mock the API client response
            var taskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            taskCompletionSource.SetResult(expectedResponse);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);
        }

        [TestMethod]
        public void ProcessRecord_CreatePortAlias_WithProtocol_ReturnsUuid()
        {
            // Arrange
            _cmdlet.Name = "WebPorts";
            _cmdlet.Type = "port";
            _cmdlet.Content = "80,443";
            _cmdlet.Description = "Web Ports";
            _cmdlet.Protocol = "TCP";

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            // Mock the API client response
            var taskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            taskCompletionSource.SetResult(expectedResponse);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);

            // Verify the correct protocol was set
            _apiClientMock.Verify(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Protocol").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "TCP")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_CreateUrlAlias_WithUpdateFrequency_ReturnsUuid()
        {
            // Arrange
            _cmdlet.Name = "BlockList";
            _cmdlet.Type = "url";
            _cmdlet.Content = "https://example.com/blocklist.txt";
            _cmdlet.Description = "Block List";
            _cmdlet.UpdateFrequency = 1;

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            // Mock the API client response
            var taskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            taskCompletionSource.SetResult(expectedResponse);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);

            // Verify the correct update frequency was set
            _apiClientMock.Verify(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("UpdateFrequency").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "1")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_CreateDisabledAlias_ReturnsUuid()
        {
            // Arrange
            _cmdlet.Name = "WebServers";
            _cmdlet.Type = "host";
            _cmdlet.Content = "192.168.1.10,192.168.1.11";
            _cmdlet.Description = "Web Servers";
            _cmdlet.Disabled = true;

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            // Mock the API client response
            var taskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            taskCompletionSource.SetResult(expectedResponse);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);

            // Verify the alias was created as disabled
            _apiClientMock.Verify(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Enabled").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "0")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_CreateAliasWithCounters_ReturnsUuid()
        {
            // Arrange
            _cmdlet.Name = "WebServers";
            _cmdlet.Type = "host";
            _cmdlet.Content = "192.168.1.10,192.168.1.11";
            _cmdlet.Description = "Web Servers";
            _cmdlet.EnableCounters = true;

            var expectedResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            // Mock the API client response
            var taskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            taskCompletionSource.SetResult(expectedResponse);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);

            // Verify the alias was created with counters enabled
            _apiClientMock.Verify(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Counters").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "1")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_CreateAliasAndApply_ReconfiguresAliases()
        {
            // Arrange
            _cmdlet.Name = "WebServers";
            _cmdlet.Type = "host";
            _cmdlet.Content = "192.168.1.10,192.168.1.11";
            _cmdlet.Description = "Web Servers";
            _cmdlet.Apply = true;

            var createResponse = new AliasCreateResponse
            {
                Uuid = "new-uuid"
            };

            var reconfigureResponse = new AliasReconfigureResponse
            {
                Status = "ok"
            };

            // Mock the API client responses
            var createTaskCompletionSource = new TaskCompletionSource<AliasCreateResponse>();
            createTaskCompletionSource.SetResult(createResponse);
            var createTask = createTaskCompletionSource.Task;

            var reconfigureTaskCompletionSource = new TaskCompletionSource<AliasReconfigureResponse>();
            reconfigureTaskCompletionSource.SetResult(reconfigureResponse);
            var reconfigureTask = reconfigureTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.PostAsync<AliasCreateResponse>("firewall/alias/addItem", It.IsAny<object>()))
                .Returns(createTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"))
                .Returns(reconfigureTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("new-uuid", results[0]);

            // Verify the reconfigure method was called
            _apiClientMock.Verify(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"), Times.Once);
        }
    }
}
