using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a port forwarding rule from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSensePortForwardingRule cmdlet removes a port forwarding rule from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Remove a port forwarding rule</para>
    ///     <code>Remove-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSensePortForwardingRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveOPNSensePortForwardingRuleCmdlet : OPNSenseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the port forwarding rule to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            var portForwardingService = new PortForwardingService(SessionState.ApiClient);

            // Get the rule to display information in the confirmation message
            var rule = Task.Run(async () => await portForwardingService.GetPortForwardingRuleAsync(Uuid)).GetAwaiter().GetResult();

            if (rule == null)
            {
                WriteWarning($"Port forwarding rule with UUID '{Uuid}' not found.");
                return;
            }

            string confirmationMessage = $"Remove port forwarding rule";
            if (!string.IsNullOrEmpty(rule.Description))
            {
                confirmationMessage += $" '{rule.Description}'";
            }
            confirmationMessage += $" with UUID '{Uuid}'";

            if (Force || ShouldProcess($"OPNSense firewall", confirmationMessage))
            {
                var success = Task.Run(async () => await portForwardingService.DeletePortForwardingRuleAsync(Uuid)).GetAwaiter().GetResult();

                if (success)
                {
                    WriteVerbose($"Port forwarding rule with UUID '{Uuid}' removed successfully.");
                }
                else
                {
                    WriteError(new ErrorRecord(
                        new PSInvalidOperationException($"Failed to remove port forwarding rule with UUID '{Uuid}'."),
                        "PortForwardingRuleRemovalFailed",
                        ErrorCategory.InvalidOperation,
                        Uuid));
                }
            }
        }
    }
}
