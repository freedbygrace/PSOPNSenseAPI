# Visual Studio Solution

This document provides information about the Visual Studio solution for the PSOPNSenseAPI project.

## Overview

The PSOPNSenseAPI solution is a Visual Studio solution that contains the source code for the PSOPNSenseAPI PowerShell module. The solution is designed to be opened in Visual Studio for development, debugging, and troubleshooting.

## Solution Structure

The solution contains the following projects:

- **PSOPNSenseAPI**: The main project containing the PowerShell module code.
- **PSOPNSenseAPI.Tests**: The test project containing unit tests for the PowerShell module.

## Opening the Solution

To open the solution in Visual Studio:

1. Launch Visual Studio
2. Select "Open a project or solution"
3. Navigate to the root directory of the PSOPNSenseAPI repository
4. Select the `PSOPNSenseAPI.sln` file
5. Click "Open"

## Building the Solution

To build the solution in Visual Studio:

1. Open the solution in Visual Studio
2. Select the desired build configuration (Debug or Release) from the dropdown in the toolbar
3. Select "Build > Build Solution" from the menu or press F6

Alternatively, you can build the solution from the command line using the build scripts:

```powershell
# Build the module
.\build\build.ps1

# Build a release version with automatic versioning
.\build\build-release.ps1 -Clean -Test -Package
```

## Debugging

To debug the module in Visual Studio:

1. Open the solution in Visual Studio
2. Set breakpoints in the code
3. Select "Debug > Start Debugging" from the menu or press F5

For debugging PowerShell cmdlets, you can also use the PowerShell Integrated Scripting Environment (ISE) or Visual Studio Code with the PowerShell extension.

## Troubleshooting IPNetwork2 Issues

If you encounter issues with the IPNetwork2 library, here are some common troubleshooting steps:

1. **Check the reference**: Ensure that the IPNetwork2 package is properly referenced in the project file.
   ```xml
   <PackageReference Include="IPNetwork2" Version="3.1.764" />
   ```

2. **Verify the namespace**: The correct namespace for IPNetwork2 is `System.Net`.
   ```csharp
   using System.Net;
   ```

3. **Check the class name**: The main class in IPNetwork2 is `IPNetwork`, not `IPNetwork2`.
   ```csharp
   // Correct usage
   System.Net.IPNetwork network = System.Net.IPNetwork.Parse("192.168.1.0/24");

   // Incorrect usage
   IPNetwork2 network = IPNetwork2.Parse("192.168.1.0/24");
   ```

4. **Update package references**: If you're still having issues, try updating the package reference to the latest version.
   ```powershell
   # In Package Manager Console
   Update-Package IPNetwork2
   ```

5. **Restore NuGet packages**: Ensure all NuGet packages are properly restored.
   ```powershell
   # In Package Manager Console
   Restore-Package
   ```

6. **Clean and rebuild**: Sometimes a clean rebuild can resolve reference issues.
   ```powershell
   # In Package Manager Console
   Clean-Project
   Build-Project
   ```

## Adding New Features

When adding new features to the solution:

1. Create appropriate model classes in the `Models` directory
2. Implement service classes in the `Services` directory
3. Create PowerShell cmdlets in the `Cmdlets` directory
4. Update the module manifest (`PSOPNSenseAPI.psd1`) to export the new cmdlets
5. Add tests for the new features in the `PSOPNSenseAPI.Tests` project
6. Update documentation

## Testing

The solution includes unit tests that can be run from Visual Studio using the Test Explorer or from the command line:

```powershell
# Run tests
.\build\test.ps1
```

## Manual Release Process

The project uses a manual release process. To create a new release:

1. Update the version in the project file and module manifest
2. Build the solution in Release mode
3. Create a zip file of the module
4. Create a GitHub release with the zip file attached

## Dependencies

The solution has the following dependencies:

- PowerShellStandard.Library (5.1.1)
- Newtonsoft.Json (13.0.3)
- System.Net.Http (4.3.4)
- IPNetwork2 (3.1.764)

## Notes

- The solution is configured to target both .NET Framework 4.7.2 and .NET Standard 2.0 to support both Windows PowerShell 5.1 and PowerShell 7+.
- The module uses a versioning scheme of `yyyy.MM.dd.HHmm` for releases.
- The solution includes XML documentation comments that are used to generate help content for the PowerShell cmdlets.
- The module uses System.Reflection.Assembly::LoadBytes to load DLLs to avoid file lock issues.
- DLLs are organized in a lib subfolder for neatness.

## Module Structure

The module has the following structure:

```
PSOPNSenseAPI/
├── lib/                  # DLL files
│   ├── Newtonsoft.Json.dll
│   ├── PSOPNSenseAPI.dll
│   ├── System.Net.IPNetwork.dll
│   └── ...
├── PSOPNSenseAPI.psd1    # Module manifest
└── PSOPNSenseAPI.psm1    # Module script that loads DLLs
```

The PSM1 file uses System.Reflection.Assembly::LoadBytes to load the DLLs from the lib folder, which avoids file lock issues that can occur when PowerShell loads DLLs directly.
