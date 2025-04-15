# Remove-OPNSenseDHCPStaticMapping

## SYNOPSIS
Removes a DHCP static mapping from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseDHCPStaticMapping -Interface <String> -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseDHCPStaticMapping cmdlet removes a DHCP static mapping from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a DHCP static mapping
```powershell
Remove-OPNSenseDHCPStaticMapping -Interface "lan" -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example removes the DHCP static mapping with the specified UUID from the LAN interface.

### Example 2: Remove a DHCP static mapping with confirmation
```powershell
Remove-OPNSenseDHCPStaticMapping -Interface "lan" -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Confirm
```

This example prompts for confirmation before removing the DHCP static mapping.

## PARAMETERS

### -Interface
The interface from which to remove the DHCP static mapping.

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

### -UUID
The UUID of the DHCP static mapping to remove.

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
You can pipe strings containing the interface name and UUID to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After removing a DHCP static mapping, the DHCP server service may need to be restarted for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
