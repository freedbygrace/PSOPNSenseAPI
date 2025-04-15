using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disables a cron job on an OPNSense firewall.</para>
    /// <para type="description">The Disable-OPNSenseCronJob cmdlet disables a cron job on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Disable a cron job</para>
    ///     <code>Disable-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example disables a cron job by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Disable, "OPNSenseCronJob")]
    [OutputType(typeof(void))]
    public class DisableOPNSenseCronJobCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the cron job to disable.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

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
            var cronService = new CronService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => cronService.ToggleJobAsync(Uuid, false));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Cron job {Uuid} disabled: {result.Result}");

            // Apply the changes if requested
            if (Apply.IsPresent)
            {
                // Use our safe execution method
                var applyResult = ExecuteAsyncTask(() => cronService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"Cron changes applied: {applyResult.Status}");
            }
        }
    }
}
