# Get-OPNSenseSystemInfo

## SYNOPSIS
Gets system information from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseSystemInfo
```

## DESCRIPTION
The Get-OPNSenseSystemInfo cmdlet retrieves system information from an OPNSense firewall, including version, platform, hardware details, and installed packages.

## EXAMPLES

### Example 1: Get system information
```powershell
Get-OPNSenseSystemInfo
```

This example retrieves system information from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the system information.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Get-OPNSenseSystemStatus](Get-OPNSenseSystemStatus.md)
[Restart-OPNSenseSystem](Restart-OPNSenseSystem.md)
[Stop-OPNSenseSystem](Stop-OPNSenseSystem.md)
