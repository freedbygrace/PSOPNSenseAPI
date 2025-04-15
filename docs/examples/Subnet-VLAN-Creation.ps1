# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get available interfaces
$interfaces = Get-OPNSenseInterface
$interfaces | Format-Table -Property Name, Description, Status

# Create VLANs for subnets with sequential VLAN IDs
$result1 = New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.0.0/24" -SubnetMaskBits 27 -StartingVlanId 10 -VlanIdIncrement 1
Write-Output "Created VLANs with sequential IDs:"
$result1.VLANs | Format-Table -Property VlanId, Subnet, Gateway, Status

# Create VLANs for subnets with VLAN IDs in multiples of 10
$result2 = New-OPNSenseSubnetVLANs -ParentInterface "em1" -Network "10.0.0.0/16" -SubnetMaskBits 24 -StartingVlanId 100 -VlanIdIncrement 10
Write-Output "Created VLANs with IDs in multiples of 10:"
$result2.VLANs | Format-Table -Property VlanId, Subnet, Gateway, Status

# Create VLANs for subnets with DHCP enabled
$result3 = New-OPNSenseSubnetVLANs -ParentInterface "em2" -Network "172.16.0.0/20" -SubnetMaskBits 24 -StartingVlanId 200 -VlanIdIncrement 1 -EnableDHCP -DescriptionPrefix "VLAN-DHCP"
Write-Output "Created VLANs with DHCP enabled:"
$result3.VLANs | Format-Table -Property VlanId, Subnet, Gateway, Status

# Get all VLANs
$vlans = Get-OPNSenseVLAN
$vlans | Format-Table -Property Uuid, Tag, Interface, Description

# Disconnect from the firewall
Disconnect-OPNSense
