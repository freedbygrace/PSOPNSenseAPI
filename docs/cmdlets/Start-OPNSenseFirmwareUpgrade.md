# Start-OPNSenseFirmwareUpgrade

## SYNOPSIS
Starts a firmware upgrade on an OPNSense firewall.

## SYNTAX

```
Start-OPNSenseFirmwareUpgrade [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Start-OPNSenseFirmwareUpgrade cmdlet starts a firmware upgrade on an OPNSense firewall. This cmdlet performs a major version upgrade, not just an update.

## EXAMPLES

### Example 1: Start a firmware upgrade
```powershell
Start-OPNSenseFirmwareUpgrade
```

This example starts a firmware upgrade on the OPNSense firewall.

### Example 2: Start a firmware upgrade with confirmation
```powershell
Start-OPNSenseFirmwareUpgrade -Confirm
```

This example prompts for confirmation before starting the firmware upgrade.

## PARAMETERS

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
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Upgrading the firmware will require a reboot of the firewall for the changes to take effect.
It is strongly recommended to create a backup of the firewall configuration before upgrading the firmware.
This cmdlet performs a major version upgrade, which may have significant changes and potential compatibility issues.

## RELATED LINKS

[Get-OPNSenseFirmware](Get-OPNSenseFirmware.md)
[Update-OPNSenseFirmware](Update-OPNSenseFirmware.md)
