# Get-OPNSenseSystemDNS

## SYNOPSIS
Gets system DNS settings from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseSystemDNS
```

## DESCRIPTION
The Get-OPNSenseSystemDNS cmdlet retrieves system DNS settings from an OPNSense firewall, including DNS servers and domain search list.

## EXAMPLES

### Example 1: Get system DNS settings
```powershell
Get-OPNSenseSystemDNS
```

This example retrieves the system DNS settings from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the system DNS settings, including DNS servers and domain search list.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Set-OPNSenseSystemDNS](Set-OPNSenseSystemDNS.md)
[Get-OPNSenseDNSServer](Get-OPNSenseDNSServer.md)
[Set-OPNSenseDNSServer](Set-OPNSenseDNSServer.md)
