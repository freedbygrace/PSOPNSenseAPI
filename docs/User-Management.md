# User Management

This component provides cmdlets for managing users on OPNSense firewalls.

## Overview

The User Management component allows you to create, view, modify, and delete users on OPNSense firewalls. It provides cmdlets for managing user accounts, privileges, and API keys.

## Cmdlets

### Get-OPNSenseUser

Retrieves users from an OPNSense firewall.

#### Parameters

- **Username** - The username of a specific user to retrieve. If not specified, all users are returned.
- **Uid** - The UID of a specific user to retrieve.

#### Examples

```powershell
# Get all users
Get-OPNSenseUser

# Get a specific user by username
Get-OPNSenseUser -Username "admin"

# Get a specific user by UID
Get-OPNSenseUser -Uid 1000
```

### New-OPNSenseUser

Creates a new user on an OPNSense firewall.

#### Parameters

- **Username** - The username for the new user.
- **Password** - The password for the new user.
- **FullName** - The full name of the user.
- **Email** - The email address of the user.
- **Authorized** - Whether the user is authorized to log in. Default is true.
- **Privileges** - The privileges to assign to the user.
- **Group** - The group to assign the user to.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic user
New-OPNSenseUser -Username "john" -Password "SecurePassword123" -FullName "John Doe" -Email "john@example.com"

# Create a user with specific privileges
New-OPNSenseUser -Username "jane" -Password "SecurePassword456" -FullName "Jane Smith" -Email "jane@example.com" -Privileges "page-system-access", "page-diagnostics-logs"

# Create a user in a specific group
New-OPNSenseUser -Username "bob" -Password "SecurePassword789" -FullName "Bob Johnson" -Email "bob@example.com" -Group "admins"

# Create a user without confirmation
New-OPNSenseUser -Username "alice" -Password "SecurePassword101" -FullName "Alice Brown" -Email "alice@example.com" -Force
```

### Set-OPNSenseUser

Updates an existing user on an OPNSense firewall.

#### Parameters

- **Username** - The username of the user to update.
- **Password** - The new password for the user.
- **FullName** - The new full name of the user.
- **Email** - The new email address of the user.
- **Authorized** - Whether the user is authorized to log in.
- **Privileges** - The new privileges to assign to the user.
- **Group** - The new group to assign the user to.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated user.

#### Examples

```powershell
# Update a user's email address
Set-OPNSenseUser -Username "john" -Email "john.doe@example.com"

# Update a user's password
Set-OPNSenseUser -Username "jane" -Password "NewSecurePassword456"

# Update a user's privileges
Set-OPNSenseUser -Username "bob" -Privileges "page-system-access", "page-diagnostics-logs", "page-firewall-rules"

# Update a user's group
Set-OPNSenseUser -Username "alice" -Group "operators"

# Disable a user
Set-OPNSenseUser -Username "temp" -Authorized:$false

# Update multiple properties of a user
Set-OPNSenseUser -Username "john" -FullName "John A. Doe" -Email "john.a.doe@example.com" -Privileges "page-system-access", "page-diagnostics-logs" -PassThru
```

### Remove-OPNSenseUser

Removes a user from an OPNSense firewall.

#### Parameters

- **Username** - The username of the user to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a user
Remove-OPNSenseUser -Username "temp"

# Remove a user without confirmation
Remove-OPNSenseUser -Username "guest" -Force
```

### Get-OPNSenseUserGroup

Retrieves user groups from an OPNSense firewall.

#### Parameters

- **Name** - The name of a specific group to retrieve. If not specified, all groups are returned.

#### Examples

```powershell
# Get all user groups
Get-OPNSenseUserGroup

# Get a specific user group by name
Get-OPNSenseUserGroup -Name "admins"
```

### New-OPNSenseUserGroup

Creates a new user group on an OPNSense firewall.

#### Parameters

- **Name** - The name of the new group.
- **Description** - A description for the group.
- **Privileges** - The privileges to assign to the group.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic user group
New-OPNSenseUserGroup -Name "operators" -Description "System Operators"

# Create a user group with specific privileges
New-OPNSenseUserGroup -Name "firewall-admins" -Description "Firewall Administrators" -Privileges "page-firewall-rules", "page-firewall-nat"

# Create a user group without confirmation
New-OPNSenseUserGroup -Name "readonly" -Description "Read-Only Users" -Privileges "page-system-access" -Force
```

### Set-OPNSenseUserGroup

Updates an existing user group on an OPNSense firewall.

#### Parameters

- **Name** - The name of the group to update.
- **Description** - The new description for the group.
- **Privileges** - The new privileges to assign to the group.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated group.

#### Examples

```powershell
# Update a user group's description
Set-OPNSenseUserGroup -Name "operators" -Description "System Operators with Limited Access"

# Update a user group's privileges
Set-OPNSenseUserGroup -Name "firewall-admins" -Privileges "page-firewall-rules", "page-firewall-nat", "page-firewall-aliases"

# Update multiple properties of a user group
Set-OPNSenseUserGroup -Name "readonly" -Description "Read-Only Access Users" -Privileges "page-system-access", "page-diagnostics-logs" -PassThru
```

### Remove-OPNSenseUserGroup

Removes a user group from an OPNSense firewall.

#### Parameters

- **Name** - The name of the group to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a user group
Remove-OPNSenseUserGroup -Name "temp-group"

# Remove a user group without confirmation
Remove-OPNSenseUserGroup -Name "guest-group" -Force
```

### Get-OPNSenseApiKey

Retrieves API keys for a user on an OPNSense firewall.

#### Parameters

- **Username** - The username to get API keys for.

#### Examples

```powershell
# Get API keys for a user
Get-OPNSenseApiKey -Username "admin"
```

### New-OPNSenseApiKey

Creates a new API key for a user on an OPNSense firewall.

#### Parameters

- **Username** - The username to create an API key for.
- **Description** - A description for the API key.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create an API key for a user
New-OPNSenseApiKey -Username "admin" -Description "Automation API Key"

# Create an API key without confirmation
New-OPNSenseApiKey -Username "john" -Description "Monitoring API Key" -Force
```

### Remove-OPNSenseApiKey

Removes an API key from an OPNSense firewall.

#### Parameters

- **KeyId** - The ID of the API key to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove an API key
Remove-OPNSenseApiKey -KeyId "abcdef123456"

# Remove an API key without confirmation
Remove-OPNSenseApiKey -KeyId "abcdef123456" -Force
```

## Common Scenarios

### Creating a New Administrator User

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a new administrator user
New-OPNSenseUser -Username "admin2" -Password "SecurePassword123" -FullName "Secondary Admin" -Email "admin2@example.com" -Group "admins" -Force

# Create an API key for the new user
New-OPNSenseApiKey -Username "admin2" -Description "Admin API Key" -Force

# Get the API key details
$apiKeys = Get-OPNSenseApiKey -Username "admin2"
Write-Output "API Key created for admin2:"
$apiKeys | Format-Table -Property KeyId, Secret, Description, Created

# Disconnect from the firewall
Disconnect-OPNSense
```

### Creating Users with Different Privilege Levels

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create user groups with different privilege levels
New-OPNSenseUserGroup -Name "firewall-admins" -Description "Firewall Administrators" -Privileges "page-firewall-rules", "page-firewall-nat", "page-firewall-aliases" -Force
New-OPNSenseUserGroup -Name "network-admins" -Description "Network Administrators" -Privileges "page-interfaces", "page-routing", "page-diagnostics-interface" -Force
New-OPNSenseUserGroup -Name "monitoring" -Description "Monitoring Users" -Privileges "page-diagnostics-logs", "page-diagnostics-system", "page-status-dashboard" -Force

# Create users in different groups
$users = @(
    @{ Username = "firewall"; FullName = "Firewall Admin"; Email = "firewall@example.com"; Group = "firewall-admins" },
    @{ Username = "network"; FullName = "Network Admin"; Email = "network@example.com"; Group = "network-admins" },
    @{ Username = "monitor"; FullName = "Monitoring User"; Email = "monitor@example.com"; Group = "monitoring" }
)

foreach ($user in $users) {
    # Generate a secure password
    $password = -join ((65..90) + (97..122) + (48..57) | Get-Random -Count 16 | ForEach-Object { [char]$_ })
    
    # Create the user
    New-OPNSenseUser -Username $user.Username -Password $password -FullName $user.FullName -Email $user.Email -Group $user.Group -Force
    
    Write-Output "Created user: $($user.Username) with password: $password"
}

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing API Keys

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all users
$users = Get-OPNSenseUser

# Create API keys for users without them
foreach ($user in $users) {
    $apiKeys = Get-OPNSenseApiKey -Username $user.Username
    
    if (-not $apiKeys) {
        New-OPNSenseApiKey -Username $user.Username -Description "Automated API Key" -Force
        Write-Output "Created API key for user: $($user.Username)"
    } else {
        Write-Output "User $($user.Username) already has $($apiKeys.Count) API key(s)"
    }
}

# Disconnect from the firewall
Disconnect-OPNSense
```

### Cleaning Up Inactive Users

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all users
$users = Get-OPNSenseUser

# Define a cutoff date (e.g., 90 days ago)
$cutoffDate = (Get-Date).AddDays(-90)

# Disable inactive users
foreach ($user in $users) {
    # Skip the admin user
    if ($user.Username -eq "admin") {
        continue
    }
    
    # Check if the user has been inactive
    if ($user.LastLogin -and [DateTime]::Parse($user.LastLogin) -lt $cutoffDate) {
        Set-OPNSenseUser -Username $user.Username -Authorized:$false -Force
        Write-Output "Disabled inactive user: $($user.Username) (Last login: $($user.LastLogin))"
    }
}

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- User management requires administrative privileges.
- The `admin` user cannot be removed.
- API keys provide access to the OPNSense API and should be kept secure.
- User privileges determine what actions a user can perform in the OPNSense web interface and API.
- User groups provide a way to assign the same privileges to multiple users.
- Passwords should be strong and meet the OPNSense password policy requirements.
- When a user is removed, all associated API keys are also removed.
- API keys can be used to authenticate with the OPNSense API without a username and password.
- Consider using the `-Force` parameter when managing users in scripts to avoid confirmation prompts.
- User accounts can be disabled by setting the `Authorized` parameter to `$false`.
- The `PassThru` parameter can be used to return the updated user or group for further processing.
- User privileges are typically specified as page identifiers (e.g., "page-system-access").
- The available privileges can be found in the OPNSense web interface under System > Access > Privileges.
- User groups can be used to organize users and assign privileges more efficiently.
- API keys can have descriptions to help identify their purpose.
- When creating API keys, both the key ID and secret are returned, but the secret is only shown once.
