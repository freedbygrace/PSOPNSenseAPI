# New-OPNSenseUser

## SYNOPSIS
Creates a new user on an OPNSense firewall.

## SYNTAX

```
New-OPNSenseUser -Username <String> -Password <SecureString> [-FullName <String>] [-Email <String>] [-Comment <String>]
                 [-Disabled <Boolean>] [-ExpireDate <DateTime>] [-AuthGroup <String[]>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The New-OPNSenseUser cmdlet creates a new user on an OPNSense firewall.

## EXAMPLES

### Example 1: Create a new user
```powershell
$password = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
New-OPNSenseUser -Username "newuser" -Password $password -FullName "New User" -Email "newuser@example.com"
```

This example creates a new user with the specified username, password, full name, and email address.

### Example 2: Create a new user with an expiration date
```powershell
$password = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
$expireDate = (Get-Date).AddMonths(3)
New-OPNSenseUser -Username "tempuser" -Password $password -FullName "Temporary User" -ExpireDate $expireDate -Comment "Temporary access"
```

This example creates a new user that will expire in 3 months.

### Example 3: Create a new user with specific authorization groups
```powershell
$password = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
New-OPNSenseUser -Username "adminuser" -Password $password -FullName "Admin User" -AuthGroup "admins", "vpnusers"
```

This example creates a new user and assigns them to the "admins" and "vpnusers" authorization groups.

## PARAMETERS

### -Username
The username for the new user.

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

### -Password
The password for the new user as a SecureString.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: True
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
Default value: False
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

### None

## OUTPUTS

### System.Object
Returns an object representing the newly created user.

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
For security reasons, it is recommended to use a SecureString for the password parameter.

## RELATED LINKS

[Get-OPNSenseUser](Get-OPNSenseUser.md)
[Set-OPNSenseUser](Set-OPNSenseUser.md)
[Remove-OPNSenseUser](Remove-OPNSenseUser.md)
