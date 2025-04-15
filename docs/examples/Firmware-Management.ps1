# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get firmware status
$firmwareStatus = Get-OPNSenseFirmware
Write-Output "Firmware Status:"
$firmwareStatus

# Check for updates
Get-OPNSenseFirmware -Check

# Get firmware changelog
$changelog = Get-OPNSenseFirmware -Changelog
Write-Output "Firmware Changelog:"
$changelog

# Get firmware audit
$audit = Get-OPNSenseFirmware -Audit
$audit | Format-Table -Property Name, Version, Installed

# Update firmware (install updates)
Update-OPNSenseFirmware -Wait
Write-Output "Firmware updated successfully"

# Upgrade firmware (major version upgrade)
# Note: This will restart the firewall
Start-OPNSenseFirmwareUpgrade -Wait -Timeout 1200
Write-Output "Firmware upgraded successfully"

# Restart the firewall
Restart-OPNSenseFirewall -Wait -Timeout 300
Write-Output "Firewall restarted successfully"

# Disconnect from the firewall
Disconnect-OPNSense
