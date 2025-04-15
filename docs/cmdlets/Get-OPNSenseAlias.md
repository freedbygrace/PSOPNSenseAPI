# Get-OPNSenseAlias

## SYNOPSIS
Gets aliases from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseAlias [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSenseAlias cmdlet retrieves aliases from an OPNSense firewall. You can retrieve all aliases or a specific alias by name.

## EXAMPLES

### Example 1: Get all aliases
```powershell
Get-OPNSenseAlias
```

This example retrieves all aliases from the OPNSense firewall.

### Example 2: Get a specific alias
```powershell
Get-OPNSenseAlias -Name "LAN_HOSTS"
```

This example retrieves the alias with the name "LAN_HOSTS" from the OPNSense firewall.

## PARAMETERS

### -Name
The name of the alias to retrieve.

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
Returns objects representing the aliases, including information such as name, type, content, and description.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseAlias](New-OPNSenseAlias.md)
[Set-OPNSenseAlias](Set-OPNSenseAlias.md)
[Remove-OPNSenseAlias](Remove-OPNSenseAlias.md)
