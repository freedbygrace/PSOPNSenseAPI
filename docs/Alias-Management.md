# Alias Management

This document describes how to use the alias management cmdlets in the PSOPNSenseAPI module.

## Overview

Aliases in OPNSense are named lists of networks, hosts, ports, or URLs that can be used in firewall rules. Using aliases makes it easier to manage firewall rules, as you can update the alias content without having to modify the rules that use it.

The PSOPNSenseAPI module provides cmdlets for managing aliases:

- `Get-OPNSenseAlias`: Retrieves aliases from an OPNSense firewall
- `New-OPNSenseAlias`: Creates a new alias on an OPNSense firewall
- `Set-OPNSenseAlias`: Updates an alias on an OPNSense firewall
- `Remove-OPNSenseAlias`: Removes an alias from an OPNSense firewall

## Alias Types

OPNSense supports several types of aliases:

- `host`: A list of IP addresses
- `network`: A list of networks in CIDR notation
- `port`: A list of port numbers
- `url`: A URL that points to a list of IPs or networks
- `urltable`: A URL that points to a table of IPs or networks
- `geoip`: A list of country codes for GeoIP filtering
- `networkgroup`: A group of networks
- `mac`: A list of MAC addresses
- `interface`: A list of network interfaces
- `dynipv6host`: A list of dynamic IPv6 hosts
- `internal`: Special internal aliases
- `external`: Special external aliases

## Examples

### Getting Aliases

```powershell
# Get all aliases
Get-OPNSenseAlias

# Get a specific alias by UUID
Get-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"

# Get a specific alias by name
Get-OPNSenseAlias -Name "WebServers"

# Get all host aliases
Get-OPNSenseAlias -Type host

# Get all port aliases
Get-OPNSenseAlias -Type port
```

### Creating Aliases

```powershell
# Create a host alias
New-OPNSenseAlias -Name "WebServers" -Type host -Content "192.168.1.10,192.168.1.11" -Description "Web Servers" -Apply

# Create a network alias
New-OPNSenseAlias -Name "InternalNetworks" -Type network -Content "192.168.1.0/24,192.168.2.0/24" -Description "Internal Networks" -Apply

# Create a port alias
New-OPNSenseAlias -Name "WebPorts" -Type port -Content "80,443" -Protocol TCP -Description "Web Ports" -Apply

# Create a URL alias
New-OPNSenseAlias -Name "BlockList" -Type url -Content "https://example.com/blocklist.txt" -UpdateFrequency 1 -Description "Block List" -Apply
```

### Updating Aliases

```powershell
# Update an alias's content
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Content "192.168.1.10,192.168.1.11,192.168.1.12" -Apply

# Update an alias's description
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Description "Updated description" -Apply

# Disable an alias
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Disabled -Apply

# Enable an alias
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Enabled -Apply

# Enable counters for an alias
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -EnableCounters -Apply
```

### Removing Aliases

```powershell
# Remove an alias by UUID
Remove-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply

# Remove an alias with confirmation
Remove-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply

# Remove an alias without confirmation
Remove-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Force -Apply

# Remove aliases by pipeline
Get-OPNSenseAlias -Type host | Remove-OPNSenseAlias -Force -Apply
```

## Using Aliases in Firewall Rules

Once you have created aliases, you can use them in firewall rules:

```powershell
# Create a firewall rule using aliases
New-OPNSenseFirewallRule -Description "Allow Web Traffic" -Protocol TCP -SourceNet "InternalNetworks" -DestinationPort "WebPorts" -Action pass -Apply
```

## Best Practices

1. **Use descriptive names**: Choose alias names that clearly indicate their purpose.
2. **Use aliases for groups of objects**: Aliases are most useful when they group related objects.
3. **Keep aliases organized**: Use a consistent naming convention for aliases.
4. **Document aliases**: Use the description field to document the purpose of the alias.
5. **Apply changes**: Always use the `-Apply` parameter to apply changes immediately, or apply them later using `Apply-OPNSenseFirewallChanges`.
