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
    public class GetOPNSenseAliasCmdletTests
    {
        private Mock<OPNSenseApiClient> _apiClientMock;
        private Mock<ILogger> _loggerMock;
        private GetOPNSenseAliasCmdlet _cmdlet;

        [TestInitialize]
        public void Initialize()
        {
            _apiClientMock = new Mock<OPNSenseApiClient>("https://example.com", "key", "secret", false, Mock.Of<ILogger>());
            _loggerMock = new Mock<ILogger>();

            _cmdlet = new GetOPNSenseAliasCmdlet();

            // Use reflection to set private fields
            typeof(OPNSenseBaseCmdlet).GetProperty("ApiClient").SetValue(_cmdlet, _apiClientMock.Object);
            typeof(OPNSenseBaseCmdlet).GetProperty("Logger").SetValue(_cmdlet, _loggerMock.Object);
        }

        [TestMethod]
        public void ProcessRecord_GetAllAliases_WritesAliasesToPipeline()
        {
            // Arrange
            var aliases = new List<Alias>
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
            };

            var response = new AliasListResponse
            {
                Total = aliases.Count,
                RowCount = aliases.Count,
                Current = 1,
                Rows = aliases
            };

            var aliasServiceMock = new Mock<AliasService>(_apiClientMock.Object, _loggerMock.Object);
            aliasServiceMock.Setup(x => x.GetAliasesAsync()).ReturnsAsync(response);

            // Mock the AliasService creation
            var taskCompletionSource = new TaskCompletionSource<AliasListResponse>();
            taskCompletionSource.SetResult(response);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasListResponse>("firewall/alias/searchItem"))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.IsInstanceOfType(results[0], typeof(Alias));
            Assert.IsInstanceOfType(results[1], typeof(Alias));

            var alias1 = results[0] as Alias;
            var alias2 = results[1] as Alias;

            Assert.AreEqual("WebServers", alias1.Name);
            Assert.AreEqual("WebPorts", alias2.Name);
        }

        [TestMethod]
        public void ProcessRecord_GetAliasByUuid_WritesAliasDetailToPipeline()
        {
            // Arrange
            string uuid = "uuid1";
            _cmdlet.Uuid = uuid;

            // Use reflection to set the parameter set name
            typeof(PSCmdlet).GetProperty("ParameterSetName").SetValue(_cmdlet, "ByUuid");

            var aliasDetail = new AliasDetail
            {
                Name = "WebServers",
                Type = "host",
                Content = "192.168.1.10,192.168.1.11",
                Description = "Web Servers",
                Protocol = "",
                UpdateFrequency = "",
                Counters = "0",
                Enabled = "1"
            };

            var response = new AliasResponse
            {
                Alias = aliasDetail
            };

            var aliasServiceMock = new Mock<AliasService>(_apiClientMock.Object, _loggerMock.Object);
            aliasServiceMock.Setup(x => x.GetAliasAsync(uuid)).ReturnsAsync(response);

            // Mock the AliasService creation
            var taskCompletionSource = new TaskCompletionSource<AliasResponse>();
            taskCompletionSource.SetResult(response);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasResponse>($"firewall/alias/getItem/{uuid}"))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.IsInstanceOfType(results[0], typeof(AliasDetail));

            var alias = results[0] as AliasDetail;

            Assert.AreEqual("WebServers", alias.Name);
            Assert.AreEqual("host", alias.Type);
            Assert.AreEqual("192.168.1.10,192.168.1.11", alias.Content);
        }

        [TestMethod]
        public void ProcessRecord_GetAliasByName_WritesFilteredAliasesToPipeline()
        {
            // Arrange
            string name = "WebServers";
            _cmdlet.Name = name;

            // Use reflection to set the parameter set name
            typeof(PSCmdlet).GetProperty("ParameterSetName").SetValue(_cmdlet, "ByName");

            var aliases = new List<Alias>
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
            };

            var response = new AliasListResponse
            {
                Total = aliases.Count,
                RowCount = aliases.Count,
                Current = 1,
                Rows = aliases
            };

            var aliasServiceMock = new Mock<AliasService>(_apiClientMock.Object, _loggerMock.Object);
            aliasServiceMock.Setup(x => x.GetAliasesAsync()).ReturnsAsync(response);

            // Mock the AliasService creation
            var taskCompletionSource = new TaskCompletionSource<AliasListResponse>();
            taskCompletionSource.SetResult(response);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasListResponse>("firewall/alias/searchItem"))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.IsInstanceOfType(results[0], typeof(Alias));

            var alias = results[0] as Alias;

            Assert.AreEqual("WebServers", alias.Name);
            Assert.AreEqual("host", alias.Type);
        }

        [TestMethod]
        public void ProcessRecord_GetAliasByType_WritesFilteredAliasesToPipeline()
        {
            // Arrange
            string type = "host";
            _cmdlet.Type = type;

            var aliases = new List<Alias>
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
            };

            var response = new AliasListResponse
            {
                Total = aliases.Count,
                RowCount = aliases.Count,
                Current = 1,
                Rows = aliases
            };

            var aliasServiceMock = new Mock<AliasService>(_apiClientMock.Object, _loggerMock.Object);
            aliasServiceMock.Setup(x => x.GetAliasesAsync()).ReturnsAsync(response);

            // Mock the AliasService creation
            var taskCompletionSource = new TaskCompletionSource<AliasListResponse>();
            taskCompletionSource.SetResult(response);
            var task = taskCompletionSource.Task;

            _apiClientMock.Setup(x => x.GetAsync<AliasListResponse>("firewall/alias/searchItem"))
                .Returns(task);

            // Create a PowerShell pipeline to capture output
            var results = new List<object>();
            _cmdlet.CommandRuntime = new MockCommandRuntime(output => results.Add(output));

            // Act
            _cmdlet.ProcessRecord();

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.IsInstanceOfType(results[0], typeof(Alias));

            var alias = results[0] as Alias;

            Assert.AreEqual("WebServers", alias.Name);
            Assert.AreEqual("host", alias.Type);
        }
    }

    // Helper class for testing cmdlets
    public class MockCommandRuntime : ICommandRuntime
    {
        private readonly Action<object> _outputHandler;
        private readonly Action<string> _warningHandler;
        private readonly Action<string> _verboseHandler;
        private readonly bool _shouldProcessResult;

        public MockCommandRuntime(Action<object> outputHandler, Action<string> warningHandler = null, Action<string> verboseHandler = null, bool shouldProcessResult = true)
        {
            _outputHandler = outputHandler;
            _warningHandler = warningHandler ?? (_ => { });
            _verboseHandler = verboseHandler ?? (_ => { });
            _shouldProcessResult = shouldProcessResult;
        }

        public PSHost Host => throw new NotImplementedException();

        public PSTransactionContext CurrentPSTransaction => throw new NotImplementedException();

        public virtual bool ShouldProcess(string target) => _shouldProcessResult;

        public virtual bool ShouldProcess(string target, string action) => _shouldProcessResult;

        public virtual bool ShouldProcess(string verboseDescription, string verboseWarning, string caption) => _shouldProcessResult;

        public virtual bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out ShouldProcessReason shouldProcessReason)
        {
            shouldProcessReason = ShouldProcessReason.None;
            return _shouldProcessResult;
        }

        public bool ShouldContinue(string query, string caption) => true;

        public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll) => true;

        public void WriteCommandDetail(string text) { }

        public void WriteDebug(string text) { }

        public void WriteError(ErrorRecord errorRecord) { throw errorRecord.Exception; }

        public void WriteObject(object sendToPipeline) => _outputHandler(sendToPipeline);

        public void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            if (enumerateCollection && sendToPipeline is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    _outputHandler(item);
                }
            }
            else
            {
                _outputHandler(sendToPipeline);
            }
        }

        public void WriteProgress(ProgressRecord progressRecord) { }

        public void WriteProgress(long sourceId, ProgressRecord progressRecord) { }

        public void WriteVerbose(string text) => _verboseHandler(text);

        public void WriteWarning(string text) => _warningHandler(text);
    }
}
