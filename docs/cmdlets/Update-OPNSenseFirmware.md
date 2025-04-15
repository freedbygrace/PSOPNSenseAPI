# Update-OPNSenseFirmware

## SYNOPSIS
Updates the firmware on an OPNSense firewall.

## SYNTAX

```
Update-OPNSenseFirmware [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Update-OPNSenseFirmware cmdlet updates the firmware on an OPNSense firewall. This cmdlet checks for updates and installs them if available.

## EXAMPLES

### Example 1: Update firmware
```powershell
Update-OPNSenseFirmware
```

This example updates the firmware on the OPNSense firewall.

### Example 2: Update firmware with confirmation
```powershell
Update-OPNSenseFirmware -Confirm
```

This example prompts for confirmation before updating the firmware.

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
Updating the firmware may require a reboot of the firewall for the changes to take effect.
It is recommended to create a backup of the firewall configuration before updating the firmware.

## RELATED LINKS

[Get-OPNSenseFirmware](Get-OPNSenseFirmware.md)
[Start-OPNSenseFirmwareUpgrade](Start-OPNSenseFirmwareUpgrade.md)
