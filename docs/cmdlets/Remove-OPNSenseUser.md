# Remove-OPNSenseUser

## SYNOPSIS
Removes a user from an OPNSense firewall.

## SYNTAX

```
Remove-OPNSenseUser -Username <String> [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Remove-OPNSenseUser cmdlet removes a user from an OPNSense firewall.

## EXAMPLES

### Example 1: Remove a user
```powershell
Remove-OPNSenseUser -Username "tempuser"
```

This example removes the user with the username "tempuser" from the OPNSense firewall.

### Example 2: Remove a user with confirmation
```powershell
Remove-OPNSenseUser -Username "olduser" -Confirm
```

This example prompts for confirmation before removing the user.

## PARAMETERS

### -Username
The username of the user to remove.

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
You can pipe a string containing the username to this cmdlet.

## OUTPUTS

### System.Object
Returns an object representing the result of the operation.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
Be careful when removing users, especially the "admin" user, as this could lock you out of the firewall.
You cannot remove the currently logged-in user.

## RELATED LINKS

[Get-OPNSenseUser](Get-OPNSenseUser.md)
[New-OPNSenseUser](New-OPNSenseUser.md)
[Set-OPNSenseUser](Set-OPNSenseUser.md)
