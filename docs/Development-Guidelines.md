# Development and Contribution Guidelines

This document provides guidelines for developing and contributing to the PSOPNSenseAPI module.

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

## Coding Standards

### C# Code Style

- Follow Microsoft's C# coding conventions
- Use meaningful names for classes, methods, and variables
- Add XML documentation comments to all public members
- Use proper exception handling
- Write unit tests for all functionality
- Keep methods small and focused on a single responsibility
- Use dependency injection where appropriate
- Avoid static methods except for utility functions

### PowerShell Cmdlet Design

- Follow the PowerShell Verb-Noun naming convention
- Use approved PowerShell verbs (Get, Set, New, Remove, etc.)
- Implement proper parameter validation
- Support pipeline input where appropriate
- Implement ShouldProcess for cmdlets that make changes
- Provide meaningful error messages
- Write detailed help content
- Support common parameters (-Verbose, -Debug, etc.)
- Return appropriate objects, not strings

### Documentation

- Document all public APIs with XML comments
- Include examples in cmdlet help
- Update README.md and other documentation when adding features
- Document breaking changes in CHANGELOG.md

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

## Development Workflow

### Feature Development

1. Create a new branch from `main`:
   ```
   git checkout -b feature/my-feature
   ```

2. Implement the feature:
   - Add model classes if needed
   - Implement service classes
   - Create PowerShell cmdlets
   - Add unit tests
   - Update documentation

3. Build and test locally:
   ```
   .\build\build.ps1
   .\build\test.ps1
   ```

4. Commit your changes with meaningful commit messages:
   ```
   git commit -m "Add feature: description of the feature"
   ```

5. Push your branch and create a pull request:
   ```
   git push origin feature/my-feature
   ```

### Bug Fixes

1. Create a new branch from `main`:
   ```
   git checkout -b fix/bug-description
   ```

2. Fix the bug:
   - Add a test that reproduces the bug
   - Fix the bug
   - Verify that the test passes

3. Commit your changes:
   ```
   git commit -m "Fix: description of the bug fix"
   ```

4. Push your branch and create a pull request:
   ```
   git push origin fix/bug-description
   ```

## Testing

### Unit Testing

- Write unit tests for all functionality
- Use xUnit for testing
- Mock external dependencies
- Aim for high code coverage
- Run tests before submitting pull requests

### Integration Testing

- Write integration tests for API interactions
- Use a test OPNSense instance if possible
- Skip integration tests if no test instance is available

### Running Tests

```powershell
# Run all tests
.\build\test.ps1

# Run specific tests
dotnet test --filter "FullyQualifiedName~PSOPNSenseAPI.Tests.NetworkUtilityTests"
```

## Building and Packaging

### Building the Module

```powershell
# Build the module
.\build\build.ps1

# Build a release version with automatic versioning
.\build\build-release.ps1 -Clean -Test -Package
```

### Versioning

The module uses a versioning scheme of `yyyy.MM.dd.HHmm` for releases, which is automatically generated during the build process.

### Creating a Release

1. Update the CHANGELOG.md file
2. Build a release package:
   ```
   .\build\build-release.ps1 -Clean -Test -Package
   ```
3. Create a new release on GitHub with the generated package

## Continuous Integration

The project uses GitHub Actions for continuous integration:

- Builds and tests are run on every push to the main branch
- Release packages are automatically created with the versioning scheme
- Release artifacts are uploaded to GitHub Releases

## Pull Request Guidelines

When submitting a pull request:

1. Ensure all tests pass
2. Update documentation if needed
3. Add entries to CHANGELOG.md for new features or bug fixes
4. Follow the coding standards
5. Keep pull requests focused on a single change
6. Provide a clear description of the changes
7. Link to any related issues

## Code Review Process

All pull requests will be reviewed for:

- Code quality and style
- Test coverage
- Documentation
- Performance considerations
- Security implications

## Release Process

1. Merge approved pull requests into the main branch
2. Update the version in the module manifest
3. Update CHANGELOG.md
4. Create a new release on GitHub
5. Publish the module to PowerShell Gallery (if applicable)

## Support

If you need help with development:

- Open an issue on GitHub
- Reach out to the maintainers
- Check the documentation and existing issues

## License

All contributions to this project are subject to the terms of the MIT License.
