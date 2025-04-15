# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all port forwarding rules
$rules = Get-OPNSensePortForwardingRule
Write-Output "Current port forwarding rules:"
$rules | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Create a new port forwarding rule for HTTP
$httpRule = New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "80" -TargetIP "192.168.1.100" -TargetPort "80" -Description "Web Server HTTP"
Write-Output "Created HTTP port forwarding rule:"
$httpRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Create a new port forwarding rule for HTTPS
$httpsRule = New-OPNSensePortForwardingRule -Interface "WAN" -Protocol "tcp" -DestinationPort "443" -TargetIP "192.168.1.100" -TargetPort "443" -Description "Web Server HTTPS"
Write-Output "Created HTTPS port forwarding rule:"
$httpsRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Update the HTTP rule to point to a different server
$updatedRule = Set-OPNSensePortForwardingRule -Uuid $httpRule.Uuid -TargetIP "192.168.1.200" -Description "Updated Web Server HTTP" -PassThru
Write-Output "Updated HTTP port forwarding rule:"
$updatedRule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Get a specific rule by UUID
$rule = Get-OPNSensePortForwardingRule -Uuid $httpsRule.Uuid
Write-Output "Retrieved HTTPS port forwarding rule:"
$rule | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Remove the HTTP rule
Remove-OPNSensePortForwardingRule -Uuid $httpRule.Uuid -Force
Write-Output "Removed HTTP port forwarding rule"

# Get all port forwarding rules again to verify changes
$rules = Get-OPNSensePortForwardingRule
Write-Output "Updated port forwarding rules:"
$rules | Format-Table -Property Uuid, Description, Interface, Protocol, DestinationPort, TargetIP, TargetPort

# Disconnect from the firewall
Disconnect-OPNSense
