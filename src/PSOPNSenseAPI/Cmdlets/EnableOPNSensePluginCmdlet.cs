using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Enables a plugin on an OPNSense firewall.</para>
    /// <para type="description">The Enable-OPNSensePlugin cmdlet enables a plugin on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Enable a plugin</para>
    ///     <code>Enable-OPNSensePlugin -Name "os-acme-client"</code>
    ///     <para>This example enables the ACME client plugin on the OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Enable, "OPNSensePlugin")]
    [OutputType(typeof(void))]
    public class EnableOPNSensePluginCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the plugin to enable.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var pluginService = new PluginService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => pluginService.EnablePluginAsync(Name));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Plugin {Name} enabled: {result.Status}");
            WriteObject($"Plugin {Name} enabled: {result.Status}");
        }
    }
}
