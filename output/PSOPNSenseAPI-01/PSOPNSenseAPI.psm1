# Load the main assembly directly
$mainDllPath = Join-Path $PSScriptRoot "lib\PSOPNSenseAPI.dll"

Write-Verbose "Loading assembly from $mainDllPath"

try {
    Add-Type -Path $mainDllPath
    Write-Verbose "Successfully loaded assembly from $mainDllPath"
} catch {
    $errorMessage = $_.Exception.Message
    Write-Error "Failed to load assembly from $($mainDllPath): $($errorMessage)"
    throw
}
