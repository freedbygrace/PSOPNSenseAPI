using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets the OPNSense firewall configuration as an XML document.</para>
    /// <para type="description">The Get-OPNSenseConfig cmdlet retrieves the OPNSense firewall configuration and returns it as an XML document.</para>
    /// <example>
    ///     <para>Example 1: Get the configuration as an XML document</para>
    ///     <code>$config = Get-OPNSenseConfig</code>
    ///     <para>This example retrieves the OPNSense firewall configuration as an XML document.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get the configuration and pipe it to Restore-OPNSenseConfig</para>
    ///     <code>Get-OPNSenseConfig | Restore-OPNSenseConfig</code>
    ///     <para>This example retrieves the OPNSense firewall configuration and restores it.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseConfig")]
    [OutputType(typeof(XmlDocument))]
    public class GetOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var configService = new ConfigService(ApiClient, Logger);

            // Use our safe execution method
            var configContent = ExecuteAsyncTask(() => configService.ExportConfigAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || configContent == null)
            {
                return;
            }

            // Convert the byte array to an XML document
            var xmlDoc = new XmlDocument();
            try
            {
                using (var memoryStream = new MemoryStream(configContent))
                {
                    xmlDoc.Load(memoryStream);
                }

                WriteVerbose("Retrieved OPNSense configuration as XML document");
                WriteObject(xmlDoc);
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
                WriteWarning($"Failed to parse XML configuration: {ex.Message}");
            }
        }
    }
}
