# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all installed plugins
$installedPlugins = Get-OPNSensePlugin -Installed
$installedPlugins | Format-Table -Property Name, Version, Comment, Enabled

# Get all available plugins
$availablePlugins = Get-OPNSensePlugin -Available
$availablePlugins | Format-Table -Property Name, Version, Comment

# Install a plugin
Install-OPNSensePlugin -Name "os-acme-client" -Wait
Write-Output "Plugin installed successfully"

# Enable a plugin
Enable-OPNSensePlugin -Name "os-acme-client"

# Disable a plugin
Disable-OPNSensePlugin -Name "os-acme-client"

# Uninstall a plugin
Uninstall-OPNSensePlugin -Name "os-acme-client" -Wait -Confirm:$false

# Disconnect from the firewall
Disconnect-OPNSense
