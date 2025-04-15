# Get-OPNSenseFirewallRule

## SYNOPSIS
Gets firewall rules from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseFirewallRule [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseFirewallRule cmdlet retrieves firewall rules from an OPNSense firewall. You can retrieve all rules or a specific rule by UUID.

## EXAMPLES

### Example 1: Get all firewall rules
```powershell
Get-OPNSenseFirewallRule
```

This example retrieves all firewall rules from the OPNSense firewall.

### Example 2: Get a specific firewall rule
```powershell
Get-OPNSenseFirewallRule -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific firewall rule by its UUID.

## PARAMETERS

### -UUID
The UUID of the firewall rule to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
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
Returns objects representing the firewall rules.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseFirewallRule](New-OPNSenseFirewallRule.md)
[Set-OPNSenseFirewallRule](Set-OPNSenseFirewallRule.md)
[Remove-OPNSenseFirewallRule](Remove-OPNSenseFirewallRule.md)
[Enable-OPNSenseFirewallRule](Enable-OPNSenseFirewallRule.md)
[Disable-OPNSenseFirewallRule](Disable-OPNSenseFirewallRule.md)
[Apply-OPNSenseFirewallChanges](Apply-OPNSenseFirewallChanges.md)
