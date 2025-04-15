# Firmware Management

This component provides cmdlets for managing firmware on OPNSense firewalls.

## Overview

The Firmware Management component allows you to check for updates, install updates, and manage firmware settings on OPNSense firewalls. It provides cmdlets for checking firmware status, upgrading firmware, and managing firmware settings.

## Cmdlets

### Get-OPNSenseFirmwareStatus

Retrieves the current firmware status of an OPNSense firewall.

#### Examples

```powershell
# Get firmware status
Get-OPNSenseFirmwareStatus
```

### Get-OPNSenseFirmwareUpdate

Checks for available firmware updates for an OPNSense firewall.

#### Parameters

- **Force** - Forces a check for updates even if the update cache is current.

#### Examples

```powershell
# Check for updates
Get-OPNSenseFirmwareUpdate

# Force a check for updates
Get-OPNSenseFirmwareUpdate -Force
```

### Install-OPNSenseFirmwareUpdate

Installs available firmware updates on an OPNSense firewall.

#### Parameters

- **RebootAfterUpdate** - Whether to reboot the firewall after updating. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Install updates
Install-OPNSenseFirmwareUpdate

# Install updates and reboot
Install-OPNSenseFirmwareUpdate -RebootAfterUpdate

# Install updates without confirmation
Install-OPNSenseFirmwareUpdate -Force
```

### Invoke-OPNSenseFirmwareUpgrade

Performs a firmware upgrade on an OPNSense firewall.

#### Parameters

- **MajorVersion** - The major version to upgrade to.
- **RebootAfterUpgrade** - Whether to reboot the firewall after upgrading. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Upgrade to the latest version
Invoke-OPNSenseFirmwareUpgrade

# Upgrade to a specific major version
Invoke-OPNSenseFirmwareUpgrade -MajorVersion "23.1"

# Upgrade and reboot
Invoke-OPNSenseFirmwareUpgrade -RebootAfterUpgrade

# Upgrade without confirmation
Invoke-OPNSenseFirmwareUpgrade -Force
```

### Get-OPNSenseFirmwareSettings

Retrieves the firmware settings of an OPNSense firewall.

#### Examples

```powershell
# Get firmware settings
Get-OPNSenseFirmwareSettings
```

### Set-OPNSenseFirmwareSettings

Updates the firmware settings of an OPNSense firewall.

#### Parameters

- **Mirror** - The firmware mirror to use.
- **Flavour** - The firmware flavour to use (e.g., OpenSSL, LibreSSL).
- **Subscription** - The firmware subscription to use.
- **AutomaticUpdates** - Whether to enable automatic updates.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Set firmware mirror
Set-OPNSenseFirmwareSettings -Mirror "https://opnsense.c0urier.net"

# Enable automatic updates
Set-OPNSenseFirmwareSettings -AutomaticUpdates $true

# Set multiple firmware settings
Set-OPNSenseFirmwareSettings -Mirror "https://opnsense.c0urier.net" -Flavour "OpenSSL" -AutomaticUpdates $true

# Set firmware settings without confirmation
Set-OPNSenseFirmwareSettings -Mirror "https://opnsense.c0urier.net" -Force
```

### Get-OPNSenseFirmwareAudit

Retrieves the firmware audit log of an OPNSense firewall.

#### Parameters

- **Limit** - The maximum number of entries to retrieve. Default is 50.

#### Examples

```powershell
# Get firmware audit log
Get-OPNSenseFirmwareAudit

# Get the last 100 entries from the firmware audit log
Get-OPNSenseFirmwareAudit -Limit 100
```

### Invoke-OPNSenseReboot

Reboots an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.
- **Wait** - Whether to wait for the firewall to come back online. Default is false.
- **Timeout** - The timeout in seconds to wait for the firewall to come back online. Default is 300.

#### Examples

```powershell
# Reboot the firewall
Invoke-OPNSenseReboot

# Reboot the firewall without confirmation
Invoke-OPNSenseReboot -Force

# Reboot the firewall and wait for it to come back online
Invoke-OPNSenseReboot -Wait

# Reboot the firewall and wait with a custom timeout
Invoke-OPNSenseReboot -Wait -Timeout 600
```

## Common Scenarios

### Checking and Installing Updates

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Check firmware status
$status = Get-OPNSenseFirmwareStatus
Write-Output "Current firmware version: $($status.Version)"
Write-Output "Current firmware status: $($status.Status)"

# Check for updates
$updates = Get-OPNSenseFirmwareUpdate -Force
if ($updates.Count -gt 0) {
    Write-Output "Updates available:"
    $updates | Format-Table -Property Name, Version, Size, Description
    
    # Install updates
    Install-OPNSenseFirmwareUpdate -Force
    
    # Reboot if necessary
    if ($updates | Where-Object { $_.RebootRequired -eq $true }) {
        Write-Output "Rebooting to apply updates..."
        Invoke-OPNSenseReboot -Force -Wait
    }
} else {
    Write-Output "No updates available."
}

# Disconnect from the firewall
Disconnect-OPNSense
```

### Upgrading to a New Major Version

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Check current version
$status = Get-OPNSenseFirmwareStatus
Write-Output "Current firmware version: $($status.Version)"

# Perform a backup before upgrading
New-OPNSenseBackup -IncludeRRD -IncludeInstalled -Force
$backups = Get-OPNSenseBackup
$latestBackup = $backups | Sort-Object -Property Date -Descending | Select-Object -First 1
$backupPath = "C:\Backups\opnsense-pre-upgrade-$(Get-Date -Format 'yyyyMMdd').xml"
Save-OPNSenseBackup -BackupId $latestBackup.Id -Path $backupPath -Force
Write-Output "Backup saved to: $backupPath"

# Upgrade to the latest version
Write-Output "Upgrading firmware..."
Invoke-OPNSenseFirmwareUpgrade -RebootAfterUpgrade -Force

# Wait for the firewall to come back online
Write-Output "Waiting for the firewall to come back online..."
Start-Sleep -Seconds 300

# Reconnect to the firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Check new version
$newStatus = Get-OPNSenseFirmwareStatus
Write-Output "New firmware version: $($newStatus.Version)"

# Disconnect from the firewall
Disconnect-OPNSense
```

### Configuring Firmware Settings

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get current firmware settings
$settings = Get-OPNSenseFirmwareSettings
Write-Output "Current firmware settings:"
$settings | Format-List

# Update firmware settings
Set-OPNSenseFirmwareSettings -Mirror "https://opnsense.c0urier.net" -Flavour "OpenSSL" -AutomaticUpdates $true -Force

# Get updated firmware settings
$updatedSettings = Get-OPNSenseFirmwareSettings
Write-Output "Updated firmware settings:"
$updatedSettings | Format-List

# Disconnect from the firewall
Disconnect-OPNSense
```

### Monitoring Firmware Audit Log

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get firmware audit log
$auditLog = Get-OPNSenseFirmwareAudit -Limit 20
Write-Output "Recent firmware changes:"
$auditLog | Format-Table -Property Date, User, Action, Package

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Firmware updates and upgrades can take some time to complete.
- Always create a backup before performing firmware updates or upgrades.
- Some updates may require a reboot to take effect.
- Major version upgrades should be performed with caution and only after thorough testing.
- Automatic updates can be configured to run at specific times.
- Firmware mirrors can affect the download speed and availability of updates.
- The firmware audit log provides a history of firmware changes.
- Consider using the `-Force` parameter when performing firmware operations in scripts to avoid confirmation prompts.
- The `-Wait` parameter for `Invoke-OPNSenseReboot` can be useful for scripts that need to wait for the firewall to come back online.
- Firmware settings are typically stored in the OPNSense configuration file.
- Some firmware operations may require administrative privileges.
- Firmware updates and upgrades can fail if there is insufficient disk space.
- Always check the OPNSense release notes before upgrading to a new major version.
