using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Restores an OPNSense firewall configuration.</para>
    /// <para type="description">The Restore-OPNSenseConfig cmdlet restores an OPNSense firewall configuration from a backup or an XML document.</para>
    /// <example>
    ///     <para>Example 1: Restore a configuration backup</para>
    ///     <code>Restore-OPNSenseConfig -Filename "config-backup-20250414-1234.xml"</code>
    ///     <para>This example restores an OPNSense firewall configuration from a backup file.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Restore a configuration from an XML document</para>
    ///     <code>$config = Get-OPNSenseConfig; Restore-OPNSenseConfig -XmlDocument $config</code>
    ///     <para>This example restores an OPNSense firewall configuration from an XML document.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Restore a configuration from the pipeline</para>
    ///     <code>Get-OPNSenseConfig | Restore-OPNSenseConfig</code>
    ///     <para>This example restores an OPNSense firewall configuration from the pipeline.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Restore, "OPNSenseConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RestoreOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The filename of the backup to restore.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Filename")]
        [ValidateNotNullOrEmpty]
        public string Filename { get; set; }

        /// <summary>
        /// <para type="description">The XML document containing the configuration to restore.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "XmlDocument", ValueFromPipeline = true)]
        [ValidateNotNull]
        public XmlDocument XmlDocument { get; set; }

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
                var configService = new ConfigService(ApiClient, Logger);
                string actionDescription = "Restore configuration";
                string targetName = ParameterSetName == "Filename" ? Filename : "from XML document";

                if (!Force.IsPresent && !ShouldProcess(targetName, actionDescription))
                {
                    return;
                }

                if (ParameterSetName == "Filename")
                {
                    // Restore from backup file - execute synchronously on the main thread
                    var result = configService.RestoreConfigBackupAsync(Filename).GetAwaiter().GetResult();
                    WriteVerbose($"Restored configuration from backup: {result.Status}");
                }
                else
                {
                    // Restore from XML document
                    byte[] configContent;
                    using (var memoryStream = new MemoryStream())
                    {
                        XmlDocument.Save(memoryStream);
                        configContent = memoryStream.ToArray();
                    }

                    // Execute synchronously on the main thread
                    var result = configService.ImportConfigAsync(configContent).GetAwaiter().GetResult();
                    WriteVerbose($"Restored configuration from XML document: {result.Status}");
                }

                WriteWarning("The firewall is restarting. You may need to reconnect after it comes back online.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
