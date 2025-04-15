# IPNetwork2 Integration

This document provides information about the integration of the IPNetwork2 library in the PSOPNSenseAPI module.

## Overview

The PSOPNSenseAPI module uses the IPNetwork2 library for IP network calculations and manipulations. This library provides functionality for working with IP networks, subnets, CIDR notation, and other network-related operations.

## IPNetwork2 Library

The IPNetwork2 library is a .NET library that provides classes and methods for working with IP networks. It supports both IPv4 and IPv6 networks and provides functionality for:

- Parsing CIDR notation
- Subnetting
- Supernetting
- Network calculations
- IP address manipulation
- Network containment and overlap checks

## Integration in PSOPNSenseAPI

The IPNetwork2 library is integrated into the PSOPNSenseAPI module in the following ways:

1. **NetworkUtility Class**: The `NetworkUtility` class in the `PSOPNSenseAPI.Utilities` namespace provides wrapper methods for the IPNetwork2 functionality.

2. **Network Calculation Cmdlets**: The `Invoke-OPNSenseNetworkCalculation` cmdlet exposes the IPNetwork2 functionality to PowerShell users.

3. **VLAN and Subnet Management**: The `New-OPNSenseSubnetVLANs` cmdlet uses IPNetwork2 for subnet calculations when creating VLANs from subnet divisions.

## Using IPNetwork2 in the Code

When using IPNetwork2 in the code, it's important to use the correct namespace and class name:

```csharp
using System.Net;

// Parse a CIDR notation string
IPNetwork2 network = IPNetwork2.Parse("192.168.1.0/24");

// Get network properties
IPAddress networkAddress = network.Network;
IPAddress broadcastAddress = network.Broadcast;
int cidrPrefix = network.Cidr;
BigInteger totalHosts = network.Total;

// Subnet a network
IEnumerable<IPNetwork2> subnets = IPNetwork2.Subnet(network, 27);

// Check if an IP address is in a network
bool contains = network.Contains(IPAddress.Parse("192.168.1.100"));

// Check if networks overlap
bool overlaps = network1.Overlap(network2);

// Create a supernet
IPNetwork2[] supernet = IPNetwork2.Supernet(new[] { network1, network2 });
```

## Common Issues and Solutions

### Namespace and Class Name

The most common issue when working with IPNetwork2 is using the incorrect namespace or class name. The correct usage is:

```csharp
using System.Net;

// Use the class name directly
IPNetwork2 network = IPNetwork2.Parse("192.168.1.0/24");

// Or use the fully qualified name for clarity
System.Net.IPNetwork2 network = System.Net.IPNetwork2.Parse("192.168.1.0/24");
```

### Version Compatibility

Ensure that you're using a compatible version of IPNetwork2. The PSOPNSenseAPI module uses version 3.1.764, which is the latest version as of April 2025.

```xml
<PackageReference Include="IPNetwork2" Version="3.1.764" />
```

This version supports .NET 8.0, .NET 9.0, .NET Standard 2.0, and .NET Standard 2.1.

### Method Signatures

Be aware of the method signatures when using IPNetwork2. For example, the `Subnet` method has multiple overloads:

```csharp
// Subnet by prefix length
IEnumerable<IPNetwork2> Subnet(IPNetwork2 network, int prefixLength);

// Subnet by subnet count
IEnumerable<IPNetwork2> Subnet(IPNetwork2 network, BigInteger subnetCount);

// Subnet by target network
IEnumerable<IPNetwork2> Subnet(IPNetwork2 network, IPNetwork2 targetNetwork);
```

### Memory Usage

When working with large networks (e.g., /8 networks) and small subnets (e.g., /30), be aware that operations like subnetting can generate a large number of results and consume significant memory.

## Exposing IPNetwork2 Functionality to PowerShell

The PSOPNSenseAPI module exposes IPNetwork2 functionality to PowerShell users through the `Invoke-OPNSenseNetworkCalculation` cmdlet:

```powershell
# Show network information
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Info

# Subnet a network
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation Subnet -PrefixLength 27

# Check if networks overlap
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/16" -Operation Overlaps -AdditionalNetworks "10.0.1.0/24"
```

## Advanced Usage

### Custom Network Utility Methods

The `NetworkUtility` class provides additional methods that extend the functionality of IPNetwork2:

```csharp
// Get the first usable host address in a network
IPAddress firstHost = NetworkUtility.GetFirstUsableHost(network);

// Get the last usable host address in a network
IPAddress lastHost = NetworkUtility.GetLastUsableHost(network);

// Get the number of usable host addresses in a network
BigInteger usableHosts = NetworkUtility.GetUsableHostCount(network);

// Convert between CIDR and subnet mask
int cidr = NetworkUtility.GetCIDRFromSubnetMask("255.255.255.0");
string subnetMask = NetworkUtility.GetSubnetMaskFromCIDR(24);
```

### Network Summarization

The `SupernetSummarize` method provides advanced network summarization:

```csharp
// Summarize multiple networks into the smallest possible set
List<IPNetwork2> networks = new List<IPNetwork2>
{
    IPNetwork2.Parse("192.168.1.0/24"),
    IPNetwork2.Parse("192.168.2.0/24"),
    IPNetwork2.Parse("192.168.3.0/24"),
    IPNetwork2.Parse("192.168.4.0/24")
};

List<IPNetwork2> summarized = NetworkUtility.SupernetSummarize(networks);
// Result: 192.168.0.0/22
```

## Notes

- The IPNetwork2 library is a third-party library and is subject to its own licensing terms.
- The PSOPNSenseAPI module includes the IPNetwork2 library as a NuGet package dependency.
- The IPNetwork2 library is used for local network calculations and does not require communication with the OPNSense firewall.
- Version 3.1.764 of IPNetwork2 has moved the `IPNetwork2` class to the `System.Net` namespace, which is different from earlier versions that used the `System.Net.IPNetwork` namespace.
- When upgrading the IPNetwork2 library, ensure that all code is updated to use the correct namespace and class names.
- Version 3.1.764 supports .NET 8.0, .NET 9.0, .NET Standard 2.0, and .NET Standard 2.1.
- For .NET Standard 2.0 and 2.1, IPNetwork2 has a dependency on System.Memory (>= 4.5.5).
