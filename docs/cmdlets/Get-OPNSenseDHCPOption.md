# Get-OPNSenseDHCPOption

## SYNOPSIS
Gets DHCP options from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDHCPOption [[-Interface] <String>] [[-Number] <Int32>]
```

## DESCRIPTION
The Get-OPNSenseDHCPOption cmdlet retrieves DHCP options from an OPNSense firewall. You can retrieve all options, options for a specific interface, or a specific option by number.

## EXAMPLES

### Example 1: Get all DHCP options
```powershell
Get-OPNSenseDHCPOption
```

This example retrieves all DHCP options from the OPNSense firewall.

### Example 2: Get DHCP options for a specific interface
```powershell
Get-OPNSenseDHCPOption -Interface "lan"
```

This example retrieves DHCP options for the LAN interface.

### Example 3: Get a specific DHCP option
```powershell
Get-OPNSenseDHCPOption -Number 66
```

This example retrieves DHCP option 66 (TFTP server name) from the OPNSense firewall.

## PARAMETERS

### -Interface
The interface for which to retrieve DHCP options.

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

### -Number
The option number to retrieve.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
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
Returns objects representing the DHCP options.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseDHCPOption](New-OPNSenseDHCPOption.md)
[Set-OPNSenseDHCPOption](Set-OPNSenseDHCPOption.md)
[Remove-OPNSenseDHCPOption](Remove-OPNSenseDHCPOption.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
