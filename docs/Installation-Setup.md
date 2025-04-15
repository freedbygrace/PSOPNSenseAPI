# Installation and Setup

This document provides instructions for installing and setting up the PSOPNSenseAPI PowerShell module.

## Overview

The PSOPNSenseAPI module is a PowerShell module that provides cmdlets for managing OPNSense firewalls through the OPNSense API. This document covers the installation of the module, setting up API access on the OPNSense firewall, and establishing a connection to the firewall.

## Prerequisites

Before installing the PSOPNSenseAPI module, ensure that you have the following:

- PowerShell 5.1 or later (Windows PowerShell or PowerShell Core)
- .NET Framework 4.7.2 or later (for Windows PowerShell)
- .NET Core 3.1 or later (for PowerShell Core)
- OPNSense 21.1 or later with API access configured

## Installation Methods

### Installing from PowerShell Gallery

The recommended way to install the PSOPNSenseAPI module is from the PowerShell Gallery:

```powershell
# Install the module for the current user
Install-Module -Name PSOPNSenseAPI -Scope CurrentUser

# Install the module for all users (requires administrator privileges)
Install-Module -Name PSOPNSenseAPI -Scope AllUsers
```

### Installing from GitHub

You can also install the module directly from the GitHub repository:

```powershell
# Clone the repository
git clone https://github.com/freedbygrace/PSOPNSenseAPI.git

# Navigate to the repository directory
cd PSOPNSenseAPI

# Build the module
.\build\build.ps1

# Import the module
Import-Module .\output\PSOPNSenseAPI
```

### Manual Installation

To install the module manually:

1. Download the latest release from the [GitHub releases page](https://github.com/freedbygrace/PSOPNSenseAPI/releases)
2. Extract the archive to a module directory:
   - For the current user: `$env:USERPROFILE\Documents\WindowsPowerShell\Modules\PSOPNSenseAPI`
   - For all users: `$env:ProgramFiles\WindowsPowerShell\Modules\PSOPNSenseAPI`
3. Import the module:

```powershell
Import-Module PSOPNSenseAPI
```

## Setting Up API Access on OPNSense

Before you can use the PSOPNSenseAPI module, you need to set up API access on your OPNSense firewall:

1. Log in to the OPNSense web interface
2. Navigate to **System > Access > Users**
3. Select the user you want to use for API access or create a new user
4. Ensure the user has the necessary privileges for the operations you want to perform
5. Click on the **API keys** tab
6. Click **+** to add a new API key
7. Note the **Key** and **Secret** values, as you will need them to connect to the firewall

## Connecting to an OPNSense Firewall

Once you have installed the module and set up API access, you can connect to an OPNSense firewall:

```powershell
# Connect to the OPNSense firewall
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"

# Connect to the OPNSense firewall with certificate validation disabled (for self-signed certificates)
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck
```

## Verifying the Installation

To verify that the module is installed and working correctly:

```powershell
# Check the module version
Get-Module -Name PSOPNSenseAPI

# List available cmdlets
Get-Command -Module PSOPNSenseAPI

# Get help for a specific cmdlet
Get-Help -Name Connect-OPNSense -Full
```

## Updating the Module

To update the module to the latest version:

```powershell
# Update the module from PowerShell Gallery
Update-Module -Name PSOPNSenseAPI
```

## Uninstalling the Module

To uninstall the module:

```powershell
# Uninstall the module
Uninstall-Module -Name PSOPNSenseAPI
```

## Troubleshooting

### Connection Issues

If you encounter connection issues:

1. Verify that the OPNSense firewall is reachable from your computer
2. Check that the API key and secret are correct
3. Ensure that the user associated with the API key has the necessary privileges
4. If using HTTPS with a self-signed certificate, use the `-SkipCertificateCheck` parameter
5. Check if there are any firewall rules blocking access to the OPNSense web interface

### Module Loading Issues

If you encounter issues loading the module:

1. Verify that you have the required PowerShell and .NET versions
2. Check that the module is installed in a valid module path:

```powershell
# List module paths
$env:PSModulePath -split ';'

# Check if the module is in a valid path
Get-Module -Name PSOPNSenseAPI -ListAvailable
```

3. Try importing the module with verbose output:

```powershell
Import-Module -Name PSOPNSenseAPI -Verbose
```

### API Access Issues

If you encounter issues with API access:

1. Verify that the API is enabled on the OPNSense firewall
2. Check that the user has the necessary privileges
3. Ensure that the API key and secret are valid
4. Check if there are any firewall rules blocking access to the API

## Common Configuration

### Creating a Profile for Quick Connection

You can create a profile to store your connection information for quick access:

```powershell
# Create a directory for profiles
$profilesDir = "$env:USERPROFILE\Documents\PSOPNSenseAPI\Profiles"
New-Item -Path $profilesDir -ItemType Directory -Force

# Create a profile
$profile = @{
    Server = "https://firewall.example.com"
    ApiKey = "your_api_key"
    ApiSecret = "your_api_secret"
    SkipCertificateCheck = $true
}

# Save the profile
$profilePath = "$profilesDir\firewall.json"
$profile | ConvertTo-Json | Set-Content -Path $profilePath

# Load the profile and connect
$profile = Get-Content -Path $profilePath | ConvertFrom-Json
Connect-OPNSense -Server $profile.Server -ApiKey $profile.ApiKey -ApiSecret $profile.ApiSecret -SkipCertificateCheck:$profile.SkipCertificateCheck
```

### Setting Up a Module Import Profile

You can create a PowerShell profile that automatically imports the module:

```powershell
# Add the following line to your PowerShell profile
if (-not (Get-Module -Name PSOPNSenseAPI -ErrorAction SilentlyContinue)) {
    Import-Module -Name PSOPNSenseAPI
}

# To edit your PowerShell profile
notepad $PROFILE
```

## Notes

- Store API keys and secrets securely, as they provide access to your OPNSense firewall.
- Consider using a dedicated user with limited privileges for API access.
- Always disconnect from the firewall when you're done:

```powershell
Disconnect-OPNSense
```

- For security reasons, avoid storing API keys and secrets in scripts or profiles that are shared or stored in unsecured locations.
- When using the module in scripts, consider using secure string variables or environment variables for API keys and secrets.
- The module uses HTTPS for all communication with the OPNSense firewall, ensuring that all data is encrypted in transit.
