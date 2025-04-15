# Load assemblies from the lib folder using System.Reflection.Assembly::LoadBytes
# This avoids file lock issues when the module is loaded

$libPath = Join-Path $PSScriptRoot -ChildPath "lib"

$DLLFileList = Get-ChildItem -Path $libPath -Filter "*.dll"

$DLLFileListCount = ($DLLFileList | Measure-Object).Count

$DLLFileListCounter = 1

For ($DLLFileListIndex = 0; $DLLFileListIndex -lt $DLLFileListCount; $DLLFileListIndex++)
  {
      Try
        {
            $DLLFile = $DLLFileList[$DLLFileListIndex]

            Write-Verbose "Attempting to load DLL assembly $($DLLFileListCounter) of $($DLLFileListCount). Please Wait..."

            Write-Verbose "Path: $($DLLFile)"

            $DLLFileBytes = [System.IO.File]::ReadAllBytes($DLLFile.FullName)

            $Null = [System.Reflection.Assembly]::Load($DLLFileBytes)

            Write-Verbose "Successfully loaded DLL assembly $($DLLFileListCounter) of $($DLLFileListCount)"
        }
      Catch
        {
            Write-Warning "Failed to load DLL assembly $($DLLFileListCounter) of $($DLLFileListCount): $($_.Exception.Message)"
        }
      Finally
        {
            $DLLFileListCounter++
        }
}
