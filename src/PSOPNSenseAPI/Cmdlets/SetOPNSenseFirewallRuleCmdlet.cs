using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a firewall rule on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseFirewallRule cmdlet updates a firewall rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a firewall rule description</para>
    ///     <code>Set-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Description "Updated rule description"</code>
    ///     <para>This example updates the description of a firewall rule.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a firewall rule protocol and ports</para>
    ///     <code>Set-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Protocol TCP -DestinationPort 443</code>
    ///     <para>This example updates the protocol and destination port of a firewall rule.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseFirewallRule")]
    [OutputType(typeof(void))]
    public class SetOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the firewall rule to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The description of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The protocol of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("any", "TCP", "UDP", "ICMP", "ESP", "AH", "GRE", "IGMP", "PIM", "OSPF")]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">The source network of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourceNet { get; set; }

        /// <summary>
        /// <para type="description">The source port of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourcePort { get; set; }

        /// <summary>
        /// <para type="description">The destination network of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DestinationNet { get; set; }

        /// <summary>
        /// <para type="description">The destination port of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DestinationPort { get; set; }

        /// <summary>
        /// <para type="description">The action of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("pass", "block", "reject")]
        public string Action { get; set; }

        /// <summary>
        /// <para type="description">The sequence of the firewall rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Sequence { get; set; }

        /// <summary>
        /// <para type="description">Whether the firewall rule is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the firewall rule is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var firewallService = new FirewallService(ApiClient, Logger);

                // First, get the current rule
                var getTask = Task.Run(async () => await firewallService.GetRuleAsync(Uuid));
                var currentRule = getTask.GetAwaiter().GetResult().Rule;

                // Create the updated rule
                var rule = new FirewallRuleCreate
                {
                    Description = Description ?? currentRule.Description,
                    Protocol = Protocol ?? currentRule.Protocol,
                    SourceNet = SourceNet ?? currentRule.SourceNet,
                    SourcePort = SourcePort ?? currentRule.SourcePort,
                    DestinationNet = DestinationNet ?? currentRule.DestinationNet,
                    DestinationPort = DestinationPort ?? currentRule.DestinationPort,
                    Action = Action ?? currentRule.Action,
                    Sequence = Sequence ?? currentRule.Sequence
                };

                // Handle enabled/disabled state
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    rule.Enabled = "1";
                }
                else if (Enabled.IsPresent)
                {
                    rule.Enabled = "1";
                }
                else if (Disabled.IsPresent)
                {
                    rule.Enabled = "0";
                }
                else
                {
                    rule.Enabled = currentRule.Enabled;
                }

                // Update the rule
                var updateTask = Task.Run(async () => await firewallService.UpdateRuleAsync(Uuid, rule));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Firewall rule {Uuid} updated: {updateResult.Result}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
