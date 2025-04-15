# Stop-OPNSenseSystem

## SYNOPSIS
Shuts down an OPNSense firewall.

## SYNTAX

```
Stop-OPNSenseSystem [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Stop-OPNSenseSystem cmdlet shuts down an OPNSense firewall.

## EXAMPLES

### Example 1: Shut down the system
```powershell
Stop-OPNSenseSystem
```

This example shuts down the OPNSense firewall.

### Example 2: Shut down the system with confirmation
```powershell
Stop-OPNSenseSystem -Confirm
```

This example prompts for confirmation before shutting down the OPNSense firewall.

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
Shutting down the firewall will disrupt network connectivity.
The connection to the firewall will be lost during the shutdown.
The firewall will remain off until it is physically powered on again.

## RELATED LINKS

[Restart-OPNSenseSystem](Restart-OPNSenseSystem.md)
[Get-OPNSenseSystemStatus](Get-OPNSenseSystemStatus.md)
[Get-OPNSenseSystemInfo](Get-OPNSenseSystemInfo.md)
