# Network Utilities

This component provides cmdlets for performing network calculations and conversions.

## Overview

The Network Utilities component includes cmdlets for working with IP networks, subnets, CIDR notation, and other network-related calculations. These utilities help with planning and managing network configurations on OPNSense firewalls.

## Cmdlets

### Invoke-OPNSenseNetworkCalculation

Performs various network calculations on IP networks.

#### Parameters

- **Network** - The network in CIDR notation to perform calculations on.
- **Operation** - The operation to perform. Valid values are:
  - **Info** - Shows detailed information about the network.
  - **Subnet** - Divides the network into subnets of a specified prefix length.
  - **SubnetByCount** - Divides the network into a specified number of subnets.
  - **Contains** - Checks if an IP address or network is contained within the specified network.
  - **Overlaps** - Checks if the network overlaps with other networks.
  - **Supernet** - Combines the network with other networks into a supernet.
  - **SupernetSummarize** - Summarizes multiple networks into the smallest possible set of networks.
- **PrefixLength** - The prefix length to use for subnetting.
- **SubnetCount** - The number of subnets to create when using SubnetByCount.
- **IpAddress** - The IP address to check when using Contains.
- **AdditionalNetworks** - Additional networks to use with Overlaps, Supernet, or SupernetSummarize.

#### Examples

```powershell
# Show network information
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Info

# Subnet a network into /27 networks
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation Subnet -PrefixLength 27

# Create 4 equal-sized subnets
Invoke-OPNSenseNetworkCalculation -Network "172.16.0.0/24" -Operation SubnetByCount -SubnetCount 4

# Check if an IP is within a network
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Contains -IpAddress "192.168.1.100"

# Check if networks overlap
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/16" -Operation Overlaps -AdditionalNetworks "10.0.1.0/24","10.0.2.0/24"

# Combine networks into a supernet
Invoke-OPNSenseNetworkCalculation -Network "192.168.0.0/24" -Operation Supernet -AdditionalNetworks "192.168.1.0/24"

# Summarize multiple networks
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation SupernetSummarize -AdditionalNetworks "10.0.1.0/24","10.0.2.0/24"
```

### ConvertTo-OPNSenseNetworkNotation

Converts between different network notation formats.

#### Parameters

- **CIDR** - The CIDR prefix length to convert to a subnet mask.
- **SubnetMask** - The subnet mask to convert to a CIDR prefix length.

#### Examples

```powershell
# Convert CIDR to subnet mask
ConvertTo-OPNSenseNetworkNotation -CIDR 24  # Returns "255.255.255.0"

# Convert subnet mask to CIDR
ConvertTo-OPNSenseNetworkNotation -SubnetMask "255.255.255.0"  # Returns 24
```

## Common Scenarios

### Network Planning

```powershell
# Plan a network subdivision
$network = "10.0.0.0/16"
$subnets = Invoke-OPNSenseNetworkCalculation -Network $network -Operation Subnet -PrefixLength 24
$subnets | Format-Table -Property CIDR, Network, Broadcast, FirstUsableHost, LastUsableHost, UsableHostCount

# Create equal-sized subnets
$departmentCount = 8
$subnets = Invoke-OPNSenseNetworkCalculation -Network "172.16.0.0/20" -Operation SubnetByCount -SubnetCount $departmentCount
$subnets | Format-Table -Property CIDR, Network, Broadcast, UsableHostCount
```

### Network Verification

```powershell
# Verify if an IP address is within a network
$serverIP = "192.168.1.50"
$network = "192.168.1.0/24"
$isInNetwork = Invoke-OPNSenseNetworkCalculation -Network $network -Operation Contains -IpAddress $serverIP

# Check if networks overlap
$network1 = "10.0.0.0/16"
$network2 = "10.0.5.0/24"
$overlaps = Invoke-OPNSenseNetworkCalculation -Network $network1 -Operation Overlaps -AdditionalNetworks $network2
```

### Network Optimization

```powershell
# Optimize a list of networks by summarizing them
$networks = @(
    "192.168.1.0/24",
    "192.168.2.0/24",
    "192.168.3.0/24",
    "192.168.4.0/24"
)
$optimized = Invoke-OPNSenseNetworkCalculation -Network $networks[0] -Operation SupernetSummarize -AdditionalNetworks $networks[1..($networks.Length-1)]
$optimized | Format-Table -Property CIDR, Network, Broadcast
```

### VLAN Planning

```powershell
# Plan VLANs with appropriate subnet sizes
$corporateNetwork = "10.0.0.0/16"
$vlans = @{
    "Management" = 10
    "Servers" = 20
    "Users" = 30
    "Guest" = 40
    "IoT" = 50
}

foreach ($vlan in $vlans.GetEnumerator()) {
    $vlanName = $vlan.Key
    $vlanId = $vlan.Value
    $prefix = switch ($vlanName) {
        "Management" { 24 }  # 254 hosts
        "Servers" { 23 }     # 510 hosts
        "Users" { 22 }       # 1022 hosts
        "Guest" { 24 }       # 254 hosts
        "IoT" { 23 }         # 510 hosts
    }
    
    # Calculate network details
    $subnet = Invoke-OPNSenseNetworkCalculation -Network $corporateNetwork -Operation Subnet -PrefixLength $prefix | Select-Object -First 1
    
    Write-Output "VLAN $vlanId ($vlanName): $($subnet.CIDR) - $($subnet.UsableHostCount) usable hosts"
}
```

## Notes

- The Network Utilities component uses the IPNetwork2 library for network calculations.
- All operations are performed locally and do not require communication with the OPNSense firewall.
- These utilities can be used for planning network configurations before implementing them on the firewall.
- When working with large networks, be aware that some operations (like subnetting a /8 network into /30 networks) may generate a large number of results.
