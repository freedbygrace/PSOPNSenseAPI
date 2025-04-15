# Load assemblies from the lib folder using System.Reflection.Assembly::LoadBytes
# This avoids file lock issues when the module is loaded

$libPath = Join-Path $PSScriptRoot "lib"
$dllFiles = Get-ChildItem -Path $libPath -Filter "*.dll"

foreach ($dll in $dllFiles) {
    try {
        $dllBytes = [System.IO.File]::ReadAllBytes($dll.FullName)
        [System.Reflection.Assembly]::Load($dllBytes) | Out-Null
        Write-Verbose "Loaded assembly: $($dll.Name)"
    }
    catch {
        Write-Warning "Failed to load assembly $($dll.Name): $_"
    }
}

# Export all cmdlets
$cmdlets = [System.AppDomain]::CurrentDomain.GetAssemblies() | 
    Where-Object { $_.FullName -like "*PSOPNSenseAPI*" } | 
    ForEach-Object { $_.GetTypes() } | 
    Where-Object { $_.IsPublic -and $_.IsClass -and $_.Name -like "*Cmdlet" } | 
    ForEach-Object { $_.Name }

Export-ModuleMember -Cmdlet $cmdlets
