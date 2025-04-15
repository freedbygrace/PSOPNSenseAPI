# Remove-OPNSenseGateway

## SYNOPSIS
Removes a gateway from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseGateway -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseGateway cmdlet removes a gateway from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a gateway
```powershell
Remove-OPNSenseGateway -Name "Backup_WAN"
```

This example removes the gateway named "Backup_WAN".

### Example 2: Remove a gateway with confirmation
```powershell
Remove-OPNSenseGateway -Name "ISP2" -Confirm
```

This example prompts for confirmation before removing the gateway named "ISP2".

## PARAMETERS

### -Name
The name of the gateway to remove.

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
You can pipe a string containing the name of a gateway to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Removing a gateway may affect routing and network connectivity.
Make sure the gateway is not in use by any firewall rules or routes before removing it.

## RELATED LINKS

[Get-OPNSenseGateway](Get-OPNSenseGateway.md)
[New-OPNSenseGateway](New-OPNSenseGateway.md)
[Set-OPNSenseGateway](Set-OPNSenseGateway.md)
