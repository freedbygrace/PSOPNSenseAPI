# Get-OPNSensePortForwardingRule

## SYNOPSIS
Gets port forwarding rules from an OPNSense firewall.

## SYNTAX

```
Get-OPNSensePortForwardingRule [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSensePortForwardingRule cmdlet retrieves port forwarding rules from an OPNSense firewall. You can retrieve all rules or a specific rule by UUID.

## EXAMPLES

### Example 1: Get all port forwarding rules
```powershell
Get-OPNSensePortForwardingRule
```

This example retrieves all port forwarding rules from the OPNSense firewall.

### Example 2: Get a specific port forwarding rule
```powershell
Get-OPNSensePortForwardingRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific port forwarding rule by its UUID.

## PARAMETERS

### -UUID
The UUID of the port forwarding rule to retrieve.

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
Returns objects representing the port forwarding rules.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSensePortForwardingRule](New-OPNSensePortForwardingRule.md)
[Set-OPNSensePortForwardingRule](Set-OPNSensePortForwardingRule.md)
[Remove-OPNSensePortForwardingRule](Remove-OPNSensePortForwardingRule.md)
