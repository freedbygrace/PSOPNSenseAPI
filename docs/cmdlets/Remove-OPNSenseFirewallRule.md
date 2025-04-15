# Remove-OPNSenseFirewallRule

## SYNOPSIS
Removes a firewall rule from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseFirewallRule -UUID <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseFirewallRule cmdlet removes a firewall rule from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a firewall rule
```powershell
Remove-OPNSenseFirewallRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example removes the firewall rule with the specified UUID.

### Example 2: Remove a firewall rule with confirmation
```powershell
Remove-OPNSenseFirewallRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Confirm
```

This example prompts for confirmation before removing the firewall rule.

## PARAMETERS

### -UUID
The UUID of the firewall rule to remove.

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
After removing firewall rules, you need to apply the changes using Apply-OPNSenseFirewallChanges.

## RELATED LINKS

[Get-OPNSenseFirewallRule](Get-OPNSenseFirewallRule.md)
[New-OPNSenseFirewallRule](New-OPNSenseFirewallRule.md)
[Set-OPNSenseFirewallRule](Set-OPNSenseFirewallRule.md)
[Enable-OPNSenseFirewallRule](Enable-OPNSenseFirewallRule.md)
[Disable-OPNSenseFirewallRule](Disable-OPNSenseFirewallRule.md)
[Apply-OPNSenseFirewallChanges](Apply-OPNSenseFirewallChanges.md)
