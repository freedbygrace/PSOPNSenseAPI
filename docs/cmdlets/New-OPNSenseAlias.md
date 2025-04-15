# New-OPNSenseAlias

## SYNOPSIS
Creates a new alias on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseAlias -Name <String> -Type <String> -Content <String[]> [-Description <String>] [-Enabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseAlias cmdlet creates a new alias on an OPNSense firewall. Aliases are used to define groups of network objects that can be referenced in firewall rules.

## EXAMPLES

### Example 1: Create a new host alias
```powershell
New-OPNSenseAlias -Name "WebServers" -Type "host" -Content "192.168.1.10", "192.168.1.11" -Description "Web Servers"
```

This example creates a new host alias named "WebServers" containing two IP addresses.

### Example 2: Create a new network alias
```powershell
New-OPNSenseAlias -Name "RemoteNetworks" -Type "network" -Content "10.0.0.0/24", "172.16.0.0/16" -Description "Remote Networks"
```

This example creates a new network alias named "RemoteNetworks" containing two network ranges.

### Example 3: Create a new port alias
```powershell
New-OPNSenseAlias -Name "WebPorts" -Type "port" -Content "80", "443" -Description "Web Ports"
```

This example creates a new port alias named "WebPorts" containing two port numbers.

## PARAMETERS

### -Name
The name of the alias to create.

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

### -Type
The type of the alias (host, network, port, url, urltable, geoip).

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

### -Content
The content of the alias, which depends on the type.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
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
Default value: True
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
Returns an object representing the newly created alias.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating an alias, you may need to apply the changes for them to take effect.
Alias names must be unique and should not contain spaces or special characters.

## RELATED LINKS

[Get-OPNSenseAlias](Get-OPNSenseAlias.md)
[Set-OPNSenseAlias](Set-OPNSenseAlias.md)
[Remove-OPNSenseAlias](Remove-OPNSenseAlias.md)
