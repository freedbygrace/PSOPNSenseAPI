# Changelog

All notable changes to the PSOPNSenseAPI module will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2025.04.15.1143] - 2025-04-15

### Added
- Added `Get-OPNSenseConfig` cmdlet that retrieves the OPNSense configuration as an XML document

### Changed
- Renamed `Upgrade-OPNSenseFirmware` to `Start-OPNSenseFirmwareUpgrade` to resolve cmdlet name conflict
- Updated `Restore-OPNSenseConfig` to accept an XML document from the pipeline or as a parameter
- Changed `Export-OPNSenseConfig` and `Import-OPNSenseConfig` to use System.IO.FileInfo for the Path parameter
- Added unit tests for firmware management cmdlets

### Removed
- Removed `Backup-OPNSenseConfig` cmdlet (use `Get-OPNSenseConfig` instead)

## [0.6.0] - 2025-04-14

### Added
- Port forwarding management (Get, New, Set, Remove)
- Added unit tests for interface management

### Changed
- Updated IPNetwork2 library from version 2.6.618 to 3.1.764
- Updated code to use the new namespace (System.Net) for IPNetwork2
- Added support for .NET 8.0 and .NET 9.0
- Improved XML documentation for all classes and methods
- Made OPNSenseApiClient more testable by introducing an interface

## [0.5.0] - 2025-04-16

### Added
- Tailscale VPN management (Get, Enable, Disable, Connect, Disconnect)
- Auto-installation of Tailscale plugin if not present
- Support for configuring Tailscale settings
- Subnet route advertising for Tailscale networks

## [0.4.0] - 2025-04-15

### Added
- DHCP server management (Get, Set)
- DHCP static mapping management (Get, New, Set, Remove)
- Cron job management (Get, New, Set, Remove, Enable, Disable)
- Improved credential handling in Restart-OPNSenseFirewall
- Switched from IPNetwork to IPNetwork2 for better subnet handling
- Implemented DHCP configuration in New-OPNSenseSubnetVLANs

## [0.3.0] - 2025-04-14

### Added
- Plugin management (Get, Enable, Disable, Install, Uninstall)
- User management (Get, New, Set, Remove)
- Firmware management (Get, Update, Upgrade)
- Firewall reboot functionality with wait for reconnection
- Subnet division and VLAN creation utility
- Completed all firewall rule, alias, and NAT rule management cmdlets

## [0.2.0] - 2025-04-14

### Added
- Interface management (Get, Set, Restart, Statistics)
- VLAN management (Get, New, Set, Remove)
- DNS configuration (Servers, Domains, Overrides)
- Configuration backup and restore functionality

## [0.1.0] - 2025-04-14

### Added
- Initial release
- Basic connection management (Connect-OPNSense, Disconnect-OPNSense)
- Firewall rule management (Get, New, Set, Remove, Enable, Disable)
- Alias management (Get, New, Set, Remove)
- NAT rule management (Get, New, Set, Remove)
- Comprehensive logging system
- Error handling and retry logic
- Support for both PowerShell 5.1 and PowerShell 7
