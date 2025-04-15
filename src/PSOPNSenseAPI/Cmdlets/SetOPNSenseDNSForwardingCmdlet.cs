using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates the DNS forwarding configuration on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDNSForwarding cmdlet updates the DNS forwarding configuration on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Enable DNS forwarding</para>
    ///     <code>Set-OPNSenseDNSForwarding -Enabled -DnsServers "8.8.8.8","8.8.4.4" -Apply</code>
    ///     <para>This example enables DNS forwarding and sets the DNS servers to Google's public DNS servers.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Disable DNS forwarding</para>
    ///     <code>Set-OPNSenseDNSForwarding -Disabled -Apply</code>
    ///     <para>This example disables DNS forwarding.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDNSForwarding")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDNSForwardingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether DNS forwarding is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether DNS forwarding is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// <para type="description">The type of DNS forwarding.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("forward", "redirect", "forward_domain")]
        public string Type { get; set; }

        /// <summary>
        /// <para type="description">The DNS servers to forward to.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] DnsServers { get; set; }

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
                var dnsService = new DNSService(ApiClient, Logger);

                // Get current configuration
                var getTask = Task.Run(async () => await dnsService.GetDNSForwardingAsync());
                var currentConfig = getTask.GetAwaiter().GetResult().Forward;

                // Create updated configuration
                var config = new DNSForwardingConfig
                {
                    Type = Type ?? currentConfig.Type
                };

                // Handle enabled/disabled state
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    config.Enabled = "1";
                }
                else if (Enabled.IsPresent)
                {
                    config.Enabled = "1";
                }
                else if (Disabled.IsPresent)
                {
                    config.Enabled = "0";
                }
                else
                {
                    config.Enabled = currentConfig.Enabled;
                }

                // Handle DNS servers
                if (DnsServers != null && DnsServers.Length > 0)
                {
                    config.DnsServers = new List<string>(DnsServers);
                }
                else
                {
                    config.DnsServers = currentConfig.DnsServers;
                }

                // Update configuration
                var updateTask = Task.Run(async () => await dnsService.UpdateDNSForwardingAsync(config));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"DNS forwarding configuration updated: {updateResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await dnsService.ApplyDNSChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"DNS changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
