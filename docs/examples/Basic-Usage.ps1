# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the current connection information
Get-OPNSenseConnection

# Get all firewall rules
$rules = Get-OPNSenseFirewallRule
$rules | Format-Table -Property Uuid, Description, Protocol, SourceNet, DestinationPort, Action

# Create a new firewall rule to allow HTTP traffic
$ruleId = New-OPNSenseFirewallRule -Description "Allow HTTP" -Protocol TCP -DestinationPort 80 -Action pass
Write-Output "Created rule with ID: $ruleId"

# Apply the changes
Apply-OPNSenseFirewallChanges -CancelRollback -Timeout 30

# Disconnect from the firewall
Disconnect-OPNSense
