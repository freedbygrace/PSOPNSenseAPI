using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Enables a cron job on an OPNSense firewall.</para>
    /// <para type="description">The Enable-OPNSenseCronJob cmdlet enables a cron job on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Enable a cron job</para>
    ///     <code>Enable-OPNSenseCronJob -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example enables a cron job by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Enable, "OPNSenseCronJob")]
    [OutputType(typeof(void))]
    public class EnableOPNSenseCronJobCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the cron job to enable.</para>
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
        protected override void ProcessRecord()
        {
            try
            {
                var cronService = new CronService(ApiClient, Logger);

                var task = Task.Run(async () => await cronService.ToggleJobAsync(Uuid, true));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Cron job {Uuid} enabled: {result.Result}");

                // Apply the changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await cronService.ApplyChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();
                    
                    WriteVerbose($"Cron changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
