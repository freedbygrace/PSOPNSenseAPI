using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates an existing port forwarding rule on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSensePortForwardingRule cmdlet updates an existing port forwarding rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Update a port forwarding rule</para>
    ///     <code>Set-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -TargetIP "192.168.1.200" -Description "Updated Web Server"</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSensePortForwardingRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(PortForwardingRule))]
    public class SetOPNSensePortForwardingRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the port forwarding rule to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">Whether the rule is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">The description of the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The interface for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The TCP/IP protocol (tcp, udp, tcp/udp).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("tcp", "udp", "tcp/udp")]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">The source address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Source { get; set; }

        /// <summary>
        /// <para type="description">The source port range for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourcePort { get; set; }

        /// <summary>
        /// <para type="description">The destination address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Destination { get; set; }

        /// <summary>
        /// <para type="description">The destination port range for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DestinationPort { get; set; }

        /// <summary>
        /// <para type="description">The target IP address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string TargetIP { get; set; }

        /// <summary>
        /// <para type="description">The target port for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string TargetPort { get; set; }

        /// <summary>
        /// <para type="description">The NAT reflection mode (enable, disable, purenat).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("enable", "disable", "purenat")]
        public string NatReflection { get; set; }

        /// <summary>
        /// <para type="description">Whether to create an associated filter rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter FilterRuleAssociation { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable logging for this rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Log { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Returns the updated rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var portForwardingService = new PortForwardingService(ApiClient);

            // Get the existing rule
            var existingRule = ExecuteAsyncTask(() => portForwardingService.GetPortForwardingRuleAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null)
            {
                return;
            }

            if (existingRule == null)
            {
                ProcessingException = new PSArgumentException($"Port forwarding rule with UUID '{Uuid}' not found.");
                WriteWarning($"Port forwarding rule with UUID '{Uuid}' not found.");
                return;
            }

            // Update the rule with the provided parameters
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Enabled)))
                existingRule.Enabled = Enabled;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                existingRule.Description = Description;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Interface)))
                existingRule.Interface = Interface;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Protocol)))
                existingRule.Protocol = Protocol;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Source)))
                existingRule.Source = Source;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(SourcePort)))
                existingRule.SourcePort = SourcePort;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Destination)))
                existingRule.Destination = Destination;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DestinationPort)))
                existingRule.DestinationPort = DestinationPort;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(TargetIP)))
                existingRule.TargetIP = TargetIP;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(TargetPort)))
                existingRule.TargetPort = TargetPort;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(NatReflection)))
                existingRule.NatReflection = NatReflection;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(FilterRuleAssociation)))
                existingRule.FilterRuleAssociation = FilterRuleAssociation;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Log)))
                existingRule.Log = Log;

            if (Force || ShouldProcess($"OPNSense firewall", $"Update port forwarding rule with UUID '{Uuid}'"))
            {
                var success = ExecuteAsyncTask(() => portForwardingService.UpdatePortForwardingRuleAsync(existingRule));

                // Only continue if no exception occurred
                if (ProcessingException != null)
                {
                    return;
                }

                if (success)
                {
                    WriteVerbose($"Port forwarding rule with UUID '{Uuid}' updated successfully.");

                    if (PassThru)
                    {
                        WriteObject(existingRule);
                    }
                }
                else
                {
                    ProcessingException = new PSInvalidOperationException($"Failed to update port forwarding rule with UUID '{Uuid}'.");
                    WriteWarning($"Failed to update port forwarding rule with UUID '{Uuid}'.");
                }
            }
        }
    }
}
