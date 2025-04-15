# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all DHCP servers
$dhcpServers = Get-OPNSenseDHCPServer
$dhcpServers | Format-Table -Property Interface, Enabled, RangeFrom, RangeTo, Gateway

# Get a specific DHCP server
$lanDhcp = Get-OPNSenseDHCPServer -Interface "lan"
$lanDhcp

# Update a DHCP server
Set-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -Domain "example.local" -DnsServers "8.8.8.8","8.8.4.4" -Apply

# Get all DHCP leases
$dhcpLeases = Get-OPNSenseDHCPLease
$dhcpLeases | Format-Table -Property MacAddress, IpAddress, Hostname, State

# Get active DHCP leases
$activeLeases = $dhcpLeases | Where-Object { $_.State -eq "active" }
$activeLeases | Format-Table -Property MacAddress, IpAddress, Hostname

# Get all static mappings for an interface
$staticMappings = Get-OPNSenseDHCPStaticMapping -Interface "lan"
$staticMappings | Format-Table -Property Uuid, MacAddress, IpAddress, Hostname, Description

# Create a new static mapping
$newMapping = New-OPNSenseDHCPStaticMapping -Interface "lan" -MacAddress "00:11:22:33:44:55" -IpAddress "192.168.1.50" -Hostname "printer" -Description "Office Printer" -Apply
$newMapping

# Update a static mapping
Set-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid $newMapping -IpAddress "192.168.1.51" -Description "Updated Printer" -Apply

# Remove a static mapping
Remove-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid $newMapping -Force -Apply

# Disconnect from the firewall
Disconnect-OPNSense
