# Set-OPNSenseDHCPOption

## SYNOPSIS
Modifies an existing DHCP option on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDHCPOption -Interface <String> -Number <Int32> [-Type <String>] [-Value <String>] [-Description <String>]
                       [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDHCPOption cmdlet modifies an existing DHCP option on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a DHCP option value
```powershell
Set-OPNSenseDHCPOption -Interface "lan" -Number 66 -Value "newtftp.example.com"
```

This example updates the value of DHCP option 66 (TFTP server name) for the LAN interface.

### Example 2: Modify a DHCP option description
```powershell
Set-OPNSenseDHCPOption -Interface "lan" -Number 67 -Description "Updated Boot Filename"
```

This example updates the description of DHCP option 67 (boot filename) for the LAN interface.

## PARAMETERS

### -Interface
The interface on which the DHCP option exists.

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

### -Number
The option number to modify.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Type
The type of the DHCP option (string, integer, boolean, etc.).

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

### -Value
The value of the DHCP option.

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

### System.String
### System.Int32
You can pipe strings containing the interface name and integers containing the option number to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified DHCP option.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a DHCP option, the DHCP server service may need to be restarted for the changes to take effect.
The DHCP server must be enabled on the specified interface for options to be provided to clients.

## RELATED LINKS

[Get-OPNSenseDHCPOption](Get-OPNSenseDHCPOption.md)
[New-OPNSenseDHCPOption](New-OPNSenseDHCPOption.md)
[Remove-OPNSenseDHCPOption](Remove-OPNSenseDHCPOption.md)
[Get-OPNSenseDHCPServer](Get-OPNSenseDHCPServer.md)
[Set-OPNSenseDHCPServer](Set-OPNSenseDHCPServer.md)
