# Get-OPNSenseTailscaleConfig

## SYNOPSIS
Gets the Tailscale configuration from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseTailscaleConfig
```

## DESCRIPTION
The Get-OPNSenseTailscaleConfig cmdlet retrieves the Tailscale configuration from an OPNSense firewall, including settings such as authentication key, advertised routes, and exit node configuration.

## EXAMPLES

### Example 1: Get Tailscale configuration
```powershell
Get-OPNSenseTailscaleConfig
```

This example retrieves the Tailscale configuration from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the Tailscale configuration.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The Tailscale plugin must be installed on the OPNSense firewall.

## RELATED LINKS

[Set-OPNSenseTailscaleConfig](Set-OPNSenseTailscaleConfig.md)
[Enable-OPNSenseTailscale](Enable-OPNSenseTailscale.md)
[Disable-OPNSenseTailscale](Disable-OPNSenseTailscale.md)
[Get-OPNSenseTailscaleStatus](Get-OPNSenseTailscaleStatus.md)
[Install-OPNSenseTailscale](Install-OPNSenseTailscale.md)
