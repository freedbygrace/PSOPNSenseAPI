# Remove-OPNSenseDHCPLease

## SYNOPSIS
Removes a DHCP lease from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseDHCPLease -Interface <String> -IP <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseDHCPLease cmdlet removes a DHCP lease from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a DHCP lease
```powershell
Remove-OPNSenseDHCPLease -Interface "lan" -IP "192.168.1.100"
```

This example removes the DHCP lease for the specified IP address on the LAN interface.

### Example 2: Remove a DHCP lease with confirmation
```powershell
Remove-OPNSenseDHCPLease -Interface "lan" -IP "192.168.1.100" -Confirm
```

This example prompts for confirmation before removing the DHCP lease.

## PARAMETERS

### -Interface
The interface from which to remove the DHCP lease.

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

### -IP
The IP address of the DHCP lease to remove.

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
You can pipe strings containing the interface name and IP address to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Removing a DHCP lease will force the client to request a new lease the next time it connects.

## RELATED LINKS

[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
