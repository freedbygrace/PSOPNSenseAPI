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
        protected override void ProcessRecord()
        {
            try
            {
                string fullPath = Path.FullName;

                // Check if the file exists
                if (File.Exists(fullPath) && !Force.IsPresent)
                {
                    WriteError(new ErrorRecord(
                        new IOException($"The file '{fullPath}' already exists. Use -Force to overwrite."),
                        "FileExists",
                        ErrorCategory.ResourceExists,
                        fullPath));
                    return;
                }

                var configService = new ConfigService(ApiClient, Logger);

                // Execute the async method synchronously on the main thread
                var configContent = configService.ExportConfigAsync().GetAwaiter().GetResult();

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
                HandleException(ex);
            }
        }
    }
}
