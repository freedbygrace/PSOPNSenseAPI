@{
    RootModule = 'PSOPNSenseAPI.psm1'
    ModuleVersion = '2025.04.14.2320'
    GUID = '1f0e4b77-cc7c-4a1e-b45a-d7c51a3c562e'
    Author = 'PSOPNSenseAPI Contributors'
    CompanyName = 'PSOPNSenseAPI'
    Copyright = 'Copyright (c) 2025 PSOPNSenseAPI Contributors'
    Description = 'PowerShell module for interacting with the OPNSense API to configure firewalls'
    PowerShellVersion = '5.1'
    CompatiblePSEditions = @('Desktop', 'Core')
    DotNetFrameworkVersion = '4.7.2'
    CLRVersion = '4.0.0'
    FunctionsToExport = @()
    CmdletsToExport = @(
        'ApplyOPNSenseFirewallChanges',
        'BackupOPNSenseConfig',
        'ConnectOPNSense',
        'ConnectOPNSenseTailscale',
        'ConvertToOPNSenseNetworkNotation',
        'DisableOPNSenseCronJob',
        'DisableOPNSenseFirewallRule',
        'DisableOPNSensePlugin',
        'DisableOPNSenseTailscale',
        'DisconnectOPNSense',
        'DisconnectOPNSenseTailscale',
        'EnableOPNSenseCronJob',
        'EnableOPNSenseFirewallRule',
        'EnableOPNSensePlugin',
        'EnableOPNSenseTailscale',
        'ExportOPNSenseConfig',
        'GetOPNSenseAlias',
        'GetOPNSenseConfigBackup',
        'GetOPNSenseConnection',
        'GetOPNSenseCronJob',
        'GetOPNSenseDHCPLease',
        'GetOPNSenseDHCPOption',
        'GetOPNSenseDHCPServer',
        'GetOPNSenseDHCPStaticMapping',
        'GetOPNSenseDNSForwarding',
        'GetOPNSenseDNSForwardingHost',
        'GetOPNSenseDNSOverride',
        'GetOPNSenseDNSServer',
        'GetOPNSenseFirewallRule',
        'GetOPNSenseFirmware',
        'GetOPNSenseGateway',
        'GetOPNSenseInterface',
        'GetOPNSenseInterfaceStatistics',
        'GetOPNSensePlugin',
        'GetOPNSensePortForwardingRule',
        'GetOPNSenseRoute',
        'GetOPNSenseSystemDNS',
        'GetOPNSenseTailscaleStatus',
        'GetOPNSenseUser',
        'GetOPNSenseVLAN',
        'ImportOPNSenseConfig',
        'InstallOPNSensePlugin',
        'InvokeOPNSenseNetworkCalculation',
        'NewOPNSenseAlias',
        'NewOPNSenseCronJob',
        'NewOPNSenseDHCPOption',
        'NewOPNSenseDHCPStaticMapping',
        'NewOPNSenseDNSForwardingHost',
        'NewOPNSenseDNSOverride',
        'NewOPNSenseFirewallRule',
        'NewOPNSenseGateway',
        'NewOPNSensePortForwardingRule',
        'NewOPNSenseRoute',
        'NewOPNSenseSubnetVLANs',
        'NewOPNSenseUser',
        'NewOPNSenseVLAN',
        'RemoveOPNSenseAlias',
        'RemoveOPNSenseCronJob',
        'RemoveOPNSenseDHCPLease',
        'RemoveOPNSenseDHCPOption',
        'RemoveOPNSenseDHCPStaticMapping',
        'RemoveOPNSenseDNSForwardingHost',
        'RemoveOPNSenseDNSOverride',
        'RemoveOPNSenseFirewallRule',
        'RemoveOPNSenseGateway',
        'RemoveOPNSensePortForwardingRule',
        'RemoveOPNSenseRoute',
        'RemoveOPNSenseUser',
        'RemoveOPNSenseVLAN',
        'RestartOPNSenseFirewall',
        'RestartOPNSenseInterface',
        'RestoreOPNSenseConfig',
        'SetOPNSenseAlias',
        'SetOPNSenseCronJob',
        'SetOPNSenseDHCPOption',
        'SetOPNSenseDHCPServer',
        'SetOPNSenseDHCPStaticMapping',
        'SetOPNSenseDNSForwarding',
        'SetOPNSenseDNSForwardingHost',
        'SetOPNSenseDNSServer',
        'SetOPNSenseFirewallRule',
        'SetOPNSenseGateway',
        'SetOPNSenseInterface',
        'SetOPNSensePortForwardingRule',
        'SetOPNSenseRoute',
        'SetOPNSenseSystemDNS',
        'SetOPNSenseUser',
        'SetOPNSenseVLAN',
        'UninstallOPNSensePlugin',
        'UpdateOPNSenseFirmware',
        'UpgradeOPNSenseFirmware'
    )
    VariablesToExport = @()
    AliasesToExport = @()
    PrivateData = @{
        PSData = @{
            Tags = @('PowerShell', 'OPNSense', 'Firewall', 'API')
            LicenseUri = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/LICENSE'
            ProjectUri = 'https://github.com/freedbygrace/PSOPNSenseAPI'
            ReleaseNotes = 'https://github.com/freedbygrace/PSOPNSenseAPI/blob/main/CHANGELOG.md'
        }
    }
}
