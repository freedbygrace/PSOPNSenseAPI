# Get-OPNSenseTailscaleStatus

## SYNOPSIS
Gets the status of Tailscale on an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseTailscaleStatus
```

## DESCRIPTION
The Get-OPNSenseTailscaleStatus cmdlet retrieves the status of Tailscale on an OPNSense firewall, including connection state, node information, and subnet routes.

## EXAMPLES

### Example 1: Get Tailscale status
```powershell
Get-OPNSenseTailscaleStatus
```

This example retrieves the status of Tailscale on the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the Tailscale status.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The Tailscale plugin must be installed and configured on the OPNSense firewall.

## RELATED LINKS

[Enable-OPNSenseTailscale](Enable-OPNSenseTailscale.md)
[Disable-OPNSenseTailscale](Disable-OPNSenseTailscale.md)
[Set-OPNSenseTailscaleConfig](Set-OPNSenseTailscaleConfig.md)
[Get-OPNSenseTailscaleConfig](Get-OPNSenseTailscaleConfig.md)
[Install-OPNSenseTailscale](Install-OPNSenseTailscale.md)
