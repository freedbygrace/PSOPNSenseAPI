using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets aliases from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseAlias cmdlet retrieves aliases from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all aliases</para>
    ///     <code>Get-OPNSenseAlias</code>
    ///     <para>This example retrieves all aliases from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific alias by UUID</para>
    ///     <code>Get-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific alias by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Get aliases by type</para>
    ///     <code>Get-OPNSenseAlias | Where-Object { $_.Type -eq "host" }</code>
    ///     <para>This example retrieves all host aliases.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseAlias")]
    [OutputType(typeof(Alias), typeof(AliasDetail))]
    public class GetOPNSenseAliasCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the alias to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The name of the alias to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The type of aliases to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("host", "network", "port", "url", "urltable", "geoip", "networkgroup", "mac", "interface", "dynipv6host", "internal", "external")]
        public string Type { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var aliasService = new AliasService(ApiClient, Logger);

                if (ParameterSetName == "ByUuid")
                {
                    var task = Task.Run(async () => await aliasService.GetAliasAsync(Uuid));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Alias);
                }
                else
                {
                    var task = Task.Run(async () => await aliasService.GetAliasesAsync());
                    var result = task.GetAwaiter().GetResult();

                    // Filter by name if specified
                    if (ParameterSetName == "ByName")
                    {
                        var filteredByName = result.Rows.FindAll(a => a.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
                        
                        // Further filter by type if specified
                        if (!string.IsNullOrEmpty(Type))
                        {
                            var filteredByType = filteredByName.FindAll(a => a.Type.Equals(Type, StringComparison.OrdinalIgnoreCase));
                            WriteObject(filteredByType, true);
                        }
                        else
                        {
                            WriteObject(filteredByName, true);
                        }
                    }
                    // Filter by type if specified
                    else if (!string.IsNullOrEmpty(Type))
                    {
                        var filteredByType = result.Rows.FindAll(a => a.Type.Equals(Type, StringComparison.OrdinalIgnoreCase));
                        WriteObject(filteredByType, true);
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
