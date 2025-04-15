using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets users from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseUser cmdlet retrieves users from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all users</para>
    ///     <code>Get-OPNSenseUser</code>
    ///     <para>This example retrieves all users from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific user by UUID</para>
    ///     <code>Get-OPNSenseUser -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific user by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseUser")]
    [OutputType(typeof(User), typeof(UserDetail))]
    public class GetOPNSenseUserCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the user to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var userService = new UserService(ApiClient, Logger);

                if (ParameterSetName == "ByUuid")
                {
                    var task = Task.Run(async () => await userService.GetUserAsync(Uuid));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.User);
                }
                else
                {
                    var task = Task.Run(async () => await userService.GetUsersAsync());
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Rows, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
