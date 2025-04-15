using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new route on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseRoute cmdlet creates a new route on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new route</para>
    ///     <code>New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN_GW" -Description "Remote Office" -Apply</code>
    ///     <para>This example creates a new route to a remote office network.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseRoute")]
    [OutputType(typeof(string))]
    public class NewOPNSenseRouteCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The network of the route.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Network { get; set; }

        /// <summary>
        /// <para type="description">The gateway of the route.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Gateway { get; set; }

        /// <summary>
        /// <para type="description">The description of the route.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

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
        protected override void ProcessRecordInternal()
        {
            var routeService = new RouteService(ApiClient, Logger);

            var route = new RouteConfig
            {
                Network = Network,
                Gateway = Gateway,
                Description = Description,
                Disabled = Disabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => routeService.CreateRouteAsync(route));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created route with UUID {createResult.Uuid}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                // Use our safe execution method
                var applyResult = ExecuteAsyncTask(() => routeService.ApplyRouteChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"Route changes applied: {applyResult.Status}");
            }

            WriteObject(createResult.Uuid);
        }
    }
}
