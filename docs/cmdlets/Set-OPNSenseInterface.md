# Set-OPNSenseInterface

## SYNOPSIS
Modifies an existing network interface on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseInterface -Name <String> [-Enabled <Boolean>] [-IPv4Type <String>] [-IPv4Address <String>] [-IPv4Subnet <Int32>]
                      [-IPv6Type <String>] [-IPv6Address <String>] [-IPv6Subnet <Int32>] [-Description <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseInterface cmdlet modifies an existing network interface on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify an interface
```powershell
Set-OPNSenseInterface -Name "lan" -Description "Local Area Network" -IPv4Address "192.168.1.1" -IPv4Subnet 24
```

This example updates the description and IPv4 address/subnet for the LAN interface.

### Example 2: Disable an interface
```powershell
Set-OPNSenseInterface -Name "opt1" -Enabled $false
```

This example disables the opt1 interface.

## PARAMETERS

### -Name
The name of the interface to modify.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Enabled
Whether the interface is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv4Type
The IPv4 configuration type (none, static, dhcp).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv4Address
The IPv4 address for the interface.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv4Subnet
The IPv4 subnet mask (CIDR notation) for the interface.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv6Type
The IPv6 configuration type (none, static, dhcp6).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv6Address
The IPv6 address for the interface.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPv6Subnet
The IPv6 subnet mask (CIDR notation) for the interface.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A description for the interface.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
You can pipe a string containing the name of an interface to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified interface.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying an interface, you may need to restart it using Restart-OPNSenseInterface.

## RELATED LINKS

[Get-OPNSenseInterface](Get-OPNSenseInterface.md)
[Restart-OPNSenseInterface](Restart-OPNSenseInterface.md)
[Get-OPNSenseInterfaceStatistics](Get-OPNSenseInterfaceStatistics.md)
