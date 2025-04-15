# Export-OPNSenseConfig

## SYNOPSIS
Exports the OPNSense firewall configuration.

## SYNTAX

```
Export-OPNSenseConfig -Path <FileInfo> [-Force]
```

## DESCRIPTION
The Export-OPNSenseConfig cmdlet exports the OPNSense firewall configuration to a file.

## EXAMPLES

### Example 1: Export the configuration to a file
```powershell
$path = New-Object System.IO.FileInfo "C:\Backups\opnsense-config.xml"
Export-OPNSenseConfig -Path $path
```

This example exports the OPNSense firewall configuration to a file.

## PARAMETERS

### -Path
The path to save the configuration file. Must be a FileInfo object with an .xml extension.

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
Overwrites the file if it exists.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

## NOTES
This cmdlet requires a connection to an OPNSense firewall. Use Connect-OPNSense to establish a connection.
If the directory specified in the Path parameter does not exist, it will be created.

## RELATED LINKS

[Get-OPNSenseConfig](Get-OPNSenseConfig.md)
[Import-OPNSenseConfig](Import-OPNSenseConfig.md)
[Restore-OPNSenseConfig](Restore-OPNSenseConfig.md)
