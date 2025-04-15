# Troubleshooting Guide

This document provides troubleshooting information for common issues with the PSOPNSenseAPI module.

## Connection Issues

### Unable to Connect to OPNSense Firewall

**Symptoms:**
- `Connect-OPNSense` fails with connection errors
- Error messages about unreachable hosts or connection timeouts

**Possible Causes and Solutions:**

1. **Incorrect URL:**
   - Ensure the URL is correct and includes the protocol (https://)
   - Verify there are no typos in the domain name or IP address

   ```powershell
   # Correct format
   Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"
   ```

2. **Network Connectivity:**
   - Verify that you can reach the OPNSense firewall from your computer
   - Try pinging the firewall or accessing the web interface in a browser

   ```powershell
   # Test connectivity
   Test-NetConnection -ComputerName "firewall.example.com" -Port 443
   ```

3. **Certificate Issues:**
   - If the firewall uses a self-signed certificate, use the `-SkipCertificateCheck` parameter

   ```powershell
   Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck
   ```

4. **Firewall Rules:**
   - Check if there are any firewall rules blocking access to the OPNSense web interface
   - Ensure that port 443 (HTTPS) is open for your IP address

5. **Proxy Settings:**
   - If you're behind a proxy, configure PowerShell to use the proxy

   ```powershell
   # Configure proxy
   [System.Net.WebRequest]::DefaultWebProxy = New-Object System.Net.WebProxy("http://proxy.example.com:8080")
   [System.Net.WebRequest]::DefaultWebProxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials
   ```

### Authentication Failures

**Symptoms:**
- `Connect-OPNSense` fails with authentication errors
- Error messages about invalid credentials or unauthorized access

**Possible Causes and Solutions:**

1. **Incorrect API Key or Secret:**
   - Verify that the API key and secret are correct
   - Regenerate the API key and secret if necessary

2. **API Access Not Enabled:**
   - Ensure that API access is enabled for the user
   - Check the user's privileges in the OPNSense web interface

3. **User Permissions:**
   - Verify that the user has the necessary privileges for the operations you're trying to perform
   - Consider using a user with administrative privileges for testing

4. **API Key Expiration:**
   - Some OPNSense configurations may have API key expiration
   - Generate a new API key if the current one has expired

## Module Loading Issues

### Module Not Found

**Symptoms:**
- `Import-Module PSOPNSenseAPI` fails with module not found errors
- Commands from the module are not recognized

**Possible Causes and Solutions:**

1. **Module Not Installed:**
   - Verify that the module is installed

   ```powershell
   # Check if the module is installed
   Get-Module -Name PSOPNSenseAPI -ListAvailable
   ```

2. **Module Not in PSModulePath:**
   - Check if the module is installed in a valid module path

   ```powershell
   # List module paths
   $env:PSModulePath -split ';'
   ```

3. **Module Version Mismatch:**
   - Check if you have multiple versions of the module installed

   ```powershell
   # List all versions of the module
   Get-Module -Name PSOPNSenseAPI -ListAvailable | Select-Object Name, Version
   ```

4. **PowerShell Version Compatibility:**
   - Verify that you're using a compatible PowerShell version
   - The module requires PowerShell 5.1 or later

   ```powershell
   # Check PowerShell version
   $PSVersionTable
   ```

### Module Loading Errors

**Symptoms:**
- `Import-Module PSOPNSenseAPI` fails with loading errors
- Error messages about missing dependencies or assembly loading failures

**Possible Causes and Solutions:**

1. **Missing Dependencies:**
   - Ensure that all dependencies are installed
   - The module requires .NET Framework 4.7.2 or later for Windows PowerShell

2. **Assembly Loading Issues:**
   - Try loading the module with verbose output to identify the specific issue

   ```powershell
   # Load module with verbose output
   Import-Module -Name PSOPNSenseAPI -Verbose
   ```

3. **File Corruption:**
   - Reinstall the module to ensure all files are intact

   ```powershell
   # Uninstall and reinstall the module
   Uninstall-Module -Name PSOPNSenseAPI -Force
   Install-Module -Name PSOPNSenseAPI -Force
   ```

## Command Execution Issues

### Command Fails with Error

**Symptoms:**
- Commands fail with error messages
- Unexpected results or behavior

**Possible Causes and Solutions:**

1. **Not Connected to Firewall:**
   - Ensure that you're connected to the firewall before running commands

   ```powershell
   # Check if connected
   if (-not $OPNSenseSessionState.Instance.ApiClient) {
       Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"
   }
   ```

2. **Insufficient Permissions:**
   - Verify that the user has the necessary privileges for the operation
   - Check the error message for permission-related issues

3. **Invalid Parameters:**
   - Check that you're using the correct parameters and values
   - Use `Get-Help` to see the required and optional parameters

   ```powershell
   # Get help for a command
   Get-Help -Name New-OPNSenseFirewallRule -Full
   ```

4. **API Limitations:**
   - Some operations may not be supported by the OPNSense API
   - Check the OPNSense API documentation for supported operations

5. **Firewall Configuration:**
   - The current state of the firewall may prevent certain operations
   - Check the OPNSense logs for more information

### Changes Not Applied

**Symptoms:**
- Commands appear to succeed, but changes are not reflected on the firewall
- Configuration changes don't persist after a firewall reboot

**Possible Causes and Solutions:**

1. **Apply Changes Not Called:**
   - Many commands require an explicit call to apply the changes

   ```powershell
   # Apply changes after making configuration changes
   Apply-OPNSenseFirewallChanges
   ```

2. **Caching Issues:**
   - The OPNSense web interface may be showing cached data
   - Refresh the web interface or wait a few moments

3. **Conflicting Configurations:**
   - Other configurations may be overriding your changes
   - Check for conflicting rules or settings

4. **Firewall Reboot Required:**
   - Some changes may require a firewall reboot to take effect

   ```powershell
   # Reboot the firewall
   Invoke-OPNSenseReboot
   ```

## Performance Issues

### Slow Command Execution

**Symptoms:**
- Commands take a long time to execute
- Timeouts or performance degradation

**Possible Causes and Solutions:**

1. **Network Latency:**
   - High latency between your computer and the firewall can slow down commands
   - Consider running commands from a location with better connectivity

2. **Firewall Load:**
   - The firewall may be under heavy load
   - Try running commands during periods of lower activity

3. **Large Data Sets:**
   - Commands that retrieve large amounts of data may be slow
   - Use filtering parameters to limit the data returned

   ```powershell
   # Filter results to reduce data
   Get-OPNSenseFirewallRule -Interface "lan"
   ```

4. **Inefficient Scripts:**
   - Scripts that make many individual API calls can be slow
   - Batch operations where possible and minimize the number of API calls

## Logging and Debugging

### Enabling Verbose Logging

To troubleshoot issues, you can enable verbose logging:

```powershell
# Enable verbose output
$VerbosePreference = "Continue"

# Run commands with verbose output
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -Verbose
```

### Capturing HTTP Traffic

For advanced troubleshooting, you can capture the HTTP traffic between the module and the OPNSense firewall:

```powershell
# Enable Fiddler capture
[System.Net.WebRequest]::DefaultWebProxy = New-Object System.Net.WebProxy("http://localhost:8888")
[System.Net.WebRequest]::DefaultWebProxy.BypassProxyOnLocal = $false
```

Note: This requires [Fiddler](https://www.telerik.com/fiddler) or a similar HTTP debugging proxy to be installed and running.

### Checking OPNSense Logs

Check the OPNSense logs for additional information:

1. Log in to the OPNSense web interface
2. Navigate to **System > Log Files > General**
3. Look for entries related to the API or the operations you're performing

## Common Error Messages

### "Not connected to an OPNSense firewall"

**Cause:** You're trying to run a command without first connecting to the firewall.

**Solution:** Connect to the firewall before running commands:

```powershell
Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"
```

### "The remote server returned an error: (401) Unauthorized"

**Cause:** The API key or secret is incorrect, or the user doesn't have the necessary privileges.

**Solution:** Verify the API key and secret, and check the user's privileges.

### "The remote server returned an error: (404) Not Found"

**Cause:** The requested resource or API endpoint doesn't exist.

**Solution:** Check that you're using the correct command and parameters, and that the resource exists on the firewall.

### "The remote server returned an error: (500) Internal Server Error"

**Cause:** An error occurred on the OPNSense firewall while processing the request.

**Solution:** Check the OPNSense logs for more information, and verify that the firewall is functioning correctly.

### "Unable to find type [System.Net.IPNetwork.IPNetwork]"

**Cause:** The IPNetwork2 assembly is not loaded or is missing.

**Solution:** Reinstall the module to ensure all dependencies are properly installed.

## Getting Help

If you're still experiencing issues after trying the troubleshooting steps above:

1. Check the [GitHub repository](https://github.com/freedbygrace/PSOPNSenseAPI) for known issues and solutions
2. Open an issue on GitHub with detailed information about the problem
3. Include the following information in your issue:
   - PSOPNSenseAPI module version
   - PowerShell version
   - OPNSense version
   - Error messages (with sensitive information redacted)
   - Steps to reproduce the issue
