using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a user on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseUser cmdlet updates a user on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a user's email</para>
    ///     <code>Set-OPNSenseUser -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Email "newemail@example.com"</code>
    ///     <para>This example updates the email of a user.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a user's password</para>
    ///     <code>Set-OPNSenseUser -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Password "NewP@ssw0rd"</code>
    ///     <para>This example updates the password of a user.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseUser")]
    [OutputType(typeof(void))]
    public class SetOPNSenseUserCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the user to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The password of the user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Password { get; set; }

        /// <summary>
        /// <para type="description">The full name of the user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string FullName { get; set; }

        /// <summary>
        /// <para type="description">The email of the user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Email { get; set; }

        /// <summary>
        /// <para type="description">The groups of the user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] Groups { get; set; }

        /// <summary>
        /// <para type="description">The authorizations of the user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] Authorizations { get; set; }

        /// <summary>
        /// <para type="description">Whether the user is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the user is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var userService = new UserService(ApiClient, Logger);

            // First, get the current user
            var getResult = ExecuteAsyncTask(() => userService.GetUserAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentUser = getResult.User;

            // Create the updated user
            var user = new UserConfig
            {
                Username = currentUser.Username,
                Password = Password ?? "",
                FullName = FullName ?? currentUser.FullName,
                Email = Email ?? currentUser.Email,
                Groups = Groups != null ? new List<string>(Groups) : currentUser.Groups,
                Authorizations = Authorizations != null ? new List<string>(Authorizations) : currentUser.Authorizations
            };

            // Handle enabled/disabled state
            if (Enabled.IsPresent && Disabled.IsPresent)
            {
                WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                user.Disabled = "0";
            }
            else if (Enabled.IsPresent)
            {
                user.Disabled = "0";
            }
            else if (Disabled.IsPresent)
            {
                user.Disabled = "1";
            }
            else
            {
                user.Disabled = currentUser.Disabled;
            }

            // Update the user
            var updateResult = ExecuteAsyncTask(() => userService.UpdateUserAsync(Uuid, user));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"User {Uuid} updated: {updateResult.Result}");
        }
    }
}
