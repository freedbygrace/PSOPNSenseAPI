# Install-OPNSenseTailscale

## SYNOPSIS
Installs the Tailscale plugin on an OPNSense firewall.

## SYNTAX

```
Install-OPNSenseTailscale [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Install-OPNSenseTailscale cmdlet installs the Tailscale plugin on an OPNSense firewall if it is not already installed.

## EXAMPLES

### Example 1: Install Tailscale
```powershell
Install-OPNSenseTailscale
```

This example installs the Tailscale plugin on the OPNSense firewall.

### Example 2: Install Tailscale with confirmation
```powershell
Install-OPNSenseTailscale -Confirm
```

This example prompts for confirmation before installing the Tailscale plugin.

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
After installing the Tailscale plugin, you need to configure it using Set-OPNSenseTailscaleConfig and enable it using Enable-OPNSenseTailscale.
The firewall may need to be restarted after installing the plugin.

## RELATED LINKS

[Get-OPNSenseTailscaleStatus](Get-OPNSenseTailscaleStatus.md)
[Get-OPNSenseTailscaleConfig](Get-OPNSenseTailscaleConfig.md)
[Set-OPNSenseTailscaleConfig](Set-OPNSenseTailscaleConfig.md)
[Enable-OPNSenseTailscale](Enable-OPNSenseTailscale.md)
[Disable-OPNSenseTailscale](Disable-OPNSenseTailscale.md)
