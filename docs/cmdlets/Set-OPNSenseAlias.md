# Set-OPNSenseAlias

## SYNOPSIS
Modifies an existing alias on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseAlias -Name <String> [-Type <String>] [-Content <String[]>] [-Description <String>] [-Enabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseAlias cmdlet modifies an existing alias on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify an alias description
```powershell
Set-OPNSenseAlias -Name "WebServers" -Description "Updated Web Servers"
```

This example updates the description of the alias named "WebServers".

### Example 2: Modify alias content
```powershell
Set-OPNSenseAlias -Name "WebServers" -Content "192.168.1.10", "192.168.1.11", "192.168.1.12"
```

This example updates the content of the alias named "WebServers" to include an additional IP address.

### Example 3: Disable an alias
```powershell
Set-OPNSenseAlias -Name "WebServers" -Enabled $false
```

This example disables the alias named "WebServers".

## PARAMETERS

### -Name
The name of the alias to modify.

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

### -Type
The type of the alias (host, network, port, url, urltable, geoip).

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

### -Content
The content of the alias, which depends on the type.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A description for the alias.

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
Whether the alias is enabled.

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

### System.String
You can pipe a string containing the alias name to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified alias.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying an alias, you may need to apply the changes for them to take effect.
Changing the type of an alias may require adjusting the content to match the new type.

## RELATED LINKS

[Get-OPNSenseAlias](Get-OPNSenseAlias.md)
[New-OPNSenseAlias](New-OPNSenseAlias.md)
[Remove-OPNSenseAlias](Remove-OPNSenseAlias.md)
