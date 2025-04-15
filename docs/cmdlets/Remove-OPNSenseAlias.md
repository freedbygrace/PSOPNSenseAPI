# Remove-OPNSenseAlias

## SYNOPSIS
Removes an alias from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseAlias -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseAlias cmdlet removes an alias from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove an alias
```powershell
Remove-OPNSenseAlias -Name "WebServers"
```

This example removes the alias named "WebServers" from the OPNSense firewall.

### Example 2: Remove an alias with confirmation
```powershell
Remove-OPNSenseAlias -Name "RemoteNetworks" -Confirm
```

This example prompts for confirmation before removing the alias.

## PARAMETERS

### -Name
The name of the alias to remove.

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
You can pipe a string containing the alias name to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After removing an alias, you may need to apply the changes for them to take effect.
Be careful when removing aliases that are used in firewall rules, as this may cause the rules to stop working.

## RELATED LINKS

[Get-OPNSenseAlias](Get-OPNSenseAlias.md)
[New-OPNSenseAlias](New-OPNSenseAlias.md)
[Set-OPNSenseAlias](Set-OPNSenseAlias.md)
