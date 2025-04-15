# New-OPNSenseFirewallRule

## SYNOPSIS
Creates a new firewall rule on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseFirewallRule -Action <String> -Interface <String> -Protocol <String> [-Source <String>] [-SourcePort <String>]
                         [-Destination <String>] [-DestinationPort <String>] [-Description <String>] [-Enabled <Boolean>]
                         [-Direction <String>] [-IPProtocol <String>] [-Gateway <String>] [-Log <Boolean>]
                         [-Sequence <Int32>] [-Category <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseFirewallRule cmdlet creates a new firewall rule on an OPNSense firewall.

## EXAMPLES

### Example 1: Create a basic firewall rule
```powershell
New-OPNSenseFirewallRule -Action "pass" -Interface "lan" -Protocol "tcp" -Destination "any" -DestinationPort "80" -Description "Allow HTTP"
```

This example creates a firewall rule to allow HTTP traffic on the LAN interface.

### Example 2: Create a more complex firewall rule
```powershell
New-OPNSenseFirewallRule -Action "block" -Interface "wan" -Protocol "tcp/udp" -Source "10.0.0.0/24" -SourcePort "any" -Destination "192.168.1.0/24" -DestinationPort "3389" -Description "Block RDP" -Direction "in" -Log $true
```

This example creates a firewall rule to block RDP traffic from a specific source network to a specific destination network, with logging enabled.

## PARAMETERS

### -Action
The action to take for the rule (pass, block, reject).

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
The interface to apply the rule to.

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
The protocol for the rule (tcp, udp, tcp/udp, icmp, etc.).

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

### -Source
The source address or network for the rule.

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

### -Destination
The destination address or network for the rule.

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

### -DestinationPort
The destination port for the rule.

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

### -Direction
The direction for the rule (in, out).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: in
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPProtocol
The IP protocol version for the rule (inet, inet6).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: inet
Accept pipeline input: False
Accept wildcard characters: False
```

### -Gateway
The gateway to use for the rule.

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

### -Log
Whether to log matches for the rule.

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

### -Sequence
The sequence number for the rule.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -Category
The category for the rule.

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
Returns an object representing the newly created firewall rule.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating firewall rules, you need to apply the changes using Apply-OPNSenseFirewallChanges.

## RELATED LINKS

[Get-OPNSenseFirewallRule](Get-OPNSenseFirewallRule.md)
[Set-OPNSenseFirewallRule](Set-OPNSenseFirewallRule.md)
[Remove-OPNSenseFirewallRule](Remove-OPNSenseFirewallRule.md)
[Enable-OPNSenseFirewallRule](Enable-OPNSenseFirewallRule.md)
[Disable-OPNSenseFirewallRule](Disable-OPNSenseFirewallRule.md)
[Apply-OPNSenseFirewallChanges](Apply-OPNSenseFirewallChanges.md)
