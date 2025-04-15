[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $Configuration = "Release",

    [Parameter()]
    [switch]
    $Clean,

    [Parameter()]
    [switch]
    $UpdateVersion
)

$ErrorActionPreference = 'Stop'

# Define paths
$rootDir = Split-Path -Parent $PSScriptRoot
$srcDir = Join-Path -Path $rootDir -ChildPath "src"
$outputDir = Join-Path -Path $rootDir -ChildPath "output\PSOPNSenseAPI"
$moduleDir = Join-Path -Path $rootDir -ChildPath "PSOPNSenseAPI"

# Clean output directory if requested
if ($Clean -and (Test-Path -Path $outputDir)) {
    Write-Verbose "Cleaning output directory: $outputDir"
    Remove-Item -Path $outputDir -Recurse -Force
}

# Create output directory if it doesn't exist
if (-not (Test-Path -Path $outputDir)) {
    Write-Verbose "Creating output directory: $outputDir"
    New-Item -Path $outputDir -ItemType Directory -Force | Out-Null
}

# Update version if requested
if ($UpdateVersion) {
    $version = Get-Date -Format "yyyy.MM.dd.HHmm"
    Write-Output "Updating version to: $version"

    # Update version in project file
    $projectFile = Join-Path -Path "$srcDir\PSOPNSenseAPI" -ChildPath "PSOPNSenseAPI.csproj"
    if (Test-Path $projectFile) {
        $projectXml = [xml](Get-Content $projectFile)
        $versionNodes = $projectXml.SelectNodes("//Version")
        if ($versionNodes.Count -gt 0) {
            foreach ($node in $versionNodes) {
                $node.InnerText = $version
            }
            $projectXml.Save($projectFile)
            Write-Output "Updated version in project file to $version"
        } else {
            Write-Warning "Could not find Version node in project file"
        }
    } else {
        Write-Warning "Project file not found at $projectFile"
    }

    # Update version in module manifest
    $manifestFile = Join-Path -Path $moduleDir -ChildPath "PSOPNSenseAPI.psd1"
    if (Test-Path $manifestFile) {
        $manifestContent = Get-Content -Path $manifestFile -Raw
        $newManifestContent = $manifestContent -replace "ModuleVersion\s*=\s*['`"].*['`"]", "ModuleVersion = '$version'"
        Set-Content -Path $manifestFile -Value $newManifestContent
        Write-Output "Updated version in module manifest to $version"
    } else {
        Write-Warning "Module manifest file not found at $manifestFile"
    }
}

# Build the solution
Write-Output "Building solution in $Configuration configuration..."
dotnet build "$srcDir\PSOPNSenseAPI" -c $Configuration -o "$outputDir\bin"

if ($LASTEXITCODE -ne 0) {
    throw "Build failed with exit code $LASTEXITCODE"
}

# Copy module files
Write-Verbose "Copying module files to output directory"
Copy-Item -Path "$moduleDir\*" -Destination $outputDir -Recurse -Force

# Create module directory in PSModulePath if it doesn't exist
$modulePath = $env:PSModulePath -split ';' | Select-Object -First 1
$installPath = Join-Path -Path $modulePath -ChildPath "PSOPNSenseAPI"

Write-Verbose "Module can be installed to: $installPath"
Write-Verbose "To install, run: Copy-Item -Path '$outputDir\*' -Destination '$installPath' -Recurse -Force"

Write-Output "Build completed successfully!"
Write-Output "Module output is available at: $outputDir"
