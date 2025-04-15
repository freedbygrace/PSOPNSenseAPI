# Remove-OPNSenseDHCPOption

## SYNOPSIS
Removes a DHCP option from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseDHCPOption -Interface <String> -Number <Int32> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseDHCPOption cmdlet removes a DHCP option from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a DHCP option
```powershell
Remove-OPNSenseDHCPOption -Interface "lan" -Number 66
```

This example removes DHCP option 66 (TFTP server name) from the LAN interface.

### Example 2: Remove a DHCP option with confirmation
```powershell
Remove-OPNSenseDHCPOption -Interface "lan" -Number 67 -Confirm
```

This example prompts for confirmation before removing DHCP option 67 (boot filename) from the LAN interface.

## PARAMETERS

### -Interface
The interface from which to remove the DHCP option.

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

### -Number
The option number to remove.

```yaml
Type: Int32
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
### System.Int32
You can pipe strings containing the interface name and integers containing the option number to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After removing a DHCP option, the DHCP server service may need to be restarted for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseDHCPOption](Get-OPNSenseDHCPOption.md)
[New-OPNSenseDHCPOption](New-OPNSenseDHCPOption.md)
[Set-OPNSenseDHCPOption](Set-OPNSenseDHCPOption.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
