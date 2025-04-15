# Get-OPNSenseSystemStatus

## SYNOPSIS
Gets system status information from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseSystemStatus
```

## DESCRIPTION
The Get-OPNSenseSystemStatus cmdlet retrieves system status information from an OPNSense firewall, including CPU usage, memory usage, disk usage, and uptime.

## EXAMPLES

### Example 1: Get system status
```powershell
Get-OPNSenseSystemStatus
```

This example retrieves system status information from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the system status information.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Get-OPNSenseSystemInfo](Get-OPNSenseSystemInfo.md)
[Restart-OPNSenseSystem](Restart-OPNSenseSystem.md)
[Stop-OPNSenseSystem](Stop-OPNSenseSystem.md)
