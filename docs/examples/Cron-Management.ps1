# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all cron jobs
$cronJobs = Get-OPNSenseCronJob
$cronJobs | Format-Table -Property Uuid, Description, Command, Minutes, Hours, Days, Months, Weekdays, Enabled

# Get a specific cron job
$job = Get-OPNSenseCronJob -Uuid $cronJobs[0].Uuid
$job

# Create a new cron job that runs daily at 2:00 AM
$dailyBackup = New-OPNSenseCronJob -Description "Daily backup" -Command "/usr/local/bin/backup.sh" -Minutes "0" -Hours "2" -Days "*" -Months "*" -Weekdays "*" -Apply
$dailyBackup

# Create a new cron job that runs weekly on Sunday at 3:00 AM
$weeklyCleanup = New-OPNSenseCronJob -Description "Weekly cleanup" -Command "/usr/local/bin/cleanup.sh" -Minutes "0" -Hours "3" -Days "*" -Months "*" -Weekdays "0" -Apply
$weeklyCleanup

# Update a cron job
Set-OPNSenseCronJob -Uuid $dailyBackup -Hours "3" -Description "Updated daily backup" -Apply

# Disable a cron job
Disable-OPNSenseCronJob -Uuid $dailyBackup -Apply

# Enable a cron job
Enable-OPNSenseCronJob -Uuid $dailyBackup -Apply

# Remove a cron job
Remove-OPNSenseCronJob -Uuid $weeklyCleanup -Force -Apply

# Disconnect from the firewall
Disconnect-OPNSense
