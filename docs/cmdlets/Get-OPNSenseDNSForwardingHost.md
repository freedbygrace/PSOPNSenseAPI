# Get-OPNSenseDNSForwardingHost

## SYNOPSIS
Gets DNS forwarding hosts from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDNSForwardingHost [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseDNSForwardingHost cmdlet retrieves DNS forwarding hosts from an OPNSense firewall. You can retrieve all forwarding hosts or a specific forwarding host by UUID.

## EXAMPLES

### Example 1: Get all DNS forwarding hosts
```powershell
Get-OPNSenseDNSForwardingHost
```

This example retrieves all DNS forwarding hosts from the OPNSense firewall.

### Example 2: Get a specific DNS forwarding host
```powershell
Get-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific DNS forwarding host by its UUID.

## PARAMETERS

### -UUID
The UUID of the DNS forwarding host to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
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
Returns objects representing the DNS forwarding hosts.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseDNSForwardingHost](New-OPNSenseDNSForwardingHost.md)
[Set-OPNSenseDNSForwardingHost](Set-OPNSenseDNSForwardingHost.md)
[Remove-OPNSenseDNSForwardingHost](Remove-OPNSenseDNSForwardingHost.md)
[Get-OPNSenseDNSForwarding](Get-OPNSenseDNSForwarding.md)
[Set-OPNSenseDNSForwarding](Set-OPNSenseDNSForwarding.md)
