using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new alias on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseAlias cmdlet creates a new alias on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new host alias</para>
    ///     <code>New-OPNSenseAlias -Name "WebServers" -Type host -Content "192.168.1.10,192.168.1.11" -Description "Web Servers" -Apply</code>
    ///     <para>This example creates a new host alias for web servers.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create a new network alias</para>
    ///     <code>New-OPNSenseAlias -Name "InternalNetworks" -Type network -Content "192.168.1.0/24,192.168.2.0/24" -Description "Internal Networks" -Apply</code>
    ///     <para>This example creates a new network alias for internal networks.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Create a new port alias</para>
    ///     <code>New-OPNSenseAlias -Name "WebPorts" -Type port -Content "80,443" -Protocol TCP -Description "Web Ports" -Apply</code>
    ///     <para>This example creates a new port alias for web ports.</para>
    /// </example>
    /// <example>
    ///     <para>Example 4: Create a new URL alias</para>
    ///     <code>New-OPNSenseAlias -Name "BlockList" -Type url -Content "https://example.com/blocklist.txt" -UpdateFrequency 1 -Description "Block List" -Apply</code>
    ///     <para>This example creates a new URL alias for a block list that updates daily.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseAlias")]
    [OutputType(typeof(string))]
    public class NewOPNSenseAliasCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the alias.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The type of the alias.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateSet("host", "network", "port", "url", "urltable", "geoip", "networkgroup", "mac", "interface", "dynipv6host", "internal", "external")]
        public string Type { get; set; }

        /// <summary>
        /// <para type="description">The content of the alias. For host/network/port types, use comma-separated values. For URL types, specify the URL.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Content { get; set; }

        /// <summary>
        /// <para type="description">The description of the alias.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The protocol for port aliases (TCP, UDP, or TCP/UDP).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("TCP", "UDP", "TCP/UDP")]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">The update frequency for URL aliases (in days).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 365)]
        public int? UpdateFrequency { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable counters for the alias.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableCounters { get; set; }

        /// <summary>
        /// <para type="description">Whether the alias is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

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
                var aliasService = new AliasService(ApiClient, Logger);

                var alias = new AliasConfig
                {
                    Name = Name,
                    Type = Type.ToLower(),
                    Content = Content,
                    Description = Description,
                    Enabled = Disabled.IsPresent ? "0" : "1",
                    Counters = EnableCounters.IsPresent ? "1" : "0"
                };

                // Set protocol for port aliases
                if (Type.Equals("port", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(Protocol))
                {
                    alias.Protocol = Protocol.ToUpper();
                }

                // Set update frequency for URL aliases
                if ((Type.Equals("url", StringComparison.OrdinalIgnoreCase) || Type.Equals("urltable", StringComparison.OrdinalIgnoreCase)) && UpdateFrequency.HasValue)
                {
                    alias.UpdateFrequency = UpdateFrequency.Value.ToString();
                }

                var createTask = Task.Run(async () => await aliasService.CreateAliasAsync(alias));
                var createResult = createTask.GetAwaiter().GetResult();

                WriteVerbose($"Created alias with UUID {createResult.Uuid}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await aliasService.ReconfigureAliasesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Alias changes applied: {applyResult.Status}");
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
