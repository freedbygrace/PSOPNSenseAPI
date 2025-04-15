# Restart-OPNSenseService

## SYNOPSIS
Restarts a service on an OPNSense firewall.

## SYNTAX

```
Restart-OPNSenseService -Name <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Restart-OPNSenseService cmdlet restarts a service on an OPNSense firewall.

## EXAMPLES

### Example 1: Restart a service
```powershell
Restart-OPNSenseService -Name "dhcpd"
```

This example restarts the DHCP service on the OPNSense firewall.

### Example 2: Restart a service with confirmation
```powershell
Restart-OPNSenseService -Name "unbound" -Confirm
```

This example prompts for confirmation before restarting the DNS resolver service.

## PARAMETERS

### -Name
The name of the service to restart.

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
Restarting essential services may temporarily disrupt network connectivity or firewall functionality.

## RELATED LINKS

[Get-OPNSenseService](Get-OPNSenseService.md)
[Start-OPNSenseService](Start-OPNSenseService.md)
[Stop-OPNSenseService](Stop-OPNSenseService.md)
