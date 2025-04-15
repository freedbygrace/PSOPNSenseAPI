using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets firewall rules from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseFirewallRule cmdlet retrieves firewall rules from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all firewall rules</para>
    ///     <code>Get-OPNSenseFirewallRule</code>
    ///     <para>This example retrieves all firewall rules from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific firewall rule by UUID</para>
    ///     <code>Get-OPNSenseFirewallRule -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific firewall rule by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Search for firewall rules by description</para>
    ///     <code>Get-OPNSenseFirewallRule -SearchPhrase "Allow HTTP"</code>
    ///     <para>This example searches for firewall rules with a description containing "Allow HTTP".</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseFirewallRule")]
    [OutputType(typeof(FirewallRule), typeof(FirewallRuleDetails))]
    public class GetOPNSenseFirewallRuleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the firewall rule to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The search phrase to filter rules by.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "BySearch")]
        public string SearchPhrase { get; set; } = "";

        /// <summary>
        /// <para type="description">The page number to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "BySearch")]
        [ValidateRange(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// <para type="description">The number of rules to retrieve per page.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "BySearch")]
        [ValidateRange(1, 1000)]
        public int RowCount { get; set; } = 100;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var firewallService = new FirewallService(ApiClient, Logger);

            if (ParameterSetName == "ByUuid")
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => firewallService.GetRuleAsync(Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Rule);
            }
            else
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => firewallService.GetRulesAsync(SearchPhrase, Page, RowCount));

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
