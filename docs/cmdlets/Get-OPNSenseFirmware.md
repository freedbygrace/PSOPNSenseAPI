# Get-OPNSenseFirmware

## SYNOPSIS
Gets firmware information from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseFirmware
```

## DESCRIPTION
The Get-OPNSenseFirmware cmdlet retrieves firmware information from an OPNSense firewall, including the current version and available updates.

## EXAMPLES

### Example 1: Get firmware information
```powershell
Get-OPNSenseFirmware
```

This example retrieves firmware information from the OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns an object representing the firmware information, including the current version, available updates, and update status.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Update-OPNSenseFirmware](Update-OPNSenseFirmware.md)
[Start-OPNSenseFirmwareUpgrade](Start-OPNSenseFirmwareUpgrade.md)
