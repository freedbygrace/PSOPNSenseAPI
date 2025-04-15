@{
    RootModule = 'PSOPNSenseAPI.dll'
    ModuleVersion = '2025.04.14.2246'
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
        'Connect-OPNSense',
        'Disconnect-OPNSense',
        'Get-OPNSenseInterface',
        'Set-OPNSenseInterface',
        'Restart-OPNSenseInterface',
        'Get-OPNSenseInterfaceStatistics',
        'Get-OPNSenseVLAN',
        'New-OPNSenseVLAN',
        'Set-OPNSenseVLAN',
        'Remove-OPNSenseVLAN',
        'Get-OPNSenseFirewallRule',
        'New-OPNSenseFirewallRule',
        'Set-OPNSenseFirewallRule',
        'Remove-OPNSenseFirewallRule',
        'Enable-OPNSenseFirewallRule',
        'Disable-OPNSenseFirewallRule',
        'Get-OPNSenseAlias',
        'New-OPNSenseAlias',
        'Set-OPNSenseAlias',
        'Remove-OPNSenseAlias',
        'Get-OPNSenseNATRule',
        'New-OPNSenseNATRule',
        'Set-OPNSenseNATRule',
        'Remove-OPNSenseNATRule',
        'Get-OPNSensePlugin',
        'Enable-OPNSensePlugin',
        'Disable-OPNSensePlugin',
        'Install-OPNSensePlugin',
        'Uninstall-OPNSensePlugin',
        'Get-OPNSenseUser',
        'New-OPNSenseUser',
        'Set-OPNSenseUser',
        'Remove-OPNSenseUser',
        'Get-OPNSenseFirmware',
        'Update-OPNSenseFirmware',
        'Upgrade-OPNSenseFirmware',
        'Restart-OPNSenseFirewall',
        'New-OPNSenseSubnetVLANs',
        'Get-OPNSenseDHCPServer',
        'Set-OPNSenseDHCPServer',
        'Get-OPNSenseDHCPStaticMapping',
        'New-OPNSenseDHCPStaticMapping',
        'Set-OPNSenseDHCPStaticMapping',
        'Remove-OPNSenseDHCPStaticMapping',
        'Get-OPNSenseCronJob',
        'New-OPNSenseCronJob',
        'Set-OPNSenseCronJob',
        'Remove-OPNSenseCronJob',
        'Enable-OPNSenseCronJob',
        'Disable-OPNSenseCronJob',
        'Get-OPNSenseTailscale',
        'Enable-OPNSenseTailscale',
        'Disable-OPNSenseTailscale',
        'Connect-OPNSenseTailscale',
        'Disconnect-OPNSenseTailscale',
        'Get-OPNSensePortForwardingRule',
        'New-OPNSensePortForwardingRule',
        'Set-OPNSensePortForwardingRule',
        'Remove-OPNSensePortForwardingRule'
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
