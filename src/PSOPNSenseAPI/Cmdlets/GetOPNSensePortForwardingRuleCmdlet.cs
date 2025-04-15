using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets port forwarding rules from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSensePortForwardingRule cmdlet gets port forwarding rules from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Get all port forwarding rules</para>
    ///     <code>Get-OPNSensePortForwardingRule</code>
    /// </example>
    /// <example>
    ///     <para>Get a specific port forwarding rule by UUID</para>
    ///     <code>Get-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSensePortForwardingRule")]
    [OutputType(typeof(PortForwardingRule))]
    public class GetOPNSensePortForwardingRuleCmdlet : OPNSenseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the port forwarding rule to get. If not specified, all rules are returned.</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = false)]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            var portForwardingService = new PortForwardingService(SessionState.ApiClient);

            if (!string.IsNullOrEmpty(Uuid))
            {
                // Get a specific rule
                var rule = Task.Run(async () => await portForwardingService.GetPortForwardingRuleAsync(Uuid)).GetAwaiter().GetResult();
                if (rule != null)
                {
                    WriteObject(rule);
                }
                else
                {
                    WriteWarning($"Port forwarding rule with UUID '{Uuid}' not found.");
                }
            }
            else
            {
                // Get all rules
                var rules = Task.Run(async () => await portForwardingService.GetPortForwardingRulesAsync()).GetAwaiter().GetResult();
                WriteObject(rules, true);
            }
        }
    }
}
