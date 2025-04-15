# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get DNS server configuration
$dnsServer = Get-OPNSenseDNSServer
Write-Output "DNS Server Configuration:"
$dnsServer | Format-List

# Update DNS server configuration
Set-OPNSenseDNSServer -Forwarding -Forwarders "8.8.8.8","8.8.4.4" -Apply
Write-Output "DNS Server updated to use Google DNS"

# Get DNS forwarding configuration
$dnsForwarding = Get-OPNSenseDNSForwarding
Write-Output "DNS Forwarding Configuration:"
$dnsForwarding | Format-List

# Enable DNS forwarding
Set-OPNSenseDNSForwarding -Enabled -DnsServers "1.1.1.1","1.0.0.1" -Apply
Write-Output "DNS Forwarding enabled with Cloudflare DNS"

# Get DNS forwarding hosts
$forwardingHosts = Get-OPNSenseDNSForwardingHost
Write-Output "DNS Forwarding Hosts:"
$forwardingHosts | Format-Table

# Create a new DNS forwarding host
$newHost = New-OPNSenseDNSForwardingHost -Domain "example.com" -Server "192.168.1.10" -Description "Internal DNS Server" -Apply
Write-Output "Created new forwarding host with UUID: $newHost"

# Update a DNS forwarding host
Set-OPNSenseDNSForwardingHost -Uuid $newHost -Server "192.168.1.20" -Apply
Write-Output "Updated forwarding host server"

# Remove a DNS forwarding host
Remove-OPNSenseDNSForwardingHost -Uuid $newHost -Force -Apply
Write-Output "Removed forwarding host"

# Get system DNS configuration
$systemDNS = Get-OPNSenseSystemDNS
Write-Output "System DNS Configuration:"
$systemDNS | Format-List

# Update system DNS configuration
Set-OPNSenseSystemDNS -Hostname "firewall" -Domain "example.com" -DnsServers "8.8.8.8","8.8.4.4" -Apply
Write-Output "Updated system DNS configuration"

# Disconnect from the firewall
Disconnect-OPNSense
