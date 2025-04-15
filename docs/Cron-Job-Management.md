# Cron Job Management

This component provides cmdlets for managing cron jobs on OPNSense firewalls.

## Overview

The Cron Job Management component allows you to configure and manage scheduled tasks (cron jobs) on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting cron jobs.

## Cmdlets

### Get-OPNSenseCronJob

Retrieves cron jobs from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific cron job to retrieve. If not specified, all cron jobs are returned.
- **Description** - Filter cron jobs by description.
- **Command** - Filter cron jobs by command.

#### Examples

```powershell
# Get all cron jobs
Get-OPNSenseCronJob

# Get a specific cron job by UUID
Get-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get cron jobs with a specific description
Get-OPNSenseCronJob -Description "Backup"

# Get cron jobs with a specific command
Get-OPNSenseCronJob -Command "configctl"
```

### New-OPNSenseCronJob

Creates a new cron job on an OPNSense firewall.

#### Parameters

- **Description** - A description for the cron job.
- **Command** - The command to execute.
- **Parameters** - The parameters for the command.
- **Minute** - The minute(s) when the job should run (0-59, *, */5, etc.).
- **Hour** - The hour(s) when the job should run (0-23, *, */2, etc.).
- **Day** - The day(s) of the month when the job should run (1-31, *, */2, etc.).
- **Month** - The month(s) when the job should run (1-12, *, */2, etc.).
- **Weekday** - The weekday(s) when the job should run (0-7, *, etc., where 0 and 7 are Sunday).
- **Enabled** - Whether the cron job is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a daily backup cron job
New-OPNSenseCronJob -Description "Daily Backup" -Command "configctl" -Parameters "system backup" -Minute "0" -Hour "2" -Day "*" -Month "*" -Weekday "*"

# Create a weekly update cron job
New-OPNSenseCronJob -Description "Weekly Update" -Command "configctl" -Parameters "firmware update" -Minute "0" -Hour "3" -Day "*" -Month "*" -Weekday "0"

# Create a monthly log rotation cron job
New-OPNSenseCronJob -Description "Monthly Log Rotation" -Command "configctl" -Parameters "system rotate" -Minute "0" -Hour "4" -Day "1" -Month "*" -Weekday "*"

# Create a disabled cron job
New-OPNSenseCronJob -Description "Temporary Job" -Command "configctl" -Parameters "system reboot" -Minute "0" -Hour "5" -Day "*" -Month "*" -Weekday "*" -Enabled:$false
```

### Set-OPNSenseCronJob

Updates an existing cron job on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the cron job to update.
- **Description** - A description for the cron job.
- **Command** - The command to execute.
- **Parameters** - The parameters for the command.
- **Minute** - The minute(s) when the job should run (0-59, *, */5, etc.).
- **Hour** - The hour(s) when the job should run (0-23, *, */2, etc.).
- **Day** - The day(s) of the month when the job should run (1-31, *, */2, etc.).
- **Month** - The month(s) when the job should run (1-12, *, */2, etc.).
- **Weekday** - The weekday(s) when the job should run (0-7, *, etc., where 0 and 7 are Sunday).
- **Enabled** - Whether the cron job is enabled.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated cron job.

#### Examples

```powershell
# Update a cron job's description
Set-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated Backup Job"

# Update a cron job's schedule
Set-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Minute "30" -Hour "3"

# Update a cron job's command and parameters
Set-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Command "configctl" -Parameters "system backup full"

# Disable a cron job
Set-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Enabled:$false

# Update multiple properties of a cron job
Set-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Full Backup" -Command "configctl" -Parameters "system backup full" -Minute "0" -Hour "1" -PassThru
```

### Remove-OPNSenseCronJob

Removes a cron job from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the cron job to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a cron job
Remove-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a cron job without confirmation
Remove-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Enable-OPNSenseCronJob

Enables a cron job on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the cron job to enable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated cron job.

#### Examples

```powershell
# Enable a cron job
Enable-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Enable a cron job and return the updated job
Enable-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Disable-OPNSenseCronJob

Disables a cron job on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the cron job to disable.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated cron job.

#### Examples

```powershell
# Disable a cron job
Disable-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Disable a cron job and return the updated job
Disable-OPNSenseCronJob -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -PassThru
```

### Apply-OPNSenseCronJobChanges

Applies pending cron job changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply cron job changes
Apply-OPNSenseCronJobChanges

# Apply cron job changes without confirmation
Apply-OPNSenseCronJobChanges -Force
```

## Common Scenarios

### Basic Backup Schedule

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a daily backup cron job
New-OPNSenseCronJob -Description "Daily Configuration Backup" -Command "configctl" -Parameters "system backup" -Minute "0" -Hour "2" -Day "*" -Month "*" -Weekday "*" -Force

# Apply the changes
Apply-OPNSenseCronJobChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Comprehensive Maintenance Schedule

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define maintenance jobs
$cronJobs = @(
    @{ Description = "Daily Backup"; Command = "configctl"; Parameters = "system backup"; Minute = "0"; Hour = "2"; Day = "*"; Month = "*"; Weekday = "*" },
    @{ Description = "Weekly Full Backup"; Command = "configctl"; Parameters = "system backup full"; Minute = "0"; Hour = "3"; Day = "*"; Month = "*"; Weekday = "0" },
    @{ Description = "Daily Log Rotation"; Command = "configctl"; Parameters = "system rotate"; Minute = "30"; Hour = "2"; Day = "*"; Month = "*"; Weekday = "*" },
    @{ Description = "Weekly Update Check"; Command = "configctl"; Parameters = "firmware check"; Minute = "0"; Hour = "4"; Day = "*"; Month = "*"; Weekday = "1" },
    @{ Description = "Monthly Cleanup"; Command = "configctl"; Parameters = "system cleanup"; Minute = "0"; Hour = "5"; Day = "1"; Month = "*"; Weekday = "*" }
)

# Create cron jobs
foreach ($job in $cronJobs) {
    New-OPNSenseCronJob -Description $job.Description -Command $job.Command -Parameters $job.Parameters -Minute $job.Minute -Hour $job.Hour -Day $job.Day -Month $job.Month -Weekday $job.Weekday -Force
}

# Apply the changes
Apply-OPNSenseCronJobChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Existing Cron Jobs

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all cron jobs
$cronJobs = Get-OPNSenseCronJob

# Update backup jobs to run at a different time
$backupJobs = $cronJobs | Where-Object { $_.Description -like "*Backup*" }
foreach ($job in $backupJobs) {
    Set-OPNSenseCronJob -Uuid $job.Uuid -Hour "4" -Force
    Write-Output "Updated job: $($job.Description) to run at 4:00 AM"
}

# Disable temporary jobs
$tempJobs = $cronJobs | Where-Object { $_.Description -like "*Temporary*" }
foreach ($job in $tempJobs) {
    Disable-OPNSenseCronJob -Uuid $job.Uuid -Force
    Write-Output "Disabled job: $($job.Description)"
}

# Remove obsolete jobs
$obsoleteJobs = $cronJobs | Where-Object { $_.Description -like "*Obsolete*" }
foreach ($job in $obsoleteJobs) {
    Remove-OPNSenseCronJob -Uuid $job.Uuid -Force
    Write-Output "Removed job: $($job.Description)"
}

# Apply the changes
Apply-OPNSenseCronJobChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Scheduling Network Tests

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define network test jobs
$testJobs = @(
    @{ Description = "Hourly Gateway Ping"; Command = "configctl"; Parameters = "gateway pingall"; Minute = "0"; Hour = "*"; Day = "*"; Month = "*"; Weekday = "*" },
    @{ Description = "Daily DNS Test"; Command = "configctl"; Parameters = "system dns test"; Minute = "15"; Hour = "*/6"; Day = "*"; Month = "*"; Weekday = "*" },
    @{ Description = "Weekly Network Diagnostics"; Command = "configctl"; Parameters = "system diagnostics"; Minute = "30"; Hour = "5"; Day = "*"; Month = "*"; Weekday = "0" }
)

# Create cron jobs
foreach ($job in $testJobs) {
    New-OPNSenseCronJob -Description $job.Description -Command $job.Command -Parameters $job.Parameters -Minute $job.Minute -Hour $job.Hour -Day $job.Day -Month $job.Month -Weekday $job.Weekday -Force
}

# Apply the changes
Apply-OPNSenseCronJobChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Scheduling Firewall Reboots

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a monthly reboot cron job
New-OPNSenseCronJob -Description "Monthly Maintenance Reboot" -Command "configctl" -Parameters "system reboot" -Minute "0" -Hour "3" -Day "15" -Month "*" -Weekday "*" -Force

# Apply the changes
Apply-OPNSenseCronJobChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Cron job changes are not applied until you call `Apply-OPNSenseCronJobChanges`.
- The cron job schedule uses the standard cron format:
  - **Minute**: 0-59, *, */5 (every 5 minutes), etc.
  - **Hour**: 0-23, *, */2 (every 2 hours), etc.
  - **Day**: 1-31, *, */2 (every 2 days), etc.
  - **Month**: 1-12, *, */2 (every 2 months), etc.
  - **Weekday**: 0-7, * (where 0 and 7 are Sunday), etc.
- The `*` character means "every" (e.g., every minute, every hour, etc.).
- The `*/n` format means "every n" (e.g., every 5 minutes, every 2 hours, etc.).
- Multiple values can be specified with commas (e.g., "1,3,5" for 1st, 3rd, and 5th).
- Ranges can be specified with hyphens (e.g., "1-5" for 1st through 5th).
- The `Command` parameter typically uses `configctl` for OPNSense system commands.
- Consider using the `-PassThru` parameter when updating cron jobs to verify the changes.
- Cron job descriptions should be descriptive and follow a consistent naming convention.
- Be careful when scheduling system reboots, as they will interrupt all services.
- Cron jobs run with system privileges, so they can execute any command available to the system.
- For complex schedules, consider using multiple cron jobs instead of a single job with a complex schedule.
