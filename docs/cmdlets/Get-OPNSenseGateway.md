# Get-OPNSenseGateway

## SYNOPSIS
Gets gateways from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseGateway [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSenseGateway cmdlet retrieves gateways from an OPNSense firewall. You can retrieve all gateways or a specific gateway by name.

## EXAMPLES

### Example 1: Get all gateways
```powershell
Get-OPNSenseGateway
```

This example retrieves all gateways from the OPNSense firewall.

### Example 2: Get a specific gateway
```powershell
Get-OPNSenseGateway -Name "WAN_GW"
```

This example retrieves the gateway named "WAN_GW" from the OPNSense firewall.

## PARAMETERS

### -Name
The name of the gateway to retrieve.

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
Returns objects representing the gateways.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseGateway](New-OPNSenseGateway.md)
[Set-OPNSenseGateway](Set-OPNSenseGateway.md)
[Remove-OPNSenseGateway](Remove-OPNSenseGateway.md)
