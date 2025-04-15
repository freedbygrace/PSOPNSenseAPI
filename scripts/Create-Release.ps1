# Create-Release.ps1
# This script creates a new release of the PSOPNSenseAPI module

# Get the current date and time in the format yyyy.MM.dd.HHmm
$version = Get-Date -Format "yyyy.MM.dd.HHmm"

# Update the version in the project file
$projectFile = "src\PSOPNSenseAPI\PSOPNSenseAPI.csproj"
$projectContent = Get-Content $projectFile
$projectContent = $projectContent -replace "<Version>.*</Version>", "<Version>$version</Version>"
Set-Content $projectFile $projectContent

# Update the version in the module manifest files
$sourcePsd1 = "src\PSOPNSenseAPI\PSOPNSenseAPI.psd1"
$sourcePsd1Content = Get-Content $sourcePsd1
$sourcePsd1Content = $sourcePsd1Content -replace "ModuleVersion = '.*'", "ModuleVersion = '$version'"
Set-Content $sourcePsd1 $sourcePsd1Content

$outputPsd1 = "output\PSOPNSenseAPI\PSOPNSenseAPI.psd1"
$outputPsd1Content = Get-Content $outputPsd1
$outputPsd1Content = $outputPsd1Content -replace "ModuleVersion = '.*'", "ModuleVersion = '$version'"
Set-Content $outputPsd1 $outputPsd1Content

# Build the project
dotnet build -c Release

# Copy the DLLs to the output folder
$libPath = "output\PSOPNSenseAPI\lib-new"
New-Item -Path $libPath -ItemType Directory -Force | Out-Null
Copy-Item -Path "src\PSOPNSenseAPI\bin\Release\net472\*.dll" -Destination $libPath
Remove-Item -Path "output\PSOPNSenseAPI\lib" -Recurse -Force -ErrorAction SilentlyContinue
Rename-Item -Path $libPath -NewName "lib"

# Create the PSM1 file
$psm1Path = "output\PSOPNSenseAPI\PSOPNSenseAPI.psm1"
$psm1Content = @"
# Load assemblies from the lib folder using System.Reflection.Assembly::LoadBytes
# This avoids file lock issues when the module is loaded

`$libPath = Join-Path `$PSScriptRoot "lib"
`$dllFiles = Get-ChildItem -Path `$libPath -Filter "*.dll"

foreach (`$dll in `$dllFiles) {
    try {
        Write-Verbose "Attempting to load assembly: `$(`$dll.FullName)"
        `$dllBytes = [System.IO.File]::ReadAllBytes(`$dll.FullName)
        `$Null = [System.Reflection.Assembly]::Load(`$dllBytes)
        Write-Verbose "Successfully loaded assembly: `$(`$dll.FullName)"
    }
    catch {
        Write-Warning "Failed to load assembly `$(`$dll.Name): `$_"
    }
}

# Export all cmdlets from the PSD1 file
"@
Set-Content -Path $psm1Path -Value $psm1Content

# Create a release archive
$zipFile = "output\PSOPNSenseAPI-$version.zip"
Compress-Archive -Path "output\PSOPNSenseAPI\*" -DestinationPath $zipFile -Force

# Clean up old zip files
Get-ChildItem -Path "output" -Filter "*.zip" |
    Where-Object { $_.Name -ne "PSOPNSenseAPI-$version.zip" } |
    Remove-Item -Force

# Commit the changes
git add .
git commit -m "Update version to $version"
git push origin main

# Create a GitHub release
Write-Host "To create a GitHub release, go to https://github.com/freedbygrace/PSOPNSenseAPI/releases/new"
Write-Host "Tag version: v$version"
Write-Host "Release title: v$version"
Write-Host "Description: See CHANGELOG.md for details"
Write-Host "Attach the file: $zipFile"

# Create a release note file
$releaseNotes = @"
# Release v$version

## What's New

See [CHANGELOG.md](CHANGELOG.md) for details.

## Installation

1. Download the release from the GitHub releases page: https://github.com/freedbygrace/PSOPNSenseAPI/releases/tag/v$version
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
"@

Set-Content -Path "RELEASE.md" -Value $releaseNotes

# Commit the release notes
git add RELEASE.md
git commit -m "Add release notes for v$version"
git push origin main

Write-Host "Release created successfully!"
