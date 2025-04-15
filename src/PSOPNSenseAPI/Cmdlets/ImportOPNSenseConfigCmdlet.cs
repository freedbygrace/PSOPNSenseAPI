using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Imports an OPNSense firewall configuration.</para>
    /// <para type="description">The Import-OPNSenseConfig cmdlet imports an OPNSense firewall configuration from a file.</para>
    /// <example>
    ///     <para>Example 1: Import a configuration from a file</para>
    ///     <code>Import-OPNSenseConfig -Path "C:\Backups\opnsense-config.xml"</code>
    ///     <para>This example imports an OPNSense firewall configuration from a file.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Import, "OPNSenseConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class ImportOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The path to the configuration file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNull]
        public FileInfo Path { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            string fullPath = Path.FullName;

            // Check if the file exists
            if (!File.Exists(fullPath))
            {
                ProcessingException = new FileNotFoundException($"The file '{fullPath}' does not exist.");
                WriteWarning($"The file '{fullPath}' does not exist.");
                return;
            }

            if (!Force.IsPresent && !ShouldProcess(fullPath, "Import configuration"))
            {
                return;
            }

            var configService = new ConfigService(ApiClient, Logger);

            try
            {
                // Read the configuration file
                var configContent = File.ReadAllBytes(fullPath);

                // Use our safe execution method
                var result = ExecuteAsyncTask(() => configService.ImportConfigAsync(configContent));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteVerbose($"Imported configuration from {fullPath}: {result.Status}");
                WriteWarning("The firewall is restarting. You may need to reconnect after it comes back online.");
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
                WriteWarning($"Failed to read or process configuration file: {ex.Message}");
            }
        }
    }
}
