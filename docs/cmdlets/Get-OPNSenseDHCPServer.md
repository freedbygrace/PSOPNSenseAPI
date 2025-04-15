# Get-OPNSenseDHCPServer

## SYNOPSIS
Gets DHCP server settings from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDHCPServer [[-Interface] <String>]
```

## DESCRIPTION
The Get-OPNSenseDHCPServer cmdlet retrieves DHCP server settings from an OPNSense firewall. You can retrieve settings for all interfaces or a specific interface.

## EXAMPLES

### Example 1: Get DHCP server settings for all interfaces
```powershell
Get-OPNSenseDHCPServer
```

This example retrieves DHCP server settings for all interfaces from the OPNSense firewall.

### Example 2: Get DHCP server settings for a specific interface
```powershell
Get-OPNSenseDHCPServer -Interface "lan"
```

This example retrieves DHCP server settings for the LAN interface from the OPNSense firewall.

## PARAMETERS

### -Interface
The interface for which to retrieve DHCP server settings.

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
Returns objects representing the DHCP server settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
