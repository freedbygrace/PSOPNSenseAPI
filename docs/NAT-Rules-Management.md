# NAT Rules Management

This component provides cmdlets for managing Network Address Translation (NAT) rules on OPNSense firewalls.

## Overview

The NAT Rules Management component allows you to configure and manage NAT rules on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting different types of NAT rules, including port forwarding, outbound NAT, and 1:1 NAT.

## Cmdlets

### Get-OPNSenseNatRule

Retrieves NAT rules from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific NAT rule to retrieve. If not specified, all NAT rules are returned.
- **Type** - Filter rules by type (port_forward, outbound, 1to1).
- **Interface** - Filter rules by interface.
- **Description** - Filter rules by description.

#### Examples

```powershell
# Get all NAT rules
Get-OPNSenseNatRule

# Get a specific NAT rule by UUID
Get-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get all port forwarding rules
Get-OPNSenseNatRule -Type "port_forward"

# Get all NAT rules for a specific interface
Get-OPNSenseNatRule -Interface "wan"
```

### New-OPNSensePortForwardRule

Creates a new port forwarding rule on an OPNSense firewall.

#### Parameters

- **Interface** - The interface for the rule.
- **Protocol** - The protocol for the rule (tcp, udp, tcp/udp).
- **Source** - The source address for the rule. Default is "any".
- **SourcePort** - The source port for the rule. Default is "any".
- **Destination** - The destination address for the rule. Default is "wanip".
- **DestinationPort** - The destination port for the rule.
- **TargetIP** - The target IP address for the rule.
- **TargetPort** - The target port for the rule.
- **Description** - A description for the rule.
- **NatReflection** - The NAT reflection mode (enable, disable, purenat). Default is "enable".
- **FilterRuleAssociation** - Whether to create an associated filter rule. Default is true.
- **Enabled** - Whether the rule is enabled. Default is true.
- **Log** - Whether to log matches for this rule. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic port forwarding rule
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server"

# Create a port forwarding rule with custom source and destination
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -Source "203.0.113.0/24" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Secure Web Server"

# Create a port forwarding rule with NAT reflection disabled
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -DestinationPort "3389" -TargetIP "192.168.1.200" -TargetPort "3389" -Description "RDP Server" -NatReflection "disable"

# Create a port forwarding rule without an associated filter rule
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -DestinationPort "22" -TargetIP "192.168.1.200" -TargetPort "22" -Description "SSH Server" -FilterRuleAssociation:$false
```

### New-OPNSenseOutboundNatRule

Creates a new outbound NAT rule on an OPNSense firewall.

#### Parameters

- **Interface** - The interface for the rule.
- **Source** - The source address for the rule.
- **Destination** - The destination address for the rule. Default is "any".
- **TranslationAddress** - The translation address for the rule.
- **Description** - A description for the rule.
- **Enabled** - Whether the rule is enabled. Default is true.
- **Log** - Whether to log matches for this rule. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic outbound NAT rule
New-OPNSenseOutboundNatRule -Interface "wan" -Source "192.168.1.0/24" -TranslationAddress "203.0.113.1" -Description "Outbound NAT for LAN"

# Create an outbound NAT rule with a specific destination
New-OPNSenseOutboundNatRule -Interface "wan" -Source "192.168.1.0/24" -Destination "198.51.100.0/24" -TranslationAddress "203.0.113.1" -Description "Outbound NAT for specific destination"

# Create an outbound NAT rule with logging enabled
New-OPNSenseOutboundNatRule -Interface "wan" -Source "192.168.1.0/24" -TranslationAddress "203.0.113.1" -Description "Logged Outbound NAT" -Log
```

### New-OPNSense1to1NatRule

Creates a new 1:1 NAT rule on an OPNSense firewall.

#### Parameters

- **Interface** - The interface for the rule.
- **ExternalNetwork** - The external network for the rule.
- **InternalIP** - The internal IP address for the rule.
- **Description** - A description for the rule.
- **NatReflection** - The NAT reflection mode (enable, disable, purenat). Default is "enable".
- **Enabled** - Whether the rule is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic 1:1 NAT rule
New-OPNSense1to1NatRule -Interface "wan" -ExternalNetwork "203.0.113.10" -InternalIP "192.168.1.10" -Description "1:1 NAT for Server"

# Create a 1:1 NAT rule with NAT reflection disabled
New-OPNSense1to1NatRule -Interface "wan" -ExternalNetwork "203.0.113.11" -InternalIP "192.168.1.11" -Description "1:1 NAT without reflection" -NatReflection "disable"

# Create a 1:1 NAT rule for a network
New-OPNSense1to1NatRule -Interface "wan" -ExternalNetwork "203.0.113.0/29" -InternalIP "192.168.1.0/29" -Description "1:1 NAT for Network"
```

### Set-OPNSenseNatRule

Updates an existing NAT rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the NAT rule to update.
- **Interface** - The interface for the rule.
- **Protocol** - The protocol for the rule (tcp, udp, tcp/udp).
- **Source** - The source address for the rule.
- **SourcePort** - The source port for the rule.
- **Destination** - The destination address for the rule.
- **DestinationPort** - The destination port for the rule.
- **TargetIP** - The target IP address for the rule.
- **TargetPort** - The target port for the rule.
- **TranslationAddress** - The translation address for the rule.
- **ExternalNetwork** - The external network for the rule.
- **InternalIP** - The internal IP address for the rule.
- **Description** - A description for the rule.
- **NatReflection** - The NAT reflection mode (enable, disable, purenat).
- **FilterRuleAssociation** - Whether to create an associated filter rule.
- **Enabled** - Whether the rule is enabled.
- **Log** - Whether to log matches for this rule.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Update a port forwarding rule's target IP
Set-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -TargetIP "192.168.1.101"

# Update a port forwarding rule's description
Set-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated Web Server"

# Update an outbound NAT rule's translation address
Set-OPNSenseNatRule -Uuid "b2c3d4e5-f6g7-h8i9-j0k1-l2m3n4o5p6q7" -TranslationAddress "203.0.113.2"

# Update a 1:1 NAT rule's internal IP
Set-OPNSenseNatRule -Uuid "c3d4e5f6-g7h8-i9j0-k1l2-m3n4o5p6q7r8" -InternalIP "192.168.1.12"

# Disable a NAT rule
Set-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Enabled:$false

# Update multiple properties of a NAT rule
Set-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -TargetIP "192.168.1.101" -TargetPort "8080" -Description "Updated Web Server" -PassThru
```

### Remove-OPNSenseNatRule

Removes a NAT rule from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the NAT rule to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a NAT rule
Remove-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a NAT rule without confirmation
Remove-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Enable-OPNSenseNatRule

Enables a NAT rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the NAT rule to enable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Enable a NAT rule
Enable-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Enable a NAT rule and return the updated rule
Enable-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Disable-OPNSenseNatRule

Disables a NAT rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the NAT rule to disable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Disable a NAT rule
Disable-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Disable a NAT rule and return the updated rule
Disable-OPNSenseNatRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Apply-OPNSenseNatChanges

Applies pending NAT changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply NAT changes
Apply-OPNSenseNatChanges

# Apply NAT changes without confirmation
Apply-OPNSenseNatChanges -Force
```

## Common Scenarios

### Setting Up Port Forwarding for a Web Server

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create port forwarding rules for HTTP and HTTPS
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server HTTP" -Force
New-OPNSensePortForwardRule -Interface "wan" -Protocol "tcp" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Web Server HTTPS" -Force

# Apply the changes
Apply-OPNSenseNatChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Configuring Outbound NAT for Multiple Networks

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define networks and their translation addresses
$networks = @(
    @{ Source = "192.168.1.0/24"; Translation = "203.0.113.1"; Description = "LAN Outbound NAT" },
    @{ Source = "192.168.2.0/24"; Translation = "203.0.113.2"; Description = "Guest Outbound NAT" },
    @{ Source = "192.168.3.0/24"; Translation = "203.0.113.3"; Description = "DMZ Outbound NAT" }
)

# Create outbound NAT rules
foreach ($network in $networks) {
    New-OPNSenseOutboundNatRule -Interface "wan" -Source $network.Source -TranslationAddress $network.Translation -Description $network.Description -Force
}

# Apply the changes
Apply-OPNSenseNatChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Setting Up 1:1 NAT for a Server Farm

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define server mappings
$servers = @(
    @{ External = "203.0.113.10"; Internal = "192.168.1.10"; Description = "Web Server 1" },
    @{ External = "203.0.113.11"; Internal = "192.168.1.11"; Description = "Web Server 2" },
    @{ External = "203.0.113.12"; Internal = "192.168.1.12"; Description = "Database Server" },
    @{ External = "203.0.113.13"; Internal = "192.168.1.13"; Description = "Mail Server" }
)

# Create 1:1 NAT rules
foreach ($server in $servers) {
    New-OPNSense1to1NatRule -Interface "wan" -ExternalNetwork $server.External -InternalIP $server.Internal -Description $server.Description -Force
}

# Apply the changes
Apply-OPNSenseNatChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Existing NAT Rules

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all NAT rules
$natRules = Get-OPNSenseNatRule

# Update port forwarding rules to use a new target IP
$portForwardRules = $natRules | Where-Object { $_.Type -eq "port_forward" -and $_.TargetIP -eq "192.168.1.100" }
foreach ($rule in $portForwardRules) {
    Set-OPNSenseNatRule -Uuid $rule.Uuid -TargetIP "192.168.1.101" -Description "$($rule.Description) (Updated)" -Force
    Write-Output "Updated rule: $($rule.Description)"
}

# Disable temporary NAT rules
$tempRules = $natRules | Where-Object { $_.Description -like "*Temporary*" }
foreach ($rule in $tempRules) {
    Disable-OPNSenseNatRule -Uuid $rule.Uuid -Force
    Write-Output "Disabled rule: $($rule.Description)"
}

# Remove obsolete NAT rules
$obsoleteRules = $natRules | Where-Object { $_.Description -like "*Obsolete*" }
foreach ($rule in $obsoleteRules) {
    Remove-OPNSenseNatRule -Uuid $rule.Uuid -Force
    Write-Output "Removed rule: $($rule.Description)"
}

# Apply the changes
Apply-OPNSenseNatChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- NAT rules are processed in order, with the first matching rule being applied.
- Changes to NAT rules are not applied until you call `Apply-OPNSenseNatChanges`.
- Port forwarding rules create a mapping from an external port to an internal IP address and port.
- Outbound NAT rules control how internal addresses are translated when accessing external networks.
- 1:1 NAT rules create a one-to-one mapping between an external IP address and an internal IP address.
- NAT reflection allows internal clients to access forwarded services using the external IP address.
- The `FilterRuleAssociation` parameter for port forwarding rules determines whether a corresponding firewall rule is created.
- When creating or updating NAT rules, consider the rule order and potential security implications.
- Use the `-Log` parameter for rules that you want to monitor for security purposes.
- Consider using the `-PassThru` parameter when updating NAT rules to verify the changes.
- NAT rules can reference aliases for source, destination, and port fields.
- The `NatReflection` parameter can be set to "enable", "disable", or "purenat".
- The `Source` parameter can be set to "any" to match any source address.
- The `Destination` parameter for port forwarding rules can be set to "wanip" to use the WAN IP address.
- The `TranslationAddress` parameter for outbound NAT rules can be set to "wanip" to use the WAN IP address.
