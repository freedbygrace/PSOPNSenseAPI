# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all configuration backups
$backups = Get-OPNSenseConfigBackup
$backups | Format-Table -Property Filename, Description, FileSize, Date, Version

# Get the current configuration as an XML document
$config = Get-OPNSenseConfig
Write-Output "Retrieved configuration with root element: $($config.DocumentElement.Name)"

# Export the configuration to a file
$exportPath = New-Object System.IO.FileInfo "C:\Backups\opnsense-config.xml"
Export-OPNSenseConfig -Path $exportPath -Force
Write-Output "Exported configuration to: $($exportPath.FullName)"

# Import a configuration from a file
# Note: This will restart the firewall
# Import-OPNSenseConfig -Path $exportPath -Force

# Restore a configuration backup by filename
# Note: This will restart the firewall
# Restore-OPNSenseConfig -Filename "config-backup-20250415-1234.xml" -Force

# Restore a configuration from an XML document
# Note: This will restart the firewall
# Restore-OPNSenseConfig -XmlDocument $config -Force

# Pipeline example: Get and restore configuration in one line
# Get-OPNSenseConfig | Restore-OPNSenseConfig -Force

# Disconnect from the firewall
Disconnect-OPNSense
