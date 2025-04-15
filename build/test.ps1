[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $Configuration = "Debug"
)

$ErrorActionPreference = 'Stop'

# Define paths
$rootDir = Split-Path -Parent $PSScriptRoot
$srcDir = Join-Path -Path $rootDir -ChildPath "src"
$testDir = Join-Path -Path $srcDir -ChildPath "PSOPNSenseAPI.Tests"

# Run the tests
Write-Verbose "Running tests in $Configuration configuration"
dotnet test "$testDir" -c $Configuration

if ($LASTEXITCODE -ne 0) {
    throw "Tests failed with exit code $LASTEXITCODE"
}

Write-Output "Tests completed successfully!"
