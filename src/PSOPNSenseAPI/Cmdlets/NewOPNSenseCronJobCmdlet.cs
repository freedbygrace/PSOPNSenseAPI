using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new cron job on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseCronJob cmdlet creates a new cron job on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new cron job</para>
    ///     <code>New-OPNSenseCronJob -Description "Daily backup" -Command "/usr/local/bin/backup.sh" -Minutes "0" -Hours "2" -Days "*" -Months "*" -Weekdays "*"</code>
    ///     <para>This example creates a new cron job that runs a backup script at 2:00 AM every day.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create a new cron job that runs weekly</para>
    ///     <code>New-OPNSenseCronJob -Description "Weekly cleanup" -Command "/usr/local/bin/cleanup.sh" -Minutes "0" -Hours "3" -Days "*" -Months "*" -Weekdays "0"</code>
    ///     <para>This example creates a new cron job that runs a cleanup script at 3:00 AM every Sunday.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseCronJob")]
    [OutputType(typeof(string))]
    public class NewOPNSenseCronJobCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The description of the cron job.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The command to run.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Command { get; set; }

        /// <summary>
        /// <para type="description">The minutes part of the cron schedule (0-59, *, */5, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Minutes { get; set; } = "*";

        /// <summary>
        /// <para type="description">The hours part of the cron schedule (0-23, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Hours { get; set; } = "*";

        /// <summary>
        /// <para type="description">The days part of the cron schedule (1-31, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Days { get; set; } = "*";

        /// <summary>
        /// <para type="description">The months part of the cron schedule (1-12, *, */2, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Months { get; set; } = "*";

        /// <summary>
        /// <para type="description">The weekdays part of the cron schedule (0-6, *, etc.).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Weekdays { get; set; } = "*";

        /// <summary>
        /// <para type="description">Whether the cron job is enabled.</para>
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
            var cronService = new CronService(ApiClient, Logger);

            var job = new CronJobConfig
            {
                Description = Description,
                Command = Command,
                Minutes = Minutes,
                Hours = Hours,
                Days = Days,
                Months = Months,
                Weekdays = Weekdays,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => cronService.CreateJobAsync(job));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created cron job with UUID {createResult.Uuid}");

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

            WriteObject(createResult.Uuid);
        }
    }
}
