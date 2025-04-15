using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes an alias from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseAlias cmdlet removes an alias from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove an alias</para>
    ///     <code>Remove-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply</code>
    ///     <para>This example removes an alias by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove an alias with confirmation</para>
    ///     <code>Remove-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply</code>
    ///     <para>This example removes an alias by its UUID after confirmation.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Remove aliases by pipeline</para>
    ///     <code>Get-OPNSenseAlias -Type host | Remove-OPNSenseAlias -Apply</code>
    ///     <para>This example removes all host aliases.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseAlias", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseAliasCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the alias to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

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

                // Get the alias details for the confirmation message
                var getTask = Task.Run(async () => await aliasService.GetAliasAsync(Uuid));
                var alias = getTask.GetAwaiter().GetResult().Alias;

                string confirmMessage = $"Alias: {alias.Name} ({alias.Type})";
                if (!string.IsNullOrEmpty(alias.Description))
                {
                    confirmMessage += $" - {alias.Description}";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await aliasService.DeleteAliasAsync(Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"Alias {Uuid} removed: {deleteResult.Result}");

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
