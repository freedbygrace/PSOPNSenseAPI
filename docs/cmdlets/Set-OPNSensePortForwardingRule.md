# Set-OPNSensePortForwardingRule

## SYNOPSIS
Modifies an existing port forwarding rule on an OPNSense firewall.

## SYNTAX

```
Set-OPNSensePortForwardingRule -UUID <String> [-Interface <String>] [-Protocol <String>] [-SourceAddress <String>]
                              [-SourcePort <String>] [-DestinationAddress <String>] [-DestinationPort <String>]
                              [-TargetIP <String>] [-TargetPort <String>] [-Description <String>] [-Enabled <Boolean>]
                              [-NATReflection <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSensePortForwardingRule cmdlet modifies an existing port forwarding rule on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a port forwarding rule description
```powershell
Set-OPNSensePortForwardingRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated Web Server"
```

This example updates the description of an existing port forwarding rule.

### Example 2: Change the target IP for a port forwarding rule
```powershell
Set-OPNSensePortForwardingRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -TargetIP "192.168.1.11"
```

This example changes the internal IP address to which traffic is forwarded.

### Example 3: Disable a port forwarding rule
```powershell
Set-OPNSensePortForwardingRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Enabled $false
```

This example disables an existing port forwarding rule.

## PARAMETERS

### -UUID
The UUID of the port forwarding rule to modify.

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
The interface on which the rule applies.

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

### -Protocol
The protocol for the rule (tcp, udp, tcp/udp).

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

### -SourceAddress
The source address or network for the rule.

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

### -SourcePort
The source port for the rule.

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

### -DestinationAddress
The destination address for the rule (usually "WAN address").

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

### -DestinationPort
The destination port for the rule.

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

### -TargetIP
The internal IP address to which traffic will be forwarded.

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
Default value: None
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
You can pipe a string containing the UUID of a port forwarding rule to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified port forwarding rule.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a port forwarding rule, you may need to apply the changes for them to take effect.
Modifying port forwarding rules may require corresponding changes to firewall rules.

## RELATED LINKS

[Get-OPNSensePortForwardingRule](Get-OPNSensePortForwardingRule.md)
[New-OPNSensePortForwardingRule](New-OPNSensePortForwardingRule.md)
[Remove-OPNSensePortForwardingRule](Remove-OPNSensePortForwardingRule.md)
