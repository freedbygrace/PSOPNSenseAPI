# Import-OPNSenseConfig

## SYNOPSIS
Imports an OPNSense firewall configuration.

## SYNTAX

```
Import-OPNSenseConfig -Path <FileInfo> [-Force] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Import-OPNSenseConfig cmdlet imports an OPNSense firewall configuration from a file.

## EXAMPLES

### Example 1: Import a configuration from a file
```powershell
$path = New-Object System.IO.FileInfo "C:\Backups\opnsense-config.xml"
Import-OPNSenseConfig -Path $path
```

This example imports an OPNSense firewall configuration from a file.

## PARAMETERS

### -Path
The path to the configuration file. Must be a FileInfo object with an .xml extension.

```yaml
Type: FileInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Suppresses the confirmation prompt.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The firewall will restart after the configuration is imported.

## RELATED LINKS

[Get-OPNSenseConfig](Get-OPNSenseConfig.md)
[Export-OPNSenseConfig](Export-OPNSenseConfig.md)
[Restore-OPNSenseConfig](Restore-OPNSenseConfig.md)
