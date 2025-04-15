using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new firewall rule on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseFirewallRule cmdlet creates a new firewall rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a simple firewall rule</para>
    ///     <code>New-OPNSenseFirewallRule -Description "Allow HTTP" -Protocol TCP -DestinationPort 80 -Action pass</code>
    ///     <para>This example creates a new firewall rule that allows HTTP traffic.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create a more specific firewall rule</para>
    ///     <code>New-OPNSenseFirewallRule -Description "Allow HTTP from LAN" -Protocol TCP -SourceNet "192.168.1.0/24" -DestinationPort 80 -Action pass</code>
    ///     <para>This example creates a new firewall rule that allows HTTP traffic from the LAN network.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseFirewallRule")]
    [OutputType(typeof(string))]
    public class NewOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The description of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The protocol of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("any", "TCP", "UDP", "ICMP", "ESP", "AH", "GRE", "IGMP", "PIM", "OSPF")]
        public string Protocol { get; set; } = "any";

        /// <summary>
        /// <para type="description">The source network of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourceNet { get; set; } = "any";

        /// <summary>
        /// <para type="description">The source port of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourcePort { get; set; } = "any";

        /// <summary>
        /// <para type="description">The destination network of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DestinationNet { get; set; } = "any";

        /// <summary>
        /// <para type="description">The destination port of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DestinationPort { get; set; } = "any";

        /// <summary>
        /// <para type="description">The action of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("pass", "block", "reject")]
        public string Action { get; set; } = "pass";

        /// <summary>
        /// <para type="description">The sequence of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Sequence { get; set; }

        /// <summary>
        /// <para type="description">Whether the firewall rule is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; } = true;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var firewallService = new FirewallService(ApiClient, Logger);

            var rule = new FirewallRuleCreate
            {
                Description = Description,
                Protocol = Protocol,
                SourceNet = SourceNet,
                SourcePort = SourcePort,
                DestinationNet = DestinationNet,
                DestinationPort = DestinationPort,
                Action = Action,
                Sequence = Sequence,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => firewallService.CreateRuleAsync(rule));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Created firewall rule with UUID {result.Uuid}");
            WriteObject(result.Uuid);
        }
    }
}
