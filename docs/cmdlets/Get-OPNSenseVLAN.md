# Get-OPNSenseVLAN

## SYNOPSIS
Gets VLANs from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseVLAN [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseVLAN cmdlet retrieves VLANs from an OPNSense firewall. You can retrieve all VLANs or a specific VLAN by UUID.

## EXAMPLES

### Example 1: Get all VLANs
```powershell
Get-OPNSenseVLAN
```

This example retrieves all VLANs from the OPNSense firewall.

### Example 2: Get a specific VLAN
```powershell
Get-OPNSenseVLAN -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific VLAN by its UUID.

## PARAMETERS

### -UUID
The UUID of the VLAN to retrieve.

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
Returns objects representing the VLANs.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseVLAN](New-OPNSenseVLAN.md)
[Set-OPNSenseVLAN](Set-OPNSenseVLAN.md)
[Remove-OPNSenseVLAN](Remove-OPNSenseVLAN.md)
[New-OPNSenseSubnetVLANs](New-OPNSenseSubnetVLANs.md)
