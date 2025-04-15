using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a user from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseUser cmdlet removes a user from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a user</para>
    ///     <code>Remove-OPNSenseUser -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a user by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a user with confirmation</para>
    ///     <code>Remove-OPNSenseUser -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a user by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseUserCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the user to remove.</para>
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
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var userService = new UserService(ApiClient, Logger);

                // Get the user details for the confirmation message
                var getTask = Task.Run(async () => await userService.GetUserAsync(Uuid));
                var user = getTask.GetAwaiter().GetResult().User;

                string confirmMessage = $"User: {user.Username}";
                if (!string.IsNullOrEmpty(user.FullName))
                {
                    confirmMessage += $" ({user.FullName})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await userService.DeleteUserAsync(Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"User {Uuid} removed: {deleteResult.Result}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
