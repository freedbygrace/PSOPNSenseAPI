# New-OPNSenseDNSForwardingHost

## SYNOPSIS
Creates a new DNS forwarding host on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseDNSForwardingHost -Domain <String> -Server <String> [-Description <String>] [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseDNSForwardingHost cmdlet creates a new DNS forwarding host on an OPNSense firewall. This allows you to forward DNS queries for specific domains to specific DNS servers.

## EXAMPLES

### Example 1: Create a new DNS forwarding host
```powershell
New-OPNSenseDNSForwardingHost -Domain "example.com" -Server "192.168.1.10" -Description "Internal DNS Server"
```

This example creates a new DNS forwarding host that forwards queries for example.com to the DNS server at 192.168.1.10.

### Example 2: Create a disabled DNS forwarding host
```powershell
New-OPNSenseDNSForwardingHost -Domain "test.local" -Server "10.0.0.53" -Description "Test DNS Server" -Disabled $true
```

This example creates a new DNS forwarding host that is initially disabled.

## PARAMETERS

### -Domain
The domain for which DNS queries should be forwarded.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server
The DNS server to which queries for the domain should be forwarded.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A description for the DNS forwarding host.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Disabled
Whether the DNS forwarding host is disabled.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
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
Returns an object representing the newly created DNS forwarding host.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a DNS forwarding host, you may need to apply the changes for them to take effect.
DNS forwarding must be enabled using Set-OPNSenseDNSForwarding for these settings to be used.

## RELATED LINKS

[Get-OPNSenseDNSForwardingHost](Get-OPNSenseDNSForwardingHost.md)
[Set-OPNSenseDNSForwardingHost](Set-OPNSenseDNSForwardingHost.md)
[Remove-OPNSenseDNSForwardingHost](Remove-OPNSenseDNSForwardingHost.md)
[Get-OPNSenseDNSForwarding](Get-OPNSenseDNSForwarding.md)
[Set-OPNSenseDNSForwarding](Set-OPNSenseDNSForwarding.md)
