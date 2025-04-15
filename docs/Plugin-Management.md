# Plugin Management

This component provides cmdlets for managing plugins on OPNSense firewalls.

## Overview

The Plugin Management component allows you to install, configure, and manage plugins on OPNSense firewalls. It provides cmdlets for listing available plugins, installing and uninstalling plugins, and managing plugin configurations.

## Cmdlets

### Get-OPNSensePlugin

Retrieves a list of available and installed plugins on an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific plugin to retrieve. If not specified, all plugins are returned.
- **Category** - Filter plugins by category.
- **Installed** - Filter plugins by installation status.

#### Examples

```powershell
# Get all plugins
Get-OPNSensePlugin

# Get a specific plugin by name
Get-OPNSensePlugin -Name "os-tailscale"

# Get all installed plugins
Get-OPNSensePlugin -Installed $true

# Get all plugins in a specific category
Get-OPNSensePlugin -Category "security"
```

### Install-OPNSensePlugin

Installs a plugin on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to install.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Install a plugin
Install-OPNSensePlugin -Name "os-tailscale"

# Install a plugin without confirmation
Install-OPNSensePlugin -Name "os-wireguard" -Force
```

### Uninstall-OPNSensePlugin

Uninstalls a plugin from an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to uninstall.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Uninstall a plugin
Uninstall-OPNSensePlugin -Name "os-tailscale"

# Uninstall a plugin without confirmation
Uninstall-OPNSensePlugin -Name "os-wireguard" -Force
```

### Enable-OPNSensePlugin

Enables a plugin on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to enable.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Enable a plugin
Enable-OPNSensePlugin -Name "os-tailscale"

# Enable a plugin without confirmation
Enable-OPNSensePlugin -Name "os-wireguard" -Force
```

### Disable-OPNSensePlugin

Disables a plugin on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to disable.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Disable a plugin
Disable-OPNSensePlugin -Name "os-tailscale"

# Disable a plugin without confirmation
Disable-OPNSensePlugin -Name "os-wireguard" -Force
```

### Get-OPNSensePluginConfiguration

Retrieves the configuration of a plugin on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to get the configuration for.

#### Examples

```powershell
# Get the configuration of a plugin
Get-OPNSensePluginConfiguration -Name "os-tailscale"
```

### Set-OPNSensePluginConfiguration

Updates the configuration of a plugin on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to update the configuration for.
- **Configuration** - The configuration to set.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Update the configuration of a plugin
$config = @{
    enabled = "1"
    settings = @{
        hostname = "opnsense-tailscale"
        acceptDns = "1"
    }
}
Set-OPNSensePluginConfiguration -Name "os-tailscale" -Configuration $config

# Update the configuration of a plugin without confirmation
Set-OPNSensePluginConfiguration -Name "os-tailscale" -Configuration $config -Force
```

### Apply-OPNSensePluginChanges

Applies pending plugin changes on an OPNSense firewall.

#### Parameters

- **Name** - The name of the plugin to apply changes for.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply changes for a plugin
Apply-OPNSensePluginChanges -Name "os-tailscale"

# Apply changes for a plugin without confirmation
Apply-OPNSensePluginChanges -Name "os-tailscale" -Force
```

## Common Scenarios

### Installing and Configuring a Plugin

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Install the Tailscale plugin
Install-OPNSensePlugin -Name "os-tailscale" -Force

# Configure the plugin
$config = @{
    enabled = "1"
    settings = @{
        hostname = "opnsense-tailscale"
        acceptDns = "1"
        acceptRoutes = "1"
        advertiseRoutes = "192.168.1.0/24,10.0.0.0/8"
    }
}
Set-OPNSensePluginConfiguration -Name "os-tailscale" -Configuration $config -Force

# Apply the changes
Apply-OPNSensePluginChanges -Name "os-tailscale" -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Multiple Plugins

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define plugins to install
$plugins = @(
    "os-tailscale",
    "os-wireguard",
    "os-acme-client",
    "os-theme-vicuna"
)

# Install plugins
foreach ($plugin in $plugins) {
    Install-OPNSensePlugin -Name $plugin -Force
    Write-Output "Installed plugin: $plugin"
}

# Get all installed plugins
$installedPlugins = Get-OPNSensePlugin -Installed $true
Write-Output "Installed plugins:"
$installedPlugins | Format-Table -Property Name, Version, Description

# Disconnect from the firewall
Disconnect-OPNSense
```

### Updating Plugin Configurations

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the current configuration of a plugin
$currentConfig = Get-OPNSensePluginConfiguration -Name "os-wireguard"

# Update the configuration
$currentConfig.server.enabled = "1"
$currentConfig.server.port = "51820"
$currentConfig.server.dns = "192.168.1.1"

# Set the updated configuration
Set-OPNSensePluginConfiguration -Name "os-wireguard" -Configuration $currentConfig -Force

# Apply the changes
Apply-OPNSensePluginChanges -Name "os-wireguard" -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Cleaning Up Unused Plugins

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all installed plugins
$installedPlugins = Get-OPNSensePlugin -Installed $true

# Define plugins to keep
$pluginsToKeep = @(
    "os-tailscale",
    "os-wireguard",
    "os-acme-client"
)

# Uninstall unused plugins
foreach ($plugin in $installedPlugins) {
    if ($pluginsToKeep -notcontains $plugin.Name) {
        Uninstall-OPNSensePlugin -Name $plugin.Name -Force
        Write-Output "Uninstalled plugin: $($plugin.Name)"
    }
}

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Plugin installation and uninstallation may require a firewall reboot to take effect.
- Some plugins may have dependencies that need to be installed first.
- Plugin configurations vary widely depending on the plugin.
- Always check the OPNSense documentation for specific plugin requirements and configuration options.
- Plugin management requires administrative privileges.
- Plugin installation and uninstallation can take some time, especially for larger plugins.
- Some plugins may not be available for all OPNSense versions.
- Plugin configurations are typically stored in XML format.
- Changes to plugin configurations may not take effect until the plugin service is restarted or the firewall is rebooted.
- Consider using the `-Force` parameter when installing or uninstalling plugins in scripts to avoid confirmation prompts.
