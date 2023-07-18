# SupplyDeploy
Util for deploy tessa via command line.

### How to use it
1. Check version of powershell via powershell command `$PSVersionTable.PSVersion` (open powershell.exe and execute it). It must be at least 5 major version.
   1. If version below 5 (default for Windows 7 or below), install Powershell V5 via link https://www.microsoft.com/en-us/download/details.aspx?id=54616
1. Enable PowerShell execution policy via command: `Set-ExecutionPolicy RemoteSigned`.
1. Setup setting at file `SupplyConfiguration\configuration.json` (just copy file to `configuration_localhost.json`).
1. Copy `SupplyDeploy.sample.bat` and rename to `SupplyDeploy.bat`.
1. Edit bat file with Notepad++, provide path to settings file.
1. Enable WinRM, executing commands with cmd from adminitrator.
   1. Execute `winrm set winrm/config/client '@{TrustedHosts="Computer1,Computer2"}'` (only client, if computer is not in domain, could be used `*` instead using computer name)
   1. Execute `Enable-PSRemoting -Force` (only server)
   1. Execute `winrm quickconfig`
   1. Execute `Set-Service WinRM -StartMode Automatic` (only server)
   1. Reboot remote computer.
1. Run bat file from administrator.

### Typical Problems
1. `Access denied` when connecting to remote computer.
   1. Reboot remote computer.
   1. Check firewall (try to start script from localhost).
   1. Execute from powershell `reg add HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v LocalAccountTokenFilterPolicy /t REG_DWORD /d 1 /f` and reboot again.