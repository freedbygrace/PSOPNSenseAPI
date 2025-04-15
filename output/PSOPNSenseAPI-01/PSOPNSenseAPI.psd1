@{
    # Script module or binary module file associated with this manifest.
    RootModule = 'PSOPNSenseAPI.psm1'

    # Version number of this module.
    ModuleVersion = '2025.04.15.1143'

    # Supported PSEditions
    CompatiblePSEditions = @('Desktop', 'Core')

    # ID used to uniquely identify this module
    GUID = '9a3b4c55-5f9a-4b8c-87d9-9a7a5cdd5c9f'

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
    ClrVersion = '4.0'

    # Processor architecture (None, X86, Amd64) required by this module
    # ProcessorArchitecture = ''

    # Modules that must be imported into the global environment prior to importing this module
    # RequiredModules = @()

    # Assemblies that must be loaded prior to importing this module
    # RequiredAssemblies = @()

    # Script files (.ps1) that are run in the caller's environment prior to importing this module.
    # ScriptsToProcess = @()

    # Type files (.ps1xml) to be loaded when importing this module
    # TypesToProcess = @()

    # Format files (.ps1xml) to be loaded when importing this module
    # FormatsToProcess = @()

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules = @('lib\PSOPNSenseAPI.dll')

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @()

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport = @(
        'Apply-OPNSenseFirewallChanges',
        'Connect-OPNSense',
        'Connect-OPNSenseTailscale',
        'ConvertTo-OPNSenseNetworkNotation',
        'Disable-OPNSenseCronJob',
        'Disable-OPNSenseFirewallRule',
        'Disable-OPNSensePlugin',
        'Disable-OPNSenseTailscale',
        'Disconnect-OPNSense',
        'Disconnect-OPNSenseTailscale',
        'Enable-OPNSenseCronJob',
        'Enable-OPNSenseFirewallRule',
        'Enable-OPNSensePlugin',
        'Enable-OPNSenseTailscale',
        'Export-OPNSenseConfig',
        'Get-OPNSenseAlias',
        'Get-OPNSenseConfig',
        'Get-OPNSenseConfigBackup',
        'Get-OPNSenseConnection',
        'Get-OPNSenseCronJob',
        'Get-OPNSenseDHCPLease',
        'Get-OPNSenseDHCPOption',
        'Get-OPNSenseDHCPServer',
        'Get-OPNSenseDHCPStaticMapping',
        'Get-OPNSenseDNSForwarding',
        'Get-OPNSenseDNSForwardingHost',
        'Get-OPNSenseDNSOverride',
        'Get-OPNSenseDNSServer',
        'Get-OPNSenseFirewallRule',
        'Get-OPNSenseFirmware',
        'Get-OPNSenseGateway',
        'Get-OPNSenseInterface',
        'Get-OPNSenseInterfaceStatistics',
        'Get-OPNSensePlugin',
        'Get-OPNSensePortForwardingRule',
        'Get-OPNSenseRoute',
        'Get-OPNSenseSystemDNS',
        'Get-OPNSenseTailscaleStatus',
        'Get-OPNSenseUser',
        'Get-OPNSenseVLAN',
        'Import-OPNSenseConfig',
        'Install-OPNSensePlugin',
        'Invoke-OPNSenseNetworkCalculation',
        'New-OPNSenseAlias',
        'New-OPNSenseCronJob',
        'New-OPNSenseDHCPOption',
        'New-OPNSenseDHCPStaticMapping',
        'New-OPNSenseDNSForwardingHost',
        'New-OPNSenseDNSOverride',
        'New-OPNSenseFirewallRule',
        'New-OPNSenseGateway',
        'New-OPNSensePortForwardingRule',
        'New-OPNSenseRoute',
        'New-OPNSenseSubnetVLANs',
        'New-OPNSenseUser',
        'New-OPNSenseVLAN',
        'Remove-OPNSenseAlias',
        'Remove-OPNSenseCronJob',
        'Remove-OPNSenseDHCPLease',
        'Remove-OPNSenseDHCPOption',
        'Remove-OPNSenseDHCPStaticMapping',
        'Remove-OPNSenseDNSForwardingHost',
        'Remove-OPNSenseDNSOverride',
        'Remove-OPNSenseFirewallRule',
        'Remove-OPNSenseGateway',
        'Remove-OPNSensePortForwardingRule',
        'Remove-OPNSenseRoute',
        'Remove-OPNSenseUser',
        'Remove-OPNSenseVLAN',
        'Restart-OPNSenseFirewall',
        'Restart-OPNSenseInterface',
        'Restore-OPNSenseConfig',
        'Set-OPNSenseAlias',
        'Set-OPNSenseCronJob',
        'Set-OPNSenseDHCPOption',
        'Set-OPNSenseDHCPServer',
        'Set-OPNSenseDHCPStaticMapping',
        'Set-OPNSenseDNSForwarding',
        'Set-OPNSenseDNSForwardingHost',
        'Set-OPNSenseDNSServer',
        'Set-OPNSenseFirewallRule',
        'Set-OPNSenseGateway',
        'Set-OPNSenseInterface',
        'Set-OPNSensePortForwardingRule',
        'Set-OPNSenseRoute',
        'Set-OPNSenseSystemDNS',
        'Set-OPNSenseUser',
        'Set-OPNSenseVLAN',
        'Uninstall-OPNSensePlugin',
        'Update-OPNSenseFirmware',
        'Start-OPNSenseFirmwareUpgrade'
    )

    # Variables to export from this module
    VariablesToExport = @()

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
            Tags = @('PowerShell', 'OPNSense', 'Firewall', 'API')

            # A URL to the license for this module.
            LicenseUri = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/LICENSE'

            # A URL to the main website for this project.
            ProjectUri = 'https://github.com/freedbygrace/PSOPNSenseAPI'

            # A URL to an icon representing this module.
            # IconUri = ''

            # ReleaseNotes of this module
            ReleaseNotes = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/CHANGELOG.md'

            # Prerelease string of this module
            # Prerelease = ''

            # Flag to indicate whether the module requires explicit user acceptance for install/update/save
            # RequireLicenseAcceptance = $false

            # External dependent modules of this module
            # ExternalModuleDependencies = @()

        } # End of PSData hashtable

    } # End of PrivateData hashtable

    # HelpInfoURI of this module
    # HelpInfoURI = ''

    # Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
    # DefaultCommandPrefix = ''
}
