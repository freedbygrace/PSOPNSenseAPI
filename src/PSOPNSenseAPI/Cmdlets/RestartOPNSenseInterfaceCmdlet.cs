using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Restarts an interface on an OPNSense firewall.</para>
    /// <para type="description">The Restart-OPNSenseInterface cmdlet restarts an interface on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Restart an interface</para>
    ///     <code>Restart-OPNSenseInterface -Name "lan"</code>
    ///     <para>This example restarts the LAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Restart, "OPNSenseInterface", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RestartOPNSenseInterfaceCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the interface to restart.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            if (!Force.IsPresent && !ShouldProcess(Name, "Restart interface"))
            {
                return;
            }

            var interfaceService = new InterfaceService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => interfaceService.RestartInterfaceAsync(Name));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Interface {Name} restarted: {result.Status}");
        }
    }
}
