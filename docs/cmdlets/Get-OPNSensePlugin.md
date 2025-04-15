# Get-OPNSensePlugin

## SYNOPSIS
Gets plugins from an OPNSense firewall.

## SYNTAX

```
Get-OPNSensePlugin [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSensePlugin cmdlet retrieves plugins from an OPNSense firewall. You can retrieve all plugins or a specific plugin by name.

## EXAMPLES

### Example 1: Get all plugins
```powershell
Get-OPNSensePlugin
```

This example retrieves all plugins from the OPNSense firewall.

### Example 2: Get a specific plugin
```powershell
Get-OPNSensePlugin -Name "os-acme-client"
```

This example retrieves the plugin with the name "os-acme-client" from the OPNSense firewall.

## PARAMETERS

### -Name
The name of the plugin to retrieve.

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
Returns objects representing the plugins, including information such as name, version, and installation status.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Install-OPNSensePlugin](Install-OPNSensePlugin.md)
[Uninstall-OPNSensePlugin](Uninstall-OPNSensePlugin.md)
[Enable-OPNSensePlugin](Enable-OPNSensePlugin.md)
[Disable-OPNSensePlugin](Disable-OPNSensePlugin.md)
