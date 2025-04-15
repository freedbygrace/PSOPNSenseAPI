# Release v2025.04.14.2340

## What's New

### Added
- Port forwarding management (Get, New, Set, Remove)
- Added unit tests for interface management
- Improved XML documentation for all classes and methods

### Changed
- Updated IPNetwork2 library from version 2.6.618 to 3.1.764
- Updated code to use the new namespace (System.Net) for IPNetwork2
- Added support for .NET 8.0 and .NET 9.0
- Made OPNSenseApiClient more testable by introducing an interface
- Fixed warnings in test project
- Updated version format to use datetime (yyyy.MM.dd.HHmm)
- Organized DLLs in a subfolder for neatness
- Used System.Reflection.Assembly::LoadBytes to avoid file lock issues
- Removed GitHub workflow
- Updated documentation for Tailscale integration
- Updated Visual Studio solution documentation
- Updated PSD1 files to include all cmdlets

## Installation

1. Download the release from the GitHub releases page: https://github.com/freedbygrace/PSOPNSenseAPI/releases/tag/v2025.04.14.2340
2. Extract the zip file to a folder in your PowerShell module path
3. Import the module: `Import-Module PSOPNSenseAPI`

## Usage

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck

# Get the interfaces
Get-OPNSenseInterface

# Disconnect from the firewall
Disconnect-OPNSense
```

For more examples, see the documentation in the `docs` folder.
