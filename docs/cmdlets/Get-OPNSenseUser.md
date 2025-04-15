# Get-OPNSenseUser

## SYNOPSIS
Gets users from an OPNSense firewall.

## SYNTAX

```
Get-OPNSenseUser [[-Username] <String>]
```

## DESCRIPTION
The Get-OPNSenseUser cmdlet retrieves users from an OPNSense firewall. You can retrieve all users or a specific user by username.

## EXAMPLES

### Example 1: Get all users
```powershell
Get-OPNSenseUser
```

This example retrieves all users from the OPNSense firewall.

### Example 2: Get a specific user
```powershell
Get-OPNSenseUser -Username "admin"
```

This example retrieves the user with the username "admin" from the OPNSense firewall.

## PARAMETERS

### -Username
The username of the user to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
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
Returns objects representing the users.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
For security reasons, user passwords are not included in the output.

## RELATED LINKS

[New-OPNSenseUser](New-OPNSenseUser.md)
[Set-OPNSenseUser](Set-OPNSenseUser.md)
[Remove-OPNSenseUser](Remove-OPNSenseUser.md)
