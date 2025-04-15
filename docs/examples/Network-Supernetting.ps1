# Import the module
Import-Module PSOPNSenseAPI

# Basic supernetting of two networks
$supernet = Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Supernet -AdditionalNetworks "192.168.2.0/24"
Write-Output "Supernet of 192.168.1.0/24 and 192.168.2.0/24:"
$supernet

# Supernetting of multiple networks
$supernet = Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation Supernet -AdditionalNetworks "10.0.1.0/24","10.0.2.0/24","10.0.3.0/24"
Write-Output "Supernet of multiple /24 networks:"
$supernet

# Advanced supernetting with summarization
$summarizedNetworks = Invoke-OPNSenseNetworkCalculation -Network "172.16.0.0/24" -Operation SupernetSummarize -AdditionalNetworks "172.16.1.0/24","172.16.2.0/24","172.16.4.0/24","172.16.5.0/24"
Write-Output "Summarized networks:"
$summarizedNetworks | Format-Table

# Example of non-contiguous networks being summarized efficiently
$summarizedNetworks = Invoke-OPNSenseNetworkCalculation -Network "10.1.0.0/24" -Operation SupernetSummarize -AdditionalNetworks "10.1.1.0/24","10.1.2.0/24","10.2.0.0/24","10.2.1.0/24"
Write-Output "Summarized non-contiguous networks:"
$summarizedNetworks | Format-Table

# Example with mixed IPv4 and IPv6 networks
$summarizedNetworks = Invoke-OPNSenseNetworkCalculation -Network "192.168.0.0/24" -Operation SupernetSummarize -AdditionalNetworks "192.168.1.0/24","2001:db8::/64","2001:db8:1::/64"
Write-Output "Summarized mixed IPv4 and IPv6 networks:"
$summarizedNetworks | Format-Table

# Practical example: Summarizing a large number of networks
$networks = @(
    "10.0.0.0/24"
    "10.0.1.0/24"
    "10.0.2.0/24"
    "10.0.3.0/24"
    "10.0.4.0/24"
    "10.0.5.0/24"
    "10.0.6.0/24"
    "10.0.7.0/24"
    "10.1.0.0/24"
    "10.1.1.0/24"
    "10.2.0.0/24"
    "10.2.1.0/24"
    "172.16.0.0/24"
    "172.16.1.0/24"
    "172.16.2.0/24"
    "172.16.3.0/24"
)

$summarizedNetworks = Invoke-OPNSenseNetworkCalculation -Network $networks[0] -Operation SupernetSummarize -AdditionalNetworks $networks[1..($networks.Length-1)]
Write-Output "Summarized large set of networks:"
$summarizedNetworks | Format-Table

# Using the summarized networks for route configuration
Write-Output "Example of using summarized networks for route configuration:"
foreach ($network in $summarizedNetworks) {
    Write-Output "New-OPNSenseRoute -Network '$network' -Gateway 'WAN_GW' -Description 'Summarized Route' -Apply"
}
