using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates DNS server configuration on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDNSServer cmdlet updates DNS server configuration on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Enable DNS forwarding</para>
    ///     <code>Set-OPNSenseDNSServer -Forwarding -Forwarders "8.8.8.8","8.8.4.4"</code>
    ///     <para>This example enables DNS forwarding and sets Google DNS servers as forwarders.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update DNS server port</para>
    ///     <code>Set-OPNSenseDNSServer -Port 5353</code>
    ///     <para>This example changes the DNS server port to 5353.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDNSServer")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDNSServerCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether the DNS server is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">The DNS server port.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 65535)]
        public int? Port { get; set; }

        /// <summary>
        /// <para type="description">The interfaces to listen on.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] Interfaces { get; set; }

        /// <summary>
        /// <para type="description">Whether DNS forwarding is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Forwarding { get; set; }

        /// <summary>
        /// <para type="description">The DNS forwarders to use.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] Forwarders { get; set; }

        /// <summary>
        /// <para type="description">Whether to register DHCP leases.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter RegisterDhcp { get; set; }

        /// <summary>
        /// <para type="description">The domain to use for DHCP registrations.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string RegisterDhcpDomain { get; set; }

        /// <summary>
        /// <para type="description">Whether to register static DHCP leases.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter RegisterDhcpStatic { get; set; }

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

                // First, get the current DNS configuration
                var getTask = Task.Run(async () => await dnsService.GetDNSConfigAsync());
                var currentConfig = getTask.GetAwaiter().GetResult().Unbound;

                // Create the updated configuration
                var dnsConfig = new DNSConfig
                {
                    Enabled = MyInvocation.BoundParameters.ContainsKey(nameof(Enabled)) ? (Enabled.IsPresent ? "1" : "0") : currentConfig.Enabled,
                    Port = Port?.ToString() ?? currentConfig.Port,
                    Interfaces = Interfaces != null ? new List<string>(Interfaces) : currentConfig.Interfaces,
                    Forwarding = MyInvocation.BoundParameters.ContainsKey(nameof(Forwarding)) ? (Forwarding.IsPresent ? "1" : "0") : currentConfig.Forwarding,
                    Forwarders = Forwarders != null ? new List<string>(Forwarders) : currentConfig.Forwarders,
                    RegisterDhcp = MyInvocation.BoundParameters.ContainsKey(nameof(RegisterDhcp)) ? (RegisterDhcp.IsPresent ? "1" : "0") : currentConfig.RegisterDhcp,
                    RegisterDhcpDomain = RegisterDhcpDomain ?? currentConfig.RegisterDhcpDomain,
                    RegisterDhcpStatic = MyInvocation.BoundParameters.ContainsKey(nameof(RegisterDhcpStatic)) ? (RegisterDhcpStatic.IsPresent ? "1" : "0") : currentConfig.RegisterDhcpStatic,
                    ActiveInterfaces = currentConfig.ActiveInterfaces
                };

                // Update the DNS configuration
                var updateTask = Task.Run(async () => await dnsService.UpdateDNSConfigAsync(dnsConfig));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"DNS server configuration updated: {updateResult.Result}");

                // Apply the changes if requested
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
