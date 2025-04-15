# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all users
$users = Get-OPNSenseUser
$users | Format-Table -Property Uuid, Username, FullName, Email, Disabled

# Create a new user
$userId = New-OPNSenseUser -Username "john" -Password "P@ssw0rd" -FullName "John Doe" -Email "john@example.com" -Groups "admins"
Write-Output "Created user with ID: $userId"

# Update a user
Set-OPNSenseUser -Uuid $userId -Email "john.doe@example.com" -FullName "John A. Doe"

# Disable a user
Set-OPNSenseUser -Uuid $userId -Disabled

# Enable a user
Set-OPNSenseUser -Uuid $userId -Enabled

# Remove a user
Remove-OPNSenseUser -Uuid $userId -Confirm:$false

# Disconnect from the firewall
Disconnect-OPNSense
