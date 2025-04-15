using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets plugins from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSensePlugin cmdlet retrieves plugins from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all installed plugins</para>
    ///     <code>Get-OPNSensePlugin -Installed</code>
    ///     <para>This example retrieves all installed plugins from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get all available plugins</para>
    ///     <code>Get-OPNSensePlugin -Available</code>
    ///     <para>This example retrieves all available plugins from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Get all plugins</para>
    ///     <code>Get-OPNSensePlugin</code>
    ///     <para>This example retrieves all installed and available plugins from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSensePlugin")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSensePluginCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Gets only installed plugins.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Installed")]
        public SwitchParameter Installed { get; set; }

        /// <summary>
        /// <para type="description">Gets only available plugins.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Available")]
        public SwitchParameter Available { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var pluginService = new PluginService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => pluginService.GetPluginsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            if (ParameterSetName == "Installed" || ParameterSetName == "")
            {
                foreach (var kvp in result.Installed)
                {
                    var plugin = new PSObject();
                    plugin.Properties.Add(new PSNoteProperty("Name", kvp.Key));
                    plugin.Properties.Add(new PSNoteProperty("Version", kvp.Value.Version));
                    plugin.Properties.Add(new PSNoteProperty("Comment", kvp.Value.Comment));
                    plugin.Properties.Add(new PSNoteProperty("Repository", kvp.Value.Repository));
                    plugin.Properties.Add(new PSNoteProperty("Origin", kvp.Value.Origin));
                    plugin.Properties.Add(new PSNoteProperty("License", kvp.Value.License));
                    plugin.Properties.Add(new PSNoteProperty("FlatSize", kvp.Value.FlatSize));
                    plugin.Properties.Add(new PSNoteProperty("Locked", kvp.Value.Locked == "1"));
                    plugin.Properties.Add(new PSNoteProperty("Enabled", kvp.Value.Enabled == "1"));
                    plugin.Properties.Add(new PSNoteProperty("Status", "Installed"));

                    WriteObject(plugin);
                }
            }

            if (ParameterSetName == "Available" || ParameterSetName == "")
            {
                foreach (var kvp in result.Available)
                {
                    // Skip if the plugin is already installed
                    if (result.Installed.ContainsKey(kvp.Key))
                        continue;

                    var plugin = new PSObject();
                    plugin.Properties.Add(new PSNoteProperty("Name", kvp.Key));
                    plugin.Properties.Add(new PSNoteProperty("Version", kvp.Value.Version));
                    plugin.Properties.Add(new PSNoteProperty("Comment", kvp.Value.Comment));
                    plugin.Properties.Add(new PSNoteProperty("Repository", kvp.Value.Repository));
                    plugin.Properties.Add(new PSNoteProperty("Origin", kvp.Value.Origin));
                    plugin.Properties.Add(new PSNoteProperty("License", kvp.Value.License));
                    plugin.Properties.Add(new PSNoteProperty("FlatSize", kvp.Value.FlatSize));
                    plugin.Properties.Add(new PSNoteProperty("Status", "Available"));

                    WriteObject(plugin);
                }
            }
        }
    }
}
