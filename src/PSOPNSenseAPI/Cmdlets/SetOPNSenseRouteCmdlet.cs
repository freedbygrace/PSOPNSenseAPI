using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a route on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseRoute cmdlet updates a route on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a route's gateway</para>
    ///     <code>Set-OPNSenseRoute -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Gateway "WAN2_GW" -Apply</code>
    ///     <para>This example updates the gateway of a route.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Enable a disabled route</para>
    ///     <code>Set-OPNSenseRoute -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Enabled -Apply</code>
    ///     <para>This example enables a previously disabled route.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseRoute")]
    [OutputType(typeof(void))]
    public class SetOPNSenseRouteCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the route to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The network of the route.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Network { get; set; }

        /// <summary>
        /// <para type="description">The gateway of the route.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Gateway { get; set; }

        /// <summary>
        /// <para type="description">The description of the route.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Whether the route is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the route is disabled.</para>
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
        protected override void ProcessRecord()
        {
            try
            {
                var routeService = new RouteService(ApiClient, Logger);

                // Get current route
                var getTask = Task.Run(async () => await routeService.GetRouteAsync(Uuid));
                var currentRoute = getTask.GetAwaiter().GetResult().Route;

                // Create updated route
                var route = new RouteConfig
                {
                    Network = Network ?? currentRoute.Network,
                    Gateway = Gateway ?? currentRoute.Gateway,
                    Description = Description ?? currentRoute.Description
                };

                // Handle enabled/disabled state
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    route.Disabled = "0";
                }
                else if (Enabled.IsPresent)
                {
                    route.Disabled = "0";
                }
                else if (Disabled.IsPresent)
                {
                    route.Disabled = "1";
                }
                else
                {
                    route.Disabled = currentRoute.Disabled;
                }

                // Update route
                var updateTask = Task.Run(async () => await routeService.UpdateRouteAsync(Uuid, route));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Route {Uuid} updated: {updateResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await routeService.ApplyRouteChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Route changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
