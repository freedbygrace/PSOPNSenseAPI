using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates the system DNS configuration on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseSystemDNS cmdlet updates the system DNS configuration on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update system DNS servers</para>
    ///     <code>Set-OPNSenseSystemDNS -DnsServers "8.8.8.8","8.8.4.4" -Apply</code>
    ///     <para>This example updates the system DNS servers to Google's public DNS servers.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update system hostname and domain</para>
    ///     <code>Set-OPNSenseSystemDNS -Hostname "firewall" -Domain "example.com" -Apply</code>
    ///     <para>This example updates the system hostname and domain.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseSystemDNS")]
    [OutputType(typeof(void))]
    public class SetOPNSenseSystemDNSCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The hostname of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Hostname { get; set; }

        /// <summary>
        /// <para type="description">The domain of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">The DNS servers of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] DnsServers { get; set; }

        /// <summary>
        /// <para type="description">Whether to allow DNS server override by DHCP/PPP.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AllowDnsOverride { get; set; }

        /// <summary>
        /// <para type="description">Whether to disallow DNS server override by DHCP/PPP.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter DisallowDnsOverride { get; set; }

        /// <summary>
        /// <para type="description">Whether to disable DNS rebinding protection.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter DisableDnsRebindProtection { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable DNS rebinding protection.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableDnsRebindProtection { get; set; }

        /// <summary>
        /// <para type="description">The timezone of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Timezone { get; set; }

        /// <summary>
        /// <para type="description">The time servers of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string TimeServers { get; set; }

        /// <summary>
        /// <para type="description">The language of the system.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Language { get; set; }

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
                var systemDNSService = new SystemDNSService(ApiClient, Logger);

                // Get current configuration
                var getTask = Task.Run(async () => await systemDNSService.GetSystemDNSAsync());
                var currentConfig = getTask.GetAwaiter().GetResult().System;

                // Create updated configuration
                var config = new SystemDNSConfig
                {
                    Hostname = Hostname ?? currentConfig.Hostname,
                    Domain = Domain ?? currentConfig.Domain,
                    Timezone = Timezone ?? currentConfig.Timezone,
                    TimeServers = TimeServers ?? currentConfig.TimeServers,
                    Language = Language ?? currentConfig.Language
                };

                // Handle DNS servers
                if (DnsServers != null && DnsServers.Length > 0)
                {
                    config.DnsServers = new List<string>(DnsServers);
                }
                else
                {
                    config.DnsServers = currentConfig.DnsServers;
                }

                // Handle DNS override
                if (AllowDnsOverride.IsPresent && DisallowDnsOverride.IsPresent)
                {
                    WriteWarning("Both -AllowDnsOverride and -DisallowDnsOverride parameters were specified. Using -AllowDnsOverride.");
                    config.DnsAllowOverride = "1";
                }
                else if (AllowDnsOverride.IsPresent)
                {
                    config.DnsAllowOverride = "1";
                }
                else if (DisallowDnsOverride.IsPresent)
                {
                    config.DnsAllowOverride = "0";
                }
                else
                {
                    config.DnsAllowOverride = currentConfig.DnsAllowOverride;
                }

                // Handle DNS rebind protection
                if (DisableDnsRebindProtection.IsPresent && EnableDnsRebindProtection.IsPresent)
                {
                    WriteWarning("Both -DisableDnsRebindProtection and -EnableDnsRebindProtection parameters were specified. Using -DisableDnsRebindProtection.");
                    config.DnssecStripped = "1";
                }
                else if (DisableDnsRebindProtection.IsPresent)
                {
                    config.DnssecStripped = "1";
                }
                else if (EnableDnsRebindProtection.IsPresent)
                {
                    config.DnssecStripped = "0";
                }
                else
                {
                    config.DnssecStripped = currentConfig.DnssecStripped;
                }

                // Update configuration
                var updateTask = Task.Run(async () => await systemDNSService.UpdateSystemDNSAsync(config));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"System DNS configuration updated: {updateResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await systemDNSService.ApplySystemDNSChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"System DNS changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
