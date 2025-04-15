using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new user on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseUser cmdlet creates a new user on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new user</para>
    ///     <code>New-OPNSenseUser -Username "john" -Password "P@ssw0rd" -FullName "John Doe" -Email "john@example.com" -Groups "admins"</code>
    ///     <para>This example creates a new user with the specified properties.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseUser")]
    [OutputType(typeof(string))]
    public class NewOPNSenseUserCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The username of the user.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Username { get; set; }

        /// <summary>
        /// <para type="description">The password of the user.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
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
        /// <para type="description">Whether the user is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var userService = new UserService(ApiClient, Logger);

                var user = new UserConfig
                {
                    Username = Username,
                    Password = Password,
                    FullName = FullName,
                    Email = Email,
                    Disabled = Disabled.IsPresent ? "1" : "0",
                    Groups = Groups != null ? new List<string>(Groups) : new List<string>(),
                    Authorizations = Authorizations != null ? new List<string>(Authorizations) : new List<string>()
                };

                var task = Task.Run(async () => await userService.CreateUserAsync(user));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Created user with UUID {result.Uuid}");
                WriteObject(result.Uuid);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
