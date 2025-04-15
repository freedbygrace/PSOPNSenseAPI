using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Exports the OPNSense firewall configuration.</para>
    /// <para type="description">The Export-OPNSenseConfig cmdlet exports the OPNSense firewall configuration to a file.</para>
    /// <example>
    ///     <para>Example 1: Export the configuration to a file</para>
    ///     <code>Export-OPNSenseConfig -Path "C:\Backups\opnsense-config.xml"</code>
    ///     <para>This example exports the OPNSense firewall configuration to a file.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Export, "OPNSenseConfig")]
    [OutputType(typeof(void))]
    public class ExportOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The path to save the configuration file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNull]
        public FileInfo Path { get; set; }

        /// <summary>
        /// <para type="description">Overwrites the file if it exists.</para>
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
            if (File.Exists(fullPath) && !Force.IsPresent)
            {
                ProcessingException = new IOException($"The file '{fullPath}' already exists. Use -Force to overwrite.");
                WriteWarning($"The file '{fullPath}' already exists. Use -Force to overwrite.");
                return;
            }

            var configService = new ConfigService(ApiClient, Logger);

            // Use our safe execution method
            var configContent = ExecuteAsyncTask(() => configService.ExportConfigAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || configContent == null)
            {
                return;
            }

            try
            {
                // Create the directory if it doesn't exist
                var directory = System.IO.Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write the configuration to the file
                File.WriteAllBytes(fullPath, configContent);

                WriteVerbose($"Exported configuration to {fullPath}");
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
                WriteWarning($"Failed to write configuration to file: {ex.Message}");
            }
        }
    }
}
