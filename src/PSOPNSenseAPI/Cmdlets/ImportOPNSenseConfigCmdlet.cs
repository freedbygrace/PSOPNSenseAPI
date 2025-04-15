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
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
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
                // Check if the file exists
                if (!File.Exists(Path))
                {
                    WriteError(new ErrorRecord(
                        new FileNotFoundException($"The file '{Path}' does not exist."),
                        "FileNotFound",
                        ErrorCategory.ObjectNotFound,
                        Path));
                    return;
                }

                if (!Force.IsPresent && !ShouldProcess(Path, "Import configuration"))
                {
                    return;
                }

                var configService = new ConfigService(ApiClient, Logger);

                // Read the configuration file
                var configContent = File.ReadAllBytes(Path);

                var task = Task.Run(async () => await configService.ImportConfigAsync(configContent));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Imported configuration from {Path}: {result.Status}");
                WriteWarning("The firewall is restarting. You may need to reconnect after it comes back online.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
