using System;
using System.Management.Automation;
using PSOPNSenseAPI.Utilities;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Converts between different network notation formats.</para>
    /// <para type="description">The ConvertTo-OPNSenseNetworkNotation cmdlet converts between CIDR notation and subnet mask notation.</para>
    /// <example>
    ///     <para>Example 1: Convert CIDR to subnet mask</para>
    ///     <code>ConvertTo-OPNSenseNetworkNotation -CIDR 24</code>
    ///     <para>This example converts the CIDR prefix length 24 to the subnet mask 255.255.255.0.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Convert subnet mask to CIDR</para>
    ///     <code>ConvertTo-OPNSenseNetworkNotation -SubnetMask "255.255.255.0"</code>
    ///     <para>This example converts the subnet mask 255.255.255.0 to the CIDR prefix length 24.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Convert IP with CIDR to IP with subnet mask</para>
    ///     <code>ConvertTo-OPNSenseNetworkNotation -IPWithCIDR "192.168.1.0/24"</code>
    ///     <para>This example converts the IP with CIDR 192.168.1.0/24 to the IP with subnet mask 192.168.1.0 255.255.255.0.</para>
    /// </example>
    /// <example>
    ///     <para>Example 4: Convert IP with subnet mask to IP with CIDR</para>
    ///     <code>ConvertTo-OPNSenseNetworkNotation -IPWithSubnetMask "192.168.1.0 255.255.255.0"</code>
    ///     <para>This example converts the IP with subnet mask 192.168.1.0 255.255.255.0 to the IP with CIDR 192.168.1.0/24.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.ConvertTo, "OPNSenseNetworkNotation")]
    [OutputType(typeof(string), typeof(int))]
    public class ConvertToOPNSenseNetworkNotationCmdlet : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The CIDR prefix length to convert to a subnet mask.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "FromCIDR")]
        [ValidateRange(0, 32)]
        public int? CIDR { get; set; }

        /// <summary>
        /// <para type="description">The subnet mask to convert to a CIDR prefix length.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "FromSubnetMask")]
        public string SubnetMask { get; set; }

        /// <summary>
        /// <para type="description">The IP address with CIDR notation to convert to IP address with subnet mask.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "FromIPWithCIDR")]
        public string IPWithCIDR { get; set; }

        /// <summary>
        /// <para type="description">The IP address with subnet mask to convert to IP address with CIDR notation.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "FromIPWithSubnetMask")]
        public string IPWithSubnetMask { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                switch (ParameterSetName)
                {
                    case "FromCIDR":
                        if (CIDR.HasValue)
                        {
                            string subnetMask = NetworkUtility.GetSubnetMaskFromCIDR(CIDR.Value);
                            WriteObject(subnetMask);
                        }
                        break;

                    case "FromSubnetMask":
                        if (!string.IsNullOrEmpty(SubnetMask))
                        {
                            int cidr = NetworkUtility.GetCIDRFromSubnetMask(SubnetMask);
                            WriteObject(cidr);
                        }
                        break;

                    case "FromIPWithCIDR":
                        if (!string.IsNullOrEmpty(IPWithCIDR))
                        {
                            string[] parts = IPWithCIDR.Split('/');
                            if (parts.Length != 2)
                            {
                                WriteError(new ErrorRecord(
                                    new ArgumentException($"Invalid IP with CIDR: {IPWithCIDR}. Expected format: 192.168.1.0/24"),
                                    "InvalidIPWithCIDR",
                                    ErrorCategory.InvalidArgument,
                                    null));
                                return;
                            }

                            string ip = parts[0];
                            if (!int.TryParse(parts[1], out int cidr))
                            {
                                WriteError(new ErrorRecord(
                                    new ArgumentException($"Invalid CIDR: {parts[1]}"),
                                    "InvalidCIDR",
                                    ErrorCategory.InvalidArgument,
                                    null));
                                return;
                            }

                            string subnetMask = NetworkUtility.GetSubnetMaskFromCIDR(cidr);
                            WriteObject($"{ip} {subnetMask}");
                        }
                        break;

                    case "FromIPWithSubnetMask":
                        if (!string.IsNullOrEmpty(IPWithSubnetMask))
                        {
                            string[] parts = IPWithSubnetMask.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length != 2)
                            {
                                WriteError(new ErrorRecord(
                                    new ArgumentException($"Invalid IP with subnet mask: {IPWithSubnetMask}. Expected format: 192.168.1.0 255.255.255.0"),
                                    "InvalidIPWithSubnetMask",
                                    ErrorCategory.InvalidArgument,
                                    null));
                                return;
                            }

                            string ip = parts[0];
                            string subnetMask = parts[1];
                            int cidr = NetworkUtility.GetCIDRFromSubnetMask(subnetMask);
                            WriteObject($"{ip}/{cidr}");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "ConversionError",
                    ErrorCategory.InvalidOperation,
                    null));
            }
        }
    }
}
