# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get DNS server configuration
$dnsConfig = Get-OPNSenseDNSServer
Write-Output "DNS Server Configuration:"
$dnsConfig

# Update DNS server configuration
Set-OPNSenseDNSServer -Forwarding -Forwarders "8.8.8.8","8.8.4.4" -RegisterDhcp -RegisterDhcpDomain "local" -Apply

# Get all DNS overrides
$overrides = Get-OPNSenseDNSOverride
$overrides | Format-Table -Property Uuid, Hostname, Domain, IpAddress, Description

# Create a new DNS override
$overrideId = New-OPNSenseDNSOverride -Hostname "server" -Domain "local" -IpAddress "192.168.1.10" -Description "Internal server" -Apply
Write-Output "Created DNS override with ID: $overrideId"

# Remove a DNS override
Remove-OPNSenseDNSOverride -Uuid $overrideId -Apply -Confirm:$false

# Disconnect from the firewall
Disconnect-OPNSense
