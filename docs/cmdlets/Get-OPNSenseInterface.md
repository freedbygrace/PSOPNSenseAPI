# Get-OPNSenseInterface

## SYNOPSIS
Gets network interfaces from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseInterface [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSenseInterface cmdlet retrieves network interfaces from an OPNSense firewall. You can retrieve all interfaces or a specific interface by name.

## EXAMPLES

### Example 1: Get all interfaces
```powershell
Get-OPNSenseInterface
```

This example retrieves all network interfaces from the OPNSense firewall.

### Example 2: Get a specific interface
```powershell
Get-OPNSenseInterface -Name "lan"
```

This example retrieves the LAN interface from the OPNSense firewall.

## PARAMETERS

### -Name
The name of the interface to retrieve.

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
Returns objects representing the network interfaces.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Set-OPNSenseInterface](Set-OPNSenseInterface.md)
[Restart-OPNSenseInterface](Restart-OPNSenseInterface.md)
[Get-OPNSenseInterfaceStatistics](Get-OPNSenseInterfaceStatistics.md)
