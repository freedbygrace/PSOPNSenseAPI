# Set-OPNSenseTailscaleConfig

## SYNOPSIS
Modifies the Tailscale configuration on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseTailscaleConfig [-AuthKey <String>] [-Hostname <String>] [-AdvertiseRoutes <String[]>] [-AcceptRoutes <Boolean>]
                           [-ExitNode <Boolean>] [-ExitNodeAllowLAN <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseTailscaleConfig cmdlet modifies the Tailscale configuration on an OPNSense firewall, including settings such as authentication key, advertised routes, and exit node configuration.

## EXAMPLES

### Example 1: Set the Tailscale authentication key
```powershell
Set-OPNSenseTailscaleConfig -AuthKey "tskey-auth-abcdefghijklmnopqrstuvwxyz"
```

This example sets the Tailscale authentication key.

### Example 2: Configure Tailscale to advertise routes
```powershell
Set-OPNSenseTailscaleConfig -AdvertiseRoutes "192.168.1.0/24", "10.0.0.0/8"
```

This example configures Tailscale to advertise specific subnet routes.

### Example 3: Configure Tailscale as an exit node
```powershell
Set-OPNSenseTailscaleConfig -ExitNode $true -ExitNodeAllowLAN $true
```

This example configures Tailscale to act as an exit node, allowing access to LAN resources.

## PARAMETERS

### -AuthKey
The Tailscale authentication key.

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

### -Hostname
The hostname to use for the Tailscale node.

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

### -AdvertiseRoutes
The subnet routes to advertise to the Tailscale network.

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

### -AcceptRoutes
Whether to accept subnet routes from other Tailscale nodes.

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

### -ExitNode
Whether to configure this node as a Tailscale exit node.

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

### -ExitNodeAllowLAN
Whether to allow access to LAN resources when acting as an exit node.

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
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The Tailscale plugin must be installed on the OPNSense firewall.
After modifying the configuration, you may need to restart Tailscale for the changes to take effect.

## RELATED LINKS

[Get-OPNSenseTailscaleConfig](Get-OPNSenseTailscaleConfig.md)
[Enable-OPNSenseTailscale](Enable-OPNSenseTailscale.md)
[Disable-OPNSenseTailscale](Disable-OPNSenseTailscale.md)
[Get-OPNSenseTailscaleStatus](Get-OPNSenseTailscaleStatus.md)
[Install-OPNSenseTailscale](Install-OPNSenseTailscale.md)
