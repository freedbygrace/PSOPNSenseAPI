# Restart-OPNSenseSystem

## SYNOPSIS
Restarts an OPNSense firewall.

## SYNTAX

```
Restart-OPNSenseSystem [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Restart-OPNSenseSystem cmdlet restarts an OPNSense firewall.

## EXAMPLES

### Example 1: Restart the system
```powershell
Restart-OPNSenseSystem
```

This example restarts the OPNSense firewall.

### Example 2: Restart the system with confirmation
```powershell
Restart-OPNSenseSystem -Confirm
```

This example prompts for confirmation before restarting the OPNSense firewall.

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
Restarting the firewall will temporarily disrupt network connectivity.
The connection to the firewall will be lost during the restart.

## RELATED LINKS

[Stop-OPNSenseSystem](Stop-OPNSenseSystem.md)
[Get-OPNSenseSystemStatus](Get-OPNSenseSystemStatus.md)
[Get-OPNSenseSystemInfo](Get-OPNSenseSystemInfo.md)
