# New-OPNSenseRoute

## SYNOPSIS
Creates a new static route on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseRoute -Network <String> -Gateway <String> [-Description <String>] [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseRoute cmdlet creates a new static route on an OPNSense firewall.

## EXAMPLES

### Example 1: Create a new static route
```powershell
New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN_GW" -Description "Remote Office Network"
```

This example creates a new static route to the 192.168.100.0/24 network via the WAN_GW gateway.

### Example 2: Create a disabled static route
```powershell
New-OPNSenseRoute -Network "10.10.0.0/16" -Gateway "Backup_WAN" -Description "Backup Route" -Disabled $true
```

This example creates a new static route that is initially disabled.

## PARAMETERS

### -Network
The destination network in CIDR notation.

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

### -Gateway
The gateway to use for the route.

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
Default value: False
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
Returns an object representing the newly created static route.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a static route, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseRoute](Get-OPNSenseRoute.md)
[Set-OPNSenseRoute](Set-OPNSenseRoute.md)
[Remove-OPNSenseRoute](Remove-OPNSenseRoute.md)
