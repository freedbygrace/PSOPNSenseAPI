# Enable-OPNSenseTailscale

## SYNOPSIS
Enables Tailscale on an OPNSense firewall.

## SYNTAX

```
Enable-OPNSenseTailscale [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Enable-OPNSenseTailscale cmdlet enables the Tailscale service on an OPNSense firewall.

## EXAMPLES

### Example 1: Enable Tailscale
```powershell
Enable-OPNSenseTailscale
```

This example enables the Tailscale service on the OPNSense firewall.

### Example 2: Enable Tailscale with confirmation
```powershell
Enable-OPNSenseTailscale -Confirm
```

This example prompts for confirmation before enabling the Tailscale service.

## PARAMETERS

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
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The Tailscale plugin must be installed on the OPNSense firewall.
Tailscale must be properly configured before enabling it. Use Set-OPNSenseTailscaleConfig to configure Tailscale.

## RELATED LINKS

[Disable-OPNSenseTailscale](Disable-OPNSenseTailscale.md)
[Get-OPNSenseTailscaleStatus](Get-OPNSenseTailscaleStatus.md)
[Get-OPNSenseTailscaleConfig](Get-OPNSenseTailscaleConfig.md)
[Set-OPNSenseTailscaleConfig](Set-OPNSenseTailscaleConfig.md)
[Install-OPNSenseTailscale](Install-OPNSenseTailscale.md)
