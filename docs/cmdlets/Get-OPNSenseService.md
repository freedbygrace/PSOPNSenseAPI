# Get-OPNSenseService

## SYNOPSIS
Gets services from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseService [[-Name] <String>]
```

## DESCRIPTION
The Get-OPNSenseService cmdlet retrieves services from an OPNSense firewall. You can retrieve all services or a specific service by name.

## EXAMPLES

### Example 1: Get all services
```powershell
Get-OPNSenseService
```

This example retrieves all services from the OPNSense firewall.

### Example 2: Get a specific service
```powershell
Get-OPNSenseService -Name "dhcpd"
```

This example retrieves the DHCP service from the OPNSense firewall.

## PARAMETERS

### -Name
The name of the service to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object
Returns objects representing the services, including information such as name, description, and status.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.

## RELATED LINKS

[Start-OPNSenseService](Start-OPNSenseService.md)
[Stop-OPNSenseService](Stop-OPNSenseService.md)
[Restart-OPNSenseService](Restart-OPNSenseService.md)
