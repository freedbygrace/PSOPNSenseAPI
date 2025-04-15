# New-OPNSenseGateway

## SYNOPSIS
Creates a new gateway on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseGateway -Name <String> -Interface <String> -IPv4Address <String> [-Description <String>] [-Priority <Int32>]
                    [-Weight <Int32>] [-Disabled <Boolean>] [-Monitor <Boolean>] [-MonitorIP <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseGateway cmdlet creates a new gateway on an OPNSense firewall.

## EXAMPLES

### Example 1: Create a new gateway
```powershell
New-OPNSenseGateway -Name "Backup_WAN" -Interface "opt1" -IPv4Address "203.0.113.1" -Description "Backup WAN Gateway"
```

This example creates a new gateway named "Backup_WAN" on the opt1 interface with the specified IPv4 address.

### Example 2: Create a new gateway with monitoring
```powershell
New-OPNSenseGateway -Name "ISP2" -Interface "wan2" -IPv4Address "198.51.100.1" -Description "Secondary ISP" -Monitor $true -MonitorIP "198.51.100.254"
```

This example creates a new gateway with monitoring enabled and a specific IP to monitor.

## PARAMETERS

### -Name
The name of the gateway.

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

### -Interface
The interface for the gateway.

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

### -IPv4Address
The IPv4 address of the gateway.

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
Default value: False
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
Default value: False
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

### None

## OUTPUTS

### System.Object
Returns an object representing the newly created gateway.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a gateway, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseGateway](Get-OPNSenseGateway.md)
[Set-OPNSenseGateway](Set-OPNSenseGateway.md)
[Remove-OPNSenseGateway](Remove-OPNSenseGateway.md)
