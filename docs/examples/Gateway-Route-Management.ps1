# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all gateways
$gateways = Get-OPNSenseGateway
Write-Output "Gateways:"
$gateways | Format-Table

# Get gateways with status information
$gatewaysWithStatus = Get-OPNSenseGateway -IncludeStatus
Write-Output "Gateways with Status:"
$gatewaysWithStatus | Format-Table

# Create a new gateway
$newGateway = New-OPNSenseGateway -Name "WAN2_GW" -Interface "opt1" -IpAddress "203.0.113.1" -Description "Secondary WAN" -Apply
Write-Output "Created new gateway with UUID: $newGateway"

# Update a gateway
Set-OPNSenseGateway -Uuid $newGateway -MonitorIp "8.8.8.8" -Weight 2 -Apply
Write-Output "Updated gateway monitor IP and weight"

# Make a gateway the default gateway
Set-OPNSenseGateway -Uuid $newGateway -Default -Apply
Write-Output "Set gateway as default"

# Get all routes
$routes = Get-OPNSenseRoute
Write-Output "Routes:"
$routes | Format-Table

# Create a new route
$newRoute = New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN2_GW" -Description "Remote Office" -Apply
Write-Output "Created new route with UUID: $newRoute"

# Update a route
Set-OPNSenseRoute -Uuid $newRoute -Gateway "WAN_GW" -Apply
Write-Output "Updated route gateway"

# Disable a route
Set-OPNSenseRoute -Uuid $newRoute -Disabled -Apply
Write-Output "Disabled route"

# Enable a route
Set-OPNSenseRoute -Uuid $newRoute -Enabled -Apply
Write-Output "Enabled route"

# Remove a route
Remove-OPNSenseRoute -Uuid $newRoute -Force -Apply
Write-Output "Removed route"

# Remove a gateway
Remove-OPNSenseGateway -Uuid $newGateway -Force -Apply
Write-Output "Removed gateway"

# Disconnect from the firewall
Disconnect-OPNSense
