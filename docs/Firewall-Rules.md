# Firewall Rules Management

This component provides cmdlets for managing firewall rules on OPNSense firewalls.

## Overview

The Firewall Rules Management component allows you to create, view, modify, and delete firewall rules on OPNSense firewalls. It provides a comprehensive set of cmdlets to manage all aspects of firewall rules, including enabling, disabling, and applying changes.

## Cmdlets

### Get-OPNSenseFirewallRule

Retrieves firewall rules from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific firewall rule to retrieve. If not specified, all rules are returned.
- **Interface** - Filter rules by interface.
- **Direction** - Filter rules by direction (in, out).
- **Protocol** - Filter rules by protocol.
- **Action** - Filter rules by action (pass, block, reject).

#### Examples

```powershell
# Get all firewall rules
Get-OPNSenseFirewallRule

# Get a specific firewall rule by UUID
Get-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get all rules for a specific interface
Get-OPNSenseFirewallRule -Interface "lan"

# Get all block rules
Get-OPNSenseFirewallRule -Action "block"
```

### New-OPNSenseFirewallRule

Creates a new firewall rule on an OPNSense firewall.

#### Parameters

- **Enabled** - Whether the rule is enabled. Default is true.
- **Action** - The action to take (pass, block, reject). Default is "pass".
- **Interface** - The interface for the rule.
- **Direction** - The direction for the rule (in, out). Default is "in".
- **Protocol** - The protocol for the rule (tcp, udp, icmp, etc.).
- **Source** - The source address for the rule. Default is "any".
- **SourcePort** - The source port for the rule. Default is "any".
- **Destination** - The destination address for the rule. Default is "any".
- **DestinationPort** - The destination port for the rule.
- **Description** - A description for the rule.
- **Log** - Whether to log matches for this rule. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a rule to allow HTTP traffic
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -Destination "any" -DestinationPort "80" -Description "Allow HTTP"

# Create a rule to block outgoing SMTP traffic
New-OPNSenseFirewallRule -Interface "lan" -Direction "out" -Protocol "tcp" -DestinationPort "25" -Action "block" -Description "Block outgoing SMTP" -Log

# Create a rule to allow traffic from a specific subnet to a specific server
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -Source "192.168.1.0/24" -Destination "192.168.2.10" -DestinationPort "443" -Description "Allow subnet to server"
```

### Set-OPNSenseFirewallRule

Updates an existing firewall rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the firewall rule to update.
- **Enabled** - Whether the rule is enabled.
- **Action** - The action to take (pass, block, reject).
- **Interface** - The interface for the rule.
- **Direction** - The direction for the rule (in, out).
- **Protocol** - The protocol for the rule.
- **Source** - The source address for the rule.
- **SourcePort** - The source port for the rule.
- **Destination** - The destination address for the rule.
- **DestinationPort** - The destination port for the rule.
- **Description** - A description for the rule.
- **Log** - Whether to log matches for this rule.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Update a firewall rule's description
Set-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated HTTP rule"

# Update a firewall rule's destination port
Set-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -DestinationPort "8080"

# Update multiple properties of a firewall rule
Set-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Action "block" -Log -Description "Block traffic" -PassThru
```

### Remove-OPNSenseFirewallRule

Removes a firewall rule from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the firewall rule to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a firewall rule
Remove-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a firewall rule without confirmation
Remove-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Enable-OPNSenseFirewallRule

Enables a firewall rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the firewall rule to enable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Enable a firewall rule
Enable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Enable a firewall rule and return the updated rule
Enable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Disable-OPNSenseFirewallRule

Disables a firewall rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the firewall rule to disable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Disable a firewall rule
Disable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Disable a firewall rule and return the updated rule
Disable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Apply-OPNSenseFirewallChanges

Applies pending firewall changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply firewall changes
Apply-OPNSenseFirewallChanges

# Apply firewall changes without confirmation
Apply-OPNSenseFirewallChanges -Force
```

## Common Scenarios

### Basic Firewall Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create rules for basic web access
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -DestinationPort "80" -Description "Allow HTTP" -Force
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -DestinationPort "443" -Description "Allow HTTPS" -Force

# Create a rule to allow DNS
New-OPNSenseFirewallRule -Interface "lan" -Protocol "udp" -DestinationPort "53" -Description "Allow DNS" -Force

# Apply the changes
Apply-OPNSenseFirewallChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Securing a Network

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Block outgoing SMTP to prevent spam
New-OPNSenseFirewallRule -Interface "lan" -Direction "out" -Protocol "tcp" -DestinationPort "25" -Action "block" -Description "Block outgoing SMTP" -Log -Force

# Allow only specific hosts to access the management interface
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -Source "192.168.1.10,192.168.1.11" -Destination "192.168.1.1" -DestinationPort "443" -Description "Allow management access" -Force

# Block all other access to the management interface
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -Destination "192.168.1.1" -DestinationPort "443" -Action "block" -Description "Block management access" -Log -Force

# Apply the changes
Apply-OPNSenseFirewallChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Existing Rules

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all firewall rules
$rules = Get-OPNSenseFirewallRule

# Disable all rules with "Temporary" in the description
$rules | Where-Object { $_.Description -like "*Temporary*" } | ForEach-Object {
    Disable-OPNSenseFirewallRule -Uuid $_.Uuid -Force
    Write-Output "Disabled rule: $($_.Description)"
}

# Update all rules with "HTTP" in the description to use port 8080 instead of 80
$rules | Where-Object { $_.Description -like "*HTTP*" -and $_.DestinationPort -eq "80" } | ForEach-Object {
    Set-OPNSenseFirewallRule -Uuid $_.Uuid -DestinationPort "8080" -Description "$($_.Description) (Updated Port)" -Force
    Write-Output "Updated rule: $($_.Description)"
}

# Remove all rules with "Obsolete" in the description
$rules | Where-Object { $_.Description -like "*Obsolete*" } | ForEach-Object {
    Remove-OPNSenseFirewallRule -Uuid $_.Uuid -Force
    Write-Output "Removed rule: $($_.Description)"
}

# Apply the changes
Apply-OPNSenseFirewallChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Firewall rules are processed in order, with the first matching rule being applied.
- Changes to firewall rules are not applied until you call `Apply-OPNSenseFirewallChanges`.
- When creating or updating rules, consider the rule order and potential security implications.
- Use the `-Log` parameter for rules that you want to monitor for security purposes.
- Use aliases for frequently used IP addresses or networks to make rules more maintainable.
- Consider using the `-PassThru` parameter when updating rules to verify the changes.
- Always apply the principle of least privilege when creating firewall rules.
