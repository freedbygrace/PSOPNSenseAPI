@{
    # Script module or binary module file associated with this manifest.
    RootModule = 'PSOPNSenseAPI.psm1'

    # Version number of this module.
    ModuleVersion = '2025.04.14.2320'

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
    # NestedModules = @()

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @()

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport = @(
        # Connection
        'Connect-OPNSense',
        'Disconnect-OPNSense',

        # Firewall Rules
        'Get-OPNSenseFirewallRule',
        'New-OPNSenseFirewallRule',
        'Set-OPNSenseFirewallRule',
        'Remove-OPNSenseFirewallRule',
        'Enable-OPNSenseFirewallRule',
        'Disable-OPNSenseFirewallRule',
        'Apply-OPNSenseFirewallChanges',

        # Aliases
        'Get-OPNSenseAlias',
        'New-OPNSenseAlias',
        'Set-OPNSenseAlias',
        'Remove-OPNSenseAlias',

        # Interfaces
        'Get-OPNSenseInterface',
        'Set-OPNSenseInterface',
        'Get-OPNSenseVLAN',
        'New-OPNSenseVLAN',
        'Remove-OPNSenseVLAN',
        'New-OPNSenseSubnetVLANs',

        # DNS
        'Get-OPNSenseDNSServer',
        'Set-OPNSenseDNSServer',
        'Get-OPNSenseDNSOverride',
        'New-OPNSenseDNSOverride',
        'Remove-OPNSenseDNSOverride',
        'Get-OPNSenseDNSForwarding',
        'Set-OPNSenseDNSForwarding',
        'Get-OPNSenseDNSForwardingHost',
        'New-OPNSenseDNSForwardingHost',
        'Set-OPNSenseDNSForwardingHost',
        'Remove-OPNSenseDNSForwardingHost',
        'Get-OPNSenseSystemDNS',
        'Set-OPNSenseSystemDNS',

        # Configuration
        'Backup-OPNSenseConfig',
        'Restore-OPNSenseConfig',
        'Export-OPNSenseConfig',
        'Import-OPNSenseConfig',

        # Plugins
        'Get-OPNSensePlugin',
        'Install-OPNSensePlugin',
        'Uninstall-OPNSensePlugin',
        'Enable-OPNSensePlugin',
        'Disable-OPNSensePlugin',

        # Users
        'Get-OPNSenseUser',
        'New-OPNSenseUser',
        'Set-OPNSenseUser',
        'Remove-OPNSenseUser',

        # Firmware
        'Get-OPNSenseFirmware',
        'Update-OPNSenseFirmware',
        'Upgrade-OPNSenseFirmware',

        # System
        'Restart-OPNSenseFirewall',

        # DHCP
        'Get-OPNSenseDHCPServer',
        'Set-OPNSenseDHCPServer',
        'Get-OPNSenseDHCPLease',
        'Remove-OPNSenseDHCPLease',
        'Get-OPNSenseDHCPStaticMapping',
        'New-OPNSenseDHCPStaticMapping',
        'Set-OPNSenseDHCPStaticMapping',
        'Remove-OPNSenseDHCPStaticMapping',
        'Get-OPNSenseDHCPOption',
        'New-OPNSenseDHCPOption',
        'Set-OPNSenseDHCPOption',
        'Remove-OPNSenseDHCPOption',

        # Cron
        'Get-OPNSenseCronJob',
        'New-OPNSenseCronJob',
        'Set-OPNSenseCronJob',
        'Remove-OPNSenseCronJob',
        'Enable-OPNSenseCronJob',
        'Disable-OPNSenseCronJob',

        # Tailscale
        'Get-OPNSenseTailscaleStatus',
        'Enable-OPNSenseTailscale',
        'Disable-OPNSenseTailscale',
        'Connect-OPNSenseTailscale',
        'Disconnect-OPNSenseTailscale',

        # Network Utilities
        'Invoke-OPNSenseNetworkCalculation',
        'ConvertTo-OPNSenseNetworkNotation',

        # Gateways and Routes
        'Get-OPNSenseGateway',
        'New-OPNSenseGateway',
        'Set-OPNSenseGateway',
        'Remove-OPNSenseGateway',
        'Get-OPNSenseRoute',
        'New-OPNSenseRoute',
        'Set-OPNSenseRoute',
        'Remove-OPNSenseRoute',

        # Port Forwarding
        'Get-OPNSensePortForwardingRule',
        'New-OPNSensePortForwardingRule',
        'Set-OPNSensePortForwardingRule',
        'Remove-OPNSensePortForwardingRule'
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
