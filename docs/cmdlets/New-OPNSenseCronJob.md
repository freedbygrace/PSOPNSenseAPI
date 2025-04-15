# New-OPNSenseCronJob

## SYNOPSIS
Creates a new cron job on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseCronJob -Minutes <String> -Hours <String> -Days <String> -Months <String> -Weekdays <String> -Command <String>
                    [-Description <String>] [-Enabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseCronJob cmdlet creates a new cron job on an OPNSense firewall. Cron jobs are used to schedule commands or scripts to run at specified times.

## EXAMPLES

### Example 1: Create a daily backup cron job
```powershell
New-OPNSenseCronJob -Minutes "0" -Hours "2" -Days "*" -Months "*" -Weekdays "*" -Command "/usr/local/bin/backup.sh" -Description "Daily Backup"
```

This example creates a cron job that runs a backup script at 2:00 AM every day.

### Example 2: Create a weekly reboot cron job
```powershell
New-OPNSenseCronJob -Minutes "0" -Hours "4" -Days "*" -Months "*" -Weekdays "0" -Command "/sbin/reboot" -Description "Weekly Reboot"
```

This example creates a cron job that reboots the firewall at 4:00 AM every Sunday.

### Example 3: Create a monthly log cleanup cron job
```powershell
New-OPNSenseCronJob -Minutes "30" -Hours "1" -Days "1" -Months "*" -Weekdays "*" -Command "/usr/local/bin/cleanup_logs.sh" -Description "Monthly Log Cleanup"
```

This example creates a cron job that runs a log cleanup script at 1:30 AM on the first day of each month.

## PARAMETERS

### -Minutes
The minutes field of the cron job (0-59, *, */5, etc.).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hours
The hours field of the cron job (0-23, *, */2, etc.).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Days
The days field of the cron job (1-31, *, */2, etc.).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Months
The months field of the cron job (1-12, *, */2, etc.).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Weekdays
The weekdays field of the cron job (0-7, *, etc., where 0 and 7 are Sunday).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Command
The command to execute.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A description for the cron job.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
Whether the cron job is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the newly created cron job.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The cron job time fields use standard cron syntax:
- * means all values
- */n means every n values
- n-m means values from n to m
- n,m means values n and m

## RELATED LINKS

[Get-OPNSenseCronJob](Get-OPNSenseCronJob.md)
[Set-OPNSenseCronJob](Set-OPNSenseCronJob.md)
[Remove-OPNSenseCronJob](Remove-OPNSenseCronJob.md)
[Enable-OPNSenseCronJob](Enable-OPNSenseCronJob.md)
[Disable-OPNSenseCronJob](Disable-OPNSenseCronJob.md)
