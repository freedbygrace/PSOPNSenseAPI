# Disconnect-OPNSense

## SYNOPSIS
Disconnects from an OPNSense firewall.

## SYNTAX

```
Disconnect-OPNSense
```

## DESCRIPTION
The Disconnect-OPNSense cmdlet terminates the connection to an OPNSense firewall.

## EXAMPLES

### Example 1: Disconnect from an OPNSense firewall
```powershell
Disconnect-OPNSense
```

This example disconnects from the currently connected OPNSense firewall.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

## NOTES
This cmdlet releases resources used by the connection to the OPNSense firewall.

## RELATED LINKS

[Connect-OPNSense](Connect-OPNSense.md)
[Get-OPNSenseConnection](Get-OPNSenseConnection.md)
