# API Reference

This document provides a comprehensive reference for all cmdlets in the PSOPNSenseAPI module.

## Table of Contents

- [Connection Management](#connection-management)
- [Firewall Management](#firewall-management)
- [NAT Management](#nat-management)
- [Interface Management](#interface-management)
- [VLAN Management](#vlan-management)
- [Gateway Management](#gateway-management)
- [Route Management](#route-management)
- [DNS Management](#dns-management)
- [DHCP Management](#dhcp-management)
- [Alias Management](#alias-management)
- [User Management](#user-management)
- [Plugin Management](#plugin-management)
- [Firmware Management](#firmware-management)
- [System Management](#system-management)
- [Network Utilities](#network-utilities)
- [Tailscale Integration](#tailscale-integration)
- [Cron Job Management](#cron-job-management)

## Connection Management

### Connect-OPNSense

Establishes a connection to an OPNSense firewall.

#### Syntax

```powershell
Connect-OPNSense
    -Server <String>
    -ApiKey <String>
    -ApiSecret <String>
    [-SkipCertificateCheck]
```

#### Parameters

- **Server** - The URL of the OPNSense firewall.
- **ApiKey** - The API key for authentication.
- **ApiSecret** - The API secret for authentication.
- **SkipCertificateCheck** - Skips certificate validation for HTTPS connections.

#### Examples

```powershell
# Connect to an OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"

# Connect to an OPNSense firewall with certificate validation disabled
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck
```

### Disconnect-OPNSense

Terminates the connection to an OPNSense firewall.

#### Syntax

```powershell
Disconnect-OPNSense
```

#### Examples

```powershell
# Disconnect from an OPNSense firewall
Disconnect-OPNSense
```

## Firewall Management

### Get-OPNSenseFirewallRule

Retrieves firewall rules from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseFirewallRule
    [-Uuid <String>]
    [-Interface <String>]
    [-Direction <String>]
    [-Protocol <String>]
    [-Action <String>]
```

#### Parameters

- **Uuid** - The UUID of a specific firewall rule to retrieve.
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
```

### New-OPNSenseFirewallRule

Creates a new firewall rule on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseFirewallRule
    -Interface <String>
    [-Direction <String>]
    [-Protocol <String>]
    [-Source <String>]
    [-SourcePort <String>]
    [-Destination <String>]
    [-DestinationPort <String>]
    [-Action <String>]
    [-Description <String>]
    [-Enabled <Boolean>]
    [-Log <Boolean>]
    [-Force]
```

#### Parameters

- **Interface** - The interface for the rule.
- **Direction** - The direction for the rule (in, out). Default is "in".
- **Protocol** - The protocol for the rule (tcp, udp, icmp, etc.).
- **Source** - The source address for the rule. Default is "any".
- **SourcePort** - The source port for the rule. Default is "any".
- **Destination** - The destination address for the rule. Default is "any".
- **DestinationPort** - The destination port for the rule.
- **Action** - The action to take (pass, block, reject). Default is "pass".
- **Description** - A description for the rule.
- **Enabled** - Whether the rule is enabled. Default is true.
- **Log** - Whether to log matches for this rule. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a rule to allow HTTP traffic
New-OPNSenseFirewallRule -Interface "lan" -Protocol "tcp" -Destination "any" -DestinationPort "80" -Description "Allow HTTP"

# Create a rule to block outgoing SMTP traffic
New-OPNSenseFirewallRule -Interface "lan" -Direction "out" -Protocol "tcp" -DestinationPort "25" -Action "block" -Description "Block outgoing SMTP" -Log
```

### Set-OPNSenseFirewallRule

Updates an existing firewall rule on an OPNSense firewall.

#### Syntax

```powershell
Set-OPNSenseFirewallRule
    -Uuid <String>
    [-Interface <String>]
    [-Direction <String>]
    [-Protocol <String>]
    [-Source <String>]
    [-SourcePort <String>]
    [-Destination <String>]
    [-DestinationPort <String>]
    [-Action <String>]
    [-Description <String>]
    [-Enabled <Boolean>]
    [-Log <Boolean>]
    [-Force]
    [-PassThru]
```

#### Parameters

- **Uuid** - The UUID of the firewall rule to update.
- **Interface** - The interface for the rule.
- **Direction** - The direction for the rule (in, out).
- **Protocol** - The protocol for the rule.
- **Source** - The source address for the rule.
- **SourcePort** - The source port for the rule.
- **Destination** - The destination address for the rule.
- **DestinationPort** - The destination port for the rule.
- **Action** - The action to take (pass, block, reject).
- **Description** - A description for the rule.
- **Enabled** - Whether the rule is enabled.
- **Log** - Whether to log matches for this rule.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Update a firewall rule's description
Set-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated HTTP rule"

# Update a firewall rule's destination port
Set-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -DestinationPort "8080"
```

### Remove-OPNSenseFirewallRule

Removes a firewall rule from an OPNSense firewall.

#### Syntax

```powershell
Remove-OPNSenseFirewallRule
    -Uuid <String>
    [-Force]
```

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

#### Syntax

```powershell
Enable-OPNSenseFirewallRule
    -Uuid <String>
    [-Force]
    [-PassThru]
```

#### Parameters

- **Uuid** - The UUID of the firewall rule to enable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Enable a firewall rule
Enable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```

### Disable-OPNSenseFirewallRule

Disables a firewall rule on an OPNSense firewall.

#### Syntax

```powershell
Disable-OPNSenseFirewallRule
    -Uuid <String>
    [-Force]
    [-PassThru]
```

#### Parameters

- **Uuid** - The UUID of the firewall rule to disable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated rule.

#### Examples

```powershell
# Disable a firewall rule
Disable-OPNSenseFirewallRule -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```

### Apply-OPNSenseFirewallChanges

Applies pending firewall changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseFirewallChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply firewall changes
Apply-OPNSenseFirewallChanges

# Apply firewall changes without confirmation
Apply-OPNSenseFirewallChanges -Force
```

## NAT Management

### Get-OPNSenseNatRule

Retrieves NAT rules from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseNatRule
    [-Uuid <String>]
    [-Type <String>]
    [-Interface <String>]
    [-Description <String>]
```

#### Parameters

- **Uuid** - The UUID of a specific NAT rule to retrieve.
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
```

### New-OPNSensePortForwardRule

Creates a new port forwarding rule on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSensePortForwardRule
    -Interface <String>
    -Protocol <String>
    [-Source <String>]
    [-SourcePort <String>]
    [-Destination <String>]
    -DestinationPort <String>
    -TargetIP <String>
    -TargetPort <String>
    [-Description <String>]
    [-NatReflection <String>]
    [-FilterRuleAssociation <Boolean>]
    [-Enabled <Boolean>]
    [-Log <Boolean>]
    [-Force]
```

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
```

### Apply-OPNSenseNatChanges

Applies pending NAT changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseNatChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply NAT changes
Apply-OPNSenseNatChanges

# Apply NAT changes without confirmation
Apply-OPNSenseNatChanges -Force
```

## Interface Management

### Get-OPNSenseInterface

Retrieves interface configurations from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseInterface
    [-Name <String>]
    [-Type <String>]
    [-Enabled <Boolean>]
```

#### Parameters

- **Name** - The name of a specific interface to retrieve.
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
```

### Set-OPNSenseInterface

Updates an existing interface on an OPNSense firewall.

#### Syntax

```powershell
Set-OPNSenseInterface
    -Name <String>
    [-Description <String>]
    [-IPv4Address <String>]
    [-IPv4Subnet <String>]
    [-IPv6Address <String>]
    [-IPv6Subnet <String>]
    [-Enabled <Boolean>]
    [-BlockPrivate <Boolean>]
    [-BlockBogons <Boolean>]
    [-Force]
    [-PassThru]
```

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
```

### Apply-OPNSenseInterfaceChanges

Applies pending interface changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseInterfaceChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply interface changes
Apply-OPNSenseInterfaceChanges

# Apply interface changes without confirmation
Apply-OPNSenseInterfaceChanges -Force
```

## VLAN Management

### Get-OPNSenseVLAN

Retrieves VLAN configurations from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseVLAN
    [-Uuid <String>]
```

#### Parameters

- **Uuid** - The UUID of a specific VLAN to retrieve.

#### Examples

```powershell
# Get all VLANs
Get-OPNSenseVLAN

# Get a specific VLAN by UUID
Get-OPNSenseVLAN -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```

### New-OPNSenseVLAN

Creates a new VLAN on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseVLAN
    -Interface <String>
    -Tag <Int32>
    [-Priority <Int32>]
    [-Description <String>]
    [-Force]
```

#### Parameters

- **Interface** - The parent interface for the VLAN.
- **Tag** - The VLAN tag (1-4094).
- **Priority** - The VLAN priority (0-7).
- **Description** - A description for the VLAN.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a new VLAN
New-OPNSenseVLAN -Interface "em0" -Tag 10 -Description "Management VLAN"

# Create a new VLAN with priority
New-OPNSenseVLAN -Interface "em1" -Tag 20 -Priority 5 -Description "Voice VLAN"
```

### New-OPNSenseSubnetVLANs

Creates VLANs for subnets by dividing a network into smaller subnets.

#### Syntax

```powershell
New-OPNSenseSubnetVLANs
    -ParentInterface <String>
    -Network <String>
    -SubnetMaskBits <Int32>
    -StartingVlanId <Int32>
    [-VlanIdIncrement <Int32>]
    [-DescriptionPrefix <String>]
    [-EnableDHCP <Boolean>]
    [-DHCPRangeStart <String>]
    [-DHCPRangeEnd <String>]
    [-DHCPDomain <String>]
    [-DHCPDnsServers <String[]>]
    [-Force]
```

#### Parameters

- **ParentInterface** - The parent interface for the VLANs.
- **Network** - The network in CIDR notation to divide into subnets.
- **SubnetMaskBits** - The subnet mask bits for the subnets.
- **StartingVlanId** - The starting VLAN ID.
- **VlanIdIncrement** - The increment for VLAN IDs. Default is 1.
- **DescriptionPrefix** - The prefix for VLAN descriptions. Default is "VLAN-".
- **EnableDHCP** - Whether to enable DHCP on the created VLANs. Default is false.
- **DHCPRangeStart** - The starting IP address for the DHCP range.
- **DHCPRangeEnd** - The ending IP address for the DHCP range.
- **DHCPDomain** - The domain name for DHCP clients.
- **DHCPDnsServers** - The DNS servers for DHCP clients.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create VLANs for subnets
New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.0.0/24" -SubnetMaskBits 27 -StartingVlanId 10

# Create VLANs for subnets with DHCP enabled
New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "10.0.0.0/16" -SubnetMaskBits 24 -StartingVlanId 100 -EnableDHCP -DHCPDomain "example.com" -DHCPDnsServers "8.8.8.8","8.8.4.4"
```

## Gateway Management

### Get-OPNSenseGateway

Retrieves gateway configurations from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseGateway
    [-Name <String>]
    [-Interface <String>]
    [-Protocol <String>]
```

#### Parameters

- **Name** - The name of a specific gateway to retrieve.
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
```

### New-OPNSenseGateway

Creates a new gateway on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseGateway
    -Name <String>
    -Interface <String>
    -IPAddress <String>
    [-Description <String>]
    [-Enabled <Boolean>]
    [-Monitor <String>]
    [-MonitorDisable <Boolean>]
    [-Weight <Int32>]
    [-Protocol <String>]
    [-Force]
```

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
```

### Apply-OPNSenseGatewayChanges

Applies pending gateway changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseGatewayChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply gateway changes
Apply-OPNSenseGatewayChanges

# Apply gateway changes without confirmation
Apply-OPNSenseGatewayChanges -Force
```

## Route Management

### Get-OPNSenseRoute

Retrieves static routes from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseRoute
    [-Uuid <String>]
    [-Network <String>]
    [-Gateway <String>]
    [-Description <String>]
```

#### Parameters

- **Uuid** - The UUID of a specific route to retrieve.
- **Network** - Filter routes by network.
- **Gateway** - Filter routes by gateway.
- **Description** - Filter routes by description.

#### Examples

```powershell
# Get all static routes
Get-OPNSenseRoute

# Get a specific route by UUID
Get-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get routes for a specific network
Get-OPNSenseRoute -Network "192.168.100.0/24"
```

### New-OPNSenseRoute

Creates a new static route on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseRoute
    -Network <String>
    -Gateway <String>
    [-Description <String>]
    [-Disabled <Boolean>]
    [-Force]
```

#### Parameters

- **Network** - The network for the route in CIDR notation.
- **Gateway** - The gateway for the route.
- **Description** - A description for the route.
- **Disabled** - Whether the route is disabled. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic static route
New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN_GATEWAY" -Description "Remote Office Network"

# Create a disabled static route
New-OPNSenseRoute -Network "10.0.0.0/8" -Gateway "WAN2_GATEWAY" -Description "Corporate Network" -Disabled
```

### Apply-OPNSenseRouteChanges

Applies pending route changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseRouteChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply route changes
Apply-OPNSenseRouteChanges

# Apply route changes without confirmation
Apply-OPNSenseRouteChanges -Force
```

## DNS Management

### Get-OPNSenseDNSServer

Retrieves DNS server configurations from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseDNSServer
```

#### Examples

```powershell
# Get DNS server configuration
Get-OPNSenseDNSServer
```

### Set-OPNSenseDNSServer

Updates the DNS server configuration on an OPNSense firewall.

#### Syntax

```powershell
Set-OPNSenseDNSServer
    [-Enabled <Boolean>]
    [-ListenIPs <String[]>]
    [-Port <Int32>]
    [-DNSSECEnabled <Boolean>]
    [-ForwardingEnabled <Boolean>]
    [-ForwardingServers <String[]>]
    [-CacheEnabled <Boolean>]
    [-CacheSize <Int32>]
    [-PrefetchEnabled <Boolean>]
    [-PrefetchDomains <Boolean>]
    [-Force]
    [-PassThru]
```

#### Parameters

- **Enabled** - Whether the DNS server is enabled.
- **ListenIPs** - The IP addresses to listen on.
- **Port** - The port to listen on. Default is 53.
- **DNSSECEnabled** - Whether DNSSEC is enabled.
- **ForwardingEnabled** - Whether DNS forwarding is enabled.
- **ForwardingServers** - The DNS servers to forward queries to.
- **CacheEnabled** - Whether DNS caching is enabled.
- **CacheSize** - The size of the DNS cache in MB.
- **PrefetchEnabled** - Whether DNS prefetching is enabled.
- **PrefetchDomains** - Whether to prefetch domains.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DNS server configuration.

#### Examples

```powershell
# Enable the DNS server
Set-OPNSenseDNSServer -Enabled

# Configure DNS forwarding
Set-OPNSenseDNSServer -ForwardingEnabled -ForwardingServers "8.8.8.8","8.8.4.4"
```

### Apply-OPNSenseDNSChanges

Applies pending DNS changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseDNSChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply DNS changes
Apply-OPNSenseDNSChanges

# Apply DNS changes without confirmation
Apply-OPNSenseDNSChanges -Force
```

## DHCP Management

### Get-OPNSenseDHCPServer

Retrieves DHCP server configurations from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseDHCPServer
    [-Interface <String>]
```

#### Parameters

- **Interface** - The interface to get the DHCP server configuration for.

#### Examples

```powershell
# Get all DHCP servers
Get-OPNSenseDHCPServer

# Get the DHCP server for a specific interface
Get-OPNSenseDHCPServer -Interface "lan"
```

### New-OPNSenseDHCPServer

Creates a new DHCP server on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseDHCPServer
    -Interface <String>
    -RangeFrom <String>
    -RangeTo <String>
    [-Domain <String>]
    [-DnsServers <String[]>]
    [-GatewayIP <String>]
    [-LeaseTime <Int32>]
    [-DenyUnknownClients <Boolean>]
    [-IgnoreClientIdentifiers <Boolean>]
    [-Enabled <Boolean>]
    [-Force]
```

#### Parameters

- **Interface** - The interface to create the DHCP server on.
- **RangeFrom** - The starting IP address of the DHCP range.
- **RangeTo** - The ending IP address of the DHCP range.
- **Domain** - The domain name to provide to DHCP clients.
- **DnsServers** - The DNS servers to provide to DHCP clients.
- **GatewayIP** - The gateway IP address to provide to DHCP clients.
- **LeaseTime** - The lease time in seconds. Default is 86400 (1 day).
- **DenyUnknownClients** - Whether to deny unknown clients. Default is false.
- **IgnoreClientIdentifiers** - Whether to ignore client identifiers. Default is false.
- **Enabled** - Whether the DHCP server is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic DHCP server
New-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -Domain "example.com" -DnsServers "192.168.1.1","8.8.8.8" -GatewayIP "192.168.1.1"

# Create a DHCP server with advanced options
New-OPNSenseDHCPServer -Interface "opt1" -RangeFrom "10.0.0.100" -RangeTo "10.0.0.200" -Domain "internal.example.com" -DnsServers "10.0.0.1","8.8.4.4" -GatewayIP "10.0.0.1" -LeaseTime 43200 -DenyUnknownClients
```

### Get-OPNSenseDHCPLease

Retrieves DHCP leases from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseDHCPLease
    [-Interface <String>]
    [-MACAddress <String>]
    [-IPAddress <String>]
    [-Hostname <String>]
    [-Status <String>]
```

#### Parameters

- **Interface** - The interface to get DHCP leases for.
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
```

### Apply-OPNSenseDHCPChanges

Applies pending DHCP changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseDHCPChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply DHCP changes
Apply-OPNSenseDHCPChanges

# Apply DHCP changes without confirmation
Apply-OPNSenseDHCPChanges -Force
```

## Alias Management

### Get-OPNSenseAlias

Retrieves aliases from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseAlias
    [-Name <String>]
    [-Type <String>]
```

#### Parameters

- **Name** - The name of a specific alias to retrieve.
- **Type** - Filter aliases by type (host, network, port, url, urltable, geoip).

#### Examples

```powershell
# Get all aliases
Get-OPNSenseAlias

# Get a specific alias by name
Get-OPNSenseAlias -Name "WebServers"

# Get all network aliases
Get-OPNSenseAlias -Type "network"
```

### New-OPNSenseAlias

Creates a new alias on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseAlias
    -Name <String>
    -Type <String>
    -Content <String>
    [-Description <String>]
    [-Enabled <Boolean>]
    [-UpdateFrequency <Int32>]
    [-Force]
```

#### Parameters

- **Name** - The name of the alias.
- **Type** - The type of the alias (host, network, port, url, urltable, geoip).
- **Content** - The content of the alias (comma-separated list of values).
- **Description** - A description for the alias.
- **Enabled** - Whether the alias is enabled. Default is true.
- **UpdateFrequency** - The update frequency for URL and URL table aliases (in days).
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a host alias
New-OPNSenseAlias -Name "WebServers" -Type "host" -Content "192.168.1.10,192.168.1.11,192.168.1.12" -Description "Web Servers"

# Create a network alias
New-OPNSenseAlias -Name "InternalNetworks" -Type "network" -Content "192.168.1.0/24,192.168.2.0/24" -Description "Internal Networks"

# Create a URL table alias
New-OPNSenseAlias -Name "BlockList" -Type "urltable" -Content "https://www.example.com/blocklist.txt" -Description "Malicious IPs" -UpdateFrequency 1
```

### Apply-OPNSenseAliasChanges

Applies pending alias changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseAliasChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply alias changes
Apply-OPNSenseAliasChanges

# Apply alias changes without confirmation
Apply-OPNSenseAliasChanges -Force
```

## Network Utilities

### Invoke-OPNSenseNetworkCalculation

Performs various network calculations on IP networks.

#### Syntax

```powershell
Invoke-OPNSenseNetworkCalculation
    -Network <String>
    -Operation <String>
    [-PrefixLength <Int32>]
    [-SubnetCount <Int32>]
    [-IpAddress <String>]
    [-AdditionalNetworks <String[]>]
```

#### Parameters

- **Network** - The network in CIDR notation to perform calculations on.
- **Operation** - The operation to perform (Info, Subnet, SubnetByCount, Contains, Overlaps, Supernet, SupernetSummarize).
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

# Check if an IP is within a network
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Contains -IpAddress "192.168.1.100"
```

### ConvertTo-OPNSenseNetworkNotation

Converts between different network notation formats.

#### Syntax

```powershell
ConvertTo-OPNSenseNetworkNotation
    [-CIDR <Int32>]
    [-SubnetMask <String>]
```

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

## Tailscale Integration

### Get-OPNSenseTailscaleStatus

Retrieves the status of Tailscale on an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseTailscaleStatus
```

#### Examples

```powershell
# Get Tailscale status
Get-OPNSenseTailscaleStatus
```

### Install-OPNSenseTailscale

Installs the Tailscale plugin on an OPNSense firewall.

#### Syntax

```powershell
Install-OPNSenseTailscale
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Install Tailscale
Install-OPNSenseTailscale

# Install Tailscale without confirmation
Install-OPNSenseTailscale -Force
```

### Enable-OPNSenseTailscale

Enables Tailscale on an OPNSense firewall.

#### Syntax

```powershell
Enable-OPNSenseTailscale
    -AuthKey <String>
    [-Hostname <String>]
    [-AcceptDNS <Boolean>]
    [-AcceptRoutes <Boolean>]
    [-AdvertiseRoutes <String>]
    [-ExitNode <Boolean>]
    [-Force]
    [-PassThru]
```

#### Parameters

- **AuthKey** - The Tailscale authentication key.
- **Hostname** - The hostname to use for the Tailscale node.
- **AcceptDNS** - Whether to accept DNS settings from Tailscale.
- **AcceptRoutes** - Whether to accept routes from Tailscale.
- **AdvertiseRoutes** - The routes to advertise to Tailscale.
- **ExitNode** - Whether to use this node as an exit node.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the Tailscale status.

#### Examples

```powershell
# Enable Tailscale with basic settings
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456"

# Enable Tailscale with route advertisement
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -AdvertiseRoutes "192.168.1.0/24,10.0.0.0/8"
```

## Cron Job Management

### Get-OPNSenseCronJob

Retrieves cron jobs from an OPNSense firewall.

#### Syntax

```powershell
Get-OPNSenseCronJob
    [-Uuid <String>]
    [-Description <String>]
    [-Command <String>]
```

#### Parameters

- **Uuid** - The UUID of a specific cron job to retrieve.
- **Description** - Filter cron jobs by description.
- **Command** - Filter cron jobs by command.

#### Examples

```powershell
# Get all cron jobs
Get-OPNSenseCronJob

# Get a specific cron job by UUID
Get-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get cron jobs with a specific description
Get-OPNSenseCronJob -Description "Backup"
```

### New-OPNSenseCronJob

Creates a new cron job on an OPNSense firewall.

#### Syntax

```powershell
New-OPNSenseCronJob
    -Description <String>
    -Command <String>
    -Parameters <String>
    -Minute <String>
    -Hour <String>
    -Day <String>
    -Month <String>
    -Weekday <String>
    [-Enabled <Boolean>]
    [-Force]
```

#### Parameters

- **Description** - A description for the cron job.
- **Command** - The command to execute.
- **Parameters** - The parameters for the command.
- **Minute** - The minute(s) when the job should run (0-59, *, */5, etc.).
- **Hour** - The hour(s) when the job should run (0-23, *, */2, etc.).
- **Day** - The day(s) of the month when the job should run (1-31, *, */2, etc.).
- **Month** - The month(s) when the job should run (1-12, *, */2, etc.).
- **Weekday** - The weekday(s) when the job should run (0-7, *, etc., where 0 and 7 are Sunday).
- **Enabled** - Whether the cron job is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a daily backup cron job
New-OPNSenseCronJob -Description "Daily Backup" -Command "configctl" -Parameters "system backup" -Minute "0" -Hour "2" -Day "*" -Month "*" -Weekday "*"

# Create a weekly update cron job
New-OPNSenseCronJob -Description "Weekly Update" -Command "configctl" -Parameters "firmware update" -Minute "0" -Hour "3" -Day "*" -Month "*" -Weekday "0"
```

### Apply-OPNSenseCronJobChanges

Applies pending cron job changes on an OPNSense firewall.

#### Syntax

```powershell
Apply-OPNSenseCronJobChanges
    [-Force]
```

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply cron job changes
Apply-OPNSenseCronJobChanges

# Apply cron job changes without confirmation
Apply-OPNSenseCronJobChanges -Force
```
