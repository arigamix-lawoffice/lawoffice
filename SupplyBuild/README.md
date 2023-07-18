# SupplyBuild
Util for creation supply achives via command line.

### How to use it
1. Check version of powershell via powershell command `$PSVersionTable.PSVersion` (open powershell.exe and execute it). It must be at least 5 major version.
   1. If version below 5 (default for Windows 7 or below), install Powershell V5 via link https://www.microsoft.com/en-us/download/details.aspx?id=54616
1. Enable PowerShell execution policy via command: `Set-ExecutionPolicy RemoteSigned`.
1. Copy `SupplyBuild.sample.bat` and rename to `SupplyBuild.bat`.
1. Edit `manifest.json` file with Notepad++, enabling all features you need.
   1. If you need RMR client build, so Android Studio must be installed. Check README.md in RMR folder for detailed info.
   1. If you need FullTextIndex, so maven (https://maven.apache.org/download.cgi) and JDK 14 (https://soft.uclv.edu.cu/Java.SE.Development.Kit/14/jdk-14.0.1_windows-x64_bin.exe) must be installed.
   1. Unzip maven archive to `C:\Maven` and add this path to `PATH` param.
   1. If you have maven and Android Studio it would be failed to compile. Open `C:\Maven\bin\mvn.cmd` and add line with your JDK (for example: `set JAVA_HOME=C:\Program Files\Java\jdk-14.0.1`) after line `@REM ==== START VALIDATION ====`.