# DNS Management

This component provides cmdlets for managing DNS settings on OPNSense firewalls.

## Overview

The DNS Management component allows you to configure and manage DNS settings on OPNSense firewalls. It provides cmdlets for configuring DNS servers, DNS forwarders, DNS overrides, and DNS forwarding.

## Cmdlets

### Get-OPNSenseDNSServer

Retrieves DNS server configurations from an OPNSense firewall.

#### Examples

```powershell
# Get DNS server configuration
Get-OPNSenseDNSServer
```

### Set-OPNSenseDNSServer

Updates the DNS server configuration on an OPNSense firewall.

#### Parameters

- **Enabled** - Whether the DNS server is enabled.
- **ListenIPs** - The IP addresses to listen on.
- **Port** - The port to listen on. Default is 53.
- **DNSSECEnabled** - Whether DNSSEC is enabled.
- **ForwardingEnabled** - Whether DNS forwarding is enabled.
- **ForwardingServers** - The DNS servers to forward queries to.
- **CacheEnabled** - Whether DNS caching is enabled.
- **CacheSize** - The size of the DNS cache in MB.
- **PrefetchEnabled** - Whether DNS prefetching is enabled.
- **PrefetchDomains** - Whether to prefetch domains.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DNS server configuration.

#### Examples

```powershell
# Enable the DNS server
Set-OPNSenseDNSServer -Enabled

# Configure DNS forwarding
Set-OPNSenseDNSServer -ForwardingEnabled -ForwardingServers "8.8.8.8","8.8.4.4"

# Configure DNS caching
Set-OPNSenseDNSServer -CacheEnabled -CacheSize 10 -PrefetchEnabled -PrefetchDomains

# Configure DNS server with multiple options
Set-OPNSenseDNSServer -Enabled -ListenIPs "192.168.1.1" -Port 53 -DNSSECEnabled -ForwardingEnabled -ForwardingServers "1.1.1.1","1.0.0.1" -CacheEnabled -CacheSize 20 -PrefetchEnabled -PassThru
```

### Get-OPNSenseDNSOverride

Retrieves DNS overrides from an OPNSense firewall.

#### Parameters

- **Host** - Filter overrides by host.
- **Domain** - Filter overrides by domain.
- **IP** - Filter overrides by IP address.
- **Description** - Filter overrides by description.

#### Examples

```powershell
# Get all DNS overrides
Get-OPNSenseDNSOverride

# Get DNS overrides for a specific host
Get-OPNSenseDNSOverride -Host "server"

# Get DNS overrides for a specific domain
Get-OPNSenseDNSOverride -Domain "example.com"
```

### New-OPNSenseDNSOverride

Creates a new DNS override on an OPNSense firewall.

#### Parameters

- **Host** - The hostname to override.
- **Domain** - The domain to override.
- **IP** - The IP address to resolve to.
- **Description** - A description for the override.
- **Enabled** - Whether the override is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a DNS override for a specific host
New-OPNSenseDNSOverride -Host "server" -Domain "example.com" -IP "192.168.1.10" -Description "Internal Server"

# Create a DNS override for a wildcard domain
New-OPNSenseDNSOverride -Host "*" -Domain "example.com" -IP "192.168.1.20" -Description "All example.com hosts"

# Create a DNS override without confirmation
New-OPNSenseDNSOverride -Host "printer" -Domain "example.com" -IP "192.168.1.30" -Description "Office Printer" -Force
```

### Set-OPNSenseDNSOverride

Updates an existing DNS override on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the DNS override to update.
- **Host** - The hostname to override.
- **Domain** - The domain to override.
- **IP** - The IP address to resolve to.
- **Description** - A description for the override.
- **Enabled** - Whether the override is enabled.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DNS override.

#### Examples

```powershell
# Update a DNS override's IP address
Set-OPNSenseDNSOverride -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -IP "192.168.1.15"

# Update a DNS override's description
Set-OPNSenseDNSOverride -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated Server"

# Update multiple properties of a DNS override
Set-OPNSenseDNSOverride -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Host "newserver" -Domain "example.com" -IP "192.168.1.20" -Description "New Server" -PassThru
```

### Remove-OPNSenseDNSOverride

Removes a DNS override from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the DNS override to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a DNS override
Remove-OPNSenseDNSOverride -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a DNS override without confirmation
Remove-OPNSenseDNSOverride -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Get-OPNSenseDNSForwarder

Retrieves DNS forwarder configurations from an OPNSense firewall.

#### Examples

```powershell
# Get DNS forwarder configuration
Get-OPNSenseDNSForwarder
```

### Set-OPNSenseDNSForwarder

Updates the DNS forwarder configuration on an OPNSense firewall.

#### Parameters

- **Enabled** - Whether the DNS forwarder is enabled.
- **ListenIPs** - The IP addresses to listen on.
- **Port** - The port to listen on. Default is 53.
- **Interfaces** - The interfaces to listen on.
- **DNSSECEnabled** - Whether DNSSEC is enabled.
- **RegdhcpStatic** - Whether to register DHCP static mappings.
- **RegdhcpDynamic** - Whether to register DHCP leases.
- **StrictBind** - Whether to use strict binding.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DNS forwarder configuration.

#### Examples

```powershell
# Enable the DNS forwarder
Set-OPNSenseDNSForwarder -Enabled

# Configure DNS forwarder interfaces
Set-OPNSenseDNSForwarder -Interfaces "lan","opt1"

# Configure DNS forwarder with DHCP registration
Set-OPNSenseDNSForwarder -RegdhcpStatic -RegdhcpDynamic

# Configure DNS forwarder with multiple options
Set-OPNSenseDNSForwarder -Enabled -ListenIPs "192.168.1.1" -Port 53 -Interfaces "lan","opt1" -DNSSECEnabled -RegdhcpStatic -RegdhcpDynamic -StrictBind -PassThru
```

### Get-OPNSenseDNSForwarding

Retrieves DNS forwarding configurations from an OPNSense firewall.

#### Parameters

- **Domain** - Filter forwarding by domain.
- **Server** - Filter forwarding by server.
- **Description** - Filter forwarding by description.

#### Examples

```powershell
# Get all DNS forwarding configurations
Get-OPNSenseDNSForwarding

# Get DNS forwarding for a specific domain
Get-OPNSenseDNSForwarding -Domain "example.com"
```

### New-OPNSenseDNSForwarding

Creates a new DNS forwarding configuration on an OPNSense firewall.

#### Parameters

- **Domain** - The domain to forward.
- **Server** - The server to forward to.
- **Description** - A description for the forwarding.
- **Enabled** - Whether the forwarding is enabled. Default is true.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Create a DNS forwarding for a specific domain
New-OPNSenseDNSForwarding -Domain "example.com" -Server "192.168.1.10" -Description "Internal DNS"

# Create a DNS forwarding for a domain to multiple servers
New-OPNSenseDNSForwarding -Domain "example.org" -Server "192.168.1.10,192.168.1.11" -Description "Redundant DNS"

# Create a DNS forwarding without confirmation
New-OPNSenseDNSForwarding -Domain "example.net" -Server "192.168.1.12" -Description "External DNS" -Force
```

### Set-OPNSenseDNSForwarding

Updates an existing DNS forwarding configuration on an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the DNS forwarding to update.
- **Domain** - The domain to forward.
- **Server** - The server to forward to.
- **Description** - A description for the forwarding.
- **Enabled** - Whether the forwarding is enabled.
- **Force** - Suppresses the confirmation prompt.
- **PassThru** - Returns the updated DNS forwarding.

#### Examples

```powershell
# Update a DNS forwarding's server
Set-OPNSenseDNSForwarding -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Server "192.168.1.15"

# Update a DNS forwarding's description
Set-OPNSenseDNSForwarding -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Description "Updated DNS"

# Update multiple properties of a DNS forwarding
Set-OPNSenseDNSForwarding -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Domain "new.example.com" -Server "192.168.1.20" -Description "New DNS" -PassThru
```

### Remove-OPNSenseDNSForwarding

Removes a DNS forwarding configuration from an OPNSense firewall.

#### Parameters

- **Uuid** - The UUID of the DNS forwarding to remove.
- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Remove a DNS forwarding
Remove-OPNSenseDNSForwarding -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"

# Remove a DNS forwarding without confirmation
Remove-OPNSenseDNSForwarding -Uuid "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6" -Force
```

### Apply-OPNSenseDNSChanges

Applies pending DNS changes on an OPNSense firewall.

#### Parameters

- **Force** - Suppresses the confirmation prompt.

#### Examples

```powershell
# Apply DNS changes
Apply-OPNSenseDNSChanges

# Apply DNS changes without confirmation
Apply-OPNSenseDNSChanges -Force
```

## Common Scenarios

### Basic DNS Configuration

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Configure the DNS server
Set-OPNSenseDNSServer -Enabled -ListenIPs "192.168.1.1" -ForwardingEnabled -ForwardingServers "1.1.1.1","1.0.0.1" -CacheEnabled -CacheSize 10 -Force

# Apply the changes
Apply-OPNSenseDNSChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### DNS Overrides for Internal Services

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Create DNS overrides for internal services
$dnsOverrides = @(
    @{ Host = "www"; Domain = "example.com"; IP = "192.168.1.10"; Description = "Internal Web Server" },
    @{ Host = "mail"; Domain = "example.com"; IP = "192.168.1.11"; Description = "Internal Mail Server" },
    @{ Host = "files"; Domain = "example.com"; IP = "192.168.1.12"; Description = "Internal File Server" },
    @{ Host = "*"; Domain = "internal.example.com"; IP = "192.168.1.13"; Description = "Internal Services" }
)

foreach ($override in $dnsOverrides) {
    New-OPNSenseDNSOverride -Host $override.Host -Domain $override.Domain -IP $override.IP -Description $override.Description -Force
}

# Apply the changes
Apply-OPNSenseDNSChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### DNS Forwarding for Specific Domains

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Configure DNS forwarding for specific domains
$dnsForwarding = @(
    @{ Domain = "example.com"; Server = "192.168.1.10"; Description = "Internal Domain" },
    @{ Domain = "partner.com"; Server = "10.0.0.10"; Description = "Partner Domain" },
    @{ Domain = "vendor.com"; Server = "172.16.0.10"; Description = "Vendor Domain" }
)

foreach ($forwarding in $dnsForwarding) {
    New-OPNSenseDNSForwarding -Domain $forwarding.Domain -Server $forwarding.Server -Description $forwarding.Description -Force
}

# Apply the changes
Apply-OPNSenseDNSChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

### Managing DNS Overrides

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all DNS overrides
$overrides = Get-OPNSenseDNSOverride

# Update all overrides for a specific domain
$overrides | Where-Object { $_.Domain -eq "example.com" } | ForEach-Object {
    Set-OPNSenseDNSOverride -Uuid $_.Uuid -IP "192.168.2.$($_.IP.Split('.')[-1])" -Description "$($_.Description) (Migrated)" -Force
    Write-Output "Updated override: $($_.Host).$($_.Domain)"
}

# Remove all overrides with "Temporary" in the description
$overrides | Where-Object { $_.Description -like "*Temporary*" } | ForEach-Object {
    Remove-OPNSenseDNSOverride -Uuid $_.Uuid -Force
    Write-Output "Removed override: $($_.Host).$($_.Domain)"
}

# Apply the changes
Apply-OPNSenseDNSChanges -Force

# Disconnect from the firewall
Disconnect-OPNSense
```

## Notes

- DNS changes are not applied until you call `Apply-OPNSenseDNSChanges`.
- DNS overrides take precedence over DNS forwarding.
- When using DNS forwarding, ensure that the forwarded domains are accessible from the OPNSense firewall.
- DNS caching can improve performance by reducing the number of external DNS queries.
- DNSSEC provides additional security by validating DNS responses.
- Consider using the `-PassThru` parameter when updating DNS configurations to verify the changes.
- Wildcard DNS overrides (using "*" as the host) can be used to override all subdomains of a domain.
- DNS forwarding can be used to direct queries for specific domains to internal or external DNS servers.
