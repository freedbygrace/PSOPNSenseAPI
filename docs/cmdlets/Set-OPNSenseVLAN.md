# Set-OPNSenseVLAN

## SYNOPSIS
Modifies an existing VLAN on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseVLAN -UUID <String> [-Device <String>] [-Tag <Int32>] [-Priority <Int32>] [-Description <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseVLAN cmdlet modifies an existing VLAN on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a VLAN description
```powershell
Set-OPNSenseVLAN -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated VLAN description"
```

This example updates the description of an existing VLAN.

### Example 2: Modify a VLAN priority
```powershell
Set-OPNSenseVLAN -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Priority 7
```

This example updates the priority of an existing VLAN.

## PARAMETERS

### -UUID
The UUID of the VLAN to modify.

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

### -Device
The physical interface for the VLAN.

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

### -Tag
The VLAN tag (1-4094).

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

### System.String
You can pipe a string containing the UUID of a VLAN to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified VLAN.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Modifying a VLAN may affect network connectivity for devices using that VLAN.

## RELATED LINKS

[Get-OPNSenseVLAN](Get-OPNSenseVLAN.md)
[New-OPNSenseVLAN](New-OPNSenseVLAN.md)
[Remove-OPNSenseVLAN](Remove-OPNSenseVLAN.md)
[New-OPNSenseSubnetVLANs](New-OPNSenseSubnetVLANs.md)
