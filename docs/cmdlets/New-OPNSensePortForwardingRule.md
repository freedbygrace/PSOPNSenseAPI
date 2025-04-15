# New-OPNSensePortForwardingRule

## SYNOPSIS
Creates a new port forwarding rule on an OPNSense firewall.

## SYNTAX

```
New-OPNSensePortForwardingRule -Interface <String> -Protocol <String> -SourceAddress <String> [-SourcePort <String>]
                              -DestinationAddress <String> -DestinationPort <String> -TargetIP <String> [-TargetPort <String>]
                              [-Description <String>] [-Enabled <Boolean>] [-NATReflection <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSensePortForwardingRule cmdlet creates a new port forwarding rule on an OPNSense firewall. Port forwarding rules allow external traffic to be redirected to internal hosts.

## EXAMPLES

### Example 1: Create a basic port forwarding rule
```powershell
New-OPNSensePortForwardingRule -Interface "wan" -Protocol "tcp" -SourceAddress "any" -DestinationAddress "WAN address" -DestinationPort "80" -TargetIP "192.168.1.10" -TargetPort "80" -Description "Web Server"
```

This example creates a port forwarding rule that forwards HTTP traffic to an internal web server.

### Example 2: Create a port forwarding rule with a specific source
```powershell
New-OPNSensePortForwardingRule -Interface "wan" -Protocol "tcp" -SourceAddress "203.0.113.0/24" -SourcePort "any" -DestinationAddress "WAN address" -DestinationPort "3389" -TargetIP "192.168.1.20" -TargetPort "3389" -Description "RDP Access" -NATReflection "disable"
```

This example creates a port forwarding rule that allows RDP access only from a specific network, with NAT reflection disabled.

## PARAMETERS

### -Interface
The interface on which the rule applies.

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

### -Protocol
The protocol for the rule (tcp, udp, tcp/udp).

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

### -SourceAddress
The source address or network for the rule.

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

### -SourcePort
The source port for the rule.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: any
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationAddress
The destination address for the rule (usually "WAN address").

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

### -DestinationPort
The destination port for the rule.

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

### -TargetIP
The internal IP address to which traffic will be forwarded.

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

### -TargetPort
The internal port to which traffic will be forwarded.

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
A description for the rule.

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

### -Enabled
Whether the rule is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -NATReflection
The NAT reflection mode for the rule (enable, disable, system default).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: system default
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
Returns an object representing the newly created port forwarding rule.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a port forwarding rule, you may need to apply the changes for them to take effect.
Port forwarding rules may require corresponding firewall rules to allow the traffic.

## RELATED LINKS

[Get-OPNSensePortForwardingRule](Get-OPNSensePortForwardingRule.md)
[Set-OPNSensePortForwardingRule](Set-OPNSensePortForwardingRule.md)
[Remove-OPNSensePortForwardingRule](Remove-OPNSensePortForwardingRule.md)
