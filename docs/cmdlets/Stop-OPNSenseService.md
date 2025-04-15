# Stop-OPNSenseService

## SYNOPSIS
Stops a service on an OPNSense firewall.

## SYNTAX

```
Stop-OPNSenseService -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Stop-OPNSenseService cmdlet stops a service on an OPNSense firewall.

## EXAMPLES

### Example 1: Stop a service
```powershell
Stop-OPNSenseService -Name "dhcpd"
```

This example stops the DHCP service on the OPNSense firewall.

### Example 2: Stop a service with confirmation
```powershell
Stop-OPNSenseService -Name "unbound" -Confirm
```

This example prompts for confirmation before stopping the DNS resolver service.

## PARAMETERS

### -Name
The name of the service to stop.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
You can pipe a string containing the service name to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Stopping essential services may disrupt network connectivity or firewall functionality.

## RELATED LINKS

[Get-OPNSenseService](Get-OPNSenseService.md)
[Start-OPNSenseService](Start-OPNSenseService.md)
[Restart-OPNSenseService](Restart-OPNSenseService.md)
