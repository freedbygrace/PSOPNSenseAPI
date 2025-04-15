# Get-OPNSenseDNSForwarding

## SYNOPSIS
Gets DNS forwarding settings from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseDNSForwarding
```

## DESCRIPTION
The Get-OPNSenseDNSForwarding cmdlet retrieves DNS forwarding settings from an OPNSense firewall.

## EXAMPLES

### Example 1: Get DNS forwarding settings
```powershell
Get-OPNSenseDNSForwarding
```

This example retrieves the DNS forwarding settings from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the DNS forwarding settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Set-OPNSenseDNSForwarding](Set-OPNSenseDNSForwarding.md)
[Get-OPNSenseDNSForwardingHost](Get-OPNSenseDNSForwardingHost.md)
[New-OPNSenseDNSForwardingHost](New-OPNSenseDNSForwardingHost.md)
[Set-OPNSenseDNSForwardingHost](Set-OPNSenseDNSForwardingHost.md)
[Remove-OPNSenseDNSForwardingHost](Remove-OPNSenseDNSForwardingHost.md)
