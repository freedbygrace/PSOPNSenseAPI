using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a route from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseRoute cmdlet removes a route from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a route</para>
    ///     <code>Remove-OPNSenseRoute -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply</code>
    ///     <para>This example removes a route by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a route with confirmation</para>
    ///     <code>Remove-OPNSenseRoute -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply</code>
    ///     <para>This example removes a route by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseRoute", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseRouteCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the route to remove.</para>
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
            var routeService = new RouteService(ApiClient, Logger);

            // Get the route details for the confirmation message
            var getResult = ExecuteAsyncTask(() => routeService.GetRouteAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var route = getResult.Route;

            string confirmMessage = $"Route: {route.Network} via {route.Gateway}";
            if (!string.IsNullOrEmpty(route.Description))
            {
                confirmMessage += $" ({route.Description})";
            }

            if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
            {
                return;
            }

            var deleteResult = ExecuteAsyncTask(() => routeService.DeleteRouteAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || deleteResult == null)
            {
                return;
            }

            WriteVerbose($"Route {Uuid} removed: {deleteResult.Result}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                var applyResult = ExecuteAsyncTask(() => routeService.ApplyRouteChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"Route changes applied: {applyResult.Status}");
            }
        }
    }
}
