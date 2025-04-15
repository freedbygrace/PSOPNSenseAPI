# Tailscale Integration

This component provides cmdlets for managing Tailscale VPN on OPNSense firewalls.

## Overview

The Tailscale Integration component allows you to install, configure, and manage Tailscale VPN on OPNSense firewalls. It provides cmdlets for enabling Tailscale, configuring subnet routes, and managing the Tailscale connection.

The module will automatically install the Tailscale plugin if it doesn't exist, making it easy to set up Tailscale on a new OPNSense firewall.

## Cmdlets

### Get-OPNSenseTailscaleStatus

Retrieves the status of Tailscale on an OPNSense firewall.

#### Examples

```powershell
# Get Tailscale status
Get-OPNSenseTailscaleStatus
```

### Install-OPNSenseTailscale

Installs the Tailscale plugin on an OPNSense firewall.

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

# Enable Tailscale with a custom hostname
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -Hostname "opnsense-firewall"

# Enable Tailscale with DNS and route acceptance
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -AcceptDNS -AcceptRoutes

# Enable Tailscale with route advertisement
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -AdvertiseRoutes "192.168.1.0/24,10.0.0.0/8"

# Enable Tailscale as an exit node
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -ExitNode
```

### Disable-OPNSenseTailscale

Disables Tailscale on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Disable Tailscale
Disable-OPNSenseTailscale

# Disable Tailscale without confirmation
Disable-OPNSenseTailscale -Force
```

### Set-OPNSenseTailscaleRoutes

Updates the routes advertised by Tailscale on an OPNSense firewall.

#### Parameters

- **AdvertiseRoutes** - The routes to advertise to Tailscale.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the Tailscale status.

#### Examples

```powershell
# Set Tailscale routes
Set-OPNSenseTailscaleRoutes -AdvertiseRoutes "192.168.1.0/24,10.0.0.0/8"

# Set Tailscale routes without confirmation
Set-OPNSenseTailscaleRoutes -AdvertiseRoutes "192.168.1.0/24,10.0.0.0/8" -Force
```

### Get-OPNSenseTailscalePeers

Retrieves the Tailscale peers connected to an OPNSense firewall.

#### Examples

```powershell
# Get Tailscale peers
Get-OPNSenseTailscalePeers
```

### Apply-OPNSenseTailscaleChanges

Applies pending Tailscale changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply Tailscale changes
Apply-OPNSenseTailscaleChanges

# Apply Tailscale changes without confirmation
Apply-OPNSenseTailscaleChanges -Force
```

### Connect-OPNSenseTailscale

Connects to the Tailscale network using an authentication key.

#### Parameters

- **AuthKey** - The Tailscale authentication key.
- **InstallIfMissing** - Installs the Tailscale plugin if it's not already installed.
- **EnableIfDisabled** - Enables Tailscale if it's disabled.
- **StartIfStopped** - Starts the Tailscale service if it's stopped.
- **SubnetRoutes** - The subnet routes to advertise to Tailscale.
- **AdvertiseRoutes** - Whether to advertise routes to Tailscale.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the Tailscale status.

#### Examples

```powershell
# Connect to Tailscale with an auth key
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456"

# Connect to Tailscale with automatic installation and enablement
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing -EnableIfDisabled -StartIfStopped

# Connect to Tailscale and advertise subnet routes
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -SubnetRoutes "192.168.1.0/24","10.0.0.0/8" -AdvertiseRoutes
```

### Disconnect-OPNSenseTailscale

Disconnects from the Tailscale network.

#### Parameters

- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the Tailscale status.

#### Examples

```powershell
# Disconnect from Tailscale
Disconnect-OPNSenseTailscale

# Disconnect from Tailscale without confirmation
Disconnect-OPNSenseTailscale -Force
```

## Common Scenarios

### Installing and Configuring Tailscale

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Install Tailscale
Install-OPNSenseTailscale -Force

# Enable Tailscale with basic settings
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -Hostname "opnsense-firewall" -Force

# Apply the changes
Apply-OPNSenseTailscaleChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Simplified Installation and Connection

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Connect to Tailscale with automatic installation and enablement
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing -EnableIfDisabled -StartIfStopped -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Configuring Tailscale as a Subnet Router

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the current network interfaces
$interfaces = Get-OPNSenseInterface

# Build a list of subnets to advertise
$subnets = @()
foreach ($interface in $interfaces) {
    if ($interface.Name -ne "wan" -and $interface.IPv4Address) {
        $subnet = "$($interface.IPv4Network)/$($interface.IPv4Subnet)"
        $subnets += $subnet
    }
}

# Connect to Tailscale with subnet route advertisement
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -SubnetRoutes $subnets -AdvertiseRoutes -InstallIfMissing -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Configuring Tailscale as an Exit Node

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Enable Tailscale as an exit node
Enable-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -ExitNode -Force

# Apply the changes
Apply-OPNSenseTailscaleChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Connecting and Disconnecting from Tailscale

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Connect to Tailscale
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing -EnableIfDisabled -StartIfStopped -Force

# Get Tailscale status
$status = Get-OPNSenseTailscaleStatus
Write-Output "Tailscale Status: $($status.Status)"

# Disconnect from Tailscale
Disconnect-OPNSenseTailscale -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Updating Tailscale Routes

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the current Tailscale status
$status = Get-OPNSenseTailscaleStatus

# Add a new subnet to the advertised routes
$currentRoutes = $status.AdvertisedRoutes -split ","
$newSubnet = "172.16.0.0/16"
if ($currentRoutes -notcontains $newSubnet) {
    $currentRoutes += $newSubnet
}

# Update the advertised routes
$advertisedRoutes = $currentRoutes -join ","
Set-OPNSenseTailscaleRoutes -AdvertiseRoutes $advertisedRoutes -Force

# Apply the changes
Apply-OPNSenseTailscaleChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Monitoring Tailscale Peers

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get Tailscale peers
$peers = Get-OPNSenseTailscalePeers

# Display peer information
$peers | Format-Table -Property Name, IP, Status, LastSeen

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Tailscale requires the Tailscale plugin to be installed on the OPNSense firewall.
- The `Install-OPNSenseTailscale` cmdlet will install the plugin if it's not already installed.
- The `Connect-OPNSenseTailscale` cmdlet can automatically install the plugin, enable Tailscale, and start the service with the `-InstallIfMissing`, `-EnableIfDisabled`, and `-StartIfStopped` parameters.
- Tailscale authentication keys can be generated from the Tailscale admin console.
- Authentication keys are single-use and expire after a short period.
- When advertising routes, ensure that the routes are valid and do not overlap with Tailscale's internal routes.
- The `AcceptDNS` option allows Tailscale to configure DNS settings on the OPNSense firewall.
- The `AcceptRoutes` option allows Tailscale to add routes to the OPNSense firewall.
- The `ExitNode` option allows other Tailscale nodes to use the OPNSense firewall as an internet gateway.
- Changes to Tailscale settings are not applied until you call `Apply-OPNSenseTailscaleChanges`.
- Consider using the `-PassThru` parameter when updating Tailscale settings to verify the changes.
- The `Connect-OPNSenseTailscale` cmdlet combines multiple steps (installation, enablement, and connection) into a single command.
- The `Disconnect-OPNSenseTailscale` cmdlet disconnects from the Tailscale network but does not disable Tailscale.
