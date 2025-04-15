# Import the module
Import-Module PSOPNSenseAPI

# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all aliases
$aliases = Get-OPNSenseAlias
Write-Output "All Aliases:"
$aliases | Format-Table -Property Uuid, Name, Type, Content, Description, Enabled

# Get aliases by type
$hostAliases = Get-OPNSenseAlias -Type host
Write-Output "Host Aliases:"
$hostAliases | Format-Table -Property Uuid, Name, Content, Description

$networkAliases = Get-OPNSenseAlias -Type network
Write-Output "Network Aliases:"
$networkAliases | Format-Table -Property Uuid, Name, Content, Description

$portAliases = Get-OPNSenseAlias -Type port
Write-Output "Port Aliases:"
$portAliases | Format-Table -Property Uuid, Name, Content, Description

# Get a specific alias by name
$webServersAlias = Get-OPNSenseAlias -Name "WebServers"
Write-Output "WebServers Alias:"
$webServersAlias | Format-Table -Property Uuid, Name, Type, Content, Description

# Get a specific alias by UUID
$aliasUuid = $aliases[0].Uuid
$specificAlias = Get-OPNSenseAlias -Uuid $aliasUuid
Write-Output "Specific Alias by UUID:"
$specificAlias | Format-List

# Create a new host alias
$webServersUuid = New-OPNSenseAlias -Name "WebServers" -Type host -Content "192.168.1.10,192.168.1.11" -Description "Web Servers" -Apply
Write-Output "Created WebServers alias with UUID: $webServersUuid"

# Create a new network alias
$internalNetworksUuid = New-OPNSenseAlias -Name "InternalNetworks" -Type network -Content "192.168.1.0/24,192.168.2.0/24" -Description "Internal Networks" -Apply
Write-Output "Created InternalNetworks alias with UUID: $internalNetworksUuid"

# Create a new port alias
$webPortsUuid = New-OPNSenseAlias -Name "WebPorts" -Type port -Content "80,443" -Protocol TCP -Description "Web Ports" -Apply
Write-Output "Created WebPorts alias with UUID: $webPortsUuid"

# Create a new URL alias
$blockListUuid = New-OPNSenseAlias -Name "BlockList" -Type url -Content "https://example.com/blocklist.txt" -UpdateFrequency 1 -Description "Block List" -Apply
Write-Output "Created BlockList alias with UUID: $blockListUuid"

# Update an alias's content
Set-OPNSenseAlias -Uuid $webServersUuid -Content "192.168.1.10,192.168.1.11,192.168.1.12" -Apply
Write-Output "Updated WebServers alias content"

# Update an alias's description
Set-OPNSenseAlias -Uuid $internalNetworksUuid -Description "All Internal Networks" -Apply
Write-Output "Updated InternalNetworks alias description"

# Disable an alias
Set-OPNSenseAlias -Uuid $blockListUuid -Disabled -Apply
Write-Output "Disabled BlockList alias"

# Enable an alias
Set-OPNSenseAlias -Uuid $blockListUuid -Enabled -Apply
Write-Output "Enabled BlockList alias"

# Enable counters for an alias
Set-OPNSenseAlias -Uuid $webServersUuid -EnableCounters -Apply
Write-Output "Enabled counters for WebServers alias"

# Remove an alias
Remove-OPNSenseAlias -Uuid $blockListUuid -Force -Apply
Write-Output "Removed BlockList alias"

# Remove aliases by pipeline
Get-OPNSenseAlias -Name "WebServers" | Remove-OPNSenseAlias -Force -Apply
Write-Output "Removed WebServers alias"

# Disconnect from the firewall
Disconnect-OPNSense
