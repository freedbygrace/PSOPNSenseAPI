using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets routes from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseRoute cmdlet retrieves routes from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all routes</para>
    ///     <code>Get-OPNSenseRoute</code>
    ///     <para>This example retrieves all routes from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific route by UUID</para>
    ///     <code>Get-OPNSenseRoute -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific route by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseRoute")]
    [OutputType(typeof(Route), typeof(RouteDetail))]
    public class GetOPNSenseRouteCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the route to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var routeService = new RouteService(ApiClient, Logger);

            if (ParameterSetName == "ByUuid")
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => routeService.GetRouteAsync(Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Route);
            }
            else
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => routeService.GetRoutesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Rows, true);
            }
        }
    }
}
