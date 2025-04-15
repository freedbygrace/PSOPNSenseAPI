# VLAN and Subnet Management

This component provides cmdlets for managing VLANs and subnets on OPNSense firewalls.

## Overview

The VLAN and Subnet Management component allows you to create, view, modify, and delete VLANs on OPNSense firewalls. It also includes advanced functionality for automatically creating VLANs from subnet divisions, making it easy to implement complex network segmentation.

## Cmdlets

### Get-OPNSenseVLAN

Retrieves VLAN configurations from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific VLAN to retrieve. If not specified, all VLANs are returned.

#### Examples

```powershell
# Get all VLANs
Get-OPNSenseVLAN

# Get a specific VLAN by UUID
Get-OPNSenseVLAN -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```

### New-OPNSenseVLAN

Creates a new VLAN on an OPNSense firewall.

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

# Create a new VLAN with priority and without confirmation
New-OPNSenseVLAN -Interface "em1" -Tag 20 -Priority 5 -Description "Voice VLAN" -Force
```

### Remove-OPNSenseVLAN

Removes a VLAN from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the VLAN to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a VLAN
Remove-OPNSenseVLAN -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a VLAN without confirmation
Remove-OPNSenseVLAN -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### New-OPNSenseSubnetVLANs

Creates VLANs for subnets by dividing a network into smaller subnets.

#### Parameters

- **ParentInterface** - The parent interface for the VLANs.
- **Network** - The network in CIDR notation to divide into subnets.
- **SubnetMaskBits** - The subnet mask bits for the subnets.
- **StartingVlanId** - The starting VLAN ID.
- **VlanIdIncrement** - The increment for VLAN IDs.
- **DescriptionPrefix** - The prefix for VLAN descriptions.
- **EnableDHCP** - Whether to enable DHCP on the created VLANs.
- **DHCPRangeStart** - The starting IP address for the DHCP range.
- **DHCPRangeEnd** - The ending IP address for the DHCP range.
- **DHCPDomain** - The domain name for DHCP clients.
- **DHCPDnsServers** - The DNS servers for DHCP clients.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create VLANs for subnets with sequential VLAN IDs
New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.0.0/24" -SubnetMaskBits 27 -StartingVlanId 10 -VlanIdIncrement 1

# Create VLANs for subnets with VLAN IDs in multiples of 10
New-OPNSenseSubnetVLANs -ParentInterface "em1" -Network "10.0.0.0/16" -SubnetMaskBits 24 -StartingVlanId 100 -VlanIdIncrement 10

# Create VLANs for subnets with DHCP enabled
New-OPNSenseSubnetVLANs -ParentInterface "em2" -Network "172.16.0.0/20" -SubnetMaskBits 24 -StartingVlanId 200 -VlanIdIncrement 1 -EnableDHCP -DescriptionPrefix "VLAN-DHCP"

# Create VLANs with custom DHCP settings
New-OPNSenseSubnetVLANs -ParentInterface "em3" -Network "192.168.100.0/24" -SubnetMaskBits 26 -StartingVlanId 300 -EnableDHCP -DHCPDomain "example.com" -DHCPDnsServers "8.8.8.8","8.8.4.4"
```

## Common Scenarios

### Basic VLAN Management

```powershell
# Get all VLANs
$vlans = Get-OPNSenseVLAN
$vlans | Format-Table -Property Uuid, Tag, Interface, Description

# Create a new VLAN
New-OPNSenseVLAN -Interface "em0" -Tag 100 -Description "Server VLAN"

# Remove a VLAN
$vlanToRemove = $vlans | Where-Object { $_.Description -eq "Temporary VLAN" }
if ($vlanToRemove) {
    Remove-OPNSenseVLAN -Uuid $vlanToRemove.Uuid -Force
}
```

### Network Segmentation with VLANs

```powershell
# Create VLANs for different departments
$departments = @{
    "Management" = 10
    "Finance" = 20
    "HR" = 30
    "Engineering" = 40
    "Sales" = 50
    "Guest" = 60
}

foreach ($dept in $departments.GetEnumerator()) {
    New-OPNSenseVLAN -Interface "em0" -Tag $dept.Value -Description "$($dept.Key) VLAN" -Force
}
```

### Automated Subnet Division with VLANs

```powershell
# Divide a /24 network into /27 subnets (8 subnets) with VLANs
New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.1.0/24" -SubnetMaskBits 27 -StartingVlanId 100 -VlanIdIncrement 1 -DescriptionPrefix "Dept-"

# Divide a /16 network into /20 subnets (16 subnets) with VLANs in multiples of 10
New-OPNSenseSubnetVLANs -ParentInterface "em1" -Network "10.0.0.0/16" -SubnetMaskBits 20 -StartingVlanId 100 -VlanIdIncrement 10 -DescriptionPrefix "Branch-"

# Create VLANs with DHCP for a campus network
New-OPNSenseSubnetVLANs -ParentInterface "em2" -Network "172.16.0.0/16" -SubnetMaskBits 22 -StartingVlanId 200 -VlanIdIncrement 1 -EnableDHCP -DescriptionPrefix "Campus-" -DHCPDomain "campus.example.com"
```

### Creating a Multi-Tenant Network

```powershell
# Create a multi-tenant network with VLANs and DHCP
$tenantNetwork = "10.100.0.0/16"
$tenantCount = 16  # Number of tenants
$subnetMaskBits = 20  # /20 networks for each tenant (4,094 hosts per tenant)

New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network $tenantNetwork -SubnetMaskBits $subnetMaskBits -StartingVlanId 1000 -VlanIdIncrement 1 -EnableDHCP -DescriptionPrefix "Tenant-" -DHCPDomain "tenants.example.com" -DHCPDnsServers "10.0.0.1","10.0.0.2"
```

## Notes

- When creating VLANs, ensure that the parent interface supports VLAN tagging.
- The `New-OPNSenseSubnetVLANs` cmdlet automatically creates interfaces for each VLAN.
- If DHCP is enabled, the cmdlet configures DHCP servers for each VLAN with appropriate ranges.
- The VLAN ID must be between 1 and 4094, and must be unique for each parent interface.
- When dividing networks, ensure that the subnet mask bits are larger than the original network's mask bits (e.g., dividing a /24 into /27 subnets).
- The cmdlet automatically calculates appropriate gateway addresses for each subnet (first usable IP address).
