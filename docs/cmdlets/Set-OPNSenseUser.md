# Set-OPNSenseUser

## SYNOPSIS
Modifies an existing user on an OPNSense firewall.

## SYNTAX

```
Set-OPNSenseUser -Username <String> [-Password <SecureString>] [-FullName <String>] [-Email <String>] [-Comment <String>]
                 [-Disabled <Boolean>] [-ExpireDate <DateTime>] [-AuthGroup <String[]>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Set-OPNSenseUser cmdlet modifies an existing user on an OPNSense firewall.

## EXAMPLES

### Example 1: Modify a user's email address
```powershell
Set-OPNSenseUser -Username "existinguser" -Email "newemail@example.com"
```

This example updates the email address for the user "existinguser".

### Example 2: Change a user's password
```powershell
$newPassword = ConvertTo-SecureString "NewP@ssw0rd" -AsPlainText -Force
Set-OPNSenseUser -Username "existinguser" -Password $newPassword
```

This example changes the password for the user "existinguser".

### Example 3: Disable a user account
```powershell
Set-OPNSenseUser -Username "existinguser" -Disabled $true
```

This example disables the account for the user "existinguser".

### Example 4: Update a user's authorization groups
```powershell
Set-OPNSenseUser -Username "existinguser" -AuthGroup "users", "vpnusers"
```

This example updates the authorization groups for the user "existinguser".

## PARAMETERS

### -Username
The username of the user to modify.

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

### -Password
The new password for the user as a SecureString.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FullName
The full name of the user.

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

### -Email
The email address of the user.

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

### -Comment
A comment or description for the user.

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
Whether the user account is disabled.

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

### -ExpireDate
The date when the user account will expire.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthGroup
The authorization groups to which the user belongs.

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
Returns an object representing the modified user.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
For security reasons, it is recommended to use a SecureString for the password parameter.
Be careful when modifying the "admin" user, as this could lock you out of the firewall.

## RELATED LINKS

[Get-OPNSenseUser](Get-OPNSenseUser.md)
[New-OPNSenseUser](New-OPNSenseUser.md)
[Remove-OPNSenseUser](Remove-OPNSenseUser.md)
