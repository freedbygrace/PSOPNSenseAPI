# Get-OPNSenseConnection

## SYNOPSIS
Gets information about the current OPNSense connection.

## SYNTAX

```
Get-OPNSenseConnection
```

## DESCRIPTION
The Get-OPNSenseConnection cmdlet retrieves information about the current connection to an OPNSense firewall.

## EXAMPLES

### Example 1: Get connection information
```powershell
Get-OPNSenseConnection
```

This example retrieves information about the current connection to an OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object containing information about the current connection, including the server URL and connection status.

## NOTES
This cmdlet can be used to verify that a connection to an OPNSense firewall has been established.

## RELATED LINKS

[Connect-OPNSense](Connect-OPNSense.md)
[Disconnect-OPNSense](Disconnect-OPNSense.md)
