# Route Management

This component provides cmdlets for managing static routes on OPNSense firewalls.

## Overview

The Route Management component allows you to configure and manage static routes on OPNSense firewalls. It provides cmdlets for creating, viewing, modifying, and deleting static routes.

## Cmdlets

### Get-OPNSenseRoute

Retrieves static routes from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of a specific route to retrieve. If not specified, all routes are returned.
- **Network** - Filter routes by network.
- **Gateway** - Filter routes by gateway.
- **Description** - Filter routes by description.

#### Examples

```powershell
# Get all static routes
Get-OPNSenseRoute

# Get a specific route by UUID
Get-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Get routes for a specific network
Get-OPNSenseRoute -Network "192.168.100.0/24"

# Get routes using a specific gateway
Get-OPNSenseRoute -Gateway "WAN_GATEWAY"
```

### New-OPNSenseRoute

Creates a new static route on an OPNSense firewall.

#### Parameters

- **Network** - The network for the route in CIDR notation.
- **Gateway** - The gateway for the route.
- **Description** - A description for the route.
- **Disabled** - Whether the route is disabled. Default is false.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a basic static route
New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN_GATEWAY" -Description "Remote Office Network"

# Create a disabled static route
New-OPNSenseRoute -Network "10.0.0.0/8" -Gateway "WAN2_GATEWAY" -Description "Corporate Network" -Disabled

# Create a static route without confirmation
New-OPNSenseRoute -Network "172.16.0.0/16" -Gateway "WAN3_GATEWAY" -Description "Partner Network" -Force
```

### Set-OPNSenseRoute

Updates an existing static route on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the route to update.
- **Network** - The network for the route in CIDR notation.
- **Gateway** - The gateway for the route.
- **Description** - A description for the route.
- **Disabled** - Whether the route is disabled.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated route.

#### Examples

```powershell
# Update a route's network
Set-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Network "192.168.200.0/24"

# Update a route's gateway
Set-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Gateway "WAN2_GATEWAY"

# Update a route's description
Set-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated Remote Office Network"

# Enable a disabled route
Set-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Disabled:$false

# Update multiple properties of a route
Set-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Network "192.168.200.0/24" -Gateway "WAN3_GATEWAY" -Description "Updated Remote Office Network" -PassThru
```

### Remove-OPNSenseRoute

Removes a static route from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the route to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a route
Remove-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a route without confirmation
Remove-OPNSenseRoute -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Apply-OPNSenseRouteChanges

Applies pending route changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply route changes
Apply-OPNSenseRouteChanges

# Apply route changes without confirmation
Apply-OPNSenseRouteChanges -Force
```

## Common Scenarios

### Basic Route Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create a static route to a remote network
New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN_GATEWAY" -Description "Remote Office Network" -Force

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing Multiple Routes

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Define routes for different networks
$routes = @(
    @{ Network = "192.168.100.0/24"; Gateway = "WAN_GATEWAY"; Description = "Remote Office 1" },
    @{ Network = "192.168.200.0/24"; Gateway = "WAN_GATEWAY"; Description = "Remote Office 2" },
    @{ Network = "10.0.0.0/8"; Gateway = "WAN2_GATEWAY"; Description = "Corporate Network" },
    @{ Network = "172.16.0.0/16"; Gateway = "WAN3_GATEWAY"; Description = "Partner Network" }
)

# Create routes
foreach ($route in $routes) {
    New-OPNSenseRoute -Network $route.Network -Gateway $route.Gateway -Description $route.Description -Force
}

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Updating Routes for a Gateway Change

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all routes using a specific gateway
$routes = Get-OPNSenseRoute -Gateway "WAN_GATEWAY"

# Update routes to use a different gateway
foreach ($route in $routes) {
    Set-OPNSenseRoute -Uuid $route.Uuid -Gateway "WAN2_GATEWAY" -Description "$($route.Description) (Updated)" -Force
    Write-Output "Updated route for network $($route.Network) to use gateway WAN2_GATEWAY"
}

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Temporarily Disabling Routes

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all routes
$routes = Get-OPNSenseRoute

# Disable routes for a specific network range
foreach ($route in $routes) {
    if ($route.Network -like "10.*") {
        Set-OPNSenseRoute -Uuid $route.Uuid -Disabled:$true -Force
        Write-Output "Disabled route for network $($route.Network)"
    }
}

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Route Cleanup

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all routes
$routes = Get-OPNSenseRoute

# Remove routes with "Temporary" in the description
foreach ($route in $routes) {
    if ($route.Description -like "*Temporary*") {
        Remove-OPNSenseRoute -Uuid $route.Uuid -Force
        Write-Output "Removed route for network $($route.Network) with description '$($route.Description)'"
    }
}

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Creating Routes from Network Calculations

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Calculate subnets
$network = "10.0.0.0/16"
$prefixLength = 24
$subnets = Invoke-OPNSenseNetworkCalculation -Network $network -Operation Subnet -PrefixLength $prefixLength

# Create routes for each subnet
foreach ($subnet in $subnets) {
    # Determine the gateway based on the subnet
    $gateway = if ($subnet.CIDR -like "10.0.1*") {
        "WAN_GATEWAY"
    } elseif ($subnet.CIDR -like "10.0.2*") {
        "WAN2_GATEWAY"
    } else {
        "WAN3_GATEWAY"
    }
    
    New-OPNSenseRoute -Network $subnet.CIDR -Gateway $gateway -Description "Subnet $($subnet.CIDR)" -Force
    Write-Output "Created route for network $($subnet.CIDR) via gateway $gateway"
}

# Apply the changes
Apply-OPNSenseRouteChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- Route changes are not applied until you call `Apply-OPNSenseRouteChanges`.
- When creating a route, ensure that the gateway is properly configured and reachable.
- Static routes take precedence over dynamic routes.
- The network should be specified in CIDR notation (e.g., "192.168.1.0/24").
- Routes can be temporarily disabled without removing them.
- Consider using the `-PassThru` parameter when updating routes to verify the changes.
- Route descriptions should be descriptive and follow a consistent naming convention.
- When managing multiple routes, consider using a CSV file or other structured data source to maintain the route information.
- Static routes are useful for directing traffic to specific networks through different gateways.
- For complex routing scenarios, consider using policy-based routing instead of static routes.
