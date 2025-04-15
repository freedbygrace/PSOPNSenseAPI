using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a cron job on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseCronJob cmdlet updates a cron job on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a cron job's description</para>
    ///     <code>Set-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Description "Updated backup job"</code>
    ///     <para>This example updates the description of a cron job.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a cron job's schedule</para>
    ///     <code>Set-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Hours "3" -Minutes "30"</code>
    ///     <para>This example updates the schedule of a cron job to run at 3:30 AM.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseCronJob")]
    [OutputType(typeof(void))]
    public class SetOPNSenseCronJobCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the cron job to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The description of the cron job.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The command to run.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Command { get; set; }

        /// <summary>
        /// <para type="description">The minutes part of the cron schedule (0-59, *, */5, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Minutes { get; set; }

        /// <summary>
        /// <para type="description">The hours part of the cron schedule (0-23, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Hours { get; set; }

        /// <summary>
        /// <para type="description">The days part of the cron schedule (1-31, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Days { get; set; }

        /// <summary>
        /// <para type="description">The months part of the cron schedule (1-12, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Months { get; set; }

        /// <summary>
        /// <para type="description">The weekdays part of the cron schedule (0-6, *, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Weekdays { get; set; }

        /// <summary>
        /// <para type="description">Whether the cron job is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the cron job is disabled.</para>
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
        protected override void ProcessRecordInternal()
        {
            var cronService = new CronService(ApiClient, Logger);

            // First, get the current job
            var getResult = ExecuteAsyncTask(() => cronService.GetJobAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentJob = getResult.Job;

            // Create the updated job
            var job = new CronJobConfig
            {
                Description = Description ?? currentJob.Description,
                Command = Command ?? currentJob.Command,
                Minutes = Minutes ?? currentJob.Minutes,
                Hours = Hours ?? currentJob.Hours,
                Days = Days ?? currentJob.Days,
                Months = Months ?? currentJob.Months,
                Weekdays = Weekdays ?? currentJob.Weekdays
            };

            // Handle enabled/disabled state
            if (Enabled.IsPresent && Disabled.IsPresent)
            {
                WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                job.Enabled = "1";
            }
            else if (Enabled.IsPresent)
            {
                job.Enabled = "1";
            }
            else if (Disabled.IsPresent)
            {
                job.Enabled = "0";
            }
            else
            {
                job.Enabled = currentJob.Enabled;
            }

            // Update the job
            var updateResult = ExecuteAsyncTask(() => cronService.UpdateJobAsync(Uuid, job));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"Cron job {Uuid} updated: {updateResult.Result}");

            // Apply the changes if requested
            if (Apply.IsPresent)
            {
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
