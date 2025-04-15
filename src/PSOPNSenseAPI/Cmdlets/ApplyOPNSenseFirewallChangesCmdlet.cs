using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Applies firewall changes on an OPNSense firewall.</para>
    /// <para type="description">The Apply-OPNSenseFirewallChanges cmdlet applies pending firewall changes on an OPNSense firewall.</para>
    /// <para type="description">This cmdlet creates a savepoint before applying changes, which allows for automatic rollback if the changes cause connectivity issues.</para>
    /// <example>
    ///     <para>Example 1: Apply firewall changes</para>
    ///     <code>Apply-OPNSenseFirewallChanges</code>
    ///     <para>This example applies pending firewall changes with automatic rollback if connectivity is lost.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Apply firewall changes without automatic rollback</para>
    ///     <code>Apply-OPNSenseFirewallChanges -NoRollback</code>
    ///     <para>This example applies pending firewall changes without creating a savepoint for automatic rollback.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Update, "OPNSenseFirewallChanges")]
    [OutputType(typeof(void))]
    public class ApplyOPNSenseFirewallChangesCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Applies changes without creating a savepoint for automatic rollback.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter NoRollback { get; set; }

        /// <summary>
        /// <para type="description">Cancels the automatic rollback after applying changes.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter CancelRollback { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait before cancelling the rollback.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 300)]
        public int Timeout { get; set; } = 60;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var firewallService = new FirewallService(ApiClient, Logger);

            if (NoRollback.IsPresent)
            {
                WriteVerbose("Applying firewall changes without rollback protection");
                var applyResult = ExecuteAsyncTask(() => firewallService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"Firewall changes applied: {applyResult.Status}");
            }
            else
            {
                WriteVerbose("Creating savepoint for rollback protection");
                var savepointResult = ExecuteAsyncTask(() => firewallService.CreateSavepointAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || savepointResult == null)
                {
                    return;
                }

                var revision = savepointResult.Revision;

                WriteVerbose($"Created savepoint with revision {revision}");
                WriteVerbose("Applying firewall changes with rollback protection");

                var applyResult = ExecuteAsyncTask(() => firewallService.ApplyChangesAsync(revision));

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"Firewall changes applied: {applyResult.Status}");

                if (CancelRollback.IsPresent)
                {
                    WriteVerbose($"Waiting {Timeout} seconds before cancelling rollback");
                    System.Threading.Thread.Sleep(Timeout * 1000);

                    WriteVerbose("Cancelling automatic rollback");
                    var cancelResult = ExecuteAsyncTask(() => firewallService.CancelRollbackAsync(revision));

                    // Only continue if no exception occurred
                    if (ProcessingException != null || cancelResult == null)
                    {
                        return;
                    }

                    WriteVerbose($"Rollback cancelled: {cancelResult.Status}");
                }
                else
                {
                    WriteVerbose($"Automatic rollback will occur in 60 seconds if connectivity is lost");
                }
            }
        }
    }
}
