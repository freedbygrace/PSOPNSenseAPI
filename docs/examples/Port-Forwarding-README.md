# Port Forwarding Examples

This document provides examples of using the PSOPNSenseAPI module to manage port forwarding rules on OPNSense firewalls.

## Basic Port Forwarding

The following example demonstrates how to create, view, update, and delete port forwarding rules:

```powershell
# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all port forwarding rules
$rules = Get-OPNSensePortForwardingRule
Write-Output "Current port forwarding rules:"
$rules | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Create a new port forwarding rule for HTTP
$httpRule = New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server HTTP"
Write-Output "Created HTTP port forwarding rule:"
$httpRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Create a new port forwarding rule for HTTPS
$httpsRule = New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Web Server HTTPS"
Write-Output "Created HTTPS port forwarding rule:"
$httpsRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Update the HTTP rule to point to a different server
$updatedRule = Set-OPNSensePortForwardingRule -Uuid $httpRule.Uuid -TargetIP "192.168.1.200" -Description "Updated Web Server HTTP" -PassThru
Write-Output "Updated HTTP port forwarding rule:"
$updatedRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Get a specific rule by UUID
$rule = Get-OPNSensePortForwardingRule -Uuid $httpsRule.Uuid
Write-Output "Retrieved HTTPS port forwarding rule:"
$rule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Remove the HTTP rule
Remove-OPNSensePortForwardingRule -Uuid $httpRule.Uuid -Force
Write-Output "Removed HTTP port forwarding rule"

# Get all port forwarding rules again to verify changes
$rules = Get-OPNSensePortForwardingRule
Write-Output "Updated port forwarding rules:"
$rules | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
```

## Advanced Port Forwarding Scenarios

### Web Server with Multiple Services

This example shows how to set up port forwarding for a web server with multiple services:

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define the web server details
$webServerIP = "192.168.1.100"
$services = @(
    @{ Name = "HTTP"; Protocol = "tcp"; ExternalPort = "80"; InternalPort = "80" },
    @{ Name = "HTTPS"; Protocol = "tcp"; ExternalPort = "443"; InternalPort = "443" },
    @{ Name = "Alternative HTTP"; Protocol = "tcp"; ExternalPort = "8080"; InternalPort = "8080" },
    @{ Name = "WebSocket"; Protocol = "tcp"; ExternalPort = "9000"; InternalPort = "9000" }
)

# Create port forwarding rules for each service
foreach ($service in $services) {
    New-OPNSensePortForwardingRule -Interface "WAN" `
        -Protocol $service.Protocol `
        -DestinationPort $service.ExternalPort `
        -TargetIP $webServerIP `
        -TargetPort $service.InternalPort `
        -Description "Web Server - $($service.Name)" `
        -Force
}

# Verify the rules were created
Get-OPNSensePortForwardingRule | Where-Object { $_.TargetIP -eq $webServerIP } | 
    Format-Table -Property Description, Protocol, DestinationPort, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
```

### Game Server Port Forwarding

This example demonstrates how to set up port forwarding for a game server with multiple ports:

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define the game server details
$gameServerIP = "192.168.1.150"
$gameServerPorts = @(
    @{ Name = "Game Server - Main"; Protocol = "tcp/udp"; Ports = "27015" },
    @{ Name = "Game Server - Query"; Protocol = "udp"; Ports = "27016" },
    @{ Name = "Game Server - RCON"; Protocol = "tcp"; Ports = "27017" },
    @{ Name = "Game Server - Voice"; Protocol = "udp"; Ports = "9987-9989" }
)

# Create port forwarding rules for the game server
foreach ($port in $gameServerPorts) {
    New-OPNSensePortForwardingRule -Interface "WAN" `
        -Protocol $port.Protocol `
        -DestinationPort $port.Ports `
        -TargetIP $gameServerIP `
        -TargetPort $port.Ports `
        -Description $port.Name `
        -Force
}

# Verify the rules were created
Get-OPNSensePortForwardingRule | Where-Object { $_.TargetIP -eq $gameServerIP } | 
    Format-Table -Property Description, Protocol, DestinationPort, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
```

### Remote Access Services

This example shows how to set up port forwarding for remote access services:

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define remote access services
$remoteServices = @(
    @{ Name = "RDP - Admin Server"; TargetIP = "192.168.1.200"; ExternalPort = "33389"; InternalPort = "3389"; Protocol = "tcp" },
    @{ Name = "SSH - Dev Server"; TargetIP = "192.168.1.201"; ExternalPort = "2222"; InternalPort = "22"; Protocol = "tcp" },
    @{ Name = "VNC - Support"; TargetIP = "192.168.1.202"; ExternalPort = "5900"; InternalPort = "5900"; Protocol = "tcp" }
)

# Create port forwarding rules for remote access
foreach ($service in $remoteServices) {
    New-OPNSensePortForwardingRule -Interface "WAN" `
        -Protocol $service.Protocol `
        -DestinationPort $service.ExternalPort `
        -TargetIP $service.TargetIP `
        -TargetPort $service.InternalPort `
        -Description $service.Name `
        -Log `  # Enable logging for security
        -Force
}

# Verify the rules were created
Get-OPNSensePortForwardingRule | Where-Object { $_.Description -like "* - *" } | 
    Format-Table -Property Description, Protocol, DestinationPort, TargetIP, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
```

### Bulk Management of Port Forwarding Rules

This example demonstrates how to perform bulk operations on port forwarding rules:

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all port forwarding rules
$rules = Get-OPNSensePortForwardingRule

# Disable all rules with "Temporary" in the description
$rules | Where-Object { $_.Description -like "*Temporary*" } | ForEach-Object {
    Set-OPNSensePortForwardingRule -Uuid $_.Uuid -Enabled:$false -Force
    Write-Output "Disabled rule: $($_.Description)"
}

# Update all rules pointing to a decommissioned server
$oldServerIP = "192.168.1.100"
$newServerIP = "192.168.1.150"

$rules | Where-Object { $_.TargetIP -eq $oldServerIP } | ForEach-Object {
    Set-OPNSensePortForwardingRule -Uuid $_.Uuid -TargetIP $newServerIP -Description "$($_.Description) (Migrated)" -Force
    Write-Output "Updated rule: $($_.Description) to point to $newServerIP"
}

# Delete all rules with "Obsolete" in the description
$rules | Where-Object { $_.Description -like "*Obsolete*" } | ForEach-Object {
    Remove-OPNSensePortForwardingRule -Uuid $_.Uuid -Force
    Write-Output "Removed rule: $($_.Description)"
}

# Verify changes
$updatedRules = Get-OPNSensePortForwardingRule
Write-Output "Rules after bulk operations:"
$updatedRules | Format-Table -Property Uuid, Description, Enabled, TargetIP, DestinationPort, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Port forwarding rules are applied immediately after creation, update, or deletion.
- When creating or updating rules, consider security implications and only forward necessary ports.
- Use the `FilterRuleAssociation` parameter to automatically create associated firewall rules.
- NAT reflection allows internal clients to access forwarded services using the external IP address.
- For security-sensitive rules, enable logging with the `-Log` parameter.
- Always use strong authentication and encryption for remote access services.
- Consider using non-standard external ports for common services to reduce automated scanning attempts.
