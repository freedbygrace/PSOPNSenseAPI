# Get-OPNSenseConfig

## SYNOPSIS
Gets the OPNSense firewall configuration as an XML document.

## SYNTAX

```
Get-OPNSenseConfig
```

## DESCRIPTION
The Get-OPNSenseConfig cmdlet retrieves the OPNSense firewall configuration and returns it as an XML document.

## EXAMPLES

### Example 1: Get the configuration as an XML document
```powershell
$config = Get-OPNSenseConfig
```

This example retrieves the OPNSense firewall configuration as an XML document.

### Example 2: Get the configuration and pipe it to Restore-OPNSenseConfig
```powershell
Get-OPNSenseConfig | Restore-OPNSenseConfig
```

This example retrieves the OPNSense firewall configuration and restores it.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Xml.XmlDocument
The OPNSense configuration as an XML document.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Restore-OPNSenseConfig](Restore-OPNSenseConfig.md)
[Export-OPNSenseConfig](Export-OPNSenseConfig.md)
[Import-OPNSenseConfig](Import-OPNSenseConfig.md)
