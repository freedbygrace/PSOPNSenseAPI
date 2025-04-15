# System Backup and Restore

This component provides cmdlets for backing up and restoring OPNSense firewall configurations.

## Overview

The System Backup and Restore component allows you to create, download, upload, and restore backups of OPNSense firewall configurations. It provides cmdlets for managing full and partial backups, as well as scheduling automatic backups.

## Cmdlets

### Get-OPNSenseBackup

Retrieves a list of available backups on an OPNSense firewall.

#### Examples

```powershell
# Get all available backups
Get-OPNSenseBackup
```

### New-OPNSenseBackup

Creates a new backup of an OPNSense firewall configuration.

#### Parameters

- **IncludeRRD** - Whether to include RRD data in the backup. Default is false.
- **IncludeInstalled** - Whether to include installed packages in the backup. Default is false.
- **Encrypted** - Whether to encrypt the backup. Default is false.
- **Password** - The password to use for encryption.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic configuration backup
New-OPNSenseBackup

# Create a full backup including RRD data and installed packages
New-OPNSenseBackup -IncludeRRD -IncludeInstalled

# Create an encrypted backup
New-OPNSenseBackup -Encrypted -Password "SecurePassword123"

# Create a full encrypted backup without confirmation
New-OPNSenseBackup -IncludeRRD -IncludeInstalled -Encrypted -Password "SecurePassword123" -Force
```

### Save-OPNSenseBackup

Downloads a backup from an OPNSense firewall to a local file.

#### Parameters

- **BackupId** - The ID of the backup to download.
- **Path** - The local path to save the backup to.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Download the latest backup
$backups = Get-OPNSenseBackup
$latestBackup = $backups | Sort-Object -Property Date -Descending | Select-Object -First 1
Save-OPNSenseBackup -BackupId $latestBackup.Id -Path "C:\Backups\opnsense-backup.xml"

# Download a specific backup
Save-OPNSenseBackup -BackupId "config-1234567890.xml" -Path "C:\Backups\opnsense-specific-backup.xml"

# Download a backup and overwrite existing file without confirmation
Save-OPNSenseBackup -BackupId "config-1234567890.xml" -Path "C:\Backups\opnsense-backup.xml" -Force
```

### Restore-OPNSenseBackup

Restores an OPNSense firewall configuration from a backup.

#### Parameters

- **BackupId** - The ID of the backup to restore.
- **Password** - The password for encrypted backups.
- **RebootAfterRestore** - Whether to reboot the firewall after restoring. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Restore a backup
Restore-OPNSenseBackup -BackupId "config-1234567890.xml"

# Restore an encrypted backup
Restore-OPNSenseBackup -BackupId "config-1234567890.xml" -Password "SecurePassword123"

# Restore a backup and reboot the firewall
Restore-OPNSenseBackup -BackupId "config-1234567890.xml" -RebootAfterRestore

# Restore a backup without confirmation
Restore-OPNSenseBackup -BackupId "config-1234567890.xml" -Force
```

### Import-OPNSenseBackup

Uploads and restores a backup file to an OPNSense firewall.

#### Parameters

- **Path** - The local path of the backup file to upload.
- **Password** - The password for encrypted backups.
- **RebootAfterRestore** - Whether to reboot the firewall after restoring. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Upload and restore a backup
Import-OPNSenseBackup -Path "C:\Backups\opnsense-backup.xml"

# Upload and restore an encrypted backup
Import-OPNSenseBackup -Path "C:\Backups\opnsense-encrypted-backup.xml" -Password "SecurePassword123"

# Upload and restore a backup and reboot the firewall
Import-OPNSenseBackup -Path "C:\Backups\opnsense-backup.xml" -RebootAfterRestore

# Upload and restore a backup without confirmation
Import-OPNSenseBackup -Path "C:\Backups\opnsense-backup.xml" -Force
```

### Remove-OPNSenseBackup

Removes a backup from an OPNSense firewall.

#### Parameters

- **BackupId** - The ID of the backup to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a backup
Remove-OPNSenseBackup -BackupId "config-1234567890.xml"

# Remove a backup without confirmation
Remove-OPNSenseBackup -BackupId "config-1234567890.xml" -Force
```

## Common Scenarios

### Creating and Downloading a Backup

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a full backup
New-OPNSenseBackup -IncludeRRD -IncludeInstalled -Force

# Get the latest backup
$backups = Get-OPNSenseBackup
$latestBackup = $backups | Sort-Object -Property Date -Descending | Select-Object -First 1

# Download the backup
$backupPath = "C:\Backups\opnsense-$(Get-Date -Format 'yyyyMMdd-HHmmss').xml"
Save-OPNSenseBackup -BackupId $latestBackup.Id -Path $backupPath -Force

Write-Output "Backup saved to: $backupPath"

# Disconnect from the firewall
Disconnect-OPNSense
```

### Scheduled Backup with Rotation

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a new backup
New-OPNSenseBackup -IncludeRRD -IncludeInstalled -Force

# Get all backups
$backups = Get-OPNSenseBackup

# Keep only the 5 most recent backups
if ($backups.Count -gt 5) {
    $backupsToRemove = $backups | Sort-Object -Property Date -Descending | Select-Object -Skip 5
    foreach ($backup in $backupsToRemove) {
        Remove-OPNSenseBackup -BackupId $backup.Id -Force
        Write-Output "Removed old backup: $($backup.Id)"
    }
}

# Download the latest backup
$latestBackup = $backups | Sort-Object -Property Date -Descending | Select-Object -First 1
$backupPath = "C:\Backups\opnsense-$(Get-Date -Format 'yyyyMMdd').xml"
Save-OPNSenseBackup -BackupId $latestBackup.Id -Path $backupPath -Force

Write-Output "Backup saved to: $backupPath"

# Disconnect from the firewall
Disconnect-OPNSense
```

### Restoring from a Local Backup

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Upload and restore a backup
$backupPath = "C:\Backups\opnsense-backup.xml"
Import-OPNSenseBackup -Path $backupPath -RebootAfterRestore -Force

Write-Output "Backup restored from: $backupPath"
Write-Output "The firewall will reboot to apply the changes."

# Disconnect from the firewall
Disconnect-OPNSense
```

### Creating Encrypted Backups

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Generate a secure password
$password = -join ((65..90) + (97..122) + (48..57) | Get-Random -Count 16 | ForEach-Object { [char]$_ })

# Create an encrypted backup
New-OPNSenseBackup -IncludeRRD -IncludeInstalled -Encrypted -Password $password -Force

# Get the latest backup
$backups = Get-OPNSenseBackup
$latestBackup = $backups | Sort-Object -Property Date -Descending | Select-Object -First 1

# Download the backup
$backupPath = "C:\Backups\opnsense-encrypted-$(Get-Date -Format 'yyyyMMdd-HHmmss').xml"
Save-OPNSenseBackup -BackupId $latestBackup.Id -Path $backupPath -Force

# Save the password to a secure file
$passwordPath = "C:\Backups\opnsense-backup-password.txt"
$password | Out-File -FilePath $passwordPath -Force

Write-Output "Encrypted backup saved to: $backupPath"
Write-Output "Password saved to: $passwordPath"
Write-Output "Keep the password file secure, as it is required to restore the backup."

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Backup History

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all backups
$backups = Get-OPNSenseBackup

# Display backup information
Write-Output "Available backups:"
$backups | Format-Table -Property Id, Date, Size, Description

# Calculate total backup size
$totalSize = ($backups | Measure-Object -Property Size -Sum).Sum
Write-Output "Total backup size: $($totalSize / 1MB) MB"

# Remove backups older than 30 days
$cutoffDate = (Get-Date).AddDays(-30)
$oldBackups = $backups | Where-Object { [DateTime]::Parse($_.Date) -lt $cutoffDate }
foreach ($backup in $oldBackups) {
    Remove-OPNSenseBackup -BackupId $backup.Id -Force
    Write-Output "Removed old backup: $($backup.Id) from $($backup.Date)"
}

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Backups can be large, especially when including RRD data and installed packages.
- Encrypted backups provide additional security but require the password for restoration.
- The `RebootAfterRestore` parameter will cause the firewall to reboot immediately after restoring a backup.
- When restoring a backup, all current configuration settings will be replaced with those from the backup.
- Consider implementing a backup rotation strategy to manage disk space.
- Store backup passwords securely, as they are required to restore encrypted backups.
- Regular backups are essential for disaster recovery.
- Backup files contain sensitive information, so they should be stored securely.
- The backup ID is typically in the format "config-timestamp.xml".
- When scheduling automatic backups, consider using the `New-OPNSenseCronJob` cmdlet to create a scheduled task.
- Backups can be used to migrate configurations between OPNSense firewalls.
- Before major configuration changes, create a backup to allow for easy rollback if needed.
