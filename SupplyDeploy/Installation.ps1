# Execute command before use
# Set-ExecutionPolicy RemoteSigned -Force | Out-Null

$ErrorActionPreference = "Stop"

Write-Host " > Installing features"

$features = @(
'IIS-WebServerRole',
'IIS-WebServer',
'IIS-CommonHttpFeatures',
'IIS-HttpErrors',
'IIS-ApplicationDevelopment',
'IIS-HealthAndDiagnostics',
'IIS-HttpLogging',
'IIS-Security',
'IIS-RequestFiltering',
'IIS-Performance',
'IIS-WebServerManagementTools',
'IIS-StaticContent',
'IIS-DefaultDocument',
'IIS-DirectoryBrowsing',
'IIS-HttpCompressionStatic',
'IIS-ManagementConsole',
'IIS-WindowsAuthentication')

foreach ($feature in $features)
{
	$currentFeature = Get-WindowsOptionalFeature -Online -FeatureName $feature
	if ($currentFeature.State -ne "Enabled")
	{
		$ProgressPreference = 'SilentlyContinue'
		Enable-WindowsOptionalFeature -Online -FeatureName $feature | Out-Null
		$ProgressPreference = 'Continue'
	}
	Write-Host "    - $($currentFeature.DisplayName) - ok"
}

Write-Host
Write-Host " > Enabling WinRM"
$connection = Get-NetConnectionProfile | where {$_.IPv4Connectivity -eq "Internet"}
if ($connection.NetworkCategory -eq "Public")
{
	Write-Host "    - Changing connection to private"
	$connection | Set-NetConnectionProfile -NetworkCategory Private
}
Write-Host "    - Enabling PSRemoting"
Enable-PSRemoting -Force | Out-Null
Write-Host "    - Enabling automatic start service"
Set-Service WinRM -StartMode Automatic

$inDomain = (gwmi win32_computersystem).partofdomain;
if ($inDomain -eq $false)
{
	Write-Host "    - Disabling filter policy for local accounts"
	reg add HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v LocalAccountTokenFilterPolicy /t REG_DWORD /d 1 /f | Out-Null
}

$bindings = Get-WebBinding -Name 'Default Web Site'
if (($bindings.protocol -eq "https") -eq $false)
{
	Import-Module WebAdministration
	
	Write-Host "    - Adding self signed certificate"
	$loc = Get-Location
	Set-Location IIS:\SslBindings
	New-WebBinding -Name "Default Web Site" -IP "*" -Port 443 -Protocol https
	$cert = New-SelfSignedCertificate -FriendlyName "https" -DnsName "$env:COMPUTERNAME" -CertStoreLocation cert:\LocalMachine\My
	$cert | New-Item 0.0.0.0!443 | Out-Null
	Set-Location $loc
}

if ($inDomain -eq $false)
{
	Write-Host "    - Adding trusted hosts"
	$trustedHosts = Get-Item wsman:\localhost\Client\TrustedHosts
	winrm set winrm/config/client '@{TrustedHosts="*"}' | Out-Null
	
	Write-Host
	Write-Host "Trusted hosts: $($trustedHosts.Value)"
}

Write-Host
Write-Host "Now install ASP.NET Hosting bundle and change self signed certificate"
# https://download.visualstudio.microsoft.com/download/pr/bf3abcc3-5461-451c-9dd6-b74491cf0eed/84775adc7e46888289477b5c72e691fd/dotnet-hosting-5.0.12-win.exe