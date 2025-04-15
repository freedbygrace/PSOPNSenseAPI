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
    public class RemoveOPNSenseAliasCmdletTests
    {
        private Mock<OPNSenseApiClient> _apiClientMock;
        private Mock<ILogger> _loggerMock;
        private RemoveOPNSenseAliasCmdlet _cmdlet;

        [TestInitialize]
        public void Initialize()
        {
            _apiClientMock = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _loggerMock = new Mock<ILogger>();

            _cmdlet = new RemoveOPNSenseAliasCmdlet();

            // Use reflection to set private fields
            typeof(OPNSenseBaseCmdlet).GetProperty("ApiClient").SetValue(_cmdlet, _apiClientMock.Object);
            typeof(OPNSenseBaseCmdlet).GetProperty("Logger").SetValue(_cmdlet, _loggerMock.Object);
        }

        [TestMethod]
        public void ProcessRecord_RemoveAliasWithForce_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Force = true;

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

            var deleteResponse = new AliasDeleteResponse
            {
                Result = "deleted"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var deleteTaskCompletionSource = new TaskCompletionSource<AliasDeleteResponse>();
            deleteTaskCompletionSource.SetResult(deleteResponse);
            var deleteTask = deleteTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"))
                .Returns(deleteTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the delete method was called
            _apiClientMock.Verify(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"), Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_RemoveAliasWithShouldProcess_Success()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;

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

            var deleteResponse = new AliasDeleteResponse
            {
                Result = "deleted"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var deleteTaskCompletionSource = new TaskCompletionSource<AliasDeleteResponse>();
            deleteTaskCompletionSource.SetResult(deleteResponse);
            var deleteTask = deleteTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"))
                .Returns(deleteTask);

            // Create a PowerShell pipeline to capture output with ShouldProcess always returning true
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the delete method was called
            _apiClientMock.Verify(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"), Times.Once);
        }

        [TestMethod]
        public void ProcessRecord_RemoveAliasAndApply_ReconfiguresAliases()
        {
            // Arrange
            string uuid = "test-uuid";
            _cmdlet.Uuid = uuid;
            _cmdlet.Force = true;
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

            var deleteResponse = new AliasDeleteResponse
            {
                Result = "deleted"
            };

            var reconfigureResponse = new AliasReconfigureResponse
            {
                Status = "ok"
            };

            // Mock the API client responses
            var getTaskCompletionSource = new TaskCompletionSource<AliasResponse>();
            getTaskCompletionSource.SetResult(getResponse);
            var getTask = getTaskCompletionSource.Task;

            var deleteTaskCompletionSource = new TaskCompletionSource<AliasDeleteResponse>();
            deleteTaskCompletionSource.SetResult(deleteResponse);
            var deleteTask = deleteTaskCompletionSource.Task;

            var reconfigureTaskCompletionSource = new TaskCompletionSource<AliasReconfigureResponse>();
            reconfigureTaskCompletionSource.SetResult(reconfigureResponse);
            var reconfigureTask = reconfigureTaskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(getTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"))
                .Returns(deleteTask);

            _apiClientMock.Setup(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"))
                .Returns(reconfigureTask);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            // Verify the delete and reconfigure methods were called
            _apiClientMock.Verify(x => x.PostAsync<AliasDeleteResponse>($"firewall/alias/delItem/{uuid}"), Times.Once);
            _apiClientMock.Verify(x => x.PostAsync<AliasReconfigureResponse>("firewall/alias/reconfigure"), Times.Once);
        }
    }


}
