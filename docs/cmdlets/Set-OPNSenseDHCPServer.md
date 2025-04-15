# Set-OPNSenseDHCPServer

## SYNOPSIS
Modifies DHCP server settings on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDHCPServer -Interface <String> [-Enabled <Boolean>] [-RangeFrom <String>] [-RangeTo <String>]
                       [-DNSServers <String[]>] [-Domain <String>] [-Gateway <String>] [-LeaseTime <String>]
                       [-DenyUnknownClients <Boolean>] [-IgnoreClientIdentifiers <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDHCPServer cmdlet modifies DHCP server settings on an OPNSense firewall for a specific interface.

## EXAMPLES

### Example 1: Enable the DHCP server on an interface
```powershell
Set-OPNSenseDHCPServer -Interface "lan" -Enabled $true
```

This example enables the DHCP server on the LAN interface.

### Example 2: Configure DHCP server settings
```powershell
Set-OPNSenseDHCPServer -Interface "lan" -Enabled $true -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -DNSServers "192.168.1.1", "8.8.8.8" -Domain "example.local" -LeaseTime "86400"
```

This example configures multiple DHCP server settings for the LAN interface.

## PARAMETERS

### -Interface
The interface for which to modify DHCP server settings.

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

### -Enabled
Whether the DHCP server is enabled for the interface.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RangeFrom
The starting IP address of the DHCP range.

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

### -RangeTo
The ending IP address of the DHCP range.

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

### -DNSServers
The DNS servers to provide to DHCP clients.

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

### -Domain
The domain name to provide to DHCP clients.

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

### -Gateway
The gateway to provide to DHCP clients.

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

### -LeaseTime
The lease time in seconds for DHCP leases.

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

### -DenyUnknownClients
Whether to deny unknown clients.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreClientIdentifiers
Whether to ignore client identifiers.

```yaml
Type: Boolean
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

### System.String
You can pipe a string containing the interface name to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified DHCP server settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying DHCP server settings, the DHCP server service may need to be restarted for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
