# New-OPNSenseDHCPOption

## SYNOPSIS
Creates a new DHCP option on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseDHCPOption -Interface <String> -Number <Int32> -Type <String> -Value <String> [-Description <String>]
                       [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseDHCPOption cmdlet creates a new DHCP option on an OPNSense firewall. DHCP options allow you to provide additional configuration information to DHCP clients.

## EXAMPLES

### Example 1: Create a new DHCP option for TFTP server
```powershell
New-OPNSenseDHCPOption -Interface "lan" -Number 66 -Type "string" -Value "tftp.example.com" -Description "TFTP Server"
```

This example creates a new DHCP option 66 (TFTP server name) for the LAN interface.

### Example 2: Create a new DHCP option for boot filename
```powershell
New-OPNSenseDHCPOption -Interface "lan" -Number 67 -Type "string" -Value "pxelinux.0" -Description "Boot Filename"
```

This example creates a new DHCP option 67 (boot filename) for the LAN interface.

## PARAMETERS

### -Interface
The interface on which to create the DHCP option.

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

### -Number
The option number to create.

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

### -Type
The type of the DHCP option (string, integer, boolean, etc.).

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

### -Value
The value of the DHCP option.

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
A description for the DHCP option.

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
Returns an object representing the newly created DHCP option.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After creating a DHCP option, the DHCP server service may need to be restarted for the changes to take effect.
The DHCP server must be enabled on the specified interface for options to be provided to clients.

## RELATED LINKS

[Get-OPNSenseDHCPOption](Get-OPNSenseDHCPOption.md)
[Set-OPNSenseDHCPOption](Set-OPNSenseDHCPOption.md)
[Remove-OPNSenseDHCPOption](Remove-OPNSenseDHCPOption.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
