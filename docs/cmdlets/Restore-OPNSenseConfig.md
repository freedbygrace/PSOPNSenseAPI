# Restore-OPNSenseConfig

## SYNOPSIS
Restores an OPNSense firewall configuration.

## SYNTAX

### Filename
```
Restore-OPNSenseConfig -Filename <String> [-Force] [-WhatIf] [-Confirm]
```

### XmlDocument
```
Restore-OPNSenseConfig -XmlDocument <XmlDocument> [-Force] [-WhatIf] [-Confirm]
```

## DESCRIPTION
The Restore-OPNSenseConfig cmdlet restores an OPNSense firewall configuration from a backup or an XML document.

## EXAMPLES

### Example 1: Restore a configuration backup
```powershell
Restore-OPNSenseConfig -Filename "config-backup-20250414-1234.xml"
```

This example restores an OPNSense firewall configuration from a backup file.

### Example 2: Restore a configuration from an XML document
```powershell
$config = Get-OPNSenseConfig
Restore-OPNSenseConfig -XmlDocument $config
```

This example restores an OPNSense firewall configuration from an XML document.

### Example 3: Restore a configuration from the pipeline
```powershell
Get-OPNSenseConfig | Restore-OPNSenseConfig
```

This example restores an OPNSense firewall configuration from the pipeline.

## PARAMETERS

### -Filename
The filename of the backup to restore.

```yaml
Type: String
Parameter Sets: Filename
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -XmlDocument
The XML document containing the configuration to restore.

```yaml
Type: XmlDocument
Parameter Sets: XmlDocument
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### System.Xml.XmlDocument
An XML document containing the OPNSense configuration.

## OUTPUTS

### None

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
The firewall will restart after the configuration is restored.

## RELATED LINKS

[Get-OPNSenseConfig](Get-OPNSenseConfig.md)
[Export-OPNSenseConfig](Export-OPNSenseConfig.md)
[Import-OPNSenseConfig](Import-OPNSenseConfig.md)
