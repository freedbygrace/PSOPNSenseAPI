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
    public class SetOPNSenseAliasCmdletTests
    {
        private Mock<OPNSenseApiClient> _apiClientMock;
        private Mock<ILogger> _loggerMock;
        private SetOPNSenseAliasCmdlet _cmdlet;

        [TestInitialize]
        public void Initialize()
        {
            _apiClientMock = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _loggerMock = new Mock<ILogger>();
            
            _cmdlet = new SetOPNSenseAliasCmdlet();
            
            // Use reflection to set private fields
            typeof(OPNSenseBaseCmdlet).GetProperty("ApiClient").SetValue(_cmdlet, _apiClientMock.Object);
            typeof(OPNSenseBaseCmdlet).GetProperty("Logger").SetValue(_cmdlet, _loggerMock.Object);
        }

        [TestMethod]
        public void ProcessRecord_UpdateAliasContent_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Content = "192.168.1.10,192.168.1.11,192.168.1.12";

            var getResponse = new AliasResponse
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

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with the correct content
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Content").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "192.168.1.10,192.168.1.11,192.168.1.12")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_UpdateAliasDescription_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Description = "Updated Web Servers";

            var getResponse = new AliasResponse
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

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with the correct description
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Description").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "Updated Web Servers")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_DisableAlias_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Disabled = true;

            var getResponse = new AliasResponse
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

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with the alias disabled
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Enabled").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "0")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_EnableAlias_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Enabled = true;

            var getResponse = new AliasResponse
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
                    Enabled = "0"
                }
            };

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with the alias enabled
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Enabled").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "1")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_EnableCounters_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.EnableCounters = true;

            var getResponse = new AliasResponse
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

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with counters enabled
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Counters").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "1")), 
                Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_UpdateAndApply_ReconfiguresAliases()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Content = "192.168.1.10,192.168.1.11,192.168.1.12";
            _cmdlet.Apply = true;

            var getResponse = new AliasResponse
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

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            var reconfigureResponse = new AliasReconfigureResponse
            {
                Status = "ok"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            var reconfigureTaskCompletionSource = new TaskCompletionSource<AliasReconfigureResponse>();
            reconfigureTaskCompletionSource.SetResult(reconfigureResponse);
            var reconfigureTask = reconfigureTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"))
                .Returns(reconfigureTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the reconfigure method was called
            _apiClientMock.Verify(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"), Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_BothEnabledAndDisabled_UsesEnabled()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Enabled = true;
            _cmdlet.Disabled = true;

            var getResponse = new AliasResponse
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
                    Enabled = "0"
                }
            };

            var updateResponse = new AliasUpdateResponse
            {
                Result = "saved"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var updateTaskCompletionSource = new TaskCompletionSource<AliasUpdateResponse>();
            updateTaskCompletionSource.SetResult(updateResponse);
            var updateTask = updateTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", It.IsAny<object>()))
                .Returns(updateTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output), warning => { });

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the update method was called with the alias enabled
            _apiClientMock.Verify(x => x.PostAsync<AliasUpdateResponse>($"firewall/alias/setItem/{uuid}", 
                It.Is<object>(o => 
                    o.GetType().GetProperty("alias").GetValue(o, null).GetType().GetProperty("Enabled").GetValue(
                        o.GetType().GetProperty("alias").GetValue(o, null), null).ToString() == "1")), 
                Times.Once);
        }
    }
}
