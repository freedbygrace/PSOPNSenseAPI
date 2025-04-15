# Interface Management

This component provides cmdlets for managing network interfaces on OPNSense firewalls.

## Overview

The Interface Management component allows you to configure and manage network interfaces on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting interfaces, as well as configuring interface settings.

## Cmdlets

### Get-OPNSenseInterface

Retrieves interface configurations from an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific interface to retrieve. If not specified, all interfaces are returned.
- **Type** - Filter interfaces by type (e.g., physical, vlan, bridge).
- **Enabled** - Filter interfaces by enabled status.

#### Examples

```powershell
# Get all interfaces
Get-OPNSenseInterface

# Get a specific interface by name
Get-OPNSenseInterface -Name "lan"

# Get all VLAN interfaces
Get-OPNSenseInterface -Type "vlan"

# Get all enabled interfaces
Get-OPNSenseInterface -Enabled $true
```

### New-OPNSenseInterface

Creates a new interface on an OPNSense firewall.

#### Parameters

- **Name** - The name of the interface.
- **Description** - A description for the interface.
- **Type** - The type of the interface (e.g., physical, vlan, bridge).
- **Device** - The physical device for the interface.
- **IPv4Address** - The IPv4 address for the interface.
- **IPv4Subnet** - The IPv4 subnet mask for the interface.
- **IPv6Address** - The IPv6 address for the interface.
- **IPv6Subnet** - The IPv6 subnet mask for the interface.
- **Enabled** - Whether the interface is enabled. Default is true.
- **BlockPrivate** - Whether to block private networks. Default is false.
- **BlockBogons** - Whether to block bogon networks. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic interface
New-OPNSenseInterface -Name "opt1" -Description "Optional Interface 1" -Type "physical" -Device "em1" -IPv4Address "192.168.2.1" -IPv4Subnet "24"

# Create a VLAN interface
New-OPNSenseInterface -Name "vlan10" -Description "VLAN 10" -Type "vlan" -Device "em0.10" -IPv4Address "10.0.10.1" -IPv4Subnet "24"

# Create an interface with IPv6
New-OPNSenseInterface -Name "opt2" -Description "Optional Interface 2" -Type "physical" -Device "em2" -IPv4Address "192.168.3.1" -IPv4Subnet "24" -IPv6Address "2001:db8:1::1" -IPv6Subnet "64"

# Create an interface with security options
New-OPNSenseInterface -Name "dmz" -Description "DMZ Interface" -Type "physical" -Device "em3" -IPv4Address "192.168.100.1" -IPv4Subnet "24" -BlockPrivate -BlockBogons
```

### Set-OPNSenseInterface

Updates an existing interface on an OPNSense firewall.

#### Parameters

- **Name** - The name of the interface to update.
- **Description** - A description for the interface.
- **IPv4Address** - The IPv4 address for the interface.
- **IPv4Subnet** - The IPv4 subnet mask for the interface.
- **IPv6Address** - The IPv6 address for the interface.
- **IPv6Subnet** - The IPv6 subnet mask for the interface.
- **Enabled** - Whether the interface is enabled.
- **BlockPrivate** - Whether to block private networks.
- **BlockBogons** - Whether to block bogon networks.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated interface.

#### Examples

```powershell
# Update an interface's description
Set-OPNSenseInterface -Name "lan" -Description "Updated LAN Interface"

# Update an interface's IPv4 address
Set-OPNSenseInterface -Name "opt1" -IPv4Address "192.168.2.254" -IPv4Subnet "24"

# Update an interface's IPv6 address
Set-OPNSenseInterface -Name "opt2" -IPv6Address "2001:db8:2::1" -IPv6Subnet "64"

# Enable security options for an interface
Set-OPNSenseInterface -Name "dmz" -BlockPrivate -BlockBogons

# Disable an interface
Set-OPNSenseInterface -Name "opt3" -Enabled:$false

# Update multiple properties of an interface
Set-OPNSenseInterface -Name "lan" -Description "Main LAN Interface" -IPv4Address "192.168.1.254" -IPv4Subnet "24" -BlockBogons -PassThru
```

### Remove-OPNSenseInterface

Removes an interface from an OPNSense firewall.

#### Parameters

- **Name** - The name of the interface to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove an interface
Remove-OPNSenseInterface -Name "opt3"

# Remove an interface without confirmation
Remove-OPNSenseInterface -Name "vlan20" -Force
```

### Get-OPNSenseInterfaceStatistics

Retrieves interface statistics from an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific interface to get statistics for. If not specified, statistics for all interfaces are returned.

#### Examples

```powershell
# Get statistics for all interfaces
Get-OPNSenseInterfaceStatistics

# Get statistics for a specific interface
Get-OPNSenseInterfaceStatistics -Name "lan"
```

### Apply-OPNSenseInterfaceChanges

Applies pending interface changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply interface changes
Apply-OPNSenseInterfaceChanges

# Apply interface changes without confirmation
Apply-OPNSenseInterfaceChanges -Force
```

## Common Scenarios

### Basic Interface Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Configure the LAN interface
Set-OPNSenseInterface -Name "lan" -Description "Main LAN Interface" -IPv4Address "192.168.1.1" -IPv4Subnet "24" -Force

# Configure the WAN interface
Set-OPNSenseInterface -Name "wan" -Description "WAN Interface" -BlockPrivate -BlockBogons -Force

# Apply the changes
Apply-OPNSenseInterfaceChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Creating VLAN Interfaces

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define VLAN interfaces
$vlans = @(
    @{ Name = "vlan10"; Description = "Management VLAN"; Device = "em0.10"; IPv4Address = "10.0.10.1"; IPv4Subnet = "24" },
    @{ Name = "vlan20"; Description = "User VLAN"; Device = "em0.20"; IPv4Address = "10.0.20.1"; IPv4Subnet = "24" },
    @{ Name = "vlan30"; Description = "Guest VLAN"; Device = "em0.30"; IPv4Address = "10.0.30.1"; IPv4Subnet = "24" },
    @{ Name = "vlan40"; Description = "IoT VLAN"; Device = "em0.40"; IPv4Address = "10.0.40.1"; IPv4Subnet = "24" }
)

# Create VLAN interfaces
foreach ($vlan in $vlans) {
    New-OPNSenseInterface -Name $vlan.Name -Description $vlan.Description -Type "vlan" -Device $vlan.Device -IPv4Address $vlan.IPv4Address -IPv4Subnet $vlan.IPv4Subnet -Force
}

# Apply the changes
Apply-OPNSenseInterfaceChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Configuring Dual-Stack IPv4/IPv6 Interfaces

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define dual-stack interfaces
$interfaces = @(
    @{ Name = "lan"; Description = "LAN Interface"; IPv4Address = "192.168.1.1"; IPv4Subnet = "24"; IPv6Address = "2001:db8:1::1"; IPv6Subnet = "64" },
    @{ Name = "opt1"; Description = "Optional Interface 1"; IPv4Address = "192.168.2.1"; IPv4Subnet = "24"; IPv6Address = "2001:db8:2::1"; IPv6Subnet = "64" },
    @{ Name = "dmz"; Description = "DMZ Interface"; IPv4Address = "192.168.100.1"; IPv4Subnet = "24"; IPv6Address = "2001:db8:100::1"; IPv6Subnet = "64" }
)

# Configure dual-stack interfaces
foreach ($interface in $interfaces) {
    Set-OPNSenseInterface -Name $interface.Name -Description $interface.Description -IPv4Address $interface.IPv4Address -IPv4Subnet $interface.IPv4Subnet -IPv6Address $interface.IPv6Address -IPv6Subnet $interface.IPv6Subnet -Force
}

# Apply the changes
Apply-OPNSenseInterfaceChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Monitoring Interface Statistics

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get interface statistics
$statistics = Get-OPNSenseInterfaceStatistics

# Display interface statistics
$statistics | Format-Table -Property Name, Status, InBytes, OutBytes, InPackets, OutPackets, Errors

# Check for interfaces with errors
$interfacesWithErrors = $statistics | Where-Object { $_.Errors -gt 0 }
if ($interfacesWithErrors) {
    Write-Output "The following interfaces have errors:"
    $interfacesWithErrors | Format-Table -Property Name, Status, Errors
}

# Disconnect from the firewall
Disconnect-OPNSense
```

### Securing Interfaces

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all interfaces
$interfaces = Get-OPNSenseInterface

# Enable security options for external interfaces
foreach ($interface in $interfaces) {
    if ($interface.Name -eq "wan" -or $interface.Name -like "opt*") {
        Set-OPNSenseInterface -Name $interface.Name -BlockPrivate -BlockBogons -Force
        Write-Output "Enabled security options for interface $($interface.Name)"
    }
}

# Apply the changes
Apply-OPNSenseInterfaceChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Interface changes are not applied until you call `Apply-OPNSenseInterfaceChanges`.
- When creating or updating interfaces, ensure that the IP addresses do not conflict with other interfaces.
- The `BlockPrivate` option blocks traffic from private networks (RFC 1918) on the interface.
- The `BlockBogons` option blocks traffic from bogon networks (unallocated or reserved IP space) on the interface.
- Interface names should follow OPNSense conventions (e.g., "lan", "wan", "opt1", "vlan10").
- VLAN interfaces require the parent interface to be properly configured.
- Consider using the `-PassThru` parameter when updating interfaces to verify the changes.
- Interface statistics can be useful for monitoring network traffic and identifying issues.
- When removing an interface, ensure that no other configurations (e.g., firewall rules, NAT rules) reference the interface.
- IPv6 addresses should be specified in the standard format (e.g., "2001:db8::1").
- IPv4 subnet masks can be specified in CIDR notation (e.g., "24" for "255.255.255.0") or dotted decimal notation (e.g., "255.255.255.0").
