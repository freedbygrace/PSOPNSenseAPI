using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new DNS override on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseDNSOverride cmdlet creates a new DNS override on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new DNS override</para>
    ///     <code>New-OPNSenseDNSOverride -Hostname "server" -Domain "example.com" -IpAddress "192.168.1.10" -Description "Internal server"</code>
    ///     <para>This example creates a new DNS override for server.example.com pointing to 192.168.1.10.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseDNSOverride")]
    [OutputType(typeof(string))]
    public class NewOPNSenseDNSOverrideCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The hostname part of the DNS override.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Hostname { get; set; }

        /// <summary>
        /// <para type="description">The domain part of the DNS override.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">The IP address to resolve to.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string IpAddress { get; set; }

        /// <summary>
        /// <para type="description">The description of the DNS override.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Whether the DNS override is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to apply the changes immediately.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Apply { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var dnsService = new DNSService(ApiClient, Logger);

            var dnsOverride = new DNSOverrideConfig
            {
                Hostname = Hostname,
                Domain = Domain,
                IpAddress = IpAddress,
                Description = Description,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => dnsService.CreateDNSOverrideAsync(dnsOverride));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created DNS override with UUID {createResult.Uuid}");

            // Apply the changes if requested
            if (Apply.IsPresent)
            {
                // Use our safe execution method
                var applyResult = ExecuteAsyncTask(() => dnsService.ApplyDNSChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DNS changes applied: {applyResult.Status}");
            }

            WriteObject(createResult.Uuid);
        }
    }
}
