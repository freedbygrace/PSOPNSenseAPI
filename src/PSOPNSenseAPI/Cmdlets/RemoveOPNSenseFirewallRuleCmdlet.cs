using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a firewall rule from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseFirewallRule cmdlet removes a firewall rule from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a firewall rule</para>
    ///     <code>Remove-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a firewall rule by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a firewall rule with confirmation</para>
    ///     <code>Remove-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a firewall rule by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseFirewallRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the firewall rule to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

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
            var firewallService = new FirewallService(ApiClient, Logger);

            // Get the rule details for the confirmation message
            var getResult = ExecuteAsyncTask(() => firewallService.GetRuleAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var rule = getResult.Rule;

            string confirmMessage = $"Firewall rule: {rule.Description}";
            if (!string.IsNullOrEmpty(rule.Protocol) && rule.Protocol != "any")
            {
                confirmMessage += $" ({rule.Protocol})";
            }

            if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
            {
                return;
            }

            var deleteResult = ExecuteAsyncTask(() => firewallService.DeleteRuleAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || deleteResult == null)
            {
                return;
            }

            WriteVerbose($"Firewall rule {Uuid} removed: {deleteResult.Result}");
        }
    }
}
