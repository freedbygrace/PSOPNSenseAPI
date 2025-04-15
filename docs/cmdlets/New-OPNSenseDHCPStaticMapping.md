# New-OPNSenseDHCPStaticMapping

## SYNOPSIS
Creates a new DHCP static mapping on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseDHCPStaticMapping -Interface <String> -MAC <String> -IP <String> [-Hostname <String>] [-Description <String>]
                              [-DNSServers <String[]>] [-Domain <String>] [-Gateway <String>] [-ARP <Boolean>]
                              [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseDHCPStaticMapping cmdlet creates a new DHCP static mapping on an OPNSense firewall. This allows you to assign a specific IP address to a device based on its MAC address.

## EXAMPLES

### Example 1: Create a new DHCP static mapping
```powershell
New-OPNSenseDHCPStaticMapping -Interface "lan" -MAC "00:11:22:33:44:55" -IP "192.168.1.50" -Hostname "printer" -Description "Office Printer"
```

This example creates a new DHCP static mapping for a device with the specified MAC address on the LAN interface.

### Example 2: Create a new DHCP static mapping with custom DNS settings
```powershell
New-OPNSenseDHCPStaticMapping -Interface "lan" -MAC "AA:BB:CC:DD:EE:FF" -IP "192.168.1.60" -Hostname "server" -Description "File Server" -DNSServers "192.168.1.1", "8.8.8.8" -Domain "example.local"
```

This example creates a new DHCP static mapping with custom DNS servers and domain.

## PARAMETERS

### -Interface
The interface on which to create the DHCP static mapping.

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

### -MAC
The MAC address of the device.

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
The IP address to assign to the device.

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

### -Hostname
The hostname to assign to the device.

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
A description for the DHCP static mapping.

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
The DNS servers to provide to the device.

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
The domain name to provide to the device.

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
The gateway to provide to the device.

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

### -ARP
Whether to create an ARP table entry for this device.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -Disabled
Whether the DHCP static mapping is disabled.

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
Returns an object representing the newly created DHCP static mapping.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a DHCP static mapping, the DHCP server service may need to be restarted for the changes to take effect.
The DHCP server must be enabled on the specified interface for static mappings to work.

## RELATED LINKS

[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[Set-OPNSenseDHCPStaticMapping](Set-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
