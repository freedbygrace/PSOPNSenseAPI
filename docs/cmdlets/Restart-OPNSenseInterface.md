# Restart-OPNSenseInterface

## SYNOPSIS
Restarts a network interface on an OPNSense firewall.

## SYNTAX

```
Restart-OPNSenseInterface -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Restart-OPNSenseInterface cmdlet restarts a network interface on an OPNSense firewall.

## EXAMPLES

### Example 1: Restart an interface
```powershell
Restart-OPNSenseInterface -Name "lan"
```

This example restarts the LAN interface.

## PARAMETERS

### -Name
The name of the interface to restart.

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
You can pipe a string containing the name of an interface to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Restarting an interface may temporarily disrupt network connectivity.

## RELATED LINKS

[Get-OPNSenseInterface](Get-OPNSenseInterface.md)
[Set-OPNSenseInterface](Set-OPNSenseInterface.md)
[Get-OPNSenseInterfaceStatistics](Get-OPNSenseInterfaceStatistics.md)
