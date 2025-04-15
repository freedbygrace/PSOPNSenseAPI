# Disable-OPNSenseCronJob

## SYNOPSIS
Disables a cron job on an OPNSense firewall.

## SYNTAX

```
Disable-OPNSenseCronJob -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Disable-OPNSenseCronJob cmdlet disables a cron job on an OPNSense firewall without removing it.

## EXAMPLES

### Example 1: Disable a cron job
```powershell
Disable-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example disables the cron job with the specified UUID.

### Example 2: Disable a cron job with confirmation
```powershell
Disable-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Confirm
```

This example prompts for confirmation before disabling the cron job.

## PARAMETERS

### -UUID
The UUID of the cron job to disable.

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
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
This cmdlet is a shorthand for `Set-OPNSenseCronJob -UUID <UUID> -Enabled $false`.
Disabling a cron job does not remove it from the system, but prevents it from running.

## RELATED LINKS

[Get-OPNSenseCronJob](Get-OPNSenseCronJob.md)
[New-OPNSenseCronJob](New-OPNSenseCronJob.md)
[Set-OPNSenseCronJob](Set-OPNSenseCronJob.md)
[Remove-OPNSenseCronJob](Remove-OPNSenseCronJob.md)
[Enable-OPNSenseCronJob](Enable-OPNSenseCronJob.md)
