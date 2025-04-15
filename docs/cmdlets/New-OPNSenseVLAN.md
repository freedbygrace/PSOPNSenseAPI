# New-OPNSenseVLAN

## SYNOPSIS
Creates a new VLAN on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseVLAN -Device <String> -Tag <Int32> [-Priority <Int32>] [-Description <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseVLAN cmdlet creates a new VLAN on an OPNSense firewall.

## EXAMPLES

### Example 1: Create a new VLAN
```powershell
New-OPNSenseVLAN -Device "igb0" -Tag 10 -Description "Management VLAN"
```

This example creates a new VLAN with tag 10 on the igb0 interface with a description of "Management VLAN".

### Example 2: Create a new VLAN with priority
```powershell
New-OPNSenseVLAN -Device "igb0" -Tag 20 -Priority 5 -Description "Voice VLAN"
```

This example creates a new VLAN with tag 20 and priority 5 on the igb0 interface with a description of "Voice VLAN".

## PARAMETERS

### -Device
The physical interface to create the VLAN on.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tag
The VLAN tag (1-4094).

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Priority
The VLAN priority (0-7).

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
A description for the VLAN.

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

### None

## OUTPUTS

### System.Object
Returns an object representing the newly created VLAN.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a VLAN, you may need to configure an interface to use it.

## RELATED LINKS

[Get-OPNSenseVLAN](Get-OPNSenseVLAN.md)
[Set-OPNSenseVLAN](Set-OPNSenseVLAN.md)
[Remove-OPNSenseVLAN](Remove-OPNSenseVLAN.md)
[New-OPNSenseSubnetVLANs](New-OPNSenseSubnetVLANs.md)
