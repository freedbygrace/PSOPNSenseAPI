using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Enables a firewall rule on an OPNSense firewall.</para>
    /// <para type="description">The Enable-OPNSenseFirewallRule cmdlet enables a firewall rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Enable a firewall rule</para>
    ///     <code>Enable-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example enables a firewall rule by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Enable, "OPNSenseFirewallRule")]
    [OutputType(typeof(void))]
    public class EnableOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the firewall rule to enable.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var firewallService = new FirewallService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => firewallService.ToggleRuleAsync(Uuid, true));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Firewall rule {Uuid} enabled: {result.Result}");
        }
    }
}
