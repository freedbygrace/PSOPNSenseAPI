using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new gateway on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseGateway cmdlet creates a new gateway on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new gateway</para>
    ///     <code>New-OPNSenseGateway -Name "WAN2" -Interface "opt1" -IpAddress "203.0.113.1" -Description "Secondary WAN" -Apply</code>
    ///     <para>This example creates a new gateway for a secondary WAN interface.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create a new default gateway</para>
    ///     <code>New-OPNSenseGateway -Name "WAN_MAIN" -Interface "wan" -IpAddress "203.0.113.1" -Description "Main WAN" -Default -Apply</code>
    ///     <para>This example creates a new default gateway for the WAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseGateway")]
    [OutputType(typeof(string))]
    public class NewOPNSenseGatewayCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The interface of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The IP address of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string IpAddress { get; set; }

        /// <summary>
        /// <para type="description">The monitor IP of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string MonitorIp { get; set; }

        /// <summary>
        /// <para type="description">The description of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Whether the gateway is the default gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// <para type="description">Whether the gateway is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// <para type="description">The weight of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 30)]
        public int? Weight { get; set; }

        /// <summary>
        /// <para type="description">The IP protocol of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("inet", "inet6")]
        public string IpProtocol { get; set; } = "inet";

        /// <summary>
        /// <para type="description">Whether to disable monitoring for the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter DisableMonitoring { get; set; }

        /// <summary>
        /// <para type="description">Whether to force the gateway down.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter ForceDown { get; set; }

        /// <summary>
        /// <para type="description">Whether to apply the changes immediately.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Apply { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var gatewayService = new GatewayService(ApiClient, Logger);

                var gateway = new GatewayConfig
                {
                    Name = Name,
                    Interface = Interface,
                    IpAddress = IpAddress,
                    MonitorIp = MonitorIp ?? IpAddress,
                    Description = Description,
                    IsDefault = Default.IsPresent ? "1" : "0",
                    Disabled = Disabled.IsPresent ? "1" : "0",
                    Weight = Weight?.ToString() ?? "1",
                    IpProtocol = IpProtocol,
                    MonitorDisable = DisableMonitoring.IsPresent ? "1" : "0",
                    ForceDown = ForceDown.IsPresent ? "1" : "0"
                };

                var createTask = Task.Run(async () => await gatewayService.CreateGatewayAsync(gateway));
                var createResult = createTask.GetAwaiter().GetResult();

                WriteVerbose($"Created gateway with UUID {createResult.Uuid}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await gatewayService.ApplyGatewayChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Gateway changes applied: {applyResult.Status}");
                }

                WriteObject(createResult.Uuid);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
