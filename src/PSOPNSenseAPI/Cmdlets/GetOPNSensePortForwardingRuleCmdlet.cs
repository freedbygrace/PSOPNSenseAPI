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
    public class GetOPNSensePortForwardingRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the port forwarding rule to get. If not specified, all rules are returned.</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = false)]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var portForwardingService = new PortForwardingService(ApiClient);

            if (!string.IsNullOrEmpty(Uuid))
            {
                // Use our safe execution method
                var rule = ExecuteAsyncTask(() => portForwardingService.GetPortForwardingRuleAsync(Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null)
                {
                    return;
                }

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
                // Use our safe execution method
                var rules = ExecuteAsyncTask(() => portForwardingService.GetPortForwardingRulesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || rules == null)
                {
                    return;
                }

                WriteObject(rules, true);
            }
        }
    }
}
