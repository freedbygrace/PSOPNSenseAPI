using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets gateways from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseGateway cmdlet retrieves gateways from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all gateways</para>
    ///     <code>Get-OPNSenseGateway</code>
    ///     <para>This example retrieves all gateways from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific gateway by UUID</para>
    ///     <code>Get-OPNSenseGateway -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific gateway by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseGateway")]
    [OutputType(typeof(Gateway), typeof(GatewayDetail))]
    public class GetOPNSenseGatewayCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the gateway to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">Whether to include status information for the gateways.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeStatus { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var gatewayService = new GatewayService(ApiClient, Logger);

                if (ParameterSetName == "ByUuid")
                {
                    var task = Task.Run(async () => await gatewayService.GetGatewayAsync(Uuid));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Gateway);
                }
                else
                {
                    var task = Task.Run(async () => await gatewayService.GetGatewaysAsync());
                    var result = task.GetAwaiter().GetResult();

                    if (IncludeStatus.IsPresent)
                    {
                        var statusTask = Task.Run(async () => await gatewayService.GetGatewayStatusAsync());
                        var statusResult = statusTask.GetAwaiter().GetResult();

                        foreach (var gateway in result.Rows)
                        {
                            var gatewayWithStatus = new PSObject();
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("Uuid", gateway.Uuid));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("Name", gateway.Name));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("Interface", gateway.Interface));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("IpAddress", gateway.IpAddress));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("MonitorIp", gateway.MonitorIp));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("Description", gateway.Description));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("IsDefault", gateway.IsDefault == "1"));
                            gatewayWithStatus.Properties.Add(new PSNoteProperty("Disabled", gateway.Disabled == "1"));

                            if (statusResult.Items.ContainsKey(gateway.Name))
                            {
                                var status = statusResult.Items[gateway.Name];
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("Status", status.Status));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("RTT", status.RTT));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("StdDev", status.StdDev));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("Loss", status.Loss));
                            }
                            else
                            {
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("Status", "Unknown"));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("RTT", "N/A"));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("StdDev", "N/A"));
                                gatewayWithStatus.Properties.Add(new PSNoteProperty("Loss", "N/A"));
                            }

                            WriteObject(gatewayWithStatus);
                        }
                    }
                    else
                    {
                        WriteObject(result.Rows, true);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
