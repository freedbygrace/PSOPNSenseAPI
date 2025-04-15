# New-OPNSenseDNSOverride

## SYNOPSIS
Creates a new DNS override on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseDNSOverride -Host <String> -Domain <String> -IP <String> [-Description <String>] [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseDNSOverride cmdlet creates a new DNS override on an OPNSense firewall. This allows you to override DNS resolution for specific hosts.

## EXAMPLES

### Example 1: Create a new DNS override
```powershell
New-OPNSenseDNSOverride -Host "server" -Domain "example.com" -IP "192.168.1.10" -Description "Internal Server"
```

This example creates a new DNS override that resolves server.example.com to 192.168.1.10.

### Example 2: Create a disabled DNS override
```powershell
New-OPNSenseDNSOverride -Host "test" -Domain "local" -IP "10.0.0.5" -Description "Test Server" -Disabled $true
```

This example creates a new DNS override that is initially disabled.

## PARAMETERS

### -Host
The hostname part of the DNS override.

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

### -Domain
The domain part of the DNS override.

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

### -IP
The IP address to which the hostname should resolve.

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
A description for the DNS override.

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
Whether the DNS override is disabled.

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
Returns an object representing the newly created DNS override.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a DNS override, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseDNSOverride](Get-OPNSenseDNSOverride.md)
[Set-OPNSenseDNSOverride](Set-OPNSenseDNSOverride.md)
[Remove-OPNSenseDNSOverride](Remove-OPNSenseDNSOverride.md)
