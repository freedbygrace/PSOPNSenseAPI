# Remove-OPNSenseDNSForwardingHost

## SYNOPSIS
Removes a DNS forwarding host from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseDNSForwardingHost -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseDNSForwardingHost cmdlet removes a DNS forwarding host from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a DNS forwarding host
```powershell
Remove-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example removes the DNS forwarding host with the specified UUID.

### Example 2: Remove a DNS forwarding host with confirmation
```powershell
Remove-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Confirm
```

This example prompts for confirmation before removing the DNS forwarding host.

## PARAMETERS

### -UUID
The UUID of the DNS forwarding host to remove.

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
You can pipe a string containing the UUID of a DNS forwarding host to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After removing a DNS forwarding host, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseDNSForwardingHost](Get-OPNSenseDNSForwardingHost.md)
[New-OPNSenseDNSForwardingHost](New-OPNSenseDNSForwardingHost.md)
[Set-OPNSenseDNSForwardingHost](Set-OPNSenseDNSForwardingHost.md)
[Get-OPNSenseDNSForwarding](Get-OPNSenseDNSForwarding.md)
[Set-OPNSenseDNSForwarding](Set-OPNSenseDNSForwarding.md)
