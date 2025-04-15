using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a gateway on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseGateway cmdlet updates a gateway on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a gateway's monitor IP</para>
    ///     <code>Set-OPNSenseGateway -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -MonitorIp "8.8.8.8" -Apply</code>
    ///     <para>This example updates the monitor IP of a gateway.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Make a gateway the default gateway</para>
    ///     <code>Set-OPNSenseGateway -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Default -Apply</code>
    ///     <para>This example makes a gateway the default gateway.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseGateway")]
    [OutputType(typeof(void))]
    public class SetOPNSenseGatewayCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the gateway to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The name of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The interface of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The IP address of the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
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
        /// <para type="description">Whether the gateway is not the default gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter NotDefault { get; set; }

        /// <summary>
        /// <para type="description">Whether the gateway is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

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
        public string IpProtocol { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable monitoring for the gateway.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableMonitoring { get; set; }

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
        /// <para type="description">Whether to not force the gateway down.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter NoForceDown { get; set; }

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

                // Get current gateway
                var getTask = Task.Run(async () => await gatewayService.GetGatewayAsync(Uuid));
                var currentGateway = getTask.GetAwaiter().GetResult().Gateway;

                // Create updated gateway
                var gateway = new GatewayConfig
                {
                    Name = Name ?? currentGateway.Name,
                    Interface = Interface ?? currentGateway.Interface,
                    IpAddress = IpAddress ?? currentGateway.IpAddress,
                    MonitorIp = MonitorIp ?? currentGateway.MonitorIp,
                    Description = Description ?? currentGateway.Description,
                    Weight = Weight?.ToString() ?? currentGateway.Weight,
                    IpProtocol = IpProtocol ?? currentGateway.IpProtocol
                };

                // Handle default/not default
                if (Default.IsPresent && NotDefault.IsPresent)
                {
                    WriteWarning("Both -Default and -NotDefault parameters were specified. Using -Default.");
                    gateway.IsDefault = "1";
                }
                else if (Default.IsPresent)
                {
                    gateway.IsDefault = "1";
                }
                else if (NotDefault.IsPresent)
                {
                    gateway.IsDefault = "0";
                }
                else
                {
                    gateway.IsDefault = currentGateway.IsDefault;
                }

                // Handle enabled/disabled
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    gateway.Disabled = "0";
                }
                else if (Enabled.IsPresent)
                {
                    gateway.Disabled = "0";
                }
                else if (Disabled.IsPresent)
                {
                    gateway.Disabled = "1";
                }
                else
                {
                    gateway.Disabled = currentGateway.Disabled;
                }

                // Handle monitoring
                if (EnableMonitoring.IsPresent && DisableMonitoring.IsPresent)
                {
                    WriteWarning("Both -EnableMonitoring and -DisableMonitoring parameters were specified. Using -EnableMonitoring.");
                    gateway.MonitorDisable = "0";
                }
                else if (EnableMonitoring.IsPresent)
                {
                    gateway.MonitorDisable = "0";
                }
                else if (DisableMonitoring.IsPresent)
                {
                    gateway.MonitorDisable = "1";
                }
                else
                {
                    gateway.MonitorDisable = currentGateway.MonitorDisable;
                }

                // Handle force down
                if (ForceDown.IsPresent && NoForceDown.IsPresent)
                {
                    WriteWarning("Both -ForceDown and -NoForceDown parameters were specified. Using -ForceDown.");
                    gateway.ForceDown = "1";
                }
                else if (ForceDown.IsPresent)
                {
                    gateway.ForceDown = "1";
                }
                else if (NoForceDown.IsPresent)
                {
                    gateway.ForceDown = "0";
                }
                else
                {
                    gateway.ForceDown = currentGateway.ForceDown;
                }

                // Update gateway
                var updateTask = Task.Run(async () => await gatewayService.UpdateGatewayAsync(Uuid, gateway));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Gateway {Uuid} updated: {updateResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await gatewayService.ApplyGatewayChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Gateway changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
