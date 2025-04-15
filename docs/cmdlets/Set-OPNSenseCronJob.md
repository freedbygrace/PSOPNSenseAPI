# Set-OPNSenseCronJob

## SYNOPSIS
Modifies an existing cron job on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseCronJob -UUID <String> [-Minutes <String>] [-Hours <String>] [-Days <String>] [-Months <String>]
                    [-Weekdays <String>] [-Command <String>] [-Description <String>] [-Enabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseCronJob cmdlet modifies an existing cron job on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a cron job description
```powershell
Set-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated Backup Job"
```

This example updates the description of an existing cron job.

### Example 2: Change the schedule of a cron job
```powershell
Set-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Hours "3" -Minutes "30"
```

This example changes the schedule of an existing cron job to run at 3:30 AM.

### Example 3: Disable a cron job
```powershell
Set-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Enabled $false
```

This example disables an existing cron job.

## PARAMETERS

### -UUID
The UUID of the cron job to modify.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Minutes
The minutes field of the cron job (0-59, *, */5, etc.).

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

### -Hours
The hours field of the cron job (0-23, *, */2, etc.).

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

### -Days
The days field of the cron job (1-31, *, */2, etc.).

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

### -Months
The months field of the cron job (1-12, *, */2, etc.).

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

### -Weekdays
The weekdays field of the cron job (0-7, *, etc., where 0 and 7 are Sunday).

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

### -Command
The command to execute.

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
Default value: None
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

### System.String
You can pipe a string containing the UUID of a cron job to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified cron job.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The cron job time fields use standard cron syntax:
- * means all values
- */n means every n values
- n-m means values from n to m
- n,m means values n and m

## RELATED LINKS

[Get-OPNSenseCronJob](Get-OPNSenseCronJob.md)
[New-OPNSenseCronJob](New-OPNSenseCronJob.md)
[Remove-OPNSenseCronJob](Remove-OPNSenseCronJob.md)
[Enable-OPNSenseCronJob](Enable-OPNSenseCronJob.md)
[Disable-OPNSenseCronJob](Disable-OPNSenseCronJob.md)
