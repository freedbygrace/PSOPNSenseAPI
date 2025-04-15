# Set-OPNSenseRoute

## SYNOPSIS
Modifies an existing static route on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseRoute -UUID <String> [-Network <String>] [-Gateway <String>] [-Description <String>] [-Disabled <Boolean>]
                  [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseRoute cmdlet modifies an existing static route on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a static route description
```powershell
Set-OPNSenseRoute -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated Route Description"
```

This example updates the description of an existing static route.

### Example 2: Change the gateway for a static route
```powershell
Set-OPNSenseRoute -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Gateway "Backup_WAN"
```

This example changes the gateway used for an existing static route.

### Example 3: Disable a static route
```powershell
Set-OPNSenseRoute -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Disabled $true
```

This example disables an existing static route.

## PARAMETERS

### -UUID
The UUID of the route to modify.

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

### -Network
The destination network in CIDR notation.

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

### -Gateway
The gateway to use for the route.

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

### -Description
A description for the route.

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

### -Disabled
Whether the route is disabled.

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
You can pipe a string containing the UUID of a route to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified static route.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a static route, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseRoute](Get-OPNSenseRoute.md)
[New-OPNSenseRoute](New-OPNSenseRoute.md)
[Remove-OPNSenseRoute](Remove-OPNSenseRoute.md)
