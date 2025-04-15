# Get-OPNSenseDHCPStaticMapping

## SYNOPSIS
Gets DHCP static mappings from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDHCPStaticMapping [[-Interface] <String>] [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseDHCPStaticMapping cmdlet retrieves DHCP static mappings from an OPNSense firewall. You can retrieve all static mappings, mappings for a specific interface, or a specific mapping by UUID.

## EXAMPLES

### Example 1: Get all DHCP static mappings
```powershell
Get-OPNSenseDHCPStaticMapping
```

This example retrieves all DHCP static mappings from the OPNSense firewall.

### Example 2: Get DHCP static mappings for a specific interface
```powershell
Get-OPNSenseDHCPStaticMapping -Interface "lan"
```

This example retrieves DHCP static mappings for the LAN interface.

### Example 3: Get a specific DHCP static mapping
```powershell
Get-OPNSenseDHCPStaticMapping -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific DHCP static mapping by its UUID.

## PARAMETERS

### -Interface
The interface for which to retrieve DHCP static mappings.

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

### -UUID
The UUID of the DHCP static mapping to retrieve.

```yaml
Type: String
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
Returns objects representing the DHCP static mappings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
