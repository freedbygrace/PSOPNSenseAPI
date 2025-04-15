# Port Forwarding Management

This component provides cmdlets for managing port forwarding rules on OPNSense firewalls.

## Overview

Port forwarding (also known as destination NAT or DNAT) allows external traffic to be redirected to internal servers. This component provides cmdlets to create, view, modify, and delete port forwarding rules on OPNSense firewalls.

## Cmdlets

### Get-OPNSensePortForwardingRule

Retrieves port forwarding rules from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific port forwarding rule to retrieve. If not specified, all rules are returned.

#### Examples

```powershell
# Get all port forwarding rules
Get-OPNSensePortForwardingRule

# Get a specific port forwarding rule by UUID
Get-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```

### New-OPNSensePortForwardingRule

Creates a new port forwarding rule on an OPNSense firewall.

#### Parameters

- **Enabled** - Whether the rule is enabled. Default is true.
- **Description** - A description for the rule.
- **Interface** - The interface for the rule (e.g., "WAN").
- **Protocol** - The TCP/IP protocol (tcp, udp, tcp/udp). Default is "tcp".
- **Source** - The source address for the rule. Default is "any".
- **SourcePort** - The source port range for the rule. Default is "any".
- **Destination** - The destination address for the rule. Default is "wanip".
- **DestinationPort** - The destination port range for the rule.
- **TargetIP** - The target IP address for the rule.
- **TargetPort** - The target port for the rule.
- **NatReflection** - The NAT reflection mode (enable, disable, purenat). Default is "enable".
- **FilterRuleAssociation** - Whether to create an associated filter rule. Default is true.
- **Log** - Whether to enable logging for this rule. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a new port forwarding rule for HTTP
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server HTTP"

# Create a new port forwarding rule for HTTPS with logging enabled
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Web Server HTTPS" -Log -Force
```

### Set-OPNSensePortForwardingRule

Updates an existing port forwarding rule on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the port forwarding rule to update.
- **Enabled** - Whether the rule is enabled.
- **Description** - A description for the rule.
- **Interface** - The interface for the rule.
- **Protocol** - The TCP/IP protocol (tcp, udp, tcp/udp).
- **Source** - The source address for the rule.
- **SourcePort** - The source port range for the rule.
- **Destination** - The destination address for the rule.
- **DestinationPort** - The destination port range for the rule.
- **TargetIP** - The target IP address for the rule.
- **TargetPort** - The target port for the rule.
- **NatReflection** - The NAT reflection mode (enable, disable, purenat).
- **FilterRuleAssociation** - Whether to create an associated filter rule.
- **Log** - Whether to enable logging for this rule.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Update a port forwarding rule's target IP
Set-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -TargetIP "192.168.1.200"

# Update a port forwarding rule's description and enable logging
Set-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated Web Server" -Log -PassThru
```

### Remove-OPNSensePortForwardingRule

Removes a port forwarding rule from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the port forwarding rule to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a port forwarding rule
Remove-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a port forwarding rule without confirmation
Remove-OPNSensePortForwardingRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

## Common Scenarios

### Web Server Port Forwarding

```powershell
# Forward HTTP and HTTPS to an internal web server
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server HTTP"
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Web Server HTTPS"
```

### Remote Desktop Access

```powershell
# Forward RDP to an internal server on a non-standard port
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "33389" -TargetIP "192.168.1.200" -TargetPort "3389" -Description "Remote Desktop"
```

### Game Server

```powershell
# Forward multiple ports for a game server
New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp/udp" -DestinationPort "27015-27020" -TargetIP "192.168.1.150" -TargetPort "27015-27020" -Description "Game Server"
```

### Managing Rules

```powershell
# Get all port forwarding rules
$rules = Get-OPNSensePortForwardingRule

# Update all rules targeting a specific IP
$rules | Where-Object { $_.TargetIP -eq "192.168.1.100" } | ForEach-Object {
    Set-OPNSensePortForwardingRule -Uuid $_.Uuid -TargetIP "192.168.1.101" -Force
}

# Remove all rules with a specific description
$rules | Where-Object { $_.Description -like "*Temporary*" } | ForEach-Object {
    Remove-OPNSensePortForwardingRule -Uuid $_.Uuid -Force
}
```

## Notes

- Port forwarding rules are applied immediately after creation, update, or deletion.
- When creating or updating rules, consider security implications and only forward necessary ports.
- Use the `FilterRuleAssociation` parameter to automatically create associated firewall rules.
- NAT reflection allows internal clients to access forwarded services using the external IP address.
