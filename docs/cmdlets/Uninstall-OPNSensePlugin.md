# Uninstall-OPNSensePlugin

## SYNOPSIS
Uninstalls a plugin from an OPNSense firewall.

## SYNTAX

```
Uninstall-OPNSensePlugin -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Uninstall-OPNSensePlugin cmdlet uninstalls a plugin from an OPNSense firewall.

## EXAMPLES

### Example 1: Uninstall a plugin
```powershell
Uninstall-OPNSensePlugin -Name "os-acme-client"
```

This example uninstalls the plugin with the name "os-acme-client" from the OPNSense firewall.

### Example 2: Uninstall a plugin with confirmation
```powershell
Uninstall-OPNSensePlugin -Name "os-theme-cicada" -Confirm
```

This example prompts for confirmation before uninstalling the plugin.

## PARAMETERS

### -Name
The name of the plugin to uninstall.

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
You can pipe a string containing the plugin name to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Uninstalling plugins may require a reboot of the firewall for the changes to take effect.
Some plugins may have dependencies that need to be uninstalled first.
Uninstalling a plugin will remove all configuration data associated with the plugin.

## RELATED LINKS

[Get-OPNSensePlugin](Get-OPNSensePlugin.md)
[Install-OPNSensePlugin](Install-OPNSensePlugin.md)
[Enable-OPNSensePlugin](Enable-OPNSensePlugin.md)
[Disable-OPNSensePlugin](Disable-OPNSensePlugin.md)
