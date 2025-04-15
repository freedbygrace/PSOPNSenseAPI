# Connect-OPNSense

## SYNOPSIS
Connects to an OPNSense firewall.

## SYNTAX

```
Connect-OPNSense -Server <String> -ApiKey <String> -ApiSecret <String> [-SkipCertificateCheck] [-Force]
```

## DESCRIPTION
The Connect-OPNSense cmdlet establishes a connection to an OPNSense firewall using the API.
This cmdlet must be called before using any other cmdlets in the module.

## EXAMPLES

### Example 1: Connect to an OPNSense firewall
```powershell
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"
```

This example connects to an OPNSense firewall at the specified URL using the provided API credentials.

### Example 2: Connect to an OPNSense firewall with certificate validation disabled
```powershell
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck
```

This example connects to an OPNSense firewall and skips SSL certificate validation.

## PARAMETERS

### -Server
The URL of the OPNSense firewall.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApiKey
The API key for authentication.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApiSecret
The API secret for authentication.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipCertificateCheck
Skips SSL certificate validation.

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

### -Force
Forces a reconnection if already connected.

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
You must have API access enabled on your OPNSense firewall and have created an API key and secret.

## RELATED LINKS

[Disconnect-OPNSense](Disconnect-OPNSense.md)
[Get-OPNSenseConnection](Get-OPNSenseConnection.md)
