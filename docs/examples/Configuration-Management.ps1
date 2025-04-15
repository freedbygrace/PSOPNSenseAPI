# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all configuration backups
$backups = Get-OPNSenseConfigBackup
$backups | Format-Table -Property Filename, Description, FileSize, Date, Version

# Create a new configuration backup
$backupFilename = Backup-OPNSenseConfig -Filename "pre-upgrade-backup"
Write-Output "Created backup: $backupFilename"

# Export the configuration to a file
$exportPath = "C:\Backups\opnsense-config.xml"
Export-OPNSenseConfig -Path $exportPath -Force
Write-Output "Exported configuration to: $exportPath"

# Import a configuration from a file
# Note: This will restart the firewall
# Import-OPNSenseConfig -Path $exportPath -Confirm

# Restore a configuration backup
# Note: This will restart the firewall
# Restore-OPNSenseConfig -Filename $backupFilename -Confirm

# Disconnect from the firewall
Disconnect-OPNSense
