# Gateway Management

This component provides cmdlets for managing gateways on OPNSense firewalls.

## Overview

The Gateway Management component allows you to configure and manage gateways on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting gateways, as well as monitoring gateway status.

## Cmdlets

### Get-OPNSenseGateway

Retrieves gateway configurations from an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific gateway to retrieve. If not specified, all gateways are returned.
- **Interface** - Filter gateways by interface.
- **Protocol** - Filter gateways by protocol (IPv4, IPv6).

#### Examples

```powershell
# Get all gateways
Get-OPNSenseGateway

# Get a specific gateway by name
Get-OPNSenseGateway -Name "WAN_GATEWAY"

# Get all gateways for a specific interface
Get-OPNSenseGateway -Interface "wan"

# Get all IPv4 gateways
Get-OPNSenseGateway -Protocol "IPv4"
```

### New-OPNSenseGateway

Creates a new gateway on an OPNSense firewall.

#### Parameters

- **Name** - The name of the gateway.
- **Interface** - The interface for the gateway.
- **IPAddress** - The IP address of the gateway.
- **Description** - A description for the gateway.
- **Enabled** - Whether the gateway is enabled. Default is true.
- **Monitor** - The IP address to monitor for gateway status.
- **MonitorDisable** - Whether to disable monitoring for this gateway. Default is false.
- **Weight** - The weight of the gateway for load balancing. Default is 1.
- **Protocol** - The protocol for the gateway (IPv4, IPv6). Default is IPv4.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic gateway
New-OPNSenseGateway -Name "WAN2_GATEWAY" -Interface "opt1" -IPAddress "203.0.113.1" -Description "Secondary WAN Gateway"

# Create a gateway with monitoring
New-OPNSenseGateway -Name "WAN3_GATEWAY" -Interface "opt2" -IPAddress "198.51.100.1" -Description "Tertiary WAN Gateway" -Monitor "198.51.100.254"

# Create a gateway with load balancing weight
New-OPNSenseGateway -Name "LB_GATEWAY" -Interface "opt3" -IPAddress "192.0.2.1" -Description "Load Balanced Gateway" -Weight 2

# Create an IPv6 gateway
New-OPNSenseGateway -Name "WAN_IPv6_GATEWAY" -Interface "wan" -IPAddress "2001:db8::1" -Description "IPv6 WAN Gateway" -Protocol "IPv6"
```

### Set-OPNSenseGateway

Updates an existing gateway on an OPNSense firewall.

#### Parameters

- **Name** - The name of the gateway to update.
- **Interface** - The interface for the gateway.
- **IPAddress** - The IP address of the gateway.
- **Description** - A description for the gateway.
- **Enabled** - Whether the gateway is enabled.
- **Monitor** - The IP address to monitor for gateway status.
- **MonitorDisable** - Whether to disable monitoring for this gateway.
- **Weight** - The weight of the gateway for load balancing.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated gateway.

#### Examples

```powershell
# Update a gateway's IP address
Set-OPNSenseGateway -Name "WAN_GATEWAY" -IPAddress "203.0.113.2"

# Update a gateway's description
Set-OPNSenseGateway -Name "WAN2_GATEWAY" -Description "Updated Secondary WAN Gateway"

# Update a gateway's monitoring settings
Set-OPNSenseGateway -Name "WAN3_GATEWAY" -Monitor "198.51.100.253"

# Disable a gateway
Set-OPNSenseGateway -Name "LB_GATEWAY" -Enabled:$false

# Update multiple properties of a gateway
Set-OPNSenseGateway -Name "WAN_GATEWAY" -IPAddress "203.0.113.3" -Description "Updated WAN Gateway" -Weight 3 -PassThru
```

### Remove-OPNSenseGateway

Removes a gateway from an OPNSense firewall.

#### Parameters

- **Name** - The name of the gateway to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a gateway
Remove-OPNSenseGateway -Name "WAN2_GATEWAY"

# Remove a gateway without confirmation
Remove-OPNSenseGateway -Name "WAN3_GATEWAY" -Force
```

### Get-OPNSenseGatewayStatus

Retrieves the status of gateways on an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific gateway to get the status for. If not specified, the status of all gateways is returned.

#### Examples

```powershell
# Get the status of all gateways
Get-OPNSenseGatewayStatus

# Get the status of a specific gateway
Get-OPNSenseGatewayStatus -Name "WAN_GATEWAY"
```

### Apply-OPNSenseGatewayChanges

Applies pending gateway changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply gateway changes
Apply-OPNSenseGatewayChanges

# Apply gateway changes without confirmation
Apply-OPNSenseGatewayChanges -Force
```

## Common Scenarios

### Basic Gateway Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a primary WAN gateway
New-OPNSenseGateway -Name "WAN_GATEWAY" -Interface "wan" -IPAddress "203.0.113.1" -Description "Primary WAN Gateway" -Monitor "8.8.8.8" -Force

# Apply the changes
Apply-OPNSenseGatewayChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Multi-WAN Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create gateways for multiple WAN connections
$wanGateways = @(
    @{ Name = "WAN1_GATEWAY"; Interface = "wan"; IPAddress = "203.0.113.1"; Description = "Primary WAN Gateway"; Monitor = "8.8.8.8"; Weight = 1 },
    @{ Name = "WAN2_GATEWAY"; Interface = "opt1"; IPAddress = "198.51.100.1"; Description = "Secondary WAN Gateway"; Monitor = "8.8.4.4"; Weight = 2 },
    @{ Name = "WAN3_GATEWAY"; Interface = "opt2"; IPAddress = "192.0.2.1"; Description = "Tertiary WAN Gateway"; Monitor = "1.1.1.1"; Weight = 3 }
)

foreach ($gateway in $wanGateways) {
    New-OPNSenseGateway -Name $gateway.Name -Interface $gateway.Interface -IPAddress $gateway.IPAddress -Description $gateway.Description -Monitor $gateway.Monitor -Weight $gateway.Weight -Force
}

# Apply the changes
Apply-OPNSenseGatewayChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Monitoring Gateway Status

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the status of all gateways
$gatewayStatus = Get-OPNSenseGatewayStatus

# Display gateway status
$gatewayStatus | Format-Table -Property Name, Status, RTT, Loss

# Check for down gateways
$downGateways = $gatewayStatus | Where-Object { $_.Status -ne "online" }
if ($downGateways) {
    Write-Output "The following gateways are not online:"
    $downGateways | Format-Table -Property Name, Status, RTT, Loss
}

# Disconnect from the firewall
Disconnect-OPNSense
```

### Updating Gateway Monitoring

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all gateways
$gateways = Get-OPNSenseGateway

# Update monitoring settings for all gateways
foreach ($gateway in $gateways) {
    # Use different monitoring IPs based on the gateway name
    $monitorIP = switch ($gateway.Name) {
        "WAN1_GATEWAY" { "8.8.8.8" }
        "WAN2_GATEWAY" { "8.8.4.4" }
        "WAN3_GATEWAY" { "1.1.1.1" }
        default { "9.9.9.9" }
    }
    
    Set-OPNSenseGateway -Name $gateway.Name -Monitor $monitorIP -Force
    Write-Output "Updated monitoring for gateway $($gateway.Name) to $monitorIP"
}

# Apply the changes
Apply-OPNSenseGatewayChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Failover Gateway Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create primary and backup gateways
New-OPNSenseGateway -Name "PRIMARY_GW" -Interface "wan" -IPAddress "203.0.113.1" -Description "Primary Gateway" -Monitor "8.8.8.8" -Weight 1 -Force
New-OPNSenseGateway -Name "BACKUP_GW" -Interface "opt1" -IPAddress "198.51.100.1" -Description "Backup Gateway" -Monitor "8.8.4.4" -Weight 2 -Force

# Apply the changes
Apply-OPNSenseGatewayChanges -Force

# Create a gateway group for failover
New-OPNSenseGatewayGroup -Name "FAILOVER_GROUP" -Description "Failover Gateway Group" -Gateways "PRIMARY_GW,BACKUP_GW" -Trigger "1" -Force

# Apply the changes
Apply-OPNSenseGatewayGroupChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Gateway changes are not applied until you call `Apply-OPNSenseGatewayChanges`.
- When creating a gateway, ensure that the IP address is reachable from the specified interface.
- The `Monitor` parameter specifies an IP address to ping to determine the gateway status.
- The `Weight` parameter is used for load balancing when multiple gateways are used in a gateway group.
- Gateway names should be descriptive and follow a consistent naming convention.
- Consider using the `-PassThru` parameter when updating gateways to verify the changes.
- Gateway monitoring can impact performance if too many gateways are monitored with a high frequency.
- The default gateway is typically named "WAN_GATEWAY" for IPv4 and "WAN_GATEWAY_V6" for IPv6.
- Gateway groups can be used for failover or load balancing between multiple gateways.
