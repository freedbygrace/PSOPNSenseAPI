# Import the module
Import-Module PSOPNSenseAPI

# Network information
$networkInfo = Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Info
Write-Output "Network Information:"
$networkInfo | Format-List

# Subnet a network into smaller networks
$subnets = Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/16" -Operation Subnet -PrefixLength 24
Write-Output "First 5 subnets of 10.0.0.0/16 divided into /24 networks:"
$subnets | Select-Object -First 5 | Format-Table

# Subnet a network into a specific number of subnets
$subnets = Invoke-OPNSenseNetworkCalculation -Network "172.16.0.0/16" -Operation SubnetByCount -SubnetCount 4
Write-Output "172.16.0.0/16 divided into 4 equal subnets:"
$subnets | Format-Table

# Check if an IP address is within a network
$contains = Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Contains -IPAddress "192.168.1.100"
Write-Output "Is 192.168.1.100 within 192.168.1.0/24? $contains"

# Check if networks overlap
$overlaps = Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation Overlaps -AdditionalNetworks "10.0.0.128/25"
Write-Output "Network overlap check:"
$overlaps | Format-Table

# Create a supernet from multiple networks
$supernet = Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Supernet -AdditionalNetworks "192.168.2.0/24","192.168.3.0/24"
Write-Output "Supernet of 192.168.1.0/24, 192.168.2.0/24, and 192.168.3.0/24:"
$supernet

# Convert between CIDR and subnet mask
$subnetMask = ConvertTo-OPNSenseNetworkNotation -CIDR 24
Write-Output "Subnet mask for /24: $subnetMask"

$cidr = ConvertTo-OPNSenseNetworkNotation -SubnetMask "255.255.255.0"
Write-Output "CIDR for 255.255.255.0: $cidr"

$ipWithSubnetMask = ConvertTo-OPNSenseNetworkNotation -IPWithCIDR "192.168.1.0/24"
Write-Output "IP with subnet mask for 192.168.1.0/24: $ipWithSubnetMask"

$ipWithCIDR = ConvertTo-OPNSenseNetworkNotation -IPWithSubnetMask "192.168.1.0 255.255.255.0"
Write-Output "IP with CIDR for 192.168.1.0 255.255.255.0: $ipWithCIDR"
