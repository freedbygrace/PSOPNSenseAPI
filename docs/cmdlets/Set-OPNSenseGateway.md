# Set-OPNSenseGateway

## SYNOPSIS
Modifies an existing gateway on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseGateway -Name <String> [-Interface <String>] [-IPv4Address <String>] [-Description <String>] [-Priority <Int32>]
                    [-Weight <Int32>] [-Disabled <Boolean>] [-Monitor <Boolean>] [-MonitorIP <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseGateway cmdlet modifies an existing gateway on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a gateway description
```powershell
Set-OPNSenseGateway -Name "WAN_GW" -Description "Updated WAN Gateway"
```

This example updates the description of the gateway named "WAN_GW".

### Example 2: Enable monitoring for a gateway
```powershell
Set-OPNSenseGateway -Name "WAN_GW" -Monitor $true -MonitorIP "8.8.8.8"
```

This example enables monitoring for the gateway named "WAN_GW" and sets the monitoring IP to 8.8.8.8.

## PARAMETERS

### -Name
The name of the gateway to modify.

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

### -Interface
The interface for the gateway.

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
The IPv4 address of the gateway.

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
A description for the gateway.

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

### -Priority
The priority of the gateway (1-255, lower is higher priority).

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

### -Weight
The weight of the gateway for load balancing.

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

### -Disabled
Whether the gateway is disabled.

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

### -Monitor
Whether to monitor the gateway.

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

### -MonitorIP
The IP address to monitor for gateway status.

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
You can pipe a string containing the name of a gateway to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified gateway.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a gateway, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseGateway](Get-OPNSenseGateway.md)
[New-OPNSenseGateway](New-OPNSenseGateway.md)
[Remove-OPNSenseGateway](Remove-OPNSenseGateway.md)
