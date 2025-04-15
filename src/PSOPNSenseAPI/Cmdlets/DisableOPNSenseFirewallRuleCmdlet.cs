using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disables a firewall rule on an OPNSense firewall.</para>
    /// <para type="description">The Disable-OPNSenseFirewallRule cmdlet disables a firewall rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Disable a firewall rule</para>
    ///     <code>Disable-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example disables a firewall rule by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Disable, "OPNSenseFirewallRule")]
    [OutputType(typeof(void))]
    public class DisableOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the firewall rule to disable.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var firewallService = new FirewallService(ApiClient, Logger);

                var task = Task.Run(async () => await firewallService.ToggleRuleAsync(Uuid, false));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Firewall rule {Uuid} disabled: {result.Result}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
