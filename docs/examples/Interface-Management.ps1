# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all interfaces
$interfaces = Get-OPNSenseInterface
$interfaces | Format-Table -Property Name, Description, IpAddress, SubnetMask, Status

# Get interface statistics
$stats = Get-OPNSenseInterfaceStatistics
$stats | Format-Table -Property Name, InPackets, OutPackets, InBytes, OutBytes

# Update an interface
Set-OPNSenseInterface -Name "lan" -Description "Local Area Network" -IpAddress "192.168.1.1" -SubnetMask "24" -Apply

# Create a new VLAN
$vlanId = New-OPNSenseVLAN -Interface "em0" -Tag 10 -Description "Management VLAN"
Write-Output "Created VLAN with ID: $vlanId"

# Get all VLANs
$vlans = Get-OPNSenseVLAN
$vlans | Format-Table -Property Uuid, Tag, Interface, Description

# Disconnect from the firewall
Disconnect-OPNSense
