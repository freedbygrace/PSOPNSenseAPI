# Get-OPNSenseCronJob

## SYNOPSIS
Gets cron jobs from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseCronJob [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseCronJob cmdlet retrieves cron jobs from an OPNSense firewall. You can retrieve all cron jobs or a specific cron job by UUID.

## EXAMPLES

### Example 1: Get all cron jobs
```powershell
Get-OPNSenseCronJob
```

This example retrieves all cron jobs from the OPNSense firewall.

### Example 2: Get a specific cron job
```powershell
Get-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific cron job by its UUID.

## PARAMETERS

### -UUID
The UUID of the cron job to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
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
Returns objects representing the cron jobs.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseCronJob](New-OPNSenseCronJob.md)
[Set-OPNSenseCronJob](Set-OPNSenseCronJob.md)
[Remove-OPNSenseCronJob](Remove-OPNSenseCronJob.md)
[Enable-OPNSenseCronJob](Enable-OPNSenseCronJob.md)
[Disable-OPNSenseCronJob](Disable-OPNSenseCronJob.md)
