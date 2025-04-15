@{
    # Script module or binary module file associated with this manifest.
    RootModule = 'PSOPNSenseAPI.psm1'

    # Version number of this module.
    ModuleVersion = '2025.04.14.1839'

    # Supported PSEditions
    CompatiblePSEditions = @('Desktop', 'Core')

    # ID used to uniquely identify this module
    GUID = '8f7a3d7a-0e5a-4b7c-9b5a-9c5b3a5a8e7a'

    # Author of this module
    Author = 'PSOPNSenseAPI Contributors'

    # Company or vendor of this module
    CompanyName = 'PSOPNSenseAPI'

    # Copyright statement for this module
    Copyright = '(c) 2025 PSOPNSenseAPI Contributors. All rights reserved.'

    # Description of the functionality provided by this module
    Description = 'PowerShell module for interacting with the OPNSense API to configure firewalls'

    # Minimum version of the PowerShell engine required by this module
    PowerShellVersion = '5.1'

    # Name of the PowerShell host required by this module
    # PowerShellHostName = ''

    # Minimum version of the PowerShell host required by this module
    # PowerShellHostVersion = ''

    # Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
    DotNetFrameworkVersion = '4.7.2'

    # Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
    # ClrVersion = ''

    # Processor architecture (None, X86, Amd64) required by this module
    # ProcessorArchitecture = ''

    # Modules that must be imported into the global environment prior to importing this module
    # RequiredModules = @()

    # Assemblies that must be loaded prior to importing this module
    RequiredAssemblies = @('bin\PSOPNSenseAPI.dll')

    # Script files (.ps1) that are run in the caller's environment prior to importing this module.
    # ScriptsToProcess = @()

    # Type files (.ps1xml) to be loaded when importing this module
    # TypesToProcess = @()

    # Format files (.ps1xml) to be loaded when importing this module
    # FormatsToProcess = @()

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules = @('bin\PSOPNSenseAPI.dll')

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @()

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport = @(
        # Connection Management
        'Connect-OPNSense',
        'Disconnect-OPNSense',
        'Get-OPNSenseConnection',

        # Firewall Rule Management
        'Get-OPNSenseFirewallRule',
        'New-OPNSenseFirewallRule',
        'Set-OPNSenseFirewallRule',
        'Remove-OPNSenseFirewallRule',
        'Enable-OPNSenseFirewallRule',
        'Disable-OPNSenseFirewallRule',
        'Apply-OPNSenseFirewallChanges',

        # Alias Management
        'Get-OPNSenseAlias',
        'New-OPNSenseAlias',
        'Set-OPNSenseAlias',
        'Remove-OPNSenseAlias',

        # NAT Rule Management
        'Get-OPNSenseNATRule',
        'New-OPNSenseNATRule',
        'Set-OPNSenseNATRule',
        'Remove-OPNSenseNATRule',

        # Interface Management
        'Get-OPNSenseInterface',
        'Set-OPNSenseInterface',
        'Restart-OPNSenseInterface',
        'Get-OPNSenseInterfaceStatistics',
        'New-OPNSenseSubnetVLANs',

        # VLAN Management
        'Get-OPNSenseVLAN',
        'New-OPNSenseVLAN',
        'Set-OPNSenseVLAN',
        'Remove-OPNSenseVLAN',

        # DNS Configuration
        'Get-OPNSenseDNSServer',
        'Set-OPNSenseDNSServer',
        'Get-OPNSenseDNSDomain',
        'Set-OPNSenseDNSDomain',
        'Get-OPNSenseDNSOverride',
        'New-OPNSenseDNSOverride',
        'Remove-OPNSenseDNSOverride',

        # Configuration Management
        'Backup-OPNSenseConfig',
        'Restore-OPNSenseConfig',
        'Export-OPNSenseConfig',
        'Import-OPNSenseConfig',
        'Get-OPNSenseConfigBackup',

        # Plugin Management
        'Get-OPNSensePlugin',
        'Install-OPNSensePlugin',
        'Uninstall-OPNSensePlugin',
        'Enable-OPNSensePlugin',
        'Disable-OPNSensePlugin',

        # User Management
        'Get-OPNSenseUser',
        'New-OPNSenseUser',
        'Set-OPNSenseUser',
        'Remove-OPNSenseUser',

        # Firmware Management
        'Get-OPNSenseFirmware',
        'Update-OPNSenseFirmware',
        'Upgrade-OPNSenseFirmware',

        # System Management
        'Restart-OPNSenseFirewall'
    )

    # Variables to export from this module
    VariablesToExport = '*'

    # Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
    AliasesToExport = @()

    # DSC resources to export from this module
    # DscResourcesToExport = @()

    # List of all modules packaged with this module
    # ModuleList = @()

    # List of all files packaged with this module
    # FileList = @()

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData = @{

        PSData = @{

            # Tags applied to this module. These help with module discovery in online galleries.
            Tags = @('OPNSense', 'Firewall', 'API', 'Network', 'Security')

            # A URL to the license for this module.
            LicenseUri = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/LICENSE'

            # A URL to the main website for this project.
            ProjectUri = 'https://github.com/freedbygrace/PSOPNSenseAPI'

            # A URL to an icon representing this module.
            # IconUri = ''

            # ReleaseNotes of this module
            ReleaseNotes = 'Initial release of the PSOPNSenseAPI module'

            # Prerelease string of this module
            # Prerelease = ''

            # Flag to indicate whether the module requires explicit user acceptance for install/update/save
            # RequireLicenseAcceptance = $false

            # External dependent modules of this module
            # ExternalModuleDependencies = @()

        } # End of PSData hashtable

    } # End of PrivateData hashtable

    # HelpInfoURI = ''

    # Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
    # DefaultCommandPrefix = ''
}












