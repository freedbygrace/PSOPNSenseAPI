# Apply-OPNSenseFirewallChanges

## SYNOPSIS
Applies pending firewall changes on an OPNSense firewall.

## SYNTAX

```
Apply-OPNSenseFirewallChanges [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Apply-OPNSenseFirewallChanges cmdlet applies pending firewall changes on an OPNSense firewall. This is required after making changes to firewall rules.

## EXAMPLES

### Example 1: Apply firewall changes
```powershell
Apply-OPNSenseFirewallChanges
```

This example applies all pending firewall changes.

## PARAMETERS

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
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
This cmdlet should be called after making changes to firewall rules to apply the changes.

## RELATED LINKS

[Get-OPNSenseFirewallRule](Get-OPNSenseFirewallRule.md)
[New-OPNSenseFirewallRule](New-OPNSenseFirewallRule.md)
[Set-OPNSenseFirewallRule](Set-OPNSenseFirewallRule.md)
[Remove-OPNSenseFirewallRule](Remove-OPNSenseFirewallRule.md)
[Enable-OPNSenseFirewallRule](Enable-OPNSenseFirewallRule.md)
[Disable-OPNSenseFirewallRule](Disable-OPNSenseFirewallRule.md)
