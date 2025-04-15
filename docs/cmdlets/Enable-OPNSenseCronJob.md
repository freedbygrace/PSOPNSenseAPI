# Enable-OPNSenseCronJob

## SYNOPSIS
Enables a cron job on an OPNSense firewall.

## SYNTAX

```
Enable-OPNSenseCronJob -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Enable-OPNSenseCronJob cmdlet enables a previously disabled cron job on an OPNSense firewall.

## EXAMPLES

### Example 1: Enable a cron job
```powershell
Enable-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example enables the cron job with the specified UUID.

### Example 2: Enable a cron job with confirmation
```powershell
Enable-OPNSenseCronJob -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Confirm
```

This example prompts for confirmation before enabling the cron job.

## PARAMETERS

### -UUID
The UUID of the cron job to enable.

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
This cmdlet is a shorthand for `Set-OPNSenseCronJob -UUID <UUID> -Enabled $true`.

## RELATED LINKS

[Get-OPNSenseCronJob](Get-OPNSenseCronJob.md)
[New-OPNSenseCronJob](New-OPNSenseCronJob.md)
[Set-OPNSenseCronJob](Set-OPNSenseCronJob.md)
[Remove-OPNSenseCronJob](Remove-OPNSenseCronJob.md)
[Disable-OPNSenseCronJob](Disable-OPNSenseCronJob.md)
