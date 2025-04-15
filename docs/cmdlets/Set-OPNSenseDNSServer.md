# Set-OPNSenseDNSServer

## SYNOPSIS
Modifies DNS server settings on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDNSServer [-Enabled <Boolean>] [-ListenIPs <String[]>] [-Port <Int32>] [-DNSSECEnabled <Boolean>]
                      [-ForwardMode <String>] [-StrictBindingEnabled <Boolean>] [-AllQueryLogEnabled <Boolean>]
                      [-RegisterIPv4 <Boolean>] [-RegisterIPv6 <Boolean>] [-DNSv6PrefixEnabled <Boolean>]
                      [-PreferIPv4 <Boolean>] [-PreferIPv6 <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDNSServer cmdlet modifies DNS server settings on an OPNSense firewall.

## EXAMPLES

### Example 1: Enable the DNS server
```powershell
Set-OPNSenseDNSServer -Enabled $true
```

This example enables the DNS server on the OPNSense firewall.

### Example 2: Configure DNS server settings
```powershell
Set-OPNSenseDNSServer -Enabled $true -ListenIPs "127.0.0.1", "192.168.1.1" -Port 53 -DNSSECEnabled $true -ForwardMode "forward" -AllQueryLogEnabled $true
```

This example configures multiple DNS server settings on the OPNSense firewall.

## PARAMETERS

### -Enabled
Whether the DNS server is enabled.

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

### -ListenIPs
The IP addresses on which the DNS server should listen.

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

### -Port
The port on which the DNS server should listen.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DNSSECEnabled
Whether DNSSEC is enabled.

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

### -ForwardMode
The forwarding mode for the DNS server (forward, resolver, etc.).

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

### -StrictBindingEnabled
Whether strict binding is enabled.

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

### -AllQueryLogEnabled
Whether to log all DNS queries.

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

### -RegisterIPv4
Whether to register IPv4 addresses.

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

### -RegisterIPv6
Whether to register IPv6 addresses.

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

### -DNSv6PrefixEnabled
Whether DNSv6 prefix is enabled.

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

### -PreferIPv4
Whether to prefer IPv4.

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

### -PreferIPv6
Whether to prefer IPv6.

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

### None

## OUTPUTS

### System.Object
Returns an object representing the modified DNS server settings.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying DNS server settings, the DNS server service may need to be restarted.

## RELATED LINKS

[Get-OPNSenseDNSServer](Get-OPNSenseDNSServer.md)
[Get-OPNSenseSystemDNS](Get-OPNSenseSystemDNS.md)
[Set-OPNSenseSystemDNS](Set-OPNSenseSystemDNS.md)
