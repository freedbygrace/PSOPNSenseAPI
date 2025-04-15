# Get-OPNSenseInterfaceStatistics

## SYNOPSIS
Gets statistics for network interfaces on an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseInterfaceStatistics [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSenseInterfaceStatistics cmdlet retrieves statistics for network interfaces on an OPNSense firewall. You can retrieve statistics for all interfaces or a specific interface by name.

## EXAMPLES

### Example 1: Get statistics for all interfaces
```powershell
Get-OPNSenseInterfaceStatistics
```

This example retrieves statistics for all network interfaces on the OPNSense firewall.

### Example 2: Get statistics for a specific interface
```powershell
Get-OPNSenseInterfaceStatistics -Name "lan"
```

This example retrieves statistics for the LAN interface on the OPNSense firewall.

## PARAMETERS

### -Name
The name of the interface to retrieve statistics for.

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
Returns objects representing the interface statistics, including information such as packets sent/received, bytes sent/received, and errors.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Get-OPNSenseInterface](Get-OPNSenseInterface.md)
[Set-OPNSenseInterface](Set-OPNSenseInterface.md)
[Restart-OPNSenseInterface](Restart-OPNSenseInterface.md)
