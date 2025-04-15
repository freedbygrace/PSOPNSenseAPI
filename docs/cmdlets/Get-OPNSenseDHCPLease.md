# Get-OPNSenseDHCPLease

## SYNOPSIS
Gets DHCP leases from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDHCPLease [[-Interface] <String>] [[-IP] <String>] [[-MAC] <String>]
```

## DESCRIPTION
The Get-OPNSenseDHCPLease cmdlet retrieves DHCP leases from an OPNSense firewall. You can filter leases by interface, IP address, or MAC address.

## EXAMPLES

### Example 1: Get all DHCP leases
```powershell
Get-OPNSenseDHCPLease
```

This example retrieves all DHCP leases from the OPNSense firewall.

### Example 2: Get DHCP leases for a specific interface
```powershell
Get-OPNSenseDHCPLease -Interface "lan"
```

This example retrieves DHCP leases for the LAN interface.

### Example 3: Get a DHCP lease for a specific IP address
```powershell
Get-OPNSenseDHCPLease -IP "192.168.1.100"
```

This example retrieves the DHCP lease for the specified IP address.

### Example 4: Get a DHCP lease for a specific MAC address
```powershell
Get-OPNSenseDHCPLease -MAC "00:11:22:33:44:55"
```

This example retrieves the DHCP lease for the specified MAC address.

## PARAMETERS

### -Interface
The interface for which to retrieve DHCP leases.

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

### -IP
The IP address for which to retrieve a DHCP lease.

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

### -MAC
The MAC address for which to retrieve a DHCP lease.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
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
Returns objects representing the DHCP leases.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
