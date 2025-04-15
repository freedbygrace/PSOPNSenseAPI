# Set-OPNSenseSystemDNS

## SYNOPSIS
Modifies system DNS settings on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseSystemDNS [-DNSServers <String[]>] [-DNSSearchDomains <String[]>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseSystemDNS cmdlet modifies system DNS settings on an OPNSense firewall, including DNS servers and domain search list.

## EXAMPLES

### Example 1: Set system DNS servers
```powershell
Set-OPNSenseSystemDNS -DNSServers "8.8.8.8", "8.8.4.4"
```

This example sets the system DNS servers to Google's public DNS servers.

### Example 2: Set system DNS servers and search domains
```powershell
Set-OPNSenseSystemDNS -DNSServers "192.168.1.10", "192.168.1.11" -DNSSearchDomains "example.com", "local.example.com"
```

This example sets both the system DNS servers and search domains.

## PARAMETERS

### -DNSServers
The DNS servers to use for system DNS resolution.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DNSSearchDomains
The domain search list for DNS resolution.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### None

## OUTPUTS

### System.Object
Returns an object representing the modified system DNS settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying system DNS settings, the system may need to be restarted for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseSystemDNS](Get-OPNSenseSystemDNS.md)
[Get-OPNSenseDNSServer](Get-OPNSenseDNSServer.md)
[Set-OPNSenseDNSServer](Set-OPNSenseDNSServer.md)
