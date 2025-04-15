# New-OPNSenseSubnetVLANs

## SYNOPSIS
Creates multiple VLANs and interfaces for a subnet on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseSubnetVLANs -Device <String> -Subnet <String> -VLANStart <Int32> -VLANCount <Int32> [-Description <String>]
                        [-DHCP <Boolean>] [-DHCPStart <Int32>] [-DHCPEnd <Int32>] [-DHCPDNSServer <String>]
                        [-DHCPDomain <String>] [-DHCPLeaseTime <String>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseSubnetVLANs cmdlet creates multiple VLANs and interfaces for a subnet on an OPNSense firewall. This is useful for quickly setting up a network with multiple VLANs.

## EXAMPLES

### Example 1: Create multiple VLANs for a subnet
```powershell
New-OPNSenseSubnetVLANs -Device "igb0" -Subnet "10.0.0.0/16" -VLANStart 100 -VLANCount 5 -Description "Department VLANs"
```

This example creates 5 VLANs starting at VLAN 100 on the igb0 interface, with subnets derived from 10.0.0.0/16.

### Example 2: Create multiple VLANs with DHCP enabled
```powershell
New-OPNSenseSubnetVLANs -Device "igb0" -Subnet "192.168.0.0/16" -VLANStart 200 -VLANCount 3 -Description "Guest VLANs" -DHCP $true -DHCPDNSServer "8.8.8.8,8.8.4.4" -DHCPDomain "example.com"
```

This example creates 3 VLANs starting at VLAN 200 on the igb0 interface, with subnets derived from 192.168.0.0/16, and DHCP enabled with custom DNS servers and domain.

## PARAMETERS

### -Device
The physical interface to create the VLANs on.

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

### -Subnet
The base subnet in CIDR notation (e.g., 10.0.0.0/16).

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

### -VLANStart
The starting VLAN tag.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VLANCount
The number of VLANs to create.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A description prefix for the VLANs.

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

### -DHCP
Whether to enable DHCP on the VLAN interfaces.

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

### -DHCPStart
The starting IP offset for the DHCP range.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 100
Accept pipeline input: False
Accept wildcard characters: False
```

### -DHCPEnd
The ending IP offset for the DHCP range.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 200
Accept pipeline input: False
Accept wildcard characters: False
```

### -DHCPDNSServer
The DNS servers for DHCP clients.

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

### -DHCPDomain
The domain name for DHCP clients.

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

### -DHCPLeaseTime
The lease time for DHCP clients.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 7200
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
Returns objects representing the created VLANs and interfaces.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
This cmdlet creates multiple VLANs and interfaces in a single operation, which can be useful for setting up a network quickly.
The subnet is divided into smaller subnets based on the number of VLANs requested.

## RELATED LINKS

[Get-OPNSenseVLAN](Get-OPNSenseVLAN.md)
[New-OPNSenseVLAN](New-OPNSenseVLAN.md)
[Set-OPNSenseVLAN](Set-OPNSenseVLAN.md)
[Remove-OPNSenseVLAN](Remove-OPNSenseVLAN.md)
