# Get-OPNSenseDNSServer

## SYNOPSIS
Gets DNS server settings from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDNSServer
```

## DESCRIPTION
The Get-OPNSenseDNSServer cmdlet retrieves DNS server settings from an OPNSense firewall.

## EXAMPLES

### Example 1: Get DNS server settings
```powershell
Get-OPNSenseDNSServer
```

This example retrieves the DNS server settings from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the DNS server settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Set-OPNSenseDNSServer](Set-OPNSenseDNSServer.md)
[Get-OPNSenseSystemDNS](Get-OPNSenseSystemDNS.md)
[Set-OPNSenseSystemDNS](Set-OPNSenseSystemDNS.md)
