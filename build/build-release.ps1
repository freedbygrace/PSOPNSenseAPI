[CmdletBinding()]
param (
    [Parameter()]
    [switch]
    $Clean,

    [Parameter()]
    [switch]
    $Test,

    [Parameter()]
    [switch]
    $Package,

    [Parameter()]
    [switch]
    $Push,

    [Parameter()]
    [string]
    $GitHubToken
)

$ErrorActionPreference = 'Stop'

# Define paths
$rootDir = Split-Path -Parent $PSScriptRoot
$srcDir = Join-Path -Path $rootDir -ChildPath "src"
$testDir = Join-Path -Path $srcDir -ChildPath "PSOPNSenseAPI.Tests"
$projectDir = Join-Path -Path $srcDir -ChildPath "PSOPNSenseAPI"
$outputDir = Join-Path -Path $rootDir -ChildPath "output\PSOPNSenseAPI"
$moduleDir = Join-Path -Path $rootDir -ChildPath "PSOPNSenseAPI"

# Generate version number based on current date and time
$version = Get-Date -Format "yyyy.MM.dd.HHmm"
Write-Output "Building version: $version"

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

# Update version in project file
$projectFile = Join-Path -Path $projectDir -ChildPath "PSOPNSenseAPI.csproj"

# Update the version using XML manipulation
Write-Output "Updating version in project file to $version"
$projectContent = Get-Content -Path $projectFile -Raw
$updatedContent = $projectContent -replace '<Version>[^<]+</Version>', "<Version>$version</Version>"
Set-Content -Path $projectFile -Value $updatedContent

# Verify the update
$newContent = Get-Content -Path $projectFile -Raw
if ($newContent -match "<Version>$version</Version>") {
    Write-Output "Successfully updated version in project file"
} else {
    Write-Warning "Failed to update version in project file"
}

# Project file has been updated

# Update version in module manifest
$manifestFile = Join-Path -Path $moduleDir -ChildPath "PSOPNSenseAPI.psd1"
if (Test-Path $manifestFile) {
    Write-Output "Updating module manifest version to $version"
    $manifestContent = Get-Content -Path $manifestFile -Raw

    # Use a more flexible regex pattern to match the ModuleVersion line
    if ($manifestContent -match "ModuleVersion\s*=\s*['`"].*['`"]") {
        $newManifestContent = $manifestContent -replace "ModuleVersion\s*=\s*['`"].*['`"]", "ModuleVersion = '$version'"
        Set-Content -Path $manifestFile -Value $newManifestContent
        Write-Output "Successfully updated module manifest version"
    } else {
        # Try a different approach if the regex doesn't match
        $lines = Get-Content -Path $manifestFile
        $versionLineIndex = $lines | Select-String -Pattern "ModuleVersion" | Select-Object -First 1 -ExpandProperty LineNumber

        if ($versionLineIndex) {
            $lines[$versionLineIndex - 1] = "    ModuleVersion = '$version'"
            Set-Content -Path $manifestFile -Value $lines
            Write-Output "Successfully updated module manifest version using line replacement"
        } else {
            Write-Warning "Failed to update module version in manifest. ModuleVersion line not found."
        }
    }
} else {
    Write-Warning "Module manifest file not found at $manifestFile"
}

# Run tests if requested
if ($Test) {
    Write-Output "Running tests..."
    & "$PSScriptRoot\test.ps1" -Configuration "Release"

    if ($LASTEXITCODE -ne 0) {
        throw "Tests failed with exit code $LASTEXITCODE"
    }
}

# Build the solution
Write-Output "Building solution in Release configuration..."

# Create bin directory if it doesn't exist
$binDir = Join-Path -Path $outputDir -ChildPath "bin"
if (-not (Test-Path -Path $binDir)) {
    Write-Output "Creating bin directory: $binDir"
    New-Item -Path $binDir -ItemType Directory -Force | Out-Null
}

# Build the project
dotnet build "$projectDir" -c Release -o "$binDir"

if ($LASTEXITCODE -ne 0) {
    throw "Build failed with exit code $LASTEXITCODE"
}

Write-Output "DLLs built successfully to: $binDir"

# Copy module files
Write-Output "Copying module files to output directory"
Copy-Item -Path "$moduleDir\*" -Destination $outputDir -Recurse -Force

# Verify DLLs were built
$dllPath = Join-Path -Path $binDir -ChildPath "PSOPNSenseAPI.dll"
if (Test-Path $dllPath) {
    Write-Output "Verified DLL was built: $dllPath"
} else {
    Write-Warning "DLL was not found at expected location: $dllPath"
}

# Create a zip package if requested
if ($Package) {
    $packageDir = Join-Path -Path $rootDir -ChildPath "packages"
    if (-not (Test-Path -Path $packageDir)) {
        New-Item -Path $packageDir -ItemType Directory -Force | Out-Null
    }

    $zipFile = Join-Path -Path $packageDir -ChildPath "PSOPNSenseAPI-$version.zip"

    if (Test-Path -Path $zipFile) {
        Remove-Item -Path $zipFile -Force
    }

    Write-Output "Creating package: $zipFile"
    Compress-Archive -Path "$outputDir\*" -DestinationPath $zipFile
}

# Push to GitHub and create a release if requested
if ($Push) {
    if ([string]::IsNullOrEmpty($GitHubToken)) {
        throw "GitHubToken parameter is required for pushing to GitHub"
    }

    Write-Output "Committing changes to Git..."
    git add "$projectFile"
    git add "$manifestFile"
    git commit -m "Release version $version"

    Write-Output "Pushing changes to GitHub..."
    git push

    Write-Output "Creating GitHub release..."
    $releaseNotes = "Release version $version - $(Get-Date -Format 'yyyy-MM-dd')"

    # Create the release using GitHub CLI or REST API
    $headers = @{
        "Authorization" = "token $GitHubToken"
        "Accept" = "application/vnd.github.v3+json"
    }

    $releaseData = @{
        tag_name = "v$version"
        name = "Release v$version"
        body = $releaseNotes
        draft = $false
        prerelease = $false
    } | ConvertTo-Json

    $response = Invoke-RestMethod -Uri "https://api.github.com/repos/freedbygrace/PSOPNSenseAPI/releases" -Method Post -Headers $headers -Body $releaseData

    if ($Package) {
        Write-Output "Uploading package to GitHub release..."
        $uploadUrl = $response.upload_url -replace "{.*}", ""
        $fileName = Split-Path -Path $zipFile -Leaf

        Invoke-RestMethod -Uri "$uploadUrl?name=$fileName" -Method Post -Headers $headers -InFile $zipFile -ContentType "application/zip"
    }
}

Write-Output "Build completed successfully!"
Write-Output "Module output is available at: $outputDir"
if ($Package) {
    Write-Output "Package is available at: $zipFile"
}
