using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new port forwarding rule on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSensePortForwardingRule cmdlet creates a new port forwarding rule on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Create a new port forwarding rule</para>
    ///     <code>New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server"</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSensePortForwardingRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(PortForwardingRule))]
    public class NewOPNSensePortForwardingRuleCmdlet : OPNSenseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether the rule is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; } = true;

        /// <summary>
        /// <para type="description">The description of the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The interface for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The TCP/IP protocol (tcp, udp, tcp/udp).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("tcp", "udp", "tcp/udp")]
        public string Protocol { get; set; } = "tcp";

        /// <summary>
        /// <para type="description">The source address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Source { get; set; } = "any";

        /// <summary>
        /// <para type="description">The source port range for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SourcePort { get; set; } = "any";

        /// <summary>
        /// <para type="description">The destination address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Destination { get; set; } = "wanip";

        /// <summary>
        /// <para type="description">The destination port range for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string DestinationPort { get; set; }

        /// <summary>
        /// <para type="description">The target IP address for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string TargetIP { get; set; }

        /// <summary>
        /// <para type="description">The target port for the rule.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string TargetPort { get; set; }

        /// <summary>
        /// <para type="description">The NAT reflection mode (enable, disable, purenat).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("enable", "disable", "purenat")]
        public string NatReflection { get; set; } = "enable";

        /// <summary>
        /// <para type="description">Whether to create an associated filter rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter FilterRuleAssociation { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to enable logging for this rule.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Log { get; set; } = false;

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
            var rule = new PortForwardingRule
            {
                Enabled = Enabled,
                Description = Description,
                Interface = Interface,
                Protocol = Protocol,
                Source = Source,
                SourcePort = SourcePort,
                Destination = Destination,
                DestinationPort = DestinationPort,
                TargetIP = TargetIP,
                TargetPort = TargetPort,
                NatReflection = NatReflection,
                FilterRuleAssociation = FilterRuleAssociation,
                Log = Log
            };

            if (Force || ShouldProcess($"OPNSense firewall", $"Create port forwarding rule from {Destination}:{DestinationPort} to {TargetIP}:{TargetPort}"))
            {
                var portForwardingService = new PortForwardingService(SessionState.ApiClient);
                var uuid = Task.Run(async () => await portForwardingService.CreatePortForwardingRuleAsync(rule)).GetAwaiter().GetResult();

                if (!string.IsNullOrEmpty(uuid))
                {
                    rule.Uuid = uuid;
                    WriteObject(rule);
                    WriteVerbose($"Port forwarding rule created successfully with UUID: {uuid}");
                }
                else
                {
                    WriteError(new ErrorRecord(
                        new PSInvalidOperationException("Failed to create port forwarding rule."),
                        "PortForwardingRuleCreationFailed",
                        ErrorCategory.InvalidOperation,
                        null));
                }
            }
        }
    }
}
