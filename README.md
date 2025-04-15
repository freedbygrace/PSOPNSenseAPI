# PSOPNSenseAPI

PowerShell module for interacting with the OPNSense API to configure firewalls.

## Overview

PSOPNSenseAPI is a PowerShell module that provides cmdlets for managing OPNSense firewalls through their API. The module is built as a binary module in C# and is compatible with both PowerShell 5.1 and PowerShell 7.

## Features

- Connect to OPNSense firewalls using API credentials
- Manage firewall rules (create, read, update, delete)
- Configure NAT rules and port forwarding
- Manage aliases (host, network, port, URL, etc.)
- Manage network interfaces and VLANs
- Configure DNS settings, overrides, and forwarding
- Configure system DNS servers
- Backup, restore, export, and import configurations
- Manage plugins (install, uninstall, enable, disable)
- Manage users and permissions
- Update and upgrade firmware
- Reboot firewall with wait for reconnection
- Create VLANs from subnet divisions
- Configure DHCP servers, static mappings, leases, and options
- Manage cron jobs
- Configure and manage Tailscale VPN (with auto-installation)
- Manage gateways and static routes
- Network calculation utilities (subnet, supernet, CIDR conversion)
- Apply and revert configuration changes
- Extensive logging and error handling
- Compatible with both PowerShell 5.1 and PowerShell 7

## Requirements

- PowerShell 5.1 or PowerShell 7+
- .NET Framework 4.7.2+ (for PowerShell 5.1)
- .NET Core 3.1+ (for PowerShell 7)

## Installation

```powershell
# Install from PowerShell Gallery (when published)
Install-Module -Name PSOPNSenseAPI

# Or install manually
# 1. Download the module
# 2. Extract to a directory in your PSModulePath
# 3. Import the module
Import-Module PSOPNSenseAPI
```

## Quick Start

```powershell
# Connect to an OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get all firewall rules
Get-OPNSenseFirewallRule

# Create a new firewall rule
New-OPNSenseFirewallRule -Description "Allow HTTP" -Protocol TCP -SourceNet "192.168.1.0/24" -DestinationPort 80 -Action Pass

# Apply changes
Apply-OPNSenseFirewallChanges

# Manage aliases
Get-OPNSenseAlias
New-OPNSenseAlias -Name "WebServers" -Type host -Content "192.168.1.10,192.168.1.11" -Description "Web Servers" -Apply
New-OPNSenseAlias -Name "WebPorts" -Type port -Content "80,443" -Protocol TCP -Description "Web Ports" -Apply
Set-OPNSenseAlias -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Content "192.168.1.10,192.168.1.11,192.168.1.12" -Apply

# Manage interfaces
Get-OPNSenseInterface
Set-OPNSenseInterface -Name "lan" -Description "Local Network" -IpAddress "192.168.1.1" -SubnetMask "24"

# Create VLANs from subnet divisions
New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.0.0/24" -SubnetMaskBits 27 -StartingVlanId 10 -VlanIdIncrement 10 -EnableDHCP

# Configure DNS
Set-OPNSenseDNSServer -Forwarding -Forwarders "8.8.8.8","8.8.4.4" -Apply
New-OPNSenseDNSOverride -Hostname "server" -Domain "local" -IpAddress "192.168.1.10"

# Manage plugins
Get-OPNSensePlugin -Installed
Install-OPNSensePlugin -Name "os-acme-client" -Wait

# Manage users
New-OPNSenseUser -Username "john" -Password "P@ssw0rd" -FullName "John Doe" -Groups "admins"

# Configure DHCP
Get-OPNSenseDHCPServer
Set-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200" -Apply
New-OPNSenseDHCPStaticMapping -Interface "lan" -MacAddress "00:11:22:33:44:55" -IpAddress "192.168.1.50" -Hostname "printer"
Get-OPNSenseDHCPLease
Remove-OPNSenseDHCPLease -MacAddress "00:11:22:33:44:55" -Apply
Get-OPNSenseDHCPOption -Interface "lan"
New-OPNSenseDHCPOption -Interface "lan" -Number 66 -Value "192.168.1.10" -Description "TFTP Server" -Apply

# Manage cron jobs
Get-OPNSenseCronJob
New-OPNSenseCronJob -Description "Daily backup" -Command "/usr/local/bin/backup.sh" -Minutes "0" -Hours "2" -Apply

# Configure and manage Tailscale
Get-OPNSenseTailscaleStatus -IncludeInterfaces
Enable-OPNSenseTailscale -AcceptDns -AcceptRoutes -Force
# Advertise subnet routes to Tailscale network
Enable-OPNSenseTailscale -AdvertiseRoutes -SubnetRoutes "192.168.1.0/24","10.0.0.0/8" -Force
Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing -Force

# Manage DNS forwarding
Get-OPNSenseDNSForwarding
Set-OPNSenseDNSForwarding -Enabled -DnsServers "8.8.8.8","8.8.4.4" -Apply
New-OPNSenseDNSForwardingHost -Domain "example.com" -Server "192.168.1.10" -Apply

# Configure system DNS
Get-OPNSenseSystemDNS
Set-OPNSenseSystemDNS -Hostname "firewall" -Domain "example.com" -DnsServers "1.1.1.1","1.0.0.1" -Apply

# Manage gateways and routes
Get-OPNSenseGateway -IncludeStatus
New-OPNSenseGateway -Name "WAN2_GW" -Interface "opt1" -IpAddress "203.0.113.1" -Description "Secondary WAN" -Apply
Get-OPNSenseRoute
New-OPNSenseRoute -Network "192.168.100.0/24" -Gateway "WAN2_GW" -Description "Remote Office" -Apply

# Network utilities
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Info
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/16" -Operation Subnet -PrefixLength 24
ConvertTo-OPNSenseNetworkNotation -CIDR 24  # Returns "255.255.255.0"

# Advanced supernetting
Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Supernet -AdditionalNetworks "192.168.2.0/24"
Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/24" -Operation SupernetSummarize -AdditionalNetworks "10.0.1.0/24","10.0.2.0/24"

# Firmware management
Update-OPNSenseFirmware -Wait

# Reboot firewall
Restart-OPNSenseFirewall -Wait -Timeout 300

# Backup and restore configuration
Backup-OPNSenseConfig -Filename "pre-upgrade-backup"
Export-OPNSenseConfig -Path "C:\Backups\opnsense-config.xml"
```

## Documentation

For detailed documentation, see the [docs](./docs) directory or use PowerShell's built-in help:

```powershell
Get-Help Connect-OPNSense -Full
```

## Development

### Build and Test

To build and test the module locally:

```powershell
# Run tests
.\build\test.ps1

# Build the module
.\build\build.ps1

# Build a release version with automatic versioning
.\build\build-release.ps1 -Clean -Test -Package
```

### Versioning

The module uses a versioning scheme of `yyyy.MM.dd.HHmm` for releases, which is automatically generated during the build process.

### CI/CD

The project uses GitHub Actions for continuous integration and deployment:

- Builds and tests are run on every push to the main branch
- Release packages are automatically created with the versioning scheme
- Release artifacts are uploaded to GitHub Releases

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Run tests to ensure they pass
5. Commit your changes (`git commit -m 'Add some amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
