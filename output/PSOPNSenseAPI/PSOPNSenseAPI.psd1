@{
    RootModule = 'PSOPNSenseAPI.psm1'
    ModuleVersion = '2025.04.15.0739'
    GUID = '1f0e4b77-cc7c-4a1e-b45a-d7c51a3c562e'
    Author = 'PSOPNSenseAPI Contributors'
    CompanyName = 'PSOPNSenseAPI'
    Copyright = 'Copyright (c) 2025 PSOPNSenseAPI Contributors'
    Description = 'PowerShell module for interacting with the OPNSense API to configure firewalls'
    PowerShellVersion = '5.1'
    CompatiblePSEditions = @('Desktop', 'Core')
    #DotNetFrameworkVersion = '4.7.2'
    CLRVersion = '4.0.0'
    FunctionsToExport = @()
    CmdletsToExport = @(
        'Apply-OPNSenseFirewallChanges',
        'Backup-OPNSenseConfig',
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
    VariablesToExport = @()
    AliasesToExport = @()
    RequiredAssemblies = @('lib\System.Net.IPNetwork.dll', 'lib\Newtonsoft.Json.dll')
    NestedModules = @('lib\PSOPNSenseAPI.dll')
    PrivateData = @{
        PSData = @{
            Tags = @('PowerShell', 'OPNSense', 'Firewall', 'API')
            LicenseUri = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/LICENSE'
            ProjectUri = 'https://github.com/freedbygrace/PSOPNSenseAPI'
            ReleaseNotes = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/CHANGELOG.md'
        }
    }
}
