# Get-OPNSenseRoute

## SYNOPSIS
Gets static routes from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseRoute [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseRoute cmdlet retrieves static routes from an OPNSense firewall. You can retrieve all routes or a specific route by UUID.

## EXAMPLES

### Example 1: Get all routes
```powershell
Get-OPNSenseRoute
```

This example retrieves all static routes from the OPNSense firewall.

### Example 2: Get a specific route
```powershell
Get-OPNSenseRoute -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific static route by its UUID.

## PARAMETERS

### -UUID
The UUID of the route to retrieve.

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
Returns objects representing the static routes.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseRoute](New-OPNSenseRoute.md)
[Set-OPNSenseRoute](Set-OPNSenseRoute.md)
[Remove-OPNSenseRoute](Remove-OPNSenseRoute.md)
