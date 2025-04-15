# Set-OPNSenseDHCPStaticMapping

## SYNOPSIS
Modifies an existing DHCP static mapping on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDHCPStaticMapping -Interface <String> -UUID <String> [-MAC <String>] [-IP <String>] [-Hostname <String>]
                              [-Description <String>] [-DNSServers <String[]>] [-Domain <String>] [-Gateway <String>]
                              [-ARP <Boolean>] [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDHCPStaticMapping cmdlet modifies an existing DHCP static mapping on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a DHCP static mapping description
```powershell
Set-OPNSenseDHCPStaticMapping -Interface "lan" -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated Printer Description"
```

This example updates the description of an existing DHCP static mapping.

### Example 2: Change the IP address for a DHCP static mapping
```powershell
Set-OPNSenseDHCPStaticMapping -Interface "lan" -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -IP "192.168.1.55"
```

This example changes the IP address used for an existing DHCP static mapping.

### Example 3: Disable a DHCP static mapping
```powershell
Set-OPNSenseDHCPStaticMapping -Interface "lan" -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Disabled $true
```

This example disables an existing DHCP static mapping.

## PARAMETERS

### -Interface
The interface on which the DHCP static mapping exists.

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

### -UUID
The UUID of the DHCP static mapping to modify.

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

### -MAC
The MAC address of the device.

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

### -IP
The IP address to assign to the device.

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
Default value: None
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
You can pipe strings containing the interface name and UUID to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified DHCP static mapping.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a DHCP static mapping, the DHCP server service may need to be restarted for the changes to take effect.
The DHCP server must be enabled on the specified interface for static mappings to work.

## RELATED LINKS

[Get-OPNSenseDHCPStaticMapping](Get-OPNSenseDHCPStaticMapping.md)
[New-OPNSenseDHCPStaticMapping](New-OPNSenseDHCPStaticMapping.md)
[Remove-OPNSenseDHCPStaticMapping](Remove-OPNSenseDHCPStaticMapping.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
[Get-OPNSenseDHCPLease](Get-OPNSenseDHCPLease.md)
[Remove-OPNSenseDHCPLease](Remove-OPNSenseDHCPLease.md)
