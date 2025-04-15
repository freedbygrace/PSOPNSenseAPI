using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disables a plugin on an OPNSense firewall.</para>
    /// <para type="description">The Disable-OPNSensePlugin cmdlet disables a plugin on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Disable a plugin</para>
    ///     <code>Disable-OPNSensePlugin -Name "os-acme-client"</code>
    ///     <para>This example disables the ACME client plugin on the OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Disable, "OPNSensePlugin")]
    [OutputType(typeof(void))]
    public class DisableOPNSensePluginCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the plugin to disable.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var pluginService = new PluginService(ApiClient, Logger);

                var task = Task.Run(async () => await pluginService.DisablePluginAsync(Name));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Plugin {Name} disabled: {result.Status}");
                WriteObject($"Plugin {Name} disabled: {result.Status}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
