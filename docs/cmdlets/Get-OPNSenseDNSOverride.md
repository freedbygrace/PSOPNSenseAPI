# Get-OPNSenseDNSOverride

## SYNOPSIS
Gets DNS overrides from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDNSOverride [[-UUID] <String>]
```

## DESCRIPTION
The Get-OPNSenseDNSOverride cmdlet retrieves DNS overrides from an OPNSense firewall. You can retrieve all overrides or a specific override by UUID.

## EXAMPLES

### Example 1: Get all DNS overrides
```powershell
Get-OPNSenseDNSOverride
```

This example retrieves all DNS overrides from the OPNSense firewall.

### Example 2: Get a specific DNS override
```powershell
Get-OPNSenseDNSOverride -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

This example retrieves a specific DNS override by its UUID.

## PARAMETERS

### -UUID
The UUID of the DNS override to retrieve.

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
Returns objects representing the DNS overrides.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[New-OPNSenseDNSOverride](New-OPNSenseDNSOverride.md)
[Set-OPNSenseDNSOverride](Set-OPNSenseDNSOverride.md)
[Remove-OPNSenseDNSOverride](Remove-OPNSenseDNSOverride.md)
