# Set-OPNSenseDNSOverride

## SYNOPSIS
Modifies an existing DNS override on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseDNSOverride -UUID <String> [-Host <String>] [-Domain <String>] [-IP <String>] [-Description <String>]
                        [-Disabled <Boolean>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseDNSOverride cmdlet modifies an existing DNS override on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a DNS override description
```powershell
Set-OPNSenseDNSOverride -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Description "Updated Server Description"
```

This example updates the description of an existing DNS override.

### Example 2: Change the IP address for a DNS override
```powershell
Set-OPNSenseDNSOverride -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -IP "192.168.1.20"
```

This example changes the IP address used for an existing DNS override.

### Example 3: Disable a DNS override
```powershell
Set-OPNSenseDNSOverride -UUID "a1b2c3d4-e5f6-7890-abcd-ef1234567890" -Disabled $true
```

This example disables an existing DNS override.

## PARAMETERS

### -UUID
The UUID of the DNS override to modify.

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

### -Host
The hostname part of the DNS override.

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

### -Domain
The domain part of the DNS override.

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
The IP address to which the hostname should resolve.

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
You can pipe a string containing the UUID of a DNS override to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the modified DNS override.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
After modifying a DNS override, you may need to apply the changes for them to take effect.

## RELATED LINKS

[Get-OPNSenseDNSOverride](Get-OPNSenseDNSOverride.md)
[New-OPNSenseDNSOverride](New-OPNSenseDNSOverride.md)
[Remove-OPNSenseDNSOverride](Remove-OPNSenseDNSOverride.md)
