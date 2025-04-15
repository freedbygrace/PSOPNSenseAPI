using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates an alias on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseAlias cmdlet updates an alias on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update an alias's content</para>
    ///     <code>Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Content "192.168.1.10,192.168.1.11,192.168.1.12" -Apply</code>
    ///     <para>This example updates the content of an alias.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update an alias's description</para>
    ///     <code>Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Description "Updated description" -Apply</code>
    ///     <para>This example updates the description of an alias.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Disable an alias</para>
    ///     <code>Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Disabled -Apply</code>
    ///     <para>This example disables an alias.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseAlias")]
    [OutputType(typeof(void))]
    public class SetOPNSenseAliasCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the alias to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The content of the alias. For host/network/port types, use comma-separated values. For URL types, specify the URL.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
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
        /// <para type="description">Whether to disable counters for the alias.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter DisableCounters { get; set; }

        /// <summary>
        /// <para type="description">Whether the alias is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

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

                // Get current alias
                var getTask = Task.Run(async () => await aliasService.GetAliasAsync(Uuid));
                var currentAlias = getTask.GetAwaiter().GetResult().Alias;

                // Create updated alias
                var alias = new AliasConfig
                {
                    Name = currentAlias.Name,
                    Type = currentAlias.Type,
                    Content = Content ?? currentAlias.Content,
                    Description = Description ?? currentAlias.Description,
                    Protocol = Protocol?.ToUpper() ?? currentAlias.Protocol,
                    UpdateFrequency = UpdateFrequency?.ToString() ?? currentAlias.UpdateFrequency,
                };

                // Handle enabled/disabled state
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    alias.Enabled = "1";
                }
                else if (Enabled.IsPresent)
                {
                    alias.Enabled = "1";
                }
                else if (Disabled.IsPresent)
                {
                    alias.Enabled = "0";
                }
                else
                {
                    alias.Enabled = currentAlias.Enabled;
                }

                // Handle counters
                if (EnableCounters.IsPresent && DisableCounters.IsPresent)
                {
                    WriteWarning("Both -EnableCounters and -DisableCounters parameters were specified. Using -EnableCounters.");
                    alias.Counters = "1";
                }
                else if (EnableCounters.IsPresent)
                {
                    alias.Counters = "1";
                }
                else if (DisableCounters.IsPresent)
                {
                    alias.Counters = "0";
                }
                else
                {
                    alias.Counters = currentAlias.Counters;
                }

                // Update alias
                var updateTask = Task.Run(async () => await aliasService.UpdateAliasAsync(Uuid, alias));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Alias {Uuid} updated: {updateResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await aliasService.ReconfigureAliasesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Alias changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
