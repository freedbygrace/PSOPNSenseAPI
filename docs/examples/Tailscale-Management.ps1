# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get Tailscale status
$status = Get-OPNSenseTailscaleStatus
Write-Output "Tailscale Status:"
$status

# Get detailed Tailscale status with interfaces and settings
$detailedStatus = Get-OPNSenseTailscaleStatus -IncludeInterfaces -IncludeSettings
Write-Output "Tailscale Interfaces:"
$detailedStatus.Interfaces

# Enable Tailscale with default settings (will install plugin if not present)
Enable-OPNSenseTailscale -Force

# Enable Tailscale with custom settings
Enable-OPNSenseTailscale -AcceptDns -AcceptRoutes -Hostname "opnsense-firewall" -Force

# Enable Tailscale with subnet route advertising
Enable-OPNSenseTailscale -AdvertiseRoutes -SubnetRoutes "192.168.1.0/24","10.0.0.0/8" -Force

# Connect to Tailscale network with an auth key
# You can generate an auth key at https://login.tailscale.com/admin/settings/keys
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing -EnableIfDisabled -StartIfStopped -Force

# Connect to Tailscale and advertise subnet routes
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -SubnetRoutes "192.168.1.0/24","10.0.0.0/8" -AdvertiseRoutes -Force

# Get Tailscale status after connecting
$connectedStatus = Get-OPNSenseTailscaleStatus -IncludeInterfaces
Write-Output "Tailscale Connected Status:"
$connectedStatus

# Disconnect from Tailscale network
Disconnect-OPNSenseTailscale -Force

# Disable Tailscale and stop the service
Disable-OPNSenseTailscale -Stop -Force

# Disconnect from the firewall
Disconnect-OPNSense
