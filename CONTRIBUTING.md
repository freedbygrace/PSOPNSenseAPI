# Contributing to PSOPNSenseAPI

Thank you for your interest in contributing to the PSOPNSenseAPI module! This document provides guidelines and instructions for contributing to the project.

## Code of Conduct

This project adheres to a Code of Conduct that all contributors are expected to follow. Please read and understand the [Code of Conduct](CODE_OF_CONDUCT.md) before contributing.

## How Can I Contribute?

There are many ways to contribute to the PSOPNSenseAPI module:

- Reporting bugs
- Suggesting enhancements
- Writing documentation
- Submitting code changes
- Reviewing pull requests
- Helping other users

### Reporting Bugs

Before reporting a bug, please check the [issue tracker](https://github.com/freedbygrace/PSOPNSenseAPI/issues) to see if the bug has already been reported. If it has, you can add additional information to the existing issue.

When reporting a bug, please include:

- A clear and descriptive title
- A detailed description of the issue
- Steps to reproduce the bug
- Expected behavior
- Actual behavior
- Screenshots or error messages (if applicable)
- Your environment (PowerShell version, OPNSense version, module version)

### Suggesting Enhancements

Enhancement suggestions are welcome! Please include:

- A clear and descriptive title
- A detailed description of the enhancement
- The motivation for the enhancement
- Any potential implementation details
- Examples of how the enhancement would be used

### Writing Documentation

Documentation improvements are always welcome. You can contribute by:

- Fixing typos or errors in existing documentation
- Adding missing documentation
- Improving examples or explanations
- Adding tutorials or guides

### Submitting Code Changes

1. Fork the repository
2. Create a new branch for your changes
3. Make your changes
4. Write or update tests for your changes
5. Ensure all tests pass
6. Submit a pull request

#### Coding Standards

- Follow the existing code style and conventions
- Use meaningful variable and function names
- Write clear and concise comments
- Include XML documentation for all public members
- Follow PowerShell best practices

#### Testing

- Write tests for all new functionality
- Ensure all existing tests pass
- Test your changes on both PowerShell 5.1 and PowerShell 7
- Test your changes on different OPNSense versions if possible

### Pull Request Process

1. Update the README.md and documentation with details of your changes
2. Update the CHANGELOG.md with details of your changes
3. Ensure all tests pass
4. Submit the pull request with a clear description of the changes

## Development Environment Setup

### Prerequisites

- Visual Studio 2019 or later
- PowerShell 5.1 or PowerShell 7+
- .NET Framework 4.7.2 SDK (for Windows PowerShell 5.1 compatibility)
- .NET Core 3.1 SDK or later (for PowerShell 7+ compatibility)
- Git

### Setting Up the Development Environment

1. Clone the repository:
   ```
   git clone https://github.com/freedbygrace/PSOPNSenseAPI.git
   cd PSOPNSenseAPI
   ```

2. Open the solution in Visual Studio:
   ```
   start PSOPNSenseAPI.sln
   ```

3. Restore NuGet packages:
   ```
   dotnet restore
   ```

4. Build the solution:
   ```
   dotnet build
   ```

### Building and Testing

To build the module:

```powershell
.\build\build.ps1
```

To run tests:

```powershell
.\build\test.ps1
```

To build a release version:

```powershell
.\build\build-release.ps1 -Clean -Test -Package
```

## Project Structure

The project follows this structure:

- **src/PSOPNSenseAPI**: Main source code
  - **Cmdlets**: PowerShell cmdlets
  - **Models**: Data models
  - **Services**: Service classes for API communication
  - **Utilities**: Utility classes
  - **Logging**: Logging infrastructure
- **tests**: Unit and integration tests
- **docs**: Documentation
- **build**: Build scripts
- **output**: Build output (not in source control)

## Versioning

The module uses a versioning scheme of `yyyy.MM.dd.HHmm` for releases, which is automatically generated during the build process.

## Release Process

1. Update the CHANGELOG.md file
2. Build a release package:
   ```
   .\build\build-release.ps1 -Clean -Test -Package
   ```
3. Create a new release on GitHub with the generated package

## Getting Help

If you need help with contributing:

- Open an issue on GitHub
- Reach out to the maintainers
- Check the documentation and existing issues

## License

By contributing to this project, you agree that your contributions will be licensed under the project's [MIT License](LICENSE).

## Acknowledgments

Thank you to all contributors who have helped improve this module!
