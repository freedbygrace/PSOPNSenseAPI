# Set-OPNSenseDNSForwardingHost

## SYNOPSIS
Modifies an existing DNS forwarding host on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDNSForwardingHost -UUID <String> [-Domain <String>] [-Server <String>] [-Description <String>]
                              [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDNSForwardingHost cmdlet modifies an existing DNS forwarding host on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a DNS forwarding host description
```powershell
Set-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated DNS Server"
```

This example updates the description of an existing DNS forwarding host.

### Example 2: Change the server for a DNS forwarding host
```powershell
Set-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Server "192.168.2.10"
```

This example changes the DNS server used for an existing DNS forwarding host.

### Example 3: Disable a DNS forwarding host
```powershell
Set-OPNSenseDNSForwardingHost -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Disabled $true
```

This example disables an existing DNS forwarding host.

## PARAMETERS

### -UUID
The UUID of the DNS forwarding host to modify.

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

### -Domain
The domain for which DNS queries should be forwarded.

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

### -Server
The DNS server to which queries for the domain should be forwarded.

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
You can pipe a string containing the UUID of a DNS forwarding host to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified DNS forwarding host.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a DNS forwarding host, you may need to apply the changes for them to take effect.
DNS forwarding must be enabled using Set-OPNSenseDNSForwarding for these settings to be used.

## RELATED LINKS

[Get-OPNSenseDNSForwardingHost](Get-OPNSenseDNSForwardingHost.md)
[New-OPNSenseDNSForwardingHost](New-OPNSenseDNSForwardingHost.md)
[Remove-OPNSenseDNSForwardingHost](Remove-OPNSenseDNSForwardingHost.md)
[Get-OPNSenseDNSForwarding](Get-OPNSenseDNSForwarding.md)
[Set-OPNSenseDNSForwarding](Set-OPNSenseDNSForwarding.md)
