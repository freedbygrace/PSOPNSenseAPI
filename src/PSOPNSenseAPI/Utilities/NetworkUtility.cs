using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Linq;
using static System.Net.IPNetwork2;

namespace PSOPNSenseAPI.Utilities
{
    /// <summary>
    /// Utility class for network operations using IPNetwork
    /// </summary>
    public static class NetworkUtility
    {
        /// <summary>
        /// Parses a CIDR notation string into an IPNetwork2 object
        /// </summary>
        /// <param name="cidr">The CIDR notation string (e.g., "192.168.1.0/24")</param>
        /// <returns>An IPNetwork2 object</returns>
        public static IPNetwork2 ParseCIDR(string cidr)
        {
            return IPNetwork2.Parse(cidr);
        }

        /// <summary>
        /// Tries to parse a CIDR notation string into an IPNetwork2 object
        /// </summary>
        /// <param name="cidr">The CIDR notation string (e.g., "192.168.1.0/24")</param>
        /// <param name="network">The resulting IPNetwork2 object if parsing succeeds</param>
        /// <returns>True if parsing succeeds, false otherwise</returns>
        public static bool TryParseCIDR(string cidr, out IPNetwork2 network)
        {
            return IPNetwork2.TryParse(cidr, out network);
        }

        /// <summary>
        /// Checks if an IP address is within a network
        /// </summary>
        /// <param name="network">The network to check</param>
        /// <param name="ipAddress">The IP address to check</param>
        /// <returns>True if the IP address is within the network, false otherwise</returns>
        public static bool Contains(IPNetwork2 network, IPAddress ipAddress)
        {
            return network.Contains(ipAddress);
        }

        /// <summary>
        /// Checks if a network is within another network
        /// </summary>
        /// <param name="container">The container network</param>
        /// <param name="subnet">The subnet to check</param>
        /// <returns>True if the subnet is within the container network, false otherwise</returns>
        public static bool Contains(IPNetwork2 container, IPNetwork2 subnet)
        {
            return container.Contains(subnet);
        }

        /// <summary>
        /// Checks if two networks overlap
        /// </summary>
        /// <param name="network1">The first network</param>
        /// <param name="network2">The second network</param>
        /// <returns>True if the networks overlap, false otherwise</returns>
        public static bool Overlaps(IPNetwork2 network1, IPNetwork2 network2)
        {
            return network1.Overlap(network2);
        }

        /// <summary>
        /// Divides a network into subnets of a specified prefix length
        /// </summary>
        /// <param name="network">The network to divide</param>
        /// <param name="prefixLength">The prefix length of the subnets</param>
        /// <returns>An enumerable of subnets</returns>
        public static IEnumerable<IPNetwork2> Subnet(IPNetwork2 network, int prefixLength)
        {
            return network.Subnet((byte)prefixLength);
        }

        /// <summary>
        /// Divides a network into a specified number of subnets
        /// </summary>
        /// <param name="network">The network to divide</param>
        /// <param name="subnetCount">The number of subnets to create</param>
        /// <returns>An enumerable of subnets</returns>
        public static IEnumerable<IPNetwork2> SubnetByCount(IPNetwork2 network, int subnetCount)
        {
            // Calculate the prefix length needed for the specified number of subnets
            int bitsNeeded = (int)Math.Ceiling(Math.Log(subnetCount, 2));
            int newPrefixLength = network.Cidr + bitsNeeded;

            // Ensure the prefix length is valid
            if (newPrefixLength > (network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? 32 : 128))
            {
                throw new ArgumentException($"Cannot create {subnetCount} subnets from {network}. The resulting prefix length would be too large.");
            }

            return Subnet(network, newPrefixLength);
        }

        /// <summary>
        /// Gets the first usable host address in a network
        /// </summary>
        /// <param name="network">The network</param>
        /// <returns>The first usable host address</returns>
        public static IPAddress GetFirstUsableHost(IPNetwork2 network)
        {
            // For /31 and /32 networks, the network address is usable
            if (network.Cidr >= 31 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return network.Network;

            // For /127 and /128 networks, the network address is usable
            if (network.Cidr >= 127 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                return network.Network;

            // Otherwise, the first usable host is the network address + 1
            byte[] addressBytes = network.Network.GetAddressBytes();

            // Increment the last byte
            for (int i = addressBytes.Length - 1; i >= 0; i--)
            {
                if (addressBytes[i] < 255)
                {
                    addressBytes[i]++;
                    break;
                }
                addressBytes[i] = 0;
            }

            return new IPAddress(addressBytes);
        }

        /// <summary>
        /// Gets the last usable host address in a network
        /// </summary>
        /// <param name="network">The network</param>
        /// <returns>The last usable host address</returns>
        public static IPAddress GetLastUsableHost(IPNetwork2 network)
        {
            // For /31 and /32 networks, the broadcast address is usable
            if (network.Cidr >= 31 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return network.Broadcast;

            // For /127 and /128 networks, the broadcast address is usable
            if (network.Cidr >= 127 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                return network.Broadcast;

            // Otherwise, the last usable host is the broadcast address - 1
            byte[] addressBytes = network.Broadcast.GetAddressBytes();

            // Decrement the last byte
            for (int i = addressBytes.Length - 1; i >= 0; i--)
            {
                if (addressBytes[i] > 0)
                {
                    addressBytes[i]--;
                    break;
                }
                addressBytes[i] = 255;
            }

            return new IPAddress(addressBytes);
        }

        /// <summary>
        /// Gets the total number of usable host addresses in a network
        /// </summary>
        /// <param name="network">The network</param>
        /// <returns>The number of usable host addresses</returns>
        public static BigInteger GetUsableHostCount(IPNetwork2 network)
        {
            // For /31 networks, there are 2 usable hosts
            if (network.Cidr == 31 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return 2;

            // For /32 networks, there is 1 usable host
            if (network.Cidr == 32 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return 1;

            // For /127 networks, there are 2 usable hosts
            if (network.Cidr == 127 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                return 2;

            // For /128 networks, there is 1 usable host
            if (network.Cidr == 128 && network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                return 1;

            // Otherwise, the number of usable hosts is 2^(32-prefix) - 2 for IPv4
            // or 2^(128-prefix) for IPv6 (we don't subtract 2 for IPv6 as the network and broadcast addresses are usable)
            if (network.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // For IPv4, subtract 2 for network and broadcast addresses
                return network.Total - 2 > 0 ? network.Total - 2 : network.Total;
            }
            else
            {
                // For IPv6, all addresses are usable
                return network.Total;
            }
        }

        /// <summary>
        /// Supernets two or more networks into a single network
        /// </summary>
        /// <param name="networks">The networks to supernet</param>
        /// <returns>The supernet network</returns>
        public static IPNetwork2 Supernet(params IPNetwork2[] networks)
        {
            if (networks == null || networks.Length < 2)
                throw new ArgumentException("At least two networks are required for supernetting.");

            IPNetwork2[] result = IPNetwork2.Supernet(networks);
            if (result.Length > 0)
                return result[0];
            throw new InvalidOperationException("Failed to supernet the networks.");
        }

        /// <summary>
        /// Attempts to supernet a list of networks into the smallest possible set of summary networks
        /// </summary>
        /// <param name="networks">The networks to summarize</param>
        /// <returns>A list of summarized networks</returns>
        public static List<IPNetwork2> SupernetSummarize(IEnumerable<IPNetwork2> networks)
        {
            if (networks == null)
                throw new ArgumentNullException(nameof(networks));

            var networkList = networks.ToList();
            if (networkList.Count == 0)
                return new List<IPNetwork2>();
            if (networkList.Count == 1)
                return networkList;

            // Sort networks by address family, then by network address
            networkList.Sort((a, b) =>
            {
                int addressFamilyComparison = a.AddressFamily.CompareTo(b.AddressFamily);
                if (addressFamilyComparison != 0)
                    return addressFamilyComparison;

                return CompareIPAddresses(a.Network, b.Network);
            });

            var result = new List<IPNetwork2>();
            var currentGroup = new List<IPNetwork2> { networkList[0] };
            var currentAddressFamily = networkList[0].AddressFamily;

            for (int i = 1; i < networkList.Count; i++)
            {
                var network = networkList[i];

                // If address family changes, summarize the current group and start a new one
                if (network.AddressFamily != currentAddressFamily)
                {
                    result.AddRange(SupernetGroup(currentGroup));
                    currentGroup.Clear();
                    currentGroup.Add(network);
                    currentAddressFamily = network.AddressFamily;
                    continue;
                }

                // Check if the current network can be part of the current group
                var lastNetwork = currentGroup[currentGroup.Count - 1];
                if (CanBeCombined(lastNetwork, network))
                {
                    currentGroup.Add(network);
                }
                else
                {
                    // If not, summarize the current group and start a new one with this network
                    result.AddRange(SupernetGroup(currentGroup));
                    currentGroup.Clear();
                    currentGroup.Add(network);
                }
            }

            // Don't forget to process the last group
            if (currentGroup.Count > 0)
            {
                result.AddRange(SupernetGroup(currentGroup));
            }

            return result;
        }

        /// <summary>
        /// Supernets a group of networks that are known to be adjacent or overlapping
        /// </summary>
        /// <param name="networks">The networks to supernet</param>
        /// <returns>A list of summarized networks</returns>
        private static List<IPNetwork2> SupernetGroup(List<IPNetwork2> networks)
        {
            if (networks.Count == 1)
                return new List<IPNetwork2> { networks[0] };

            // Try to find the most efficient summarization
            var result = new List<IPNetwork2>();
            var currentSet = new List<IPNetwork2>();

            foreach (var network in networks)
            {
                if (currentSet.Count == 0)
                {
                    currentSet.Add(network);
                    continue;
                }

                // Try to supernet with the current set
                var tempSet = new List<IPNetwork2>(currentSet) { network };
                var supernetResult = IPNetwork2.Supernet(tempSet.ToArray());
                if (supernetResult.Length == 0)
                    continue;

                var supernetted = supernetResult[0];

                // Check if the supernet is efficient (doesn't include too many extra addresses)
                bool isEfficient = IsEfficientSupernet(tempSet, supernetted);

                if (isEfficient)
                {
                    currentSet.Add(network);
                }
                else
                {
                    // If not efficient, summarize the current set and start a new one
                    if (currentSet.Count > 1)
                    {
                        IPNetwork2[] innerSupernetResult = IPNetwork2.Supernet(currentSet.ToArray());
                        if (innerSupernetResult.Length > 0)
                            result.Add(innerSupernetResult[0]);
                        else
                            result.AddRange(currentSet);
                    }
                    else
                    {
                        result.Add(currentSet[0]);
                    }
                    currentSet.Clear();
                    currentSet.Add(network);
                }
            }

            // Process the last set
            if (currentSet.Count > 1)
            {
                IPNetwork2[] lastSupernetResult = IPNetwork2.Supernet(currentSet.ToArray());
                if (lastSupernetResult.Length > 0)
                    result.Add(lastSupernetResult[0]);
                else
                    result.AddRange(currentSet);
            }
            else if (currentSet.Count == 1)
            {
                result.Add(currentSet[0]);
            }

            return result;
        }

        /// <summary>
        /// Determines if two networks can potentially be combined in a supernet
        /// </summary>
        /// <param name="network1">The first network</param>
        /// <param name="network2">The second network</param>
        /// <returns>True if the networks can be combined, false otherwise</returns>
        private static bool CanBeCombined(IPNetwork2 network1, IPNetwork2 network2)
        {
            // Different address families cannot be combined
            if (network1.AddressFamily != network2.AddressFamily)
                return false;

            // Check if the networks are adjacent or overlapping
            if (network1.Overlap(network2))
                return true;

            // Check if they're adjacent (one network's broadcast + 1 = other network's network address)
            byte[] broadcast1 = network1.Broadcast.GetAddressBytes();
            byte[] network2Addr = network2.Network.GetAddressBytes();

            // Add 1 to broadcast1
            for (int i = broadcast1.Length - 1; i >= 0; i--)
            {
                if (broadcast1[i] < 255)
                {
                    broadcast1[i]++;
                    break;
                }
                broadcast1[i] = 0;
            }

            // Compare the arrays
            for (int i = 0; i < broadcast1.Length; i++)
            {
                if (broadcast1[i] != network2Addr[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if a supernet is efficient (doesn't include too many extra addresses)
        /// </summary>
        /// <param name="networks">The original networks</param>
        /// <param name="supernet">The supernet</param>
        /// <param name="threshold">The efficiency threshold (0.0 to 1.0) that determines if a supernet is efficient</param>
        /// <returns>True if the supernet is efficient, false otherwise</returns>
        private static bool IsEfficientSupernet(List<IPNetwork2> networks, IPNetwork2 supernet, double threshold = 0.5)
        {
            // Calculate the total addresses in the original networks
            BigInteger originalTotal = 0;
            foreach (var network in networks)
            {
                originalTotal += network.Total;
            }

            // Calculate the efficiency as a percentage
            double originalTotalDouble = (double)((decimal)originalTotal);
            double supernetTotalDouble = (double)((decimal)supernet.Total);
            double efficiency = originalTotalDouble / supernetTotalDouble;

            // Consider it efficient if it's at least the threshold percentage efficient
            return efficiency >= threshold;
        }

        /// <summary>
        /// Compares two IP addresses
        /// </summary>
        /// <param name="a">The first IP address</param>
        /// <param name="b">The second IP address</param>
        /// <returns>A negative value if a is less than b, 0 if they're equal, a positive value if a is greater than b</returns>
        private static int CompareIPAddresses(IPAddress a, IPAddress b)
        {
            byte[] bytesA = a.GetAddressBytes();
            byte[] bytesB = b.GetAddressBytes();

            // Different lengths (IPv4 vs IPv6)
            if (bytesA.Length != bytesB.Length)
                return bytesA.Length - bytesB.Length;

            // Compare byte by byte
            for (int i = 0; i < bytesA.Length; i++)
            {
                if (bytesA[i] != bytesB[i])
                    return bytesA[i] - bytesB[i];
            }

            return 0; // Equal
        }

        /// <summary>
        /// Gets the CIDR notation for a subnet mask
        /// </summary>
        /// <param name="subnetMask">The subnet mask (e.g., "255.255.255.0")</param>
        /// <returns>The CIDR prefix length</returns>
        public static int GetCIDRFromSubnetMask(string subnetMask)
        {
            IPAddress mask = IPAddress.Parse(subnetMask);
            byte[] bytes = mask.GetAddressBytes();
            int cidr = 0;

            foreach (byte b in bytes)
            {
                for (int i = 0; i < 8; i++)
                {
                    if ((b & (1 << (7 - i))) != 0)
                    {
                        cidr++;
                    }
                    else
                    {
                        // Once we hit a 0 bit, all remaining bits should be 0
                        // If not, this is an invalid subnet mask
                        for (int j = i + 1; j < 8; j++)
                        {
                            if ((b & (1 << (7 - j))) != 0)
                            {
                                throw new ArgumentException($"Invalid subnet mask: {subnetMask}");
                            }
                        }
                        return cidr;
                    }
                }
            }

            return cidr;
        }

        /// <summary>
        /// Gets the subnet mask for a CIDR prefix length
        /// </summary>
        /// <param name="cidr">The CIDR prefix length</param>
        /// <returns>The subnet mask</returns>
        public static string GetSubnetMaskFromCIDR(int cidr)
        {
            if (cidr < 0 || cidr > 32)
                throw new ArgumentException($"Invalid CIDR prefix length: {cidr}. Must be between 0 and 32.");

            uint mask = 0xffffffff;
            mask = mask << (32 - cidr);

            byte[] bytes = new byte[4];
            bytes[0] = (byte)((mask >> 24) & 0xff);
            bytes[1] = (byte)((mask >> 16) & 0xff);
            bytes[2] = (byte)((mask >> 8) & 0xff);
            bytes[3] = (byte)(mask & 0xff);

            return new IPAddress(bytes).ToString();
        }

        /// <summary>
        /// Converts an IP address to its integer representation
        /// </summary>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>The integer representation of the IP address</returns>
        public static uint IPAddressToUInt32(IPAddress ipAddress)
        {
            if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                throw new ArgumentException("Only IPv4 addresses can be converted to UInt32.");

            byte[] bytes = ipAddress.GetAddressBytes();
            return (uint)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);
        }

        /// <summary>
        /// Converts an integer to its IP address representation
        /// </summary>
        /// <param name="ipInt">The integer representation of the IP address</param>
        /// <returns>The IP address</returns>
        public static IPAddress UInt32ToIPAddress(uint ipInt)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)((ipInt >> 24) & 0xff);
            bytes[1] = (byte)((ipInt >> 16) & 0xff);
            bytes[2] = (byte)((ipInt >> 8) & 0xff);
            bytes[3] = (byte)(ipInt & 0xff);

            return new IPAddress(bytes);
        }

        /// <summary>
        /// Checks if an IP address is a private address
        /// </summary>
        /// <param name="ipAddress">The IP address to check</param>
        /// <returns>True if the IP address is private, false otherwise</returns>
        public static bool IsPrivate(IPAddress ipAddress)
        {
            if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // For IPv6, check if it's a unique local address (fc00::/7)
                byte[] bytes = ipAddress.GetAddressBytes();
                return (bytes[0] & 0xfe) == 0xfc;
            }

            uint ip = IPAddressToUInt32(ipAddress);

            // 10.0.0.0/8
            if ((ip & 0xff000000) == 0x0a000000)
                return true;

            // 172.16.0.0/12
            if ((ip & 0xfff00000) == 0xac100000)
                return true;

            // 192.168.0.0/16
            if ((ip & 0xffff0000) == 0xc0a80000)
                return true;

            // 169.254.0.0/16 (link-local)
            if ((ip & 0xffff0000) == 0xa9fe0000)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if an IP address is a loopback address
        /// </summary>
        /// <param name="ipAddress">The IP address to check</param>
        /// <returns>True if the IP address is a loopback address, false otherwise</returns>
        public static bool IsLoopback(IPAddress ipAddress)
        {
            return IPAddress.IsLoopback(ipAddress);
        }

        /// <summary>
        /// Checks if an IP address is a multicast address
        /// </summary>
        /// <param name="ipAddress">The IP address to check</param>
        /// <returns>True if the IP address is a multicast address, false otherwise</returns>
        public static bool IsMulticast(IPAddress ipAddress)
        {
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] bytes = ipAddress.GetAddressBytes();
                return (bytes[0] & 0xf0) == 0xe0; // 224.0.0.0/4
            }
            else
            {
                byte[] bytes = ipAddress.GetAddressBytes();
                return bytes[0] == 0xff; // ff00::/8
            }
        }
    }
}
