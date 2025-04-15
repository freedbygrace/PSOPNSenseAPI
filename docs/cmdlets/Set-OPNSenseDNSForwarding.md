# Set-OPNSenseDNSForwarding

## SYNOPSIS
Modifies DNS forwarding settings on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDNSForwarding [-Enabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDNSForwarding cmdlet modifies DNS forwarding settings on an OPNSense firewall.

## EXAMPLES

### Example 1: Enable DNS forwarding
```powershell
Set-OPNSenseDNSForwarding -Enabled $true
```

This example enables DNS forwarding on the OPNSense firewall.

### Example 2: Disable DNS forwarding
```powershell
Set-OPNSenseDNSForwarding -Enabled $false
```

This example disables DNS forwarding on the OPNSense firewall.

## PARAMETERS

### -Enabled
Whether DNS forwarding is enabled.

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
Returns an object representing the modified DNS forwarding settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying DNS forwarding settings, the DNS service may need to be restarted for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseDNSForwarding](Get-OPNSenseDNSForwarding.md)
[Get-OPNSenseDNSForwardingHost](Get-OPNSenseDNSForwardingHost.md)
[New-OPNSenseDNSForwardingHost](New-OPNSenseDNSForwardingHost.md)
[Set-OPNSenseDNSForwardingHost](Set-OPNSenseDNSForwardingHost.md)
[Remove-OPNSenseDNSForwardingHost](Remove-OPNSenseDNSForwardingHost.md)
