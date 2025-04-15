# DHCP Management

This component provides cmdlets for managing DHCP servers and leases on OPNSense firewalls.

## Overview

The DHCP Management component allows you to configure and manage DHCP servers on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting DHCP servers, as well as managing DHCP leases and static mappings.

## Cmdlets

### Get-OPNSenseDHCPServer

Retrieves DHCP server configurations from an OPNSense firewall.

#### Parameters

- **Interface** - The interface to get the DHCP server configuration for. If not specified, all DHCP servers are returned.

#### Examples

```powershell
# Get all DHCP servers
Get-OPNSenseDHCPServer

# Get the DHCP server for a specific interface
Get-OPNSenseDHCPServer -Interface "lan"
```

### New-OPNSenseDHCPServer

Creates a new DHCP server on an OPNSense firewall.

#### Parameters

- **Interface** - The interface to create the DHCP server on.
- **Enabled** - Whether the DHCP server is enabled. Default is true.
- **RangeFrom** - The starting IP address of the DHCP range.
- **RangeTo** - The ending IP address of the DHCP range.
- **Domain** - The domain name to provide to DHCP clients.
- **DnsServers** - The DNS servers to provide to DHCP clients.
- **GatewayIP** - The gateway IP address to provide to DHCP clients.
- **LeaseTime** - The lease time in seconds. Default is 86400 (1 day).
- **DenyUnknownClients** - Whether to deny unknown clients. Default is false.
- **IgnoreClientIdentifiers** - Whether to ignore client identifiers. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic DHCP server
New-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -Domain "example.com" -DnsServers "192.168.1.1","8.8.8.8" -GatewayIP "192.168.1.1"

# Create a DHCP server with advanced options
New-OPNSenseDHCPServer -Interface "opt1" -RangeFrom "10.0.0.100" -RangeTo "10.0.0.200" -Domain "internal.example.com" -DnsServers "10.0.0.1","8.8.4.4" -GatewayIP "10.0.0.1" -LeaseTime 43200 -DenyUnknownClients -Force
```

### Set-OPNSenseDHCPServer

Updates an existing DHCP server on an OPNSense firewall.

#### Parameters

- **Interface** - The interface of the DHCP server to update.
- **Enabled** - Whether the DHCP server is enabled.
- **RangeFrom** - The starting IP address of the DHCP range.
- **RangeTo** - The ending IP address of the DHCP range.
- **Domain** - The domain name to provide to DHCP clients.
- **DnsServers** - The DNS servers to provide to DHCP clients.
- **GatewayIP** - The gateway IP address to provide to DHCP clients.
- **LeaseTime** - The lease time in seconds.
- **DenyUnknownClients** - Whether to deny unknown clients.
- **IgnoreClientIdentifiers** - Whether to ignore client identifiers.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DHCP server.

#### Examples

```powershell
# Update a DHCP server's range
Set-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.50" -RangeTo "192.168.1.150"

# Update a DHCP server's DNS servers
Set-OPNSenseDHCPServer -Interface "opt1" -DnsServers "10.0.0.1","1.1.1.1"

# Update multiple properties of a DHCP server
Set-OPNSenseDHCPServer -Interface "lan" -Domain "updated.example.com" -LeaseTime 43200 -DenyUnknownClients -PassThru
```

### Remove-OPNSenseDHCPServer

Removes a DHCP server from an OPNSense firewall.

#### Parameters

- **Interface** - The interface of the DHCP server to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a DHCP server
Remove-OPNSenseDHCPServer -Interface "opt1"

# Remove a DHCP server without confirmation
Remove-OPNSenseDHCPServer -Interface "opt1" -Force
```

### Get-OPNSenseDHCPLease

Retrieves DHCP leases from an OPNSense firewall.

#### Parameters

- **Interface** - The interface to get DHCP leases for. If not specified, leases for all interfaces are returned.
- **MACAddress** - Filter leases by MAC address.
- **IPAddress** - Filter leases by IP address.
- **Hostname** - Filter leases by hostname.
- **Status** - Filter leases by status (active, expired, etc.).

#### Examples

```powershell
# Get all DHCP leases
Get-OPNSenseDHCPLease

# Get DHCP leases for a specific interface
Get-OPNSenseDHCPLease -Interface "lan"

# Get DHCP leases for a specific MAC address
Get-OPNSenseDHCPLease -MACAddress "00:11:22:33:44:55"

# Get active DHCP leases
Get-OPNSenseDHCPLease -Status "active"
```

### New-OPNSenseDHCPStaticMapping

Creates a new DHCP static mapping on an OPNSense firewall.

#### Parameters

- **Interface** - The interface to create the static mapping on.
- **MACAddress** - The MAC address of the client.
- **IPAddress** - The IP address to assign to the client.
- **Hostname** - The hostname of the client.
- **Description** - A description for the static mapping.
- **Enabled** - Whether the static mapping is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic static mapping
New-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress "00:11:22:33:44:55" -IPAddress "192.168.1.50" -Hostname "printer" -Description "Office Printer"

# Create a static mapping without confirmation
New-OPNSenseDHCPStaticMapping -Interface "opt1" -MACAddress "AA:BB:CC:DD:EE:FF" -IPAddress "10.0.0.10" -Hostname "server" -Description "File Server" -Force
```

### Set-OPNSenseDHCPStaticMapping

Updates an existing DHCP static mapping on an OPNSense firewall.

#### Parameters

- **Interface** - The interface of the static mapping to update.
- **MACAddress** - The MAC address of the static mapping to update.
- **IPAddress** - The IP address to assign to the client.
- **Hostname** - The hostname of the client.
- **Description** - A description for the static mapping.
- **Enabled** - Whether the static mapping is enabled.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated static mapping.

#### Examples

```powershell
# Update a static mapping's IP address
Set-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress "00:11:22:33:44:55" -IPAddress "192.168.1.60"

# Update a static mapping's description
Set-OPNSenseDHCPStaticMapping -Interface "opt1" -MACAddress "AA:BB:CC:DD:EE:FF" -Description "Updated File Server"

# Update multiple properties of a static mapping
Set-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress "00:11:22:33:44:55" -IPAddress "192.168.1.70" -Hostname "new-printer" -Description "New Office Printer" -PassThru
```

### Remove-OPNSenseDHCPStaticMapping

Removes a DHCP static mapping from an OPNSense firewall.

#### Parameters

- **Interface** - The interface of the static mapping to remove.
- **MACAddress** - The MAC address of the static mapping to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a static mapping
Remove-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress "00:11:22:33:44:55"

# Remove a static mapping without confirmation
Remove-OPNSenseDHCPStaticMapping -Interface "opt1" -MACAddress "AA:BB:CC:DD:EE:FF" -Force
```

### Apply-OPNSenseDHCPChanges

Applies pending DHCP changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply DHCP changes
Apply-OPNSenseDHCPChanges

# Apply DHCP changes without confirmation
Apply-OPNSenseDHCPChanges -Force
```

## Common Scenarios

### Basic DHCP Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a DHCP server for the LAN interface
New-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -Domain "example.com" -DnsServers "192.168.1.1","8.8.8.8" -GatewayIP "192.168.1.1" -Force

# Apply the changes
Apply-OPNSenseDHCPChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Static Mappings

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create static mappings for important devices
$staticMappings = @(
    @{ MACAddress = "00:11:22:33:44:55"; IPAddress = "192.168.1.10"; Hostname = "printer"; Description = "Office Printer" },
    @{ MACAddress = "AA:BB:CC:DD:EE:FF"; IPAddress = "192.168.1.20"; Hostname = "fileserver"; Description = "File Server" },
    @{ MACAddress = "11:22:33:44:55:66"; IPAddress = "192.168.1.30"; Hostname = "nas"; Description = "Network Storage" }
)

foreach ($mapping in $staticMappings) {
    New-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress $mapping.MACAddress -IPAddress $mapping.IPAddress -Hostname $mapping.Hostname -Description $mapping.Description -Force
}

# Apply the changes
Apply-OPNSenseDHCPChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Converting DHCP Leases to Static Mappings

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get active DHCP leases
$leases = Get-OPNSenseDHCPLease -Interface "lan" -Status "active"

# Convert leases to static mappings
foreach ($lease in $leases) {
    New-OPNSenseDHCPStaticMapping -Interface "lan" -MACAddress $lease.MACAddress -IPAddress $lease.IPAddress -Hostname $lease.Hostname -Description "Converted from lease on $(Get-Date -Format 'yyyy-MM-dd')" -Force
}

# Apply the changes
Apply-OPNSenseDHCPChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Multiple DHCP Servers

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define DHCP server configurations for different interfaces
$dhcpConfigs = @(
    @{
        Interface = "lan";
        RangeFrom = "192.168.1.100";
        RangeTo = "192.168.1.200";
        Domain = "internal.example.com";
        DnsServers = @("192.168.1.1", "8.8.8.8");
        GatewayIP = "192.168.1.1";
        LeaseTime = 86400;
    },
    @{
        Interface = "opt1";
        RangeFrom = "10.0.0.100";
        RangeTo = "10.0.0.200";
        Domain = "guest.example.com";
        DnsServers = @("10.0.0.1", "8.8.4.4");
        GatewayIP = "10.0.0.1";
        LeaseTime = 3600;
        DenyUnknownClients = $true;
    },
    @{
        Interface = "opt2";
        RangeFrom = "172.16.0.100";
        RangeTo = "172.16.0.200";
        Domain = "iot.example.com";
        DnsServers = @("172.16.0.1");
        GatewayIP = "172.16.0.1";
        LeaseTime = 86400;
    }
)

# Create or update DHCP servers
foreach ($config in $dhcpConfigs) {
    $existing = Get-OPNSenseDHCPServer -Interface $config.Interface
    
    if ($existing) {
        # Update existing DHCP server
        $params = @{
            Interface = $config.Interface
            RangeFrom = $config.RangeFrom
            RangeTo = $config.RangeTo
            Domain = $config.Domain
            DnsServers = $config.DnsServers
            GatewayIP = $config.GatewayIP
            LeaseTime = $config.LeaseTime
            Force = $true
        }
        
        if ($config.DenyUnknownClients) {
            $params.Add("DenyUnknownClients", $true)
        }
        
        Set-OPNSenseDHCPServer @params
        Write-Output "Updated DHCP server for interface $($config.Interface)"
    }
    else {
        # Create new DHCP server
        $params = @{
            Interface = $config.Interface
            RangeFrom = $config.RangeFrom
            RangeTo = $config.RangeTo
            Domain = $config.Domain
            DnsServers = $config.DnsServers
            GatewayIP = $config.GatewayIP
            LeaseTime = $config.LeaseTime
            Force = $true
        }
        
        if ($config.DenyUnknownClients) {
            $params.Add("DenyUnknownClients", $true)
        }
        
        New-OPNSenseDHCPServer @params
        Write-Output "Created DHCP server for interface $($config.Interface)"
    }
}

# Apply the changes
Apply-OPNSenseDHCPChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- DHCP servers are configured per interface on OPNSense firewalls.
- Changes to DHCP servers and static mappings are not applied until you call `Apply-OPNSenseDHCPChanges`.
- When creating a DHCP server, ensure that the range is within the subnet of the interface.
- Static mappings take precedence over dynamic leases.
- The `DenyUnknownClients` option only allows clients with static mappings to receive DHCP leases.
- The `IgnoreClientIdentifiers` option can help with clients that change their identifier between requests.
- DHCP lease times are specified in seconds. Common values are:
  - 3600 (1 hour)
  - 43200 (12 hours)
  - 86400 (1 day)
  - 604800 (1 week)
- Consider using the `-PassThru` parameter when updating DHCP servers or static mappings to verify the changes.
