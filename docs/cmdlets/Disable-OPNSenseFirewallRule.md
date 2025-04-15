# Disable-OPNSenseFirewallRule

## SYNOPSIS
Disables a firewall rule on an OPNSense firewall.

## SYNTAX

```
Disable-OPNSenseFirewallRule -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Disable-OPNSenseFirewallRule cmdlet disables an enabled firewall rule on an OPNSense firewall.

## EXAMPLES

### Example 1: Disable a firewall rule
```powershell
Disable-OPNSenseFirewallRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example disables the firewall rule with the specified UUID.

## PARAMETERS

### -UUID
The UUID of the firewall rule to disable.

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
You can pipe a string containing the UUID of a firewall rule to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After disabling firewall rules, you need to apply the changes using Apply-OPNSenseFirewallChanges.

## RELATED LINKS

[Get-OPNSenseFirewallRule](Get-OPNSenseFirewallRule.md)
[New-OPNSenseFirewallRule](New-OPNSenseFirewallRule.md)
[Set-OPNSenseFirewallRule](Set-OPNSenseFirewallRule.md)
[Remove-OPNSenseFirewallRule](Remove-OPNSenseFirewallRule.md)
[Enable-OPNSenseFirewallRule](Enable-OPNSenseFirewallRule.md)
[Apply-OPNSenseFirewallChanges](Apply-OPNSenseFirewallChanges.md)
