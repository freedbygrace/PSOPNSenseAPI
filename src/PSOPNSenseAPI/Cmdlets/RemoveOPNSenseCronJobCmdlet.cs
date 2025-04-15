using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a cron job from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseCronJob cmdlet removes a cron job from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a cron job</para>
    ///     <code>Remove-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a cron job by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a cron job with confirmation</para>
    ///     <code>Remove-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a cron job by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseCronJob", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseCronJobCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the cron job to remove.</para>
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
        protected override void ProcessRecordInternal()
        {
            var cronService = new CronService(ApiClient, Logger);

            // Get the job details for the confirmation message
            var jobResult = ExecuteAsyncTask(() => cronService.GetJobAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || jobResult == null)
            {
                return;
            }

            var job = jobResult.Job;

            string confirmMessage = $"Cron job: {job.Description}";
            if (!string.IsNullOrEmpty(job.Command))
            {
                confirmMessage += $" ({job.Command})";
            }

            if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
            {
                return;
            }

            // Use our safe execution method
            var deleteResult = ExecuteAsyncTask(() => cronService.DeleteJobAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || deleteResult == null)
            {
                return;
            }

            WriteVerbose($"Cron job {Uuid} removed: {deleteResult.Result}");

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
