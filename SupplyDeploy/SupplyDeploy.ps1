param(
	#Путь к папке поставки тессы
	[Parameter(Mandatory=$true)][string]$SupplyPath,
	#Название файла поставки
	[Parameter(Mandatory=$true)][string]$ConfigurationPath,
	#Путь к файлу лицензии
	[AllowEmptyString()][string]$LicensePath = $null,
	#Перестраивать ли индексы
	[Parameter(Mandatory=$false)][bool]$RebuildIndexes = $true,
	#Название файла манифеста
	[string]$ManifestPath = "manifest.json",
	#Название файла дополнительных действий
	[string]$AdditionalActionsPath = "actions.json",
	#Название файла c выполненными действиями
	[string]$StateActionsPath = "state.json",
	#Название файла cостояния текущей установки
	[string]$SupplyStatePath = $null,
	#Полная установка, если возможна
	[bool]$FullInstall = $true,
	#Всегда начинаем установку сначала
	[bool]$IgnoreState = $false,
	#Не запрашиваем подтверждение продолжения
	[bool]$SilentContinue = $false
)

$ErrorActionPreference = "Stop"

#-------------------------------------------------------------------------------

function Format-Json([Parameter(Mandatory, ValueFromPipeline)][String] $json) {
	$indent = 0;
	($json -Split "`n" | % {
		if ($_ -match '[\}\]]\s*,?\s*$') {
			# This line ends with ] or }, decrement the indentation level
			$indent--
		}
		$line = ("`t" * $indent) + $($_.TrimStart() -replace '":  (["{[])', '": $1' -replace ':  ', ': ')
		if ($_ -match '[\{\[]\s*$') {
			# This line ends with [ or {, increment the indentation level
			$indent++
		}
		$line
	}) -Join "`n"
}

function Validate-Json([String] $json) {
	try
	{
		$settingsJson = $json | ConvertFrom-Json;
	}
	catch [System.ArgumentException]
	{
		throw [System.ArgumentException]::new($PSItem.Exception.Message.Split('{')[0], $PSItem.Exception.InnerException)
	}
}

enum ApplyMode
{
	Add
	Remove
	Replace
}

enum LoginType
{
	Login
	Windows
	Request
	Ssh
}

enum DeployGroup
{
	Before
	Binary
	Configuration
	After
}

enum DeployTarget
{
	Configuration
	Server
	Chronos
}

enum DeployPlatform
{
	Windows
	Linux
}

enum DeployDatabase
{
	None
	MSSQL
	PostgreSQL
}

enum DeployMode
{
	Diff
	Full
	All
}

enum InstallMode
{
	All
	New
	Upgrade
}

enum MigrationType
{
	Migration
	Script
}

enum MigrationOrder
{
	Pre
	Post
	Final
}

class Context
{
	[Settings]$Settings;
	[BuildModules]$Modules;
	[MigrationModules]$Migrations;
	[string]$SupplyPath
	[string]$ToolsPath;
	[string]$TadminPath;
	[string]$LogPath;
	[int]$Version;
	[DeployDatabase]$DatabaseType = [DeployDatabase]::None;
	[string]$VersionString;
	[int]$CurrentVersion = -1;
	[string]$CurrentVersionString;
	[DeployMode]$DeployMode = [DeployMode]::All;
	[InstallMode]$InstallMode = [InstallMode]::All;
	[object]$Output = $null;
	[ServerContext]$ServerContext = $null;
	[State]$State = [State]::new();
	[string]$StateActionsPath;
	[System.PlatformID]$Platform = [System.Environment]::OSVersion.Platform;
	[bool]$UsePwsh;
	[System.Collections.Generic.List[string]]$NewActions = [System.Collections.Generic.List[string]]::new();
	[System.Collections.Generic.List[string]]$ExecutedActions = [System.Collections.Generic.List[string]]::new();
	[System.Management.Automation.Runspaces.PSSession]$TadminSession = $null;
	[System.Collections.Generic.Dictionary[string,object]]$Params = [System.Collections.Generic.Dictionary[string,object]]::new();
	
	Context([Settings]$settings, [Manifest]$manifest, [string]$supplyPath, [string]$stateActionsPath, [string]$additionalActionsPath, [string]$supplyStatePath,[bool]$fullInstall)
	{
		$this.StateActionsPath = $stateActionsPath
		
		if ([System.IO.File]::Exists($stateActionsPath))
		{
			$this.ExecutedActions = $this.LoadActionState()
		}
		
		$h = Get-Host
		$this.UsePwsh = $h.Version.Major -gt 5
		$this.NewActions = $manifest.ApplyManifest($additionalActionsPath, $this.ExecutedActions, [System.IO.Path]::Combine($supplyPath, "Actions\actions.json"));
		
		$this.Settings = $settings;
		$this.Modules = $manifest.Modules;
		$this.Migrations = $manifest.Migrations;
		$this.SupplyPath = $supplyPath;
		$this.State.Path = $supplyStatePath;
		
		$platformPath = $this.GetPlatformPath();
		$this.Params.Add("Platform", $platformPath)
		$this.ToolsPath = [System.IO.Path]::Combine($supplyPath, $platformPath, "Tools");
		$this.TadminPath = [System.IO.Path]::Combine($this.ToolsPath, "tadmin.exe");
		$this.LogPath = [System.IO.Path]::Combine($this.ToolsPath, "log.txt");
		
		$this.VersionString = & $this.TadminPath BuildVersion
		$this.Version = $this.GetVersionNumber($this.VersionString);
		
		foreach ($key in $settings.Params.Keys)
		{
			if ($this.Params.ContainsKey($key))
			{
				throw [System.SystemException] "Duplicate key $key in params section!"
			}
			$this.Params.Add($key, $settings.Params[$key])
		}
		
		$manifestPath = [System.IO.Path]::Combine($supplyPath, "manifest.json");
		$manifestJson = Get-Content $manifestPath | ConvertFrom-Json;
		$this.State.Hash = $manifestJson.Revision;
		if ([bool]::Parse($manifestJson.IsFullSupply) -eq $false -Or $fullInstall -eq $false)
		{
			$this.DeployMode = [DeployMode]::Diff;
		}
		else
		{
			$this.DeployMode = [DeployMode]::Full;
		}
		
		if ([string]::IsNullOrEmpty($this.Settings.SignatureKey) -eq $true)
		{
			$this.Settings.SignatureKey = & $this.TadminPath GetKey Signature
		}
		if ([string]::IsNullOrEmpty($this.Settings.ChipherKey) -eq $true)
		{
			$this.Settings.ChipherKey = & $this.TadminPath GetKey Cipher
		}
	}
	
	[int]GetVersionNumber([string]$version)
	{
		if ($version -eq "N/A")
		{
			return -1;
		}
		$major,$minor,$build = $version.split('.')
		if ([int]$major -eq 1)
		{
			$major = [int]$major + 3
			$minor = [int]$minor + 7
		}
		return [int]("{0:0}{1:00}{2:00}" -f [int]$major, [int]$minor, [int]$build)
	}
	
	[void]Debug([string]$message)
	{
		if ($this.Settings.Debug)
		{
			Write-Host $message
		}
	}
	
	[System.Collections.Generic.List[string]]LoadActionState()
	{
		$json = Get-Content $this.StateActionsPath | ConvertFrom-Json;
		if ($json -eq $null)
		{
			return [System.Collections.Generic.List[string]]::new();
		}
		return [System.Collections.Generic.List[string]]$json
	}
	
	[void]SaveActionState()
	{
		$this.NewActions | ConvertTo-Json -Depth 100 | Format-Json | Out-File $this.StateActionsPath -Encoding UTF8
	}
	
	[string]ReplaceVariables([string]$message)
	{
		if ([string]::IsNullOrEmpty($message) -eq $true)
		{
			return $message;
		}
		
		foreach($key in $this.Params.Keys)
		{
			$message = $message.Replace("`$$key", $this.Params[$key])
		}
		return $message;
	}
	
	[object]GetParam([string]$key)
	{
		if ($this.Params.ContainsKey($key))
		{
			return $this.Params[$key];
		}
		return $null;
	}
	
	[string]ReplaceDbms([string]$name)
	{
		return $name.Replace(".dbms.", ".$($this.GetDbType()).");
	}
	
	[string]GetDbType()
	{
		switch($this.DatabaseType)
		{
			None
			{
				return "none"
			}
			MSSQL
			{
				return "ms"
			}
			PostgreSQL
			{
				return "pg"
			}
		}
		return "none"
	}
	
	[string]GetPlatformPath()
	{
		switch($this.Platform)
		{
			Win32S
			{
				return "Windows"
			}
			Win32Windows
			{
				return "Windows"
			}
			Win32NT
			{
				return "Windows"
			}
			WinCE
			{
				return "Windows"
			}
			Unix
			{
				return "Linux"
			}
		}
		return "Windows"
	}
}

class State
{
	[string]$Hash = $null
	[string]$Path = $null;
	[int]$CurrentBlock;
	[int]$CurrentGroup;
	[int]$CurrentAction;
	
	[int]$LastBlock = -1;
	[int]$LastGroup = -1;
	[int]$LastAction = -1;
	
	[bool]$Fallback = $false;
	[string]$FallbackName;
	[System.Collections.Generic.List[string]]$FiredFallbacks = [System.Collections.Generic.List[string]]::new();
	[System.Collections.Generic.Dictionary[string,int]]$Groups = [System.Collections.Generic.Dictionary[string,int]]::new();
	
	[void]Load([System.Collections.Generic.Dictionary[string,object]]$params)
	{
		$stateJson = Get-Content $this.Path | ConvertFrom-Json;
		if ($this.Hash -ne [string]$stateJson.Hash)
		{
			Write-Host "Supply hash $($stateJson.Hash) does not match state hash $($stateJson.Hash)"
			return;
		}
		
		$this.LastBlock = [int]$stateJson.Block
		$this.LastGroup = [int]$stateJson.Group
		$this.LastAction = [int]$stateJson.Action
		
		foreach ($property in $stateJson.Params.PSObject.Properties)
		{
			if ($params.ContainsKey($property.Name))
			{
				continue
			}
			$params.Add($property.Name, $property.Value)
		}
	}
	
	[void]Save([System.Collections.Generic.Dictionary[string,object]]$params)
	{
		$st = [ordered]@{}
		$st["Hash"] = $this.Hash
		$st["Block"] = $this.CurrentBlock
		$st["Group"] = $this.CurrentGroup
		$st["Action"] = $this.CurrentAction
		$st["Params"] = $params
		$st | ConvertTo-Json -Depth 100 | Format-Json | Out-File $this.Path -Encoding UTF8
	}
	
	[void]Remove()
	{
		Remove-Item $this.Path -Force
	}
	
	[void]ResetBlock()
	{
		$this.CurrentBlock = 0
	}
	
	[void]ResetGroup()
	{
		$this.CurrentGroup = 0
		$this.Groups.Clear()
	}
	
	[void]SetGroup([int]$i)
	{
		$this.CurrentGroup = $i
	}
	
	[void]ResetAction()
	{
		$this.CurrentAction = 0
	}
	
	[void]IncreaseBlock()
	{
		$this.CurrentBlock++
	}
	
	[void]IncreaseGroup([string]$name)
	{
		$this.Groups[$name] = $this.CurrentGroup
		$this.CurrentGroup++
	}
	
	[void]IncreaseAction()
	{
		$this.CurrentAction++
	}
	
	[bool]SkipAction()
	{
		if ($this.LastBlock -gt $this.CurrentBlock)
		{
			return $true;
		}
		if ($this.LastBlock -eq $this.CurrentBlock -And $this.LastGroup -gt $this.CurrentGroup)
		{
			return $true;
		}
		if ($this.LastBlock -eq $this.CurrentBlock -And $this.LastGroup -eq $this.CurrentGroup -And $this.LastAction -gt $this.CurrentAction)
		{
			return $true;
		}
		return $false
	}
	
	[void]SetFallback([bool]$fallback, [string]$fallbackName)
	{
		$this.Fallback = $fallback
		$this.FallbackName = $fallbackName
		$this.FiredFallbacks.Add($fallbackName)
	}
	
	[string]GetFallbackName()
	{
		return $this.FallbackName
	}
	
	[bool]IsFallback()
	{
		return $this.Fallback
	}
	
	[int]GetFallbackIndex([string]$name)
	{
		if ($this.Groups.ContainsKey($name))
		{
			return $this.Groups[$name] - 1;
		}
		else
		{
			return -1;
		}
	}
}

class ServerContext
{
	[Server]$Server = $null;
	[ServerModule]$Module = $null;
	[System.Management.Automation.Runspaces.PSSession]$Session = $null;
	[System.Collections.Generic.Dictionary[string,object]]$Params = [System.Collections.Generic.Dictionary[string,object]]::new();
	
	ServerContext([Context]$context, [Server]$server)
	{
		$this.Server = $server;
		$this.Params.Add("server", $server.Address)
		
		foreach ($key in $context.Params.Keys)
		{
			if ($this.Params.ContainsKey($key))
			{
				throw [System.SystemException] "Duplicate key $key in credential section!"
			}
			$this.Params.Add($key, $context.Params[$key])
		}
	}
	
	[void]InitParams()
	{
		foreach ($key in $this.Module.Params.Keys)
		{
			$this.Params[$key] = $this.Module.Params[$key]
		}
		
		if ($this.Server.Type -eq [DeployPlatform]::Windows)
		{
			$IISPath = Invoke-Command -Session $this.Session -ScriptBlock `
			{
				if ((Get-Module -ListAvailable -Name WebAdministration))
				{
					Import-Module WebAdministration
					
					$iisWebsite = Get-WebFilePath "IIS:\Sites\Default Web Site"
					
					if ($iisWebsite -eq $null)
					{
						return $null
					}
					return $iisWebsite.FullName
				}
				else
				{
					return "";
				}
			}
			$this.Params.Add("IISPath", $IISPath)
			
			if ($this.Module.Type -eq [DeployTarget]::Chronos)
			{
				$chronosPath = Invoke-Command -Session $this.Session -ScriptBlock `
				{
					param($instanceName, $useSystemDisk)
					$root = ""
					if ($useSystemDisk -eq $false -And (Test-Path "D:\"))
					{
						$root = "D:\"
					}
					else
					{
						$root = "C:\"
					}
					$tessaDir = [System.IO.Path]::Combine($root, "Tessa");
					$instanceDir = [System.IO.Path]::Combine($tessaDir, $instanceName);
					$executableDir = [System.IO.Path]::Combine($instanceDir, "Chronos");
					[System.IO.Directory]::CreateDirectory($executableDir) | Out-Null;
					return $executableDir;
				} -Args $this.Module.Params.FolderName, $this.Module.Params.UseSystemDisk
				$this.Params.Add("ChronosPath", $chronosPath)
			}
		}
		else
		{
			$homeDir = Invoke-Command -Session $this.Session -ScriptBlock `
			{
				return $home
			}
			$this.Params.Add("HomeDir", $homeDir)
			
			if ($this.Module.Type -eq [DeployTarget]::Chronos)
			{
				$chronosPath = [System.IO.Path]::Combine($homeDir, $this.Module.Params.FolderName, "Chronos")
				$this.Params.Add("ChronosPath", $chronosPath)
			}
		}
	}
	
	[string]ReplaceVariables([string]$message)
	{
		if ([string]::IsNullOrEmpty($message) -eq $true)
		{
			return $message;
		}
		
		foreach($key in $this.Params.Keys)
		{
			$message = $message.Replace("`$$key", $this.Params[$key])
		}
		return $message;
	}
	
	[object]GetParam([string]$key)
	{
		if ($this.Params.ContainsKey($key))
		{
			return $this.Params[$key];
		}
		return $null;
	}
}

class Manifest
{
	[BuildModules]$Modules;
	[MigrationModules]$Migrations;
	
	Manifest([PSCustomObject]$manifest)
	{
		$this.Migrations = [MigrationModules]::new($manifest.Migrations);
		$this.Modules = [BuildModules]::new($manifest.Modules);
	}
	
	[System.Collections.Generic.List[string]]ApplyManifest([string]$additionalActions, [System.Collections.Generic.List[string]]$executedActions, [string]$supplyActions)
	{
		$actionList = [System.Collections.Generic.List[string]]::new($executedActions);
		if ([string]::IsNullOrWhitespace($additionalActions) -eq $false -And [System.IO.File]::Exists($additionalActions))
		{
			$actions = Get-Content $additionalActions | ConvertFrom-Json;
			foreach ($block in $actions.Always)
			{
				$this.ApplyBlock($block)
			}
			foreach ($block in $actions.OneTime)
			{
				if ($executedActions.Contains($block.Name))
				{
					continue;
				}
				$actionList.Add($block.Name)
				$this.ApplyBlock($block.Actions)
			}
		}
		if ([string]::IsNullOrWhitespace($supplyActions) -eq $false -And [System.IO.File]::Exists($supplyActions))
		{
			$actions = Get-Content $supplyActions | ConvertFrom-Json;
			foreach ($block in $actions.Always)
			{
				$this.ApplyBlock($block)
			}
			foreach ($block in $actions.OneTime)
			{
				if ($executedActions.Contains($block.Name))
				{
					continue;
				}
				$actionList.Add($block.Name)
				$this.ApplyBlock($block.Actions)
			}
		}
		return $actionList
	}
	
	[void]ApplyBlock([PSCustomObject]$block)
	{
		$blk = [BuildActionBlock]::new($block)
		switch($blk.ApplyMode)
		{
			Add
			{
				$b = $this.Modules.Blocks | Where { $_.Name -eq $blk.Name } | Select -index 0
				if ($b -ne $null)
				{
					foreach ($group in $blk.Groups)
					{
						if ([string]::IsNullOrWhitespace($group.ApendTo) -eq $false)
						{
							for ($i=0; $i -lt $b.Groups.Count; $i++)
							{
								if ($b.Groups[$i].Name -eq $group.ApendTo)
								{
									switch($group.ApplyMode)
									{
										Add
										{
											foreach ($action in $group.Actions)
											{
												if ([string]::IsNullOrWhitespace($action.ApendTo) -eq $false)
												{
													for ($j=0; $j -lt $b.Groups[$i].Actions.Count; $j++)
													{
														if ($b.Groups[$i].Actions[$j].Name -eq $action.ApendTo)
														{
															switch($action.ApplyMode)
															{
																Add
																{
																	$b.Groups[$i].Actions.Insert($j+1, $action)
																}
																Remove
																{
																	$b.Groups[$i].Actions.RemoveAt($j)
																}
																Replace
																{
																	$b.Groups[$i].Actions.Insert($j+1, $action)
																	$b.Groups[$i].Actions.RemoveAt($j)
																}
																default
																{
																	throw [System.Exception] "Invalid apply mode $($action.ApplyMode) !"
																}
															}
															break;
														}
													}
												}
												else
												{
													$b.Groups[$i].Actions.Add($action)
												}
											}
										}
										Remove
										{
											$b.Groups.RemoveAt($i)
										}
										Replace
										{
											$b.Groups.Insert($i+1, $group)
											$b.Groups.RemoveAt($i)
										}
										default
										{
											throw [System.Exception] "Invalid apply mode $($group.ApplyMode) !"
										}
									}
									break;
								}
							}
						}
						else
						{
							$b.Groups.Add($group)
						}
					}
				}
				else
				{
					if ([string]::IsNullOrWhitespace($blk.ApendTo) -eq $false)
					{
						for ($i=0; $i -lt $this.Modules.Blocks.Count; $i++)
						{
							if ($this.Modules.Blocks[$i].Name -eq $blk.ApendTo)
							{
								$this.Modules.Blocks.Insert($i+1, $blk)
								break;
							}
						}
					}
					else
					{
						$this.Modules.Blocks.Add($blk)
					}
				}
			}
			Remove
			{
				$b = $this.Modules.Blocks | Where { $_.Name -eq $blk.Name } | Select -index 0
				if ($b -ne $null)
				{
					$this.Modules.Blocks.Remove($b)
				}
			}
			Replace
			{
				$b = $this.Modules.Blocks | Where { $_.Name -eq $blk.Name } | Select -index 0
				$b.Groups.Clear();
				foreach ($action in $blk.Groups)
				{
					$b.Groups.Add($action)
				}
				$b.Enabled = $blk.Enabled;
				$b.SaveState = $blk.SaveState;
				$b.DeployMode = $blk.DeployMode
				$b.DeployGroup = $blk.DeployGroup
				$b.MinVersion = $blk.MinVersion
				$b.MaxVersion = $blk.MaxVersion
				$b.Depends = $blk.Depends
			}
			default
			{
				throw [System.Exception] "Invalid apply mode $($blk.ApplyMode) !"
			}
		}
	}
}

class Credential
{
	[int]$Id;
	[LoginType]$Type
	[System.Collections.Generic.Dictionary[string,object]]$Params
	
	Credential([PSCustomObject]$credential)
	{
		$this.Id = [int]$credential.Id
		$this.Type = [LoginType]$credential.Type
		$this.Params = [System.Collections.Generic.Dictionary[string,object]]::new();
		foreach ($property in $credential.Params.PSObject.Properties)
		{
			if ($this.Params.ContainsKey($property.Name))
			{
				throw [System.SystemException] "Duplicate key $($property.Name) in credential section!"
			}
			$this.Params.Add($property.Name, $property.Value)
		}
	}
}

class Settings
{
	[string]$Address;
	[string]$Login;
	[string]$Password;
	[bool]$Backup;
	[string]$SignatureKey;
	[string]$ChipherKey;
	[bool]$UseFullTextSearch;
	[bool]$Debug;
	[bool]$RebuildIndexes;
	[string]$LicensePath;
	[int]$TadminCredentialId;
	[System.Collections.Generic.Dictionary[int,Credential]]$Credentials;
	[System.Collections.Generic.List[Server]]$Servers;
	[System.Collections.Generic.Dictionary[string,string]]$Variables
	[System.Collections.Generic.Dictionary[string,object]]$Params;
	
	Settings([PSCustomObject]$settings, [bool]$rebuildIndexes, [string]$licensePath)
	{
		$this.Address = $settings.Settings.Address
		$this.Login = $settings.Settings.Login
		$this.Password = $settings.Settings.Password
		$this.Backup = [bool]::Parse($settings.Settings.Backup)
		$this.SignatureKey = $settings.Settings.SignatureKey
		$this.ChipherKey = $settings.Settings.ChipherKey
		$this.UseFullTextSearch = [bool]::Parse($settings.Settings.UseFullTextSearch)
		$this.Debug = [bool]::Parse($settings.Settings.Debug)
		$this.RebuildIndexes = $rebuildIndexes
		$this.LicensePath = $licensePath
		$this.TadminCredentialId = [int]$settings.Settings.TadminCredentialId
		$this.Credentials = $this.GetCredentials($settings.Settings.Credentials)
		$this.Servers = $this.GetServers($settings.Servers, $this.Credentials)
		$this.Variables = $this.GetReplacements($settings.Variables)
		$this.Params = [System.Collections.Generic.Dictionary[string,object]]::new();
		foreach ($property in $settings.Settings.Params.PSObject.Properties)
		{
			$this.Params[$property.Name] = $property.Value
		}
	}
	
	[System.Collections.Generic.Dictionary[int,Credential]]GetCredentials([PSCustomObject]$credentials)
	{
		$values = [System.Collections.Generic.Dictionary[int,Credential]]::new();
		foreach($credential in $credentials)
		{
			$crd = [Credential]::new($credential);
			$values.Add($crd.Id, $crd);
		}
		return $values;
	}
	
	[System.Collections.Generic.List[Server]]GetServers([PSCustomObject]$servers, [System.Collections.Generic.Dictionary[int,Credential]]$credentials)
	{
		$values = [System.Collections.Generic.List[Server]]::new()
		foreach($server in $servers)
		{
			$srv = [Server]::new($server);
			if ($credentials.ContainsKey($srv.CredentialId) -eq $false)
			{
				throw [System.SystemException] "Unknown credential id $($srv.CredentialId) for server $($srv.Address)"
			}
			$values.Add($srv);
		}
		return $values;
	}
	
	[System.Collections.Generic.Dictionary[string,string]]GetReplacements([PSCustomObject]$variables)
	{
		$regex = [regex] '(__\w+__)'
		# Загрузка значений для замены
		$values = [System.Collections.Generic.Dictionary[string,string]]::new();
		foreach ($variable in $variables)
		{
			if ($values.ContainsKey($variable.Name))
			{
				throw [System.SystemException] "Duplicate key $($variable.Name) in variables section!"
			}
			$match = $regex.Match($variable.Name);
			if ($match.Success -eq $false)
			{
				throw [System.SystemException] "Key $($variable.Name) has invalid chars!"
			}
			$values.Add($variable.Name, $variable.Value);
		}
		return $values;
	}
	
	[Credential]GetCredential([int]$id)
	{
		if ($this.Credentials.ContainsKey($id))
		{
			return $this.Credentials[$id];
		}
		throw [System.SystemException] "Invalid credentialId $id !"
	}
}

class Server
{
	[string]$Address
	[int]$CredentialId
	[DeployPlatform]$Type = [DeployPlatform]::Windows
	[System.Collections.Generic.List[ServerModule]]$Modules
	
	Server([PSCustomObject]$server)
	{
		$this.Address = $server.Address
		$this.CredentialId = $server.CredentialId
		if ($server.ServerType -ne $null)
		{
			$this.Type = [DeployPlatform]$server.ServerType
		}
		$this.Modules = [System.Collections.Generic.List[ServerModule]]::new()
		foreach ($module in $server.Modules)
		{
			$this.Modules.Add([ServerModule]::new($module))
		}
	}
}

class ServerModule
{
	[bool]$Enabled;
	[DeployTarget]$Type = [DeployTarget]::Server
	[System.Collections.Generic.Dictionary[string,object]]$Params;
	
	ServerModule([PSCustomObject]$module)
	{
		$this.Enabled = [bool]::Parse($module.Enabled);
		if ($module.Type -ne $null)
		{
			$this.Type = [DeployTarget]$module.Type
		}
		$this.Params = [System.Collections.Generic.Dictionary[string,object]]::new();
		foreach ($property in $module.Params.PSObject.Properties)
		{
			$this.Params[$property.Name] = $property.Value
		}
	}
}

class MigrationModules
{
	[System.Collections.Generic.List[MigrationGroup]]$Groups;
	
	MigrationModules([PSCustomObject]$migrations)
	{
		$this.Groups = [System.Collections.Generic.List[MigrationGroup]]::new()
		foreach ($migration in $migrations)
		{
			$this.Groups.Add([MigrationGroup]::new($migration))
		}
	}
}

class MigrationGroup
{
	[string]$Name;
	[string]$Message;
	[bool]$Enabled;
	[int]$Version
	[MigrationType]$Type = [MigrationType]::Migration;
	[MigrationOrder]$Order = [MigrationOrder]::Pre;
	[System.Collections.Generic.List[BuildAction]]$Actions;
	
	MigrationGroup([PSCustomObject]$migration)
	{
		$this.Name = $migration.Name;
		$this.Message = $migration.Message;
		$this.Enabled = [bool]::Parse($migration.Enabled);
		$this.Version = [int]$migration.Version;
		if ($migration.Type -ne $null)
		{
			$this.Type = [MigrationType]$migration.Type
		}
		if ($migration.Order -ne $null)
		{
			$this.Order = [MigrationOrder]$migration.Order
		}
		$this.Actions = [System.Collections.Generic.List[BuildAction]]::new()
		foreach ($action in $migration.Actions)
		{
			$this.Actions.Add([BuildAction]::new($action))
		}
	}
}

class BuildModules
{
	[System.Collections.Generic.List[BuildActionBlock]]$Blocks;
	
	BuildModules([PSCustomObject]$blocks)
	{
		$this.Blocks = [System.Collections.Generic.List[BuildActionBlock]]::new()
		foreach ($block in $blocks)
		{
			$this.Blocks.Add([BuildActionBlock]::new($block))
		}
	}
}

class BuildActionBlock
{
	[string]$Name;
	[bool]$Enabled;
	[bool]$SaveState = $true;
	[DeployMode]$DeployMode = [DeployMode]::All;
	[DeployGroup]$DeployGroup = [DeployGroup]::Before;
	[DeployDatabase]$DatabaseType = [DeployDatabase]::None;
	[InstallMode]$InstallMode = [InstallMode]::All;
	[ApplyMode]$ApplyMode = [ApplyMode]::Add;
	[string]$ApendTo;
	[int]$MinVersion = -1;
	[int]$MaxVersion = -1;
	[string]$Depends;
	[System.Collections.Generic.List[BuildActionGroup]]$Groups;
	
	BuildActionBlock([PSCustomObject]$block)
	{
		$this.Name = $block.Name;
		$this.Enabled = [bool]::Parse($block.Enabled);
		if ($block.SaveState -ne $null)
		{
			$this.SaveState = [bool]::Parse($block.SaveState);
		}
		if ($block.DeployMode -ne $null)
		{
			$this.DeployMode = [DeployMode]$block.DeployMode
		}
		if ($block.DeployGroup -ne $null)
		{
			$this.DeployGroup = [DeployGroup]$block.DeployGroup
		}
		if ($block.DatabaseType -ne $null)
		{
			$this.DatabaseType = [DeployDatabase]$block.DatabaseType
		}
		if ($block.InstallMode -ne $null)
		{
			$this.InstallMode = [InstallMode]$block.InstallMode
		}
		if ($block.ApplyMode -ne $null)
		{
			$this.ApplyMode = [ApplyMode]$block.ApplyMode
		}
		$this.ApendTo = $block.ApendTo;
		if ($block.MinVersion -ne $null)
		{
			$this.MinVersion = [int]$block.MinVersion
		}
		if ($block.MaxVersion -ne $null)
		{
			$this.MaxVersion = [int]$block.MaxVersion
		}
		$this.Depends = $block.Depends
		$this.Groups = [System.Collections.Generic.List[BuildActionGroup]]::new()
		foreach ($group in $block.Groups)
		{
			$this.Groups.Add([BuildActionGroup]::new($group))
		}
	}
}

class BuildActionGroup
{
	[string]$Name;
	[string]$Message;
	[bool]$Enabled;
	[bool]$SaveState = $true;
	[DeployMode]$DeployMode = [DeployMode]::All;
	[DeployDatabase]$DatabaseType = [DeployDatabase]::None;
	[DeployTarget]$DeployTarget = [DeployTarget]::Configuration;
	[DeployPlatform]$DeployPlatform = [DeployPlatform]::Windows;
	[InstallMode]$InstallMode = [InstallMode]::All;
	[ApplyMode]$ApplyMode = [ApplyMode]::Add;
	[string]$ApendTo;
	[int]$MinVersion = -1;
	[int]$MaxVersion = -1;
	[string]$Depends;
	[string]$FallbackGroup;
	[string]$FallbackGroupParam;
	[System.Collections.Generic.List[BuildAction]]$Actions;
	
	BuildActionGroup([PSCustomObject]$group)
	{
		$this.Name = $group.Name;
		$this.Message = $group.Message;
		$this.Enabled = [bool]::Parse($group.Enabled);
		if ($group.SaveState -ne $null)
		{
			$this.SaveState = [bool]::Parse($group.SaveState);
		}
		if ($group.DeployMode -ne $null)
		{
			$this.DeployMode = [DeployMode]$group.DeployMode
		}
		if ($group.DatabaseType -ne $null)
		{
			$this.DatabaseType = [DeployDatabase]$group.DatabaseType
		}
		if ($group.DeployTarget -ne $null)
		{
			$this.DeployTarget = [DeployTarget]$group.DeployTarget
		}
		if ($group.DeployPlatform -ne $null)
		{
			$this.DeployPlatform = [DeployPlatform]$group.DeployPlatform
		}
		if ($group.InstallMode -ne $null)
		{
			$this.InstallMode = [InstallMode]$group.InstallMode
		}
		if ($group.ApplyMode -ne $null)
		{
			$this.ApplyMode = [ApplyMode]$group.ApplyMode
		}
		$this.ApendTo = $group.ApendTo;
		if ($group.MinVersion -ne $null)
		{
			$this.MinVersion = [int]$group.MinVersion
		}
		if ($group.MaxVersion -ne $null)
		{
			$this.MaxVersion = [int]$group.MaxVersion
		}
		$this.Depends = $group.Depends
		$this.FallbackGroup = $group.FallbackGroup
		$this.FallbackGroupParam = $group.FallbackGroupParam
		$this.Actions = [System.Collections.Generic.List[BuildAction]]::new()
		foreach ($action in $group.Actions)
		{
			$this.Actions.Add([BuildAction]::new($action))
		}
	}
}

class BuildAction
{
	[string]$Name;
	[string]$Message;
	[bool]$Enabled;
	[bool]$SaveState = $true;
	[string]$Type;
	[DeployMode]$DeployMode = [DeployMode]::All;
	[DeployDatabase]$DatabaseType = [DeployDatabase]::None;
	[InstallMode]$InstallMode = [InstallMode]::All;
	[ApplyMode]$ApplyMode = [ApplyMode]::Add;
	[string]$ApendTo;
	[int]$MinVersion = -1;
	[int]$MaxVersion = -1;
	[string]$Depends;
	[System.Collections.Generic.Dictionary[string,object]]$Params;
	
	BuildAction([PSCustomObject]$action)
	{
		$this.Name = $action.Name;
		$this.Message = $action.Message;
		$this.Enabled = [bool]::Parse($action.Enabled);
		$this.Type = $action.Type;
		if ($action.SaveState -ne $null)
		{
			$this.SaveState = [bool]::Parse($action.SaveState);
		}
		if ($action.DeployMode -ne $null)
		{
			$this.DeployMode = [DeployMode]$action.DeployMode
		}
		if ($action.DatabaseType -ne $null)
		{
			$this.DatabaseType = [DeployDatabase]$action.DatabaseType
		}
		if ($action.InstallMode -ne $null)
		{
			$this.InstallMode = [InstallMode]$action.InstallMode
		}
		if ($action.ApplyMode -ne $null)
		{
			$this.ApplyMode = [ApplyMode]$action.ApplyMode
		}
		$this.ApendTo = $action.ApendTo;
		if ($action.MinVersion -ne $null)
		{
			$this.MinVersion = [int]$action.MinVersion
		}
		if ($action.MaxVersion -ne $null)
		{
			$this.MaxVersion = [int]$action.MaxVersion
		}
		$this.Depends = $action.Depends
		$this.Params = [System.Collections.Generic.Dictionary[string,object]]::new();
		foreach ($property in $action.Params.PSObject.Properties)
		{
			$this.Params[$property.Name] = $property.Value
		}
	}
}

class Handlers
{
	[System.Collections.Generic.Dictionary[string,[System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]]]$handlers;
	
	Handlers()
	{
		$this.handlers = [System.Collections.Generic.Dictionary[string, [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]]]::new();
		$this.Register("ReplaceConfiguration", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ReplaceConfiguration($context, $this.ReadValue($context, $param, "Path"), $this.ReadOptionalValue($context, $param, "CheckOnly", $false))})
		$this.Register("ReplaceConfigurationRemote", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ReplaceConfigurationRemote($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("EnablePlugins", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.EnablePlugins($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("Tadmin", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Tadmin($context, $this.ReadValue($context, $param, "Verb"), $this.ReadOptionalValue($context, $param, "Args", @()), $this.ReadOptionalValue($context, $param, "UseLogin", $false), $this.ReadOptionalValue($context, $param, "UseDb", $false), $this.ReadOptionalValue($context, $param, "UseOutput", $false), $this.ReadOptionalValue($context, $param, "SetOutput", $false), $this.ReadOptionalValue($context, $param, "ReportError", $true), $false)})
		$this.Register("ImportCards", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ImportCards($context, $this.ReadOptionalValue($context, $param, "Args", @()), $this.ReadOptionalValue($context, $param, "HashPath", $null))})
		$this.Register("ImportCardsCheck", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ImportCardsCheck($context, $this.ReadOptionalValue($context, $param, "Args", @()), $this.ReadOptionalValue($context, $param, "HashPath", $null))})
		$this.Register("SetKey", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.SetKey($context, $this.ReadValue($context, $param, "Path"), $this.ReadValue($context, $param, "IsCipher"))})
		$this.Register("Scheme", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ImportScheme($context, $this.ReadOptionalValue($context, $param, "Path", $null))})
		$this.Register("Migration", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Migration($context, $this.ReadValue($context, $param, "File"))})
		$this.Register("Script", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Script($context, $this.ReadValue($context, $param, "File"))})
		$this.Register("Sql", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Sql($context, $this.ReadOptionalValue($context, $param, "File", $null), $this.ReadOptionalValue($context, $param, "Request", $null))})
		$this.Register("Select", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Select($context, $this.ReadOptionalValue($context, $param, "File", $null), $this.ReadOptionalValue($context, $param, "Request", $null), $this.ReadOptionalValue($context, $param, "ParamName", $null))})
		$this.Register("Localization", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Localization($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("CreateAppDir", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CreateAppDir($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("RemoveAppDir", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.RemoveAppDir($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("FinalMigration", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.FinalMigration($context)})
		$this.Register("RebuildIndexes", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.RebuildIndexes($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("EmptyString", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.EmptyString()})
		$this.Register("CreatePool", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CreatePool($context, $this.ReadOptionalValue($context, $param, "MaxProcesses", 0))})
		$this.Register("StartPool", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StartPool($context)})
		$this.Register("StopPool", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StopPool($context)})
		$this.Register("CreateUnixService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CreateUnixService($context, $this.ReadValue($context, $param, "ForWeb"))})
		$this.Register("StartUnixService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StartUnixService($context, $this.ReadValue($context, $param, "ForWeb"))})
		$this.Register("StopUnixService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StopUnixService($context, $this.ReadValue($context, $param, "ForWeb"))})
		$this.Register("BackupWebservice", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BackupWebservice($context, $this.ReadValue($context, $param, "Path"), $this.ReadOptionalValue($context, $param, "BackupPath", $null))})
		$this.Register("BackupWinservice", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BackupWinservice($context, $this.ReadValue($context, $param, "Path"), $this.ReadOptionalValue($context, $param, "BackupPath", $null))})
		$this.Register("ReplaceLicense", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ReplaceLicense($context, $this.ReadValue($context, $param, "Path"))})
		$this.Register("CopyFiles", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CopyFiles($context, $this.ReadValue($context, $param, "Source"), $this.ReadValue($context, $param, "Destination"), $this.ReadOptionalValue($context, $param, "Recurse", $true), $this.ReadOptionalValue($context, $param, "IgnoreMissingFiles", $false))})
		$this.Register("CreateIISApps", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CreateIISApps($context)})
		$this.Register("StartService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StartService($context)})
		$this.Register("StopService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.StopService($context)})
		$this.Register("CreateService", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CreateService($context)})
		$this.Register("SetBinding", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.SetBinding($context, $this.ReadOptionalValue($context, $param, "Site", "Default Web Site"), $this.ReadOptionalValue($context, $param, "Binding", "*:443:"), $this.ReadOptionalValue($context, $param, "Address", "127.0.0.1"))})
		$this.Register("SetParam", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.SetParam($context, $this.ReadValue($context, $param, "Param"), $this.ReadValue($context, $param, "Value"))})
	}
	
	[void]Register([string]$name, [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]$func)
	{
		$this.handlers.Add($name, $func)
	}
	
	[System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]Get([string]$name)
	{
		if ($this.handlers.ContainsKey($name))
		{
			return $this.handlers[$name];
		}
		return $null;
	}
	
	[bool]Execute([Context]$context, [BuildAction]$action)
	{
		$func = $this.Get($action.Type);
		if ($func -eq $null)
		{
			Write-Host "Unknown action type $($action.Type)!"
			return $false;
		}
		$context.Debug("Executing $($action.Name)")
		try
		{
			$result = $func.Invoke($context, $action.Params);
			if ($result -eq $false)
			{
				return $false;
			}
			return $true;
		}
		catch [System.Management.Automation.ActionPreferenceStopException]
		{
			$exception = $_.Exception
			Write-Host "$($exception.ErrorRecord) $($exception.ErrorRecord.InvocationInfo.PositionMessage)"
			Write-Host $exception.ErrorRecord.ScriptStackTrace
			return $false
		}
		catch [system.exception]
		{
			$exception = $_.Exception
			while ($exception.InnerException) {
				$exception = $exception.InnerException
			}
			Write-Host $exception | Format-List -Force
			return $false
		}
	}
	
	[object]ReadValue([Context]$context, [System.Collections.Generic.Dictionary[string,object]]$param, [string]$key)
	{
		if ($param.ContainsKey($key))
		{
			$value = $param[$key];
			if ($value -is [string] -And $value.StartsWith("$") -And $context.Params.ContainsKey($value.Trim('$')))
			{
				return $context.Params[$value.Trim('$')]
			}
			return $value
		}
		throw [System.Exception] "Key $key not found"
	}
	
	[object]ReadOptionalValue([Context]$context, [System.Collections.Generic.Dictionary[string,object]]$param, [string]$key, [object]$defaultValue = $null)
	{
		if ($param.ContainsKey($key))
		{
			$value = $param[$key];
			if ($value -is [string] -And $value.StartsWith("$") -And $context.Params.ContainsKey($value.Trim('$')))
			{
				return $context.Params[$value.Trim('$')]
			}
			return $value
		}
		return $defaultValue;
	}
	
	[void]ReportError([Context]$context)
	{
		$this.ReportError($context, $null)
	}
	
	[void]ReportError([Context]$context, [string]$message)
	{
		if ([string]::IsNullOrWhitespace($message) -eq $false)
		{
			Write-Host $message -ForegroundColor Red
		}
		Write-Host "Installation failed with error code: $LASTEXITCODE" -ForegroundColor Red
		Write-Host "See the details in log file: $($context.LogPath)" -ForegroundColor Red
	}
	
	[bool]Migration([Context]$context, $migration)
	{
		Write-Host "    - $migration"
		$migrationFile = [System.IO.Path]::Combine($context.SupplyPath, "Fixes", $context.ReplaceDbms("$migration"))
		return $this.Sql($context, $migrationFile, $null)
	}
	
	[bool]Script([Context]$context, $path)
	{
		$context.Debug("[Script] path`: $path")
		$script = Resolve-Path($path)
		try
		{
			$ErrorActionPreference="SilentlyContinue";
			
			& "$script" | Out-Null
			if ($LASTEXITCODE -ne 0)
			{
				$this.ReportError($context, "Failed to execute script $script");
				return $false;
			}
			return $true
		}
		catch [System.SystemException]
		{
			$this.ReportError($context, $_.Exception.Message);
			return $false;
		}
	}
	
	[bool]Sql([Context]$context, [string]$path, [string]$request)
	{
		$context.Debug("[Sql] path`: $path request`: $request")
		if ([string]::IsNullOrWhitespace($request) -eq $false)
		{
			$context.Output = $request;
			return $this.Tadmin($context, "Sql", @(), $false, $true, $true, $true, $true, $false)
		}
		$path = Resolve-Path($context.ReplaceDbms($path))
		return $this.Tadmin($context, "Sql", @($path), $false, $true, $false, $false, $true, $false)
	}
	
	[bool]Select([Context]$context, [string]$path, [string]$request, [string]$paramName)
	{
		$context.Debug("[Select] path`: $path request`: $request")
		if ([string]::IsNullOrWhitespace($request) -eq $false)
		{
			$context.Output = $request;
			$result = $this.Tadmin($context, "Select", @(), $false, $true, $true, $true, $true, $false)
			if ([string]::IsNullOrEmpty($paramName) -eq $false)
			{
				$context.Params[$paramName] = $context.Output
			}
			return $result;
		}
		$path = Resolve-Path($path)
		$result = $this.Tadmin($context, "Select", @($path), $false, $true, $false, $true, $true, $false)
		if ([string]::IsNullOrEmpty($paramName) -eq $false)
		{
			$context.Params[$paramName] = $context.Output
		}
		return $result;
	}
	
	[bool]Localization([Context]$context, [string]$path)
	{
		$context.Debug("[Localization] path`: $path")
		$path = Resolve-Path($path)
		return $this.Tadmin($context, "ImportLocalization", @($path), $true, $false, $false, $false, $true, $false)
	}
	
	[bool]CreateAppDir([Context]$context, [string]$path)
	{
		$context.Debug("[CreateAppDir] path`: $path")
		if ((Test-Path $path)) 
		{
			rd $path -Force -Recurse | Out-Null
		}
		$location = Get-Location;
		[System.IO.Directory]::CreateDirectory([System.IO.Path]::Combine($location, $path))
		return $true;
	}
	
	[bool]RemoveAppDir([Context]$context, [string]$path)
	{
		$context.Debug("[RemoveAppDir] path`: $path")
		if ((Test-Path $path)) 
		{
			rd $path -Force -Recurse | Out-Null
		}
		return $true;
	}
	
	[bool]FinalMigration([Context]$context)
	{
		$context.Debug("[FinalMigration]")
		if ($context.CurrentVersion -ne -1 -And $context.CurrentVersion -ne $context.Version)
		{
			Write-Host " > Migrations after configuration is imported"
			$postMigration = $this.MigrateScheme($context, [MigrationType]::Migration, [MigrationOrder]::Final)
			if ($postMigration -eq $false)
			{
				Write-Host "Installation failed with error code: $LASTEXITCODE" -ForegroundColor Red
				Write-Host "See the details in log file: $($context.LogPath)" -ForegroundColor Red
				return $false;
			}
		}
		return $true;
	}
	
	[bool]RebuildIndexes([Context]$context, [string]$path)
	{
		$context.Debug("[RebuildIndexes] path`: $path")
		if ($context.Settings.RebuildIndexes -eq $true)
		{
			Write-Host " > Rebuilding indexes"
			return $this.Sql($context, $path, $null)
		}
		return $true;
	}
	
	[bool]EmptyString()
	{
		Write-Host
		return $true;
	}
	
	[bool]ReplaceConfiguration([Context]$context, [string]$filePath, [bool]$readOnly = $false)
	{
		$context.Debug("[ReplaceConfiguration] filePath`: $filePath readOnly`: $readOnly")
		if ((Test-Path $filePath) -eq $false)
		{
			return $true;
		}
		$path = Resolve-Path($filePath)
		$result = $true;
		$files = dir -Path $path -Include @("*.json","*.config","*.xml") -Recurse | ? { !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "backup")) -And !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "docs")) } | %{$_.FullName}
		foreach ($filePath in $files) 
		{
			$sr = [System.IO.StreamReader]::new($filePath, $true)
			[char[]] $buffer = [char[]]::new(3)
			$sr.Read($buffer, 0, 3) | Out-Null
			$encoding = $sr.CurrentEncoding
			$sr.Close()
			$content = [System.IO.File]::ReadAllText($filePath, $encoding);
			
			$regex = [regex] '(__\w+__)'
			$matches = $regex.Matches($content);
			if ($matches.Count -gt 0)
			{
				foreach($match in $matches)
				{
					if ($context.Settings.Variables.ContainsKey($match.Value))
					{
						$content = $content.Replace($match.Value, $context.Settings.Variables[$match.Value]);
					}
					else
					{
						Write-Warning "Configuration '$match' not found. File: $filePath"
						$result = $false;
					}
				}
				if ($readOnly -eq $false)
				{
					[System.IO.File]::WriteAllText($filePath, $content, $encoding);
				}
			}
		}
		return $result;
	}
	
	[bool]ReplaceConfigurationRemote([Context]$context, [string]$filePath)
	{
		$context.Debug("[ReplaceConfigurationRemote] filePath`: $filePath")
		$filePath = $context.ServerContext.ReplaceVariables($filePath)
		$result = Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($path, $replacementValues)
			
			$result = $true;
			$files = dir -Path $path -Include @("*.json","*.config","*.xml") -Recurse | ? { !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "backup")) -And !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "docs")) } | %{$_.FullName}
			foreach ($filePath in $files) 
			{
				$sr = [System.IO.StreamReader]::new($filePath, $true)
				[char[]] $buffer = [char[]]::new(3)
				$sr.Read($buffer, 0, 3) | Out-Null
				$encoding = $sr.CurrentEncoding
				$sr.Close()
				$content = [System.IO.File]::ReadAllText($filePath, $encoding);
				
				$regex = [regex] '(__\w+__)'
				$matches = $regex.Matches($content);
				if ($matches.Count -gt 0)
				{
					foreach($match in $matches)
					{
						if ($replacementValues.ContainsKey($match.Value))
						{
							$content = $content.Replace($match.Value, $replacementValues[$match.Value]);
						}
						else
						{
							Write-Warning "Configuration '$match' not found. File: $filePath"
							$result = $false;
						}
					}
					[System.IO.File]::WriteAllText($filePath, $content, $encoding);
				}
			}
			return $result;
		} -Args $filePath, $context.Settings.Variables
		return $result;
	}
	
	[bool]EnablePlugins([Context]$context, [string]$filePath)
	{
		$context.Debug("[EnablePlugins] filePath`: $filePath")
		
		$enabledPlugins = $context.ServerContext.Module.Params.EnabledPlugins;
		if ($enabledPlugins -eq $null)
		{
			return $true;
		}
		
		Write-Host "    - Enabling plugins"
		
		$filePath = $context.ServerContext.ReplaceVariables($filePath)
		$result = Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($path, $replacementValues, $enabledPlugins)
			
			$result = $true;
			$files = dir -Path $path -Include @("*.xml") -Recurse | ? { !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "backup")) -And !$_.FullName.StartsWith([System.IO.Path]::Combine($path, "docs")) } | %{$_.FullName}
			foreach ($filePath in $files) 
			{
				$sr = [System.IO.StreamReader]::new($filePath, $true)
				[char[]] $buffer = [char[]]::new(3)
				$sr.Read($buffer, 0, 3) | Out-Null
				$encoding = $sr.CurrentEncoding
				$sr.Close()
				$content = [System.IO.File]::ReadAllText($filePath, $encoding);
				
				if ($content.Contains("</plugin>") -eq $false)
				{
					continue;
				}
				
				$regex = [regex] 'disabled\s*=\s*"\w+"'
				$match = $regex.Match($content);
				if ($match.Success)
				{
					$pluginName = [System.IO.Path]::GetFileNameWithoutExtension($filePath)
					if ($enabledPlugins.Contains($pluginName))
					{
						$content = $content.Replace($match.Value, 'disabled="false"');
					}
					else
					{
						$content = $content.Replace($match.Value, 'disabled="true"');
					}
					[System.IO.File]::WriteAllText($filePath, $content, $encoding);
				}
			}
			return $result;
		} -Args $filePath, $context.Settings.Variables, $enabledPlugins
		return $result;
	}
	
	[bool]ImportCardsCheck([Context]$context, [object[]]$arg, [string]$hashPath)
	{
		$context.Debug("[ImportCardsCheck] arg`: $([string]::Join(', ', $arg)) hashPath`: $hashPath")
		$cardsCount = 0;
		$cardHash = [System.Collections.Generic.Dictionary[string,object]]::new();
		if ([string]::IsNullOrWhitespace($hashPath) -eq $false -And $context.GetParam("UseImportHash") -eq $true)
		{
			if (Test-Path $hashPath)
			{
				$cardListJson = Get-Content $hashPath | ConvertFrom-Json;
				foreach ($property in $cardListJson.PSObject.Properties)
				{
					$cardHash[$property.Name] = $property.Value
				}
			}
			foreach($ar in $arg)
			{
				if ($ar.StartsWith("/"))
				{
					continue;
				}
				if ((Test-Path $ar) -eq $false)
				{
					return $true;
				}
				
				$ext = [System.IO.Path]::GetExtension($ar);
				if ($ext -eq ".jcard")
				{
					$content = Get-Content $ar -Raw;
					$file = $content | ConvertFrom-Json
					$id = $file.Card."ID::uid"
					$cardFiles = @($ar)
					$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($ar)) ([System.IO.Path]::GetFileNameWithoutExtension($ar))
					if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
					{
						$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
					}
					$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
					$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
					if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
					{
						continue;
					}
					else
					{
						$cardsCount++
					}
				}
				elseif ($ext -eq ".cardlib")
				{
					$dir = (Get-Item $ar).Directory
					[xml]$xml = Get-Content $ar
					$node = $xml.SelectNodes('//cardLibrary');
					$nodes = $xml.SelectNodes('//card');
					foreach($item in $nodes)
					{
						$path = [System.IO.Path]::Combine($dir, $item.Path);
						$content = Get-Content $path -Raw;
						$file = $content | ConvertFrom-Json
						$id = $file.Card."ID::uid"
						$cardFiles = @($path)
						$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($path)) ([System.IO.Path]::GetFileNameWithoutExtension($path))
						if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
						{
							$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
						}
						$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
						$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
						if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
						{
							$node.RemoveChild($item)
						}
						else
						{
							$cardsCount++
						}
					}
				}
				elseif ($ext -eq ".jcardlib")
				{
					$dir = (Get-Item $ar).Directory
					$arContent = Get-Content $ar | ConvertFrom-Json;
					foreach($item in $arContent.Items)
					{
						$path = [System.IO.Path]::Combine($dir, $item.Path);
						$content = Get-Content $path -Raw;
						$file = $content | ConvertFrom-Json
						$id = $file.Card."ID::uid"
						$cardFiles = @($path)
						$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($path)) ([System.IO.Path]::GetFileNameWithoutExtension($path))
						if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
						{
							$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
						}
						$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
						$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
						if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
						{
							continue;
						}
						else
						{
							$cardsCount++
						}
					}
				}
			}
			if ($cardsCount -ne 0 -And $context.GetParam("ImportCards") -ne $true)
			{
				$context.Params["ImportCards"] = $true;
			}
		}
		else
		{
			foreach($ar in $arg)
			{
				if ($ar.StartsWith("/"))
				{
					continue;
				}
				if ((Test-Path $ar) -eq $false)
				{
					return $true;
				}
			}
			if ($context.GetParam("ImportCards") -ne $true)
			{
				$context.Params["ImportCards"] = $true;
			}
		}
		return $true
	}
	
	[bool]ImportCards([Context]$context, [object[]]$arg, [string]$hashPath)
	{
		$context.Debug("[ImportCards] arg`: $([string]::Join(', ', $arg)) hashPath`: $hashPath")
		$cardHash = [System.Collections.Generic.Dictionary[string,object]]::new();
		if ([string]::IsNullOrWhitespace($hashPath) -eq $false -And $context.GetParam("UseImportHash") -eq $true)
		{
			if (Test-Path $hashPath)
			{
				$cardListJson = Get-Content $hashPath | ConvertFrom-Json;
				foreach ($property in $cardListJson.PSObject.Properties)
				{
					$cardHash[$property.Name] = $property.Value
				}
			}
			$files = @()
			$newArgs = @()
			foreach($ar in $arg)
			{
				if ($ar.StartsWith("/"))
				{
					$newArgs += $ar
					continue;
				}
				if ((Test-Path $ar) -eq $false)
				{
					return $true;
				}
				
				$ext = [System.IO.Path]::GetExtension($ar);
				if ($ext -eq ".jcard")
				{
					$content = Get-Content $ar -Raw;
					$file = $content | ConvertFrom-Json
					$id = $file.Card."ID::uid"
					$cardFiles = @($ar)
					$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($ar)) ([System.IO.Path]::GetFileNameWithoutExtension($ar))
					if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
					{
						$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
					}
					$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
					$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
					if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
					{
						continue;
					}
					else
					{
						$cardHash[$id] = $hash.Hash
						$newArgs += $ar
					}
				}
				elseif ($ext -eq ".cardlib")
				{
					$dir = (Get-Item $ar).Directory
					[xml]$xml = Get-Content $ar
					$node = $xml.SelectNodes('//cardLibrary');
					$nodes = $xml.SelectNodes('//card');
					foreach($item in $nodes)
					{
						$path = [System.IO.Path]::Combine($dir, $item.Path);
						$content = Get-Content $path -Raw;
						$file = $content | ConvertFrom-Json
						$id = $file.Card."ID::uid"
						$cardFiles = @($path)
						$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($path)) ([System.IO.Path]::GetFileNameWithoutExtension($path))
						if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
						{
							$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
						}
						$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
						$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
						if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
						{
							$node.RemoveChild($item)
						}
						else
						{
							$cardHash[$id] = $hash.Hash
						}
					}
					$fileName = [System.IO.Path]::GetFileNameWithoutExtension($ar) + "-tmp" + [System.IO.Path]::GetExtension($ar);
					$newFileName = [System.IO.Path]::Combine($dir, $fileName);
					$xml.Save($newFileName)
					$files += $newFileName
					$newArgs += $newFileName
				}
				elseif ($ext -eq ".jcardlib")
				{
					$dir = (Get-Item $ar).Directory
					$arContent = Get-Content $ar | ConvertFrom-Json;
					foreach($item in $arContent.Items)
					{
						$path = [System.IO.Path]::Combine($dir, $item.Path);
						$content = Get-Content $path -Raw;
						$file = $content | ConvertFrom-Json
						$id = $file.Card."ID::uid"
						$created = $file.Card."Created::dtm"
						$cardFiles = @($path)
						$filesFolder = Join-Path ([System.IO.Path]::GetDirectoryName($path)) ([System.IO.Path]::GetFileNameWithoutExtension($path))
						if (($file.Count -gt 1 -Or $content.Contains("::trf")) -And (Test-Path $filesFolder) -eq $true)
						{
							$cardFiles += Get-ChildItem $filesFolder -Recurse | Sort FullName | %{ $_.FullName }
						}
						$filesHash = Get-FileHash -Algorithm MD5 -Path $cardFiles | Select-Object -Property Hash | Out-String
						$hash = Get-FileHash -Algorithm MD5 -InputStream ([IO.MemoryStream]::new([char[]]$filesHash))
						if ($cardHash.ContainsKey($id) -And $cardHash[$id] -eq $hash.Hash)
						{
							$arContent.Items = $arContent.Items | Select-Object * | Where {$_.Path -ne $item.Path}
						}
						elseif ($id -ne $null)
						{
							$cardHash[$id] = $hash.Hash
						}
					}
					$fileName = [System.IO.Path]::GetFileNameWithoutExtension($ar) + "-tmp" + [System.IO.Path]::GetExtension($ar);
					$newFileName = [System.IO.Path]::Combine($dir, $fileName);
					$arContent | ConvertTo-Json -depth 100 | Format-Json | Out-File $newFileName -Encoding UTF8
					$files += $newFileName
					$newArgs += $newFileName
				}
			}
			
			try
			{
				$result = $this.Tadmin($context, "ImportCards", $newArgs, $true, $false, $false, $false, $true, $false)
				if ($result -eq $true)
				{
					$cardHash | ConvertTo-Json -Depth 100 | Format-Json | Out-File $hashPath -Encoding UTF8
				}
				return $result;
			}
			finally
			{
				foreach($file in $files)
				{
					Remove-Item $file -Force
				}
			}
		}
		else
		{
			foreach($ar in $arg)
			{
				if ($ar.StartsWith("/"))
				{
					continue;
				}
				if ((Test-Path $ar) -eq $false)
				{
					return $true;
				}
			}
			return $this.Tadmin($context, "ImportCards", $arg, $true, $false, $false, $false, $true, $false)
		}
	}
	
	[bool]Tadmin([Context]$context, [string]$verb, [object[]]$arg, [bool]$useLogin = $false, [bool]$useDb = $false, [bool]$useOutput = $false, [bool]$setOutput = $false, [bool]$reportError = $true, [bool]$nested = $false)
	{
		$location = Get-Location;
		$ErrorActionPreference="SilentlyContinue";
		$useTadminSession = $context.TadminSession -ne $null
		$context.Debug("[Tadmin] verb`: $verb arg`: $([string]::Join(', ', $arg)) useLogin`: $useLogin useDb`: $useDb useOutput`: $useOutput setOutput`: $setOutput reportError`: $reportError useTadminSession`: $useTadminSession output`: $($context.Output)")
		
		$contextOutput = $context.Output;
		$inputArgs = @($verb)
		$inputArgs += $arg
		if ($useLogin -eq $true)
		{
			$inputArgs += @("/a:$($context.Settings.Address)","/u:$($context.Settings.Login)","/p:$($context.Settings.Password)","/q")
		}
		if ($useDb -eq $true)
		{
			$inputArgs += @("/cs:default","/q")
		}
		if ($useOutput -eq $true)
		{
			if ($context.TadminSession -ne $null)
			{
				$result = Invoke-Command -Session $context.TadminSession -ScriptBlock `
				{
					param($inpt, $tadminPath, $inputArgs, $location)
					
					$ErrorActionPreference="SilentlyContinue";
					Set-Location $location
					
					$output = Write-Output $inpt | & $tadminPath $inputArgs
					return @($LASTEXITCODE, $output);
				} -Args $context.Output, $context.TadminPath, $inputArgs, $location
				$global:LASTEXITCODE = [int]$result[0]
				if ($setOutput -eq $true)
				{
					$context.Output = $result[1]
				}
			}
			else
			{
				$output = Write-Output $context.Output | & $context.TadminPath $inputArgs
				if ($setOutput -eq $true)
				{
					$context.Output = $output
				}
			}
		}
		else
		{
			if ($context.TadminSession -ne $null)
			{
				$result = Invoke-Command -Session $context.TadminSession -ScriptBlock `
				{
					param($tadminPath, $inputArgs, $location)
					
					$ErrorActionPreference="SilentlyContinue";
					Set-Location $location
					
					$output = & $tadminPath $inputArgs
					return @($LASTEXITCODE, $output);
				} -Args $context.TadminPath, $inputArgs, $location
				$global:LASTEXITCODE = [int]$result[0]
				if ($setOutput -eq $true)
				{
					$context.Output = $result[1]
				}
			}
			else
			{
				$output = & $context.TadminPath $inputArgs
				if ($setOutput -eq $true)
				{
					$context.Output = $output
				}
			}
		}
		if ($LASTEXITCODE -ne 0)
		{
			if ($nested -eq $false)
			{
				$logPath = Join-Path $context.ToolsPath "log.txt"
				$log = Get-Content $logPath | Select-String '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d\d\d\d\|ERROR\|(.*)' -AllMatches | Foreach-Object {$_.Matches} | Foreach-Object {$_.Groups[1].Value} | select -Last 1;
				if ($log -Like "*вызвала взаимоблокировку ресурсов блокировка с другим процессом*" -Or $log -Like "*was deadlocked on lock resources with another process*")
				{
					$context.Output = $contextOutput;
					return $this.Tadmin($context, $verb, $arg, $useLogin, $useDb, $useOutput, $setOutput, $reportError, $true)
				}
			}
			if ($reportError -eq $true)
			{
				$this.ReportError($context);
			}
			return $false;
		}
		return $true
	}
	
	[bool]SetKey([Context]$context, [string]$path, [bool]$isCipher)
	{
		$context.Debug("[SetKey] path`: $path isCipher`: $isCipher")
		$keyPath = [System.IO.Path]::Combine($context.SupplyPath, $path);
		if (![System.IO.Directory]::Exists($keyPath))
		{
			return $true;
		}
		if ($isCipher -eq $true)
		{
			$value = @("Cipher", "/path:$($keyPath)", "/value:$($context.Settings.ChipherKey)", "/q")
		}
		else
		{
			$value = @("Signature", "/path:$($keyPath)", "/value:$($context.Settings.SignatureKey)", "/q")
		}
		return $this.Tadmin($context, "SetKey", $value, $false, $false, $false, $false, $true, $false)
	}
	
	[bool]ImportScheme([Context]$context, [string]$schemePath = $null)
	{
		$context.Debug("[ImportScheme] schemePath`: $schemePath")
		$ErrorActionPreference="SilentlyContinue";
		
		# Установка конфигурации
		& { $ErrorActionPreference="SilentlyContinue"; $this.Tadmin($context, "CreateDatabase", @(), $false, $true, $false, $false, $false, $false) 2>&1 } | Out-Null
		$oldVersion = ""
		if ($LASTEXITCODE -eq 0)
		{
			Write-Host " > Creating database"
			$oldVersion = "N/A"
			$context.InstallMode = [InstallMode]::New;
			$context.Params["ImportCardsWithConstraints"] = $true
		}
		elseif ($LASTEXITCODE -ne -200)
		{
			$this.ReportError($context);
			return $false;
		}
		else
		{
			if ($context.DatabaseType -eq [DeployDatabase]::MSSQL)
			{
				$this.Select($context, $null, @("IF COL_LENGTH('Tables', 'Name') IS NOT NULL BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END"), $null)
			}
			else
			{
				$this.Select($context, $null, @("SELECT CASE WHEN exist = TRUE THEN '1' ELSE '0' END FROM(SELECT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'Tables') AS exist) t;"), $null)
			}
			$isExist = $context.Output;
			if ($isExist -is [Array])
			{
				$isExist = $isExist[$isExist.Count-1]
			}
			if ($isExist -eq "0")
			{
				Write-Host " > Creating database"
				$oldVersion = "N/A"
				$context.InstallMode = [InstallMode]::New;
				$context.Params["ImportCardsWithConstraints"] = $true
			}
			else
			{
				$getBuildVersionSQL = [System.IO.Path]::Combine($context.SupplyPath, $context.ReplaceDbms("Fixes\GetBuildVersion.dbms.sql"));
				$this.Tadmin($context, "Select", @($getBuildVersionSQL), $false, $true, $true, $true, $true, $false)
				if ($LASTEXITCODE -eq -200)
				{
					$this.ReportError($context, "Failed to extract version from database");
					return $false;
				}
				$oldVersion = $context.Output;
				if ($oldVersion -is [Array])
				{
					$oldVersion = $oldVersion[$oldVersion.Count-1]
				}
			}
		}
		
		if ($context.GetParam("DisableCollationCheck") -ne $true -And $context.DatabaseType -eq [DeployDatabase]::MSSQL)
		{
			$this.Select($context, $null, @("SELECT collation_name FROM sys.databases WHERE name = 'master'"), $null)
			$collation = $context.Output;
			if ($collation -is [Array])
			{
				$collation = $collation[$collation.Count-1]
			}
			
			if ($collation -ne "Cyrillic_General_CI_AS")
			{
				$this.ReportError($context, "Collation $collation is not equals 'Cyrillic_General_CI_AS'");
				return $false;
			}
		}
		
		$context.CurrentVersionString = $oldVersion
		$context.CurrentVersion = $context.GetVersionNumber($oldVersion)
		
		try
		{
			if ($context.CurrentVersion -ne -1 -And $context.CurrentVersion -ne $context.Version)
			{
				$context.InstallMode = [InstallMode]::Upgrade;
				if ($context.DeployMode -eq [DeployMode]::Diff)
				{
					throw [System.SystemException] "Diff installation detected. Upgrades could be only at full versions!"
				}
				Write-Host " > Installed build version is $($context.CurrentVersionString), upgrading to $($context.VersionString)"
				Write-Host " > Migrations prior to importing scheme"
				$preMigration = $this.MigrateScheme($context, [MigrationType]::Migration, [MigrationOrder]::Pre)
				if ($preMigration -eq $false)
				{
					throw [System.SystemException] "Failed to execute pre migrations, rollback!"
				}
				Write-Host " > Migration scripts prior to importing scheme"
				$preMigration = $this.MigrateScheme($context, [MigrationType]::Script, [MigrationOrder]::Pre)
				if ($preMigration -eq $false)
				{
					throw [System.SystemException] "Failed to execute pre script migrations, rollback!"
				}
			}
			elseif ($context.CurrentVersion -eq -1)
			{
				Write-Host " > Installing $($context.VersionString) build version"
			}
			Write-Host " > Importing scheme"
			$schemeParams = ""
			$scheme = [System.IO.Path]::Combine($context.SupplyPath, "Configuration\Scheme");
			if ([string]::IsNullOrWhitespace($schemePath) -eq $false)
			{
				$scheme = Resolve-Path $schemePath
			}
			if ([System.IO.Directory]::Exists([System.IO.Path]::Combine($scheme, "Partitions\FullTextSearch")) -And $context.Settings.UseFullTextSearch -eq $false)
			{
				$schemeParams = "/exclude:Partitions\FullTextSearch"
			}
			$this.Tadmin($context, "ImportSchemeSql", @($scheme, $schemeParams), $false, $true, $false, $false, $true, $false) | Out-Null
			if ($LASTEXITCODE -ne 0)
			{
				throw [System.SystemException] "Failed to import scheme"
			}
			if ($context.CurrentVersion -ne -1 -And $context.CurrentVersion -ne $context.Version)
			{
				Write-Host " > Migration scripts after importing scheme"
				$postMigration = $this.MigrateScheme($context, [MigrationType]::Script, [MigrationOrder]::Post)
				if ($postMigration -eq $false)
				{
					throw [System.SystemException] "Failed to execute post script migrations, rollback!"
				}
				Write-Host " > Migrations after importing scheme"
				$postMigration = $this.MigrateScheme($context, [MigrationType]::Migration, [MigrationOrder]::Post)
				if ($postMigration -eq $false)
				{
					throw [System.SystemException] "Failed to execute post migrations, rollback!"
				}
			}
			return $true;
		}
		catch [System.SystemException]
		{
			$code = $LASTEXITCODE
			$ErrorMessage = $_.Exception.Message
			Write-Host $ErrorMessage -ForegroundColor Red
			Write-Host " > Restoring build version to $($context.CurrentVersionString)"
			$SetBuildVersion = [System.IO.Path]::Combine($context.SupplyPath,  $context.ReplaceDbms("Fixes\SetBuildVersion.dbms.sql"));
			$this.Tadmin($context, "Sql", @($SetBuildVersion, "/p:BuildVersion=$($context.CurrentVersionString)"), $false, $true, $false, $false, $true, $false) | Out-Null
			
			if ($LASTEXITCODE -ne 0)
			{
				Write-Host "Failed to restore build with error code: $LASTEXITCODE." -ForegroundColor Red
			}
			Write-Host "Installation failed with error code: $code" -ForegroundColor Red
			Write-Host "See the details in log file: $($context.LogPath)" -ForegroundColor Red
			$global:LASTEXITCODE = $code
			return $false;
		}
	}
	
	[bool]MigrateScheme([Context]$context, [MigrationType]$type, [MigrationOrder]$order)
	{
		$context.Debug("[MigrateScheme] type`: $type order`: $order")
		$migrationGroups = $context.Migrations.Groups | where { $_.Type -eq $type -And $_.Order -eq $order -And $_.Version -ge $context.CurrentVersion -And $_.Version -lt $context.Version };
		foreach($group in $migrationGroups)
		{
			$context.Debug(" > Executing migration group $($group.Name) Enabled: $($group.Enabled) Version: $($group.Version) Type: $($group.Type) Order: $($group.Order)")
			if ($group.Enabled -eq $false)
			{
				continue;
			}
			if ([string]::IsNullOrWhitespace($group.Message) -eq $false)
			{
				Write-Host " > $($group.Message)"
			}
			foreach($action in $group.Actions)
			{
				$context.Debug("    - Executing migration action $($action.Name) Enabled: $($action.Enabled) MinVersion: $($action.MinVersion) MaxVersion: $($action.MaxVersion)")
				if ($action.Enabled -eq $false)
				{
					continue;
				}
				if ($action.MinVersion -ge 0 -And $action.MinVersion -ge $context.CurrentVersion)
				{
					continue;
				}
				if ($action.MaxVersion -ge 0 -And $action.MaxVersion -lt $context.CurrentVersion)
				{
					continue;
				}
				if ([string]::IsNullOrWhitespace($action.Message) -eq $false)
				{
					Write-Host "    - $($action.Message)"
				}
				$result = $this.Execute($context, $action);
				if ($result -eq $false)
				{
					$context.Debug("    - Execution failed")
					return $false;
				}
			}
		}
		return $true;
	}
	
	[bool]CreatePool([Context]$context, [int]$limitProcesses)
	{
		$context.Debug("[CreatePool] limitProcesses`: $limitProcesses")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$pools = $this.GetPools($context)
		$newPools = [System.Collections.Generic.List[string]]::new()
		$credential = $context.Settings.GetCredential($context.ServerContext.Server.CredentialId);
		
		[System.Management.Automation.PSCredential]$cred = $null
		
		foreach ($pool in $pools)
		{
			$poolExists = $this.IsPoolExists($context.ServerContext.Session, $pool)
			if ($poolExists -eq $false)
			{
				if ($cred -eq $null)
				{
					$cred = $this.GetCredential($credential)
				}
				Write-Host "    - Creating pool $pool"
				$this.CreatePool($context.ServerContext.Session, $pool, $cred, $limitProcesses)
				$newPools.Add($pool)
			}
		}
		
		#win pool
		if ($context.ServerContext.Module.Params.UseWinAuth -eq $true)
		{
			$winPoolName = "$($context.ServerContext.Module.Params.PoolName)" + "_win"
			$winPoolExists = $this.IsPoolExists($context.ServerContext.Session, $winPoolName)
			if ($winPoolExists -eq $false)
			{
				if ($cred -eq $null)
				{
					$cred = $this.GetCredential($credential)
				}
				$context.ServerContext.Params["CreateWinPool"] = $true
				Write-Host "    - Creating win pool $winPoolName"
				$this.CreatePool($context.ServerContext.Session, $winPoolName, $cred, $limitProcesses)
			}
		}
		$context.ServerContext.Params["NewPools"] = $newPools
		return $true
	}
	
	[bool]StartPool([Context]$context)
	{
		$context.Debug("[StartPool]")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$pools = $this.GetPools($context)
		foreach ($pool in $pools)
		{
			Write-Host "    - Starting pool $pool"
			$this.StartPool($context.ServerContext.Session, $pool)
		}
		
		if ($context.ServerContext.Module.Params.UseWinAuth -eq $true)
		{
			$winPoolName = "$($context.ServerContext.Module.Params.PoolName)" + "_win"
			Write-Host "    - Starting win pool $winPoolName"
			$this.StartPool($context.ServerContext.Session, $winPoolName)
		}
		
		return $true
	}
	
	[bool]StopPool([Context]$context)
	{
		$context.Debug("[StopPool]")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$pools = $this.GetPools($context)
		foreach ($pool in $pools)
		{
			Write-Host "    - Stopping pool $pool"
			$poolStarted = $this.IsPoolStarted($context.ServerContext.Session, $pool)
			while ($poolStarted -eq $true)
			{
				$this.StopPool($context.ServerContext.Session, $pool)
				Start-Sleep -s 1 #Sleep for 1 seconds
				$poolStarted = $this.IsPoolStarted($context.ServerContext.Session, $pool)
			}
		}
		
		if ($context.ServerContext.Module.Params.UseWinAuth -eq $true)
		{
			$winPoolName = "$($context.ServerContext.Module.Params.PoolName)" + "_win"
			Write-Host "    - Stopping win pool $winPoolName"
			$poolStarted = $this.IsPoolStarted($context.ServerContext.Session, $winPoolName)
			while ($poolStarted -eq $true)
			{
				$this.StopPool($context.ServerContext.Session, $winPoolName)
				Start-Sleep -s 1 #Sleep for 1 seconds
				$poolStarted = $this.IsPoolStarted($context.ServerContext.Session, $winPoolName)
			}
		}
		
		return $true
	}
	
	[bool]CreateUnixService([Context]$context, [bool]$forWeb)
	{
		$context.Debug("[CreateUnixService] forWeb`: $forWeb")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$homeDir = $context.ServerContext.GetParam("HomeDir")
		
		if ($forWeb -eq $true)
		{
			$name = $context.ServerContext.Module.Params.PoolName
			$description = "Syntellect TESSA " + $name.Replace("tessa_", "")
			$id = $name
		}
		else
		{
			$name = $context.ServerContext.Module.Params.FolderName
			$description = $context.ServerContext.Module.Params.ServiceName
			$id = $name.Replace("tessa", "chronos")
		}
		
		$mainPath = [System.IO.Path]::Combine($homeDir, $name)
		if ($forWeb -eq $true)
		{
			$workingDirectory = [System.IO.Path]::Combine($mainPath, "webservice/web")
			$execDir = [System.IO.Path]::Combine($workingDirectory, "Tessa.Web.Server 5000")
		}
		else
		{
			$workingDirectory = [System.IO.Path]::Combine($mainPath, "Chronos")
			$execDir = [System.IO.Path]::Combine($workingDirectory, "Chronos")
		}
		
		$credential = $context.Settings.GetCredential($context.ServerContext.Server.CredentialId);
		$cred = $this.GetCredential($credential)
		$login = $cred.UserName
		$password = $cred.GetNetworkCredential().password
		
		$builder = [System.Text.StringBuilder]::new();
		[void]$builder.AppendLine("[Unit]");
		[void]$builder.AppendLine("Description=$description");
		[void]$builder.AppendLine();
		[void]$builder.AppendLine("[Service]");
		[void]$builder.AppendLine("WorkingDirectory=$workingDirectory");
		[void]$builder.AppendLine("ExecStart=$execDir");
		[void]$builder.AppendLine("Restart=always");
		[void]$builder.AppendLine("RestartSec=10");
		[void]$builder.AppendLine("SyslogIdentifier=$id");
		[void]$builder.AppendLine("User=$login");
		[void]$builder.AppendLine("UMask=002");
		[void]$builder.AppendLine("Environment=ASPNETCORE_ENVIRONMENT=Production");
		[void]$builder.AppendLine("Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false");
		[void]$builder.AppendLine();
		[void]$builder.AppendLine("[Install]");
		[void]$builder.AppendLine("WantedBy=multi-user.target");
		
		$filePath = "/etc/systemd/system/$id.service"
		
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($password, $filePath, $content)
			
			if ([System.IO.File]::Exists($filePath))
			{
				return;
			}
			
			echo $password | sudo -S `pwsh -c "[System.IO.File]::WriteAllText(`"$filePath`", `"$content`")"`;
		} -Args $password, $filePath, $builder.ToString() | Out-Null
		
		return $true
	}
	
	[bool]StopUnixService([Context]$context, [bool]$forWeb)
	{
		$context.Debug("[StopUnixService] forWeb`: $forWeb")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		if ($forWeb -eq $true)
		{
			$id = $context.ServerContext.Module.Params.PoolName
		}
		else
		{
			$id = $context.ServerContext.Module.Params.FolderName.Replace("tessa", "chronos")
		}
		
		try
		{
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($id)
				
				& sudo systemctl stop $id
			} -Args $id | Out-Null
		}
		catch [system.exception]
		{
		}
		return $true
	}
	
	[bool]StartUnixService([Context]$context, [bool]$forWeb)
	{
		$context.Debug("[StartUnixService] forWeb`: $forWeb")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		if ($forWeb -eq $true)
		{
			$id = $context.ServerContext.Module.Params.PoolName
		}
		else
		{
			$id = $context.ServerContext.Module.Params.FolderName.Replace("tessa", "chronos")
		}
		
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($id)
			
			& sudo systemctl start $id
		} -Args $id | Out-Null
		return $true
	}
	
	[bool]BackupWebservice([Context]$context, [string]$path, [string]$backupPath)
	{
		$context.Debug("[BackupWebservice] path`: $path backupPath`: $backupPath")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$path = $context.ServerContext.ReplaceVariables($path)
		$backupPath = $context.ServerContext.ReplaceVariables($backupPath)
		$isFull = $context.DeployMode -eq [DeployMode]::Full
		$isMoveData = $context.GetParam("DisableDiffCopy") -eq $true;
		#supress progress bar for file transfer
		$ProgressPreference = 'SilentlyContinue'
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock { param($poolPath) [System.IO.Directory]::CreateDirectory($poolPath) } -Args $path | Out-Null #make dir before copy files, because if dest folder not exists - files would copy to dest folder without subfolders
		if ($context.Settings.Backup -eq $true)
		{
			if ([string]::IsNullOrEmpty($backupPath) -eq $false)
			{
				Invoke-Command -Session $context.ServerContext.Session -ScriptBlock { param($path) [System.IO.Directory]::CreateDirectory($path) } -Args $backupPath | Out-Null
			}
			Write-Host "    - Backuping files"
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($poolPath, $backupPath, $isFull, $isMoveData)
				
				$directoryInfo = Get-ChildItem $poolPath | Measure-Object
				if ($directoryInfo.count -eq 0) #new folder, nothing to backup
				{
					return
				}
				
				$backupDate = [DateTime]::Now.ToString("yyy-MM-dd HH-mm-ss")
				if ([string]::IsNullOrEmpty($backupPath) -eq $true)
				{
					$backupFolder = [System.IO.Path]::Combine($poolPath, "backup\$backupDate\")
				}
				else
				{
					$backupFolder = [System.IO.Path]::Combine($backupPath, "$backupDate\")
				}
				[System.IO.Directory]::CreateDirectory($backupFolder);
				$isSameDrive = [System.IO.Directory]::GetDirectoryRoot($poolPath) -eq [System.IO.Directory]::GetDirectoryRoot($backupFolder);
				$items = Get-ChildItem -Path $poolPath -Exclude "backup"
				foreach ($item in $items)
				{
					if ($item -is [System.IO.DirectoryInfo])
					{
						$destPath = [System.IO.Path]::Combine($backupFolder, $item.Name)
						[System.IO.Directory]::CreateDirectory($destPath);
						if ($isFull -eq $true -And $isMoveData -eq $true)
						{
							if ($isSameDrive)
							{
								Get-ChildItem -Path $item.FullName | ?{$_.fullname -notlike "$item\logs"} | Move-Item -Destination $destPath #not move logs and backups folders
							}
							else
							{
								Get-ChildItem -Path $item.FullName | ?{$_.fullname -notlike "$item\logs"} | Copy-Item -Destination $destPath -Recurse #not move logs and backups folders
								Get-ChildItem -Path $item.FullName | ?{$_.fullname -notlike "$item\logs"} | Remove-Item -Recurse -Force
							}
						}
						else
						{
							Get-ChildItem -Path $item.FullName | ?{$_.fullname -notlike "$item\logs"} | Copy-Item -Destination $destPath -Recurse #not move logs and backups folders
						}
					}
					else
					{
						if ($isFull -eq $true -And $isMoveData -eq $true)
						{
							if ($isSameDrive)
							{
								Move-Item $item.FullName -Destination $backupFolder
							}
							else
							{
								Copy-Item $item.FullName -Destination $backupFolder
								Remove-Item $item.FullName -Recurse -Force
							}
						}
						else
						{
							Copy-Item $item.FullName -Destination $backupFolder
						}
					}
				}
			} -Args $path, $backupPath, $isFull, $isMoveData | Out-Null
		}
		elseif ($isFull -eq $true -And $isMoveData -eq $true)
		{
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($poolPath)
				
				$directoryInfo = Get-ChildItem $poolPath | Measure-Object
				if ($directoryInfo.count -eq 0) #new folder, nothing to backup
				{
					return
				}
				
				$items = Get-ChildItem -Path $poolPath -Exclude "backup"
				foreach ($item in $items)
				{
					if ($item -is [System.IO.DirectoryInfo])
					{
						Get-ChildItem -Path $item.FullName | ?{$_.fullname -notlike "$item\logs"} | Remove-Item -Recurse -Force
					}
					else
					{
						Remove-Item $item.FullName -Recurse -Force
					}
				}
			} -Args $path | Out-Null
		}
		$ProgressPreference = 'Continue'
		return $true
	}
	
	[bool]BackupWinservice([Context]$context, [string]$path, [string]$backupPath)
	{
		$context.Debug("[BackupWinservice] path`: $path backupPath`: $backupPath")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$path = $context.ServerContext.ReplaceVariables($path)
		$backupPath = $context.ServerContext.ReplaceVariables($backupPath)
		$isFull = $context.DeployMode -eq [DeployMode]::Full
		$isMoveData = $context.GetParam("DisableDiffCopy") -eq $true;
		#supress progress bar for file transfer
		$ProgressPreference = 'SilentlyContinue'
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock { param($path) [System.IO.Directory]::CreateDirectory($path) } -Args $path | Out-Null #make dir before copy files, because if dest folder not exists - files would copy to dest folder without subfolders
		if ($context.Settings.Backup -eq $true)
		{
			if ([string]::IsNullOrEmpty($backupPath) -eq $false)
			{
				Invoke-Command -Session $context.ServerContext.Session -ScriptBlock { param($path) [System.IO.Directory]::CreateDirectory($path) } -Args $backupPath | Out-Null
			}
			Write-Host "    - Backuping files"
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($chronosPath, $backupPath, $isFull, $isMoveData)
				
				$directoryInfo = Get-ChildItem $chronosPath | Measure-Object
				if ($directoryInfo.count -eq 0) #new folder, nothing to backup
				{
					return
				}
				
				$backupDate = [DateTime]::Now.ToString("yyy-MM-dd HH-mm-ss")
				if ([string]::IsNullOrEmpty($backupPath) -eq $true)
				{
					$backupFolder = [System.IO.Path]::Combine($poolPath, "backup\$backupDate\")
				}
				else
				{
					$backupFolder = [System.IO.Path]::Combine($backupPath, "$backupDate\")
				}
				$isSameDrive = [System.IO.Directory]::GetDirectoryRoot($chronosPath) -eq [System.IO.Directory]::GetDirectoryRoot($backupFolder);
				[System.IO.Directory]::CreateDirectory($backupFolder);
				if ($isFull -eq $true -And $isMoveData -eq $true)
				{
					if ($isSameDrive)
					{
						Get-ChildItem -Path $chronosPath -Exclude "backup", "logs" | Move-Item -Destination $backupFolder #not move logs and backups folders
					}
					else
					{
						Get-ChildItem -Path $chronosPath -Exclude "backup", "logs" | Copy-Item -Destination $backupFolder -Recurse #not move logs and backups folders
						Get-ChildItem -Path $chronosPath -Exclude "backup", "logs" | Remove-Item -Recurse -Force
					}
				}
				else
				{
					Get-ChildItem -Path $chronosPath -Exclude "backup", "logs" | Copy-Item -Destination $backupFolder -Recurse #not move logs and backups folders
				}
			} -Args $path, $backupPath, $isFull, $isMoveData | Out-Null
		}
		elseif ($isFull -eq $true -And $isMoveData -eq $true)
		{
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($chronosPath)
				
				$directoryInfo = Get-ChildItem $chronosPath | Measure-Object
				if ($directoryInfo.count -eq 0) #new folder, nothing to backup
				{
					return
				}
				
				Get-ChildItem -Path $chronosPath -Exclude "backup", "logs" | Remove-Item -Recurse -Force
			} -Args $path | Out-Null
		}
		$ProgressPreference = 'Continue'
		return $true
	}
	
	[bool]ReplaceLicense([Context]$context, [string]$path)
	{
		$context.Debug("[ReplaceLicense] path`: $path")
		$path = $context.ServerContext.ReplaceVariables($path)
		#supress progress bar for file transfer
		$ProgressPreference = 'SilentlyContinue'
		if (([string]::IsNullOrEmpty($context.Settings.LicensePath) -eq $false) -And (Test-Path $context.Settings.LicensePath))
		{
			Write-Host "    - Replacing license"
			del "$path\*.tlic"
			cp $context.Settings.LicensePath "$path" -Force
		}
		$ProgressPreference = 'Continue'
		return $true;
	}
	
	[bool]CopyFiles([Context]$context, [string]$src, [string]$dest, [bool]$recurse, [bool]$ignoreMissingFiles = $false)
	{
		$context.Debug("[CopyFiles] src`: $src dest`: $dest recurse`: $recurse ignoreMissingFiles`: $ignoreMissingFiles")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$src = $context.ServerContext.ReplaceVariables($src)
		$dest = $context.ServerContext.ReplaceVariables($dest)
		
		if ((Test-Path $src) -eq $false)
		{
			return $ignoreMissingFiles;
		}
		
		#supress progress bar for file transfer
		$ProgressPreference = 'SilentlyContinue'
		$cnt = 0;
		while($true)
		{
			try
			{
				$context.Debug("[CopyFilesDetailed] src`: $src dest`: $dest recurse`: $recurse ignoreMissingFiles`: $ignoreMissingFiles count`: $cnt")
				if ($context.GetParam("DisableDiffCopy") -eq $true -Or (Test-Path -Path $dest -PathType Leaf) -eq $true)
				{
					cp -Path $src -Destination $dest -ToSession $context.ServerContext.Session -Recurse:$recurse -Force
				}
				else
				{
					$location = Get-Location;
					$absoluteSource = [System.IO.Path]::Combine($location, $src.Trim('*'))
					$sourceFiles = Get-ChildItem -Path $src -Exclude "backup", "logs" -File -Recurse:$recurse | Get-FileHash -Algorithm SHA1 | Select-Object -Property @{n="Path";e={$_.Path.Replace($absoluteSource, "").Trim("\\")}}, Hash
					$sourceDictionary = [System.Collections.Generic.Dictionary[string,string]]::new()
					foreach ($source in $sourceFiles)
					{
						$sourceDictionary.Add($source.Path, $source.Hash)
					}
					$remoteFiles = Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
					{
						param($destPath)
						
						$allItems = @();
						$items = Get-ChildItem -Path $destPath -Exclude "backup", "logs"
						foreach ($item in $items)
						{
							if ($item -is [System.IO.DirectoryInfo])
							{
								$allItems += Get-ChildItem -Path $item.FullName -File -Recurse:$recurse | ?{$_.fullname -notlike "$item\logs\*"}
							}
							else
							{
								$allItems += $item
							}
						}
						return $allItems | Get-FileHash -Algorithm SHA1 | Select-Object -Property @{n="Path";e={$_.Path.Replace($destPath, "").Trim("\\")}}, Hash
					} -Args $dest
					$remoteDictionary = [System.Collections.Generic.Dictionary[string,string]]::new()
					foreach ($remote in $remoteFiles)
					{
						$remoteDictionary.Add($remote.Path, $remote.Hash)
					}
					foreach($remoteFile in $remoteDictionary.Keys)
					{
						if ($sourceDictionary.ContainsKey($remoteFile) -eq $false)
						{
							$fileToRemove = Join-Path $dest $remoteFile
							$context.Debug("[RemoteDiff] file`: $remoteFile Contains`: $($sourceDictionary.ContainsKey($remoteFile)) path`: $fileToRemove")
							Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
							{
								param($item)
								
								Remove-Item -Path $item -Force
							} -Args $fileToRemove
						}
					}
					foreach($sourceFile in $sourceDictionary.Keys)
					{
						if ($remoteDictionary.ContainsKey($sourceFile) -eq $false -Or $remoteDictionary[$sourceFile] -ne $sourceDictionary[$sourceFile])
						{
							$context.Debug("[SourceDiff] file`: $sourceFile Contains`: $($remoteDictionary.ContainsKey($sourceFile)) Hash: $($remoteDictionary[$sourceFile] -ne $sourceDictionary[$sourceFile]) remote`: $($remoteDictionary[$sourceFile]) source`: $($sourceDictionary[$sourceFile])")
							
							$file = Join-Path $absoluteSource $sourceFile
							$destFile = Join-Path $dest $sourceFile
							$directory = [System.IO.Path]::GetDirectoryName($destFile)
							$context.Debug("[SourceDiff] directory`: $directory destFile`: $destFile")
							Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
							{
								param($directory)
								
								[System.IO.Directory]::CreateDirectory($directory) | Out-Null;
							} -Args $directory
							cp -Path $file -Destination $destFile -ToSession $context.ServerContext.Session -Force
						}
					}
				}
				break;
			}
			catch [system.exception]
			{
				$cnt++;
				if ($cnt -gt 2)
				{
					throw;
				}
			}
		}
		$ProgressPreference = 'Continue'
		return $true;
	}
	
	[bool]CreateIISApps([Context]$context)
	{
		$context.Debug("[CreateIISApps]")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		if ($context.ServerContext.GetParam("NewPools") -eq $null)
		{
			return $true;
		}
		$newPools = $context.ServerContext.GetParam("NewPools")
		$defaultPoolName = $context.ServerContext.Module.Params.PoolName
		$winPoolName = "$defaultPoolName" + "_win"
		if ($newPools.Count -gt 0)
		{
			Write-Host "    - Creating IIS apps"
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($defaultPoolName, $newPools)
				
				Import-Module WebAdministration
				foreach ($poolName in $newPools) 
				{
					if ($poolName -eq $defaultPoolName)
					{
						$folder = "web"
					}
					else
					{
						$folder = $poolName.Substring($defaultPoolName.Length + 1)
					}
					ConvertTo-WebApplication "IIS:\Sites\Default Web Site\$defaultPoolName\$folder" -ApplicationPool $poolName -Force
					Set-WebConfigurationProperty -PsPath "IIS:\" -Location "Default Web Site/$defaultPoolName/$folder" -Filter 'system.webserver/security/access' -Name sslFlags -Value "Ssl"
					if ($folder.Name -ne "web")
					{
						Set-WebConfigurationProperty -PsPath "IIS:\" -Location "Default Web Site/$defaultPoolName/$folder" -Filter 'system.webServer/security/authentication/windowsAuthentication' -Name Enabled -Value True
					}
				}
			} -Args $defaultPoolName, $newPools | Out-Null
		}
		if ($context.ServerContext.Module.Params.UseWinAuth -eq $true -And $context.ServerContext.GetParam("CreateWinPool") -eq $true)
		{
			Write-Host "    - Creating IIS win apps"
			Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
			{
				param($poolName, $winPoolName)
				
				Import-Module WebAdministration
				
				$feature = 'IIS-WindowsAuthentication'
				
				$currentFeature = Get-WindowsOptionalFeature -Online -FeatureName $feature
				if ($currentFeature.State -ne "Enabled")
				{
					$ProgressPreference = 'SilentlyContinue'
					Enable-WindowsOptionalFeature -Online -FeatureName $feature | Out-Null
					$ProgressPreference = 'Continue'
				}
				
				$path = Get-WebFilePath "IIS:\Sites\Default Web Site\$poolName\web"
				
				New-WebApplication -Name "tw_winauth" -Site "Default Web Site\$poolName" -PhysicalPath $path -ApplicationPool $winPoolName -Force
				Set-WebConfigurationProperty -PsPath "IIS:\" -Location "Default Web Site/$poolName/tw_winauth" -Filter 'system.webserver/security/access' -Name sslFlags -Value "Ssl"
				Set-WebConfigurationProperty -PsPath "IIS:\" -Location "Default Web Site/$poolName/tw_winauth" -Filter 'system.webServer/security/authentication/anonymousAuthentication' -Name Enabled -Value False
				Set-WebConfigurationProperty -PsPath "IIS:\" -Location "Default Web Site/$poolName/tw_winauth" -Filter 'system.webServer/security/authentication/windowsAuthentication' -Name Enabled -Value True
			} -Args $defaultPoolName, $winPoolName | Out-Null
		}
		return $true;
	}
	
	[bool]StartService([Context]$context)
	{
		$context.Debug("[StartService]")
		if ($context.ServerContext.Module.Params.StartUp -eq $false)
		{
			return $true;
		}
		Write-Host "    - Starting service"
		$serviceName = $context.ServerContext.Module.Params.ServiceName
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock { param($apn) Start-Service $apn} -Args $serviceName
		return $true;
	}
	
	[bool]StopService([Context]$context)
	{
		$context.Debug("[StopService]")
		if ($context.ServerContext -eq $null)
		{
			return $false;
		}
		
		$serviceName = $context.ServerContext.Module.Params.ServiceName
		$isServiceExists = $this.IsServiceExists($context.ServerContext.Session, $serviceName)
		if ($isServiceExists -eq $true)
		{
			$servicePath = $this.GetServicePath($context.ServerContext.Session, $serviceName) #check for target chronos dir
			$chronosPath = $context.ServerContext.GetParam("ChronosPath");
			if ($servicePath -ne $chronosPath)
			{
				Write-Host "Service $serviceName is not equal for target dir!" -ForegroundColor Red
				Write-Host "Current: $servicePath" -ForegroundColor Red
				Write-Host "Target: $($chronosPath)" -ForegroundColor Red
				return $false
			}
			Write-Host "    - Stopping service"
			$serviceStarted = $this.IsServiceStarted($context.ServerContext.Session, $serviceName)
			while ($serviceStarted -eq $true)
			{
				$this.StopService($context.ServerContext.Session, $serviceName)
				Start-Sleep -s 10 #Sleep for 10 seconds
				$serviceStarted = $this.IsServiceStarted($context.ServerContext.Session, $serviceName)
			}
		}
		else
		{
			$context.ServerContext.Params["CreateService"] = $true
		}
		return $true;
	}
	
	[bool]CreateService([Context]$context)
	{
		$context.Debug("[CreateService]")
		if ($context.ServerContext.GetParam("CreateService") -ne $true)
		{
			return $true;
		}
		
		$credential = $context.Settings.GetCredential($context.ServerContext.Server.CredentialId);
		$cred = $this.GetCredential($credential)
		$serviceName = $context.ServerContext.Module.Params.ServiceName
		$login = $cred.UserName
		$password = $cred.GetNetworkCredential().password
		Write-Host "    - Creating service $serviceName"
		return Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($user, $password, $servicePath, $serviceName, $logFile)
			
			if ($user -notmatch '\\')
			{
				$user = ".\$user"
			}
			
			$ErrorActionPreference="SilentlyContinue";
			
			if ([System.IO.File]::Exists("$servicePath\Chronos.dll"))
			{
				$output = & "sc.exe" "create" $serviceName "DisplayName=$serviceName" "start=auto" "binpath=$servicePath\Chronos.exe --service"
				if ($LASTEXITCODE -ne 0)
				{
					Write-Host "Installation failed with error code: $LASTEXITCODE" -ForegroundColor Red
					Write-Host "See the details in log file: $logFile" -ForegroundColor Red
					Write-Host "Full log: $output" -ForegroundColor Red
					return $false
				}
				
				$output = & "sc.exe" "description" $serviceName "Launches Chronos plugins using defined schedule."
				$output = & "sc.exe" "config" $serviceName "obj=$user" "password=$password"
			}
			else
			{
				$output = C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /username=$user /password=$password /unattended "$servicePath\Chronos.exe"
			}
			if ($LASTEXITCODE -ne 0)
			{
				Write-Host "Installation failed with error code: $LASTEXITCODE" -ForegroundColor Red
				Write-Host "See the details in log file: $logFile" -ForegroundColor Red
				Write-Host "Full log: $output" -ForegroundColor Red
				return $false
			}
			return $true
		} -Args $login, $password, $context.ServerContext.GetParam("ChronosPath"), $serviceName, $context.LogPath
	}
	
	[bool]SetBinding([Context]$context, [string]$site, [string]$binding, [string]$ip)
	{
		$context.Debug("[SetBinding] site`: $site binding`: $binding ip`: $ip")
		if ($context.ServerContext.Module.Params.ChangeBinding -ne $true)
		{
			return $true;
		}
		
		Write-Host "    - Changing binding information to $ip"
		Invoke-Command -Session $context.ServerContext.Session -ScriptBlock `
		{
			param($site, $binding, $ip)
			
			Set-WebBinding -Name 'Default Web Site' -BindingInformation $binding -PropertyName IPAddress -Value $ip
		} -Args $site, $binding, $ip | Out-Null
		return $true;
	}
	
	[bool]SetParam([Context]$context, [string]$key, [object]$value)
	{
		if ([string]::IsNullOrEmpty($key) -eq $false)
		{
			$context.Params[$key] = $value
		}
		return $true
	}
	
	[System.Management.Automation.PSCredential]GetCredential([Credential]$credential)
	{
		if ($credential.Type -eq [LoginType]::Login)
		{
			$securePassword = ConvertTo-SecureString $credential.Params["Password"] -AsPlainText -Force
			$cred = [System.Management.Automation.PSCredential]::new($credential.Params["Login"], $securePassword)
			return $cred;
		}
		else
		{
			$cred = Get-Credential
			return $cred;
		}
	}
	
	[System.Collections.Generic.List[string]]GetPools([Context]$context)
	{
		$defaultPoolName = $context.ServerContext.Module.Params.PoolName;
		$webService = [System.IO.Path]::Combine($context.SupplyPath, "WebServices");
		
		[System.Collections.Generic.List[string]] $pools = Get-ChildItem -Path $webService -Directory -Exclude "web","docs" | Select-Object -Property Name | %{ $defaultPoolName + "_" + $_.Name}
		if ($pools -eq $null)
		{
			$pools = [System.Collections.Generic.List[string]]::new()
		}
		$pools.Add($defaultPoolName) #main pool
		return $pools;
	}
	
	#-------------------------------------------------------------------------------
	
	[void]CreatePool([System.Management.Automation.Runspaces.PSSession]$session, [string]$poolName, [System.Management.Automation.PSCredential]$credential, [int]$limitProcesses)
	{
		$login = $credential.UserName
		$password = $credential.GetNetworkCredential().password
		Invoke-Command -Session $session -ScriptBlock `
		{
			param($apn, $user, $password, $limitProcesses)
			Import-Module WebAdministration;
			
			#get pools count without default pools
			$pools = Get-ChildItem IIS:\AppPools -Exclude ".NET v4.5",".NET v4.5 Classic","DefaultAppPool"
			#calculate max process count by divide all processors count to pools count
			$maxProcesses = [System.Environment]::ProcessorCount
			
			if ($pools.Count -gt 0)
			{
				$maxProcesses = [int]([System.Environment]::ProcessorCount / ($pools.Count + 1))
			}
			
			if ($maxProcesses -lt 1)
			{
				$maxProcesses = 1;
			}
			
			if ($limitProcesses -gt 0 -And $limitProcesses -lt $maxProcesses)
			{
				$maxProcesses = $limitProcesses;
			}
			
			$appPool = New-WebAppPool -Name $apn
			$appPool.processModel.userName = $user
			$appPool.processModel.password = $password
			$appPool.processModel.maxProcesses = $maxProcesses
			$appPool.processModel.identityType = "SpecificUser"
			$appPool.managedRuntimeVersion = ""
			$appPool | Set-Item
			Start-WebAppPool $apn
		} -Args $poolName, $login, $password, $limitProcesses
	}
	
	[void]StartPool([System.Management.Automation.Runspaces.PSSession]$session, [string]$poolName)
	{
		Invoke-Command -Session $session -ScriptBlock { param($apn) Import-Module WebAdministration; Start-WebAppPool $apn} -Args $poolName
	}
	
	[void]StopPool([System.Management.Automation.Runspaces.PSSession]$session, [string]$poolName)
	{
		try
		{
			Invoke-Command -Session $session -ScriptBlock { param($apn) Import-Module WebAdministration; Stop-WebAppPool $apn} -Args $poolName
		}
		catch [system.exception]
		{
		}
	}
	
	[bool]IsPoolExists([System.Management.Automation.Runspaces.PSSession]$session, [string]$poolName)
	{
		try
		{
			Invoke-Command -Session $session -ScriptBlock { param($apn) Import-Module WebAdministration; Get-WebAppPoolState $apn} -Args $poolName
			return $true
		}
		catch [system.exception]
		{
			return $false
		}
	}
	
	[bool]IsPoolStarted([System.Management.Automation.Runspaces.PSSession]$session, [string]$poolName)
	{
		try
		{
			return Invoke-Command -Session $session -ScriptBlock `
			{ 
				param($apn) 
				Import-Module WebAdministration; 
				$state = Get-WebAppPoolState $apn
				return $state.value -eq "Started" -Or $state.value -eq "Stopping"
			} -Args $poolName
		}
		catch [system.exception]
		{
			return $false
		}
	}
	
	#-------------------------------------------------------------------------------
	
	[void]StopService($session, $serviceName)
	{
		Invoke-Command -Session $session -ScriptBlock { param($apn) Stop-Service $apn} -Args $serviceName
	}
	
	[bool]IsServiceExists($session, $serviceName)
	{
		try
		{
			Invoke-Command -Session $session -ScriptBlock { param($apn) Get-Service -Name $apn} -Args $serviceName
			return $true
		}
		catch [system.exception]
		{
			return $false
		}
	}
	
	[bool]IsServiceStarted($session, $serviceName)
	{
		$service = Invoke-Command -Session $session -ScriptBlock { param($apn) Get-Service -Name $apn} -Args $serviceName
		return $service.Status -eq "running"
	}
	
	[string]GetServicePath($session, $serviceName)
	{
		$path = Invoke-Command -Session $session -ScriptBlock `
		{
			param($apn)
			$service = Get-WmiObject win32_service | ?{$_.Name -like $apn}
			$serviceExecutePath = $service.PathName -Replace '"(.*?)".*', '$1'
			$servicePath = [System.IO.Path]::GetDirectoryName($serviceExecutePath);
			return $servicePath
		} -Args $serviceName
		return $path
	}
}

class Deployer
{
	[Handlers]$Handle = [Handlers]::new();
	
	[bool]Deploy([Context]$context)
	{
		if (![System.IO.Directory]::Exists($context.ToolsPath))
		{
			Write-Host "Tools folder not exists!" -ForegroundColor Red
			return $false;
		}
		
		$toolsJson = $this.BackupJson($context.ToolsPath)
		$replaced = $this.Handle.ReplaceConfiguration($context, $toolsJson, $false)
		if ($replaced -eq $false)
		{
			Write-Host "Missing configuration params. Fill configuration file." -ForegroundColor Red
			return $false;
		}
		
		$context.DatabaseType = $this.GetDatabaseType($context)
		if ($context.DatabaseType -eq [DeployDatabase]::None)
		{
			Write-Host "Unknown database type." -ForegroundColor Red
			return $false;
		}
		
		$context.State.ResetBlock();
		if ($context.Settings.TadminCredentialId -gt 0)
		{
			$credential = $context.Settings.GetCredential($context.Settings.TadminCredentialId);
			$context.TadminSession = $this.Login($context, ".", $credential)
		}
		else
		{
			$context.TadminSession = $null
		}
		foreach($block in $context.Modules.Blocks)
		{
			$context.State.IncreaseBlock();
			$context.Debug(" > Executing block $($block.Name) Enabled: $($block.Enabled) SaveState: $($block.SaveState) Mode: $($block.DeployMode) DatabaseType: $($block.DatabaseType) Install: $($block.InstallMode) Group: $($block.DeployGroup) MinVersion: $($block.MinVersion) MaxVersion: $($block.MaxVersion) Depends: $($block.Depends)")
			if ($block.Enabled -eq $false)
			{
				continue;
			}
			if ($block.DeployMode -ne $context.DeployMode -And $block.DeployMode -ne [DeployMode]::All)
			{
				continue;
			}
			if ($block.DatabaseType -ne $context.DatabaseType -And $block.DatabaseType -ne [DeployDatabase]::None)
			{
				continue;
			}
			if ($block.InstallMode -ne $context.InstallMode -And $block.InstallMode -ne [InstallMode]::All)
			{
				continue;
			}
			if ($block.MinVersion -ge 0 -And $block.MinVersion -gt $context.Version)
			{
				continue;
			}
			if ($block.MaxVersion -ge 0 -And $block.MaxVersion -lt $context.Version)
			{
				continue;
			}
			if ([string]::IsNullOrWhitespace($block.Depends) -eq $false -And $context.Params[$block.Depends.Trim('$')] -ne $true)
			{
				continue
			}
			Write-Host "--------------------------------------------------------------------------------"
			if ($block.DeployGroup -eq [DeployGroup]::Binary)
			{
				$result = $this.DeployBinary($context, $block)
				if ($result -eq $false)
				{
					return $false;
				}
				continue;
			}
			$context.State.ResetGroup();
			for ($i=0; $i -lt $block.Groups.Count; $i++)
			{
				$group = $block.Groups[$i]
				$context.State.IncreaseGroup($group.Name);
				$context.Debug(" > Executing group $($group.Name) Enabled: $($group.Enabled) SaveState: $($group.SaveState) Mode: $($group.DeployMode)  DatabaseType: $($group.DatabaseType) Install: $($group.InstallMode) MinVersion: $($group.MinVersion) MaxVersion: $($group.MaxVersion) Depends: $($group.Depends) FallbackGroup: $($group.FallbackGroup) FallbackGroupParam: $($group.FallbackGroupParam)")
				if ($group.Enabled -eq $false)
				{
					continue;
				}
				if ($group.DeployMode -ne $context.DeployMode -And $group.DeployMode -ne [DeployMode]::All)
				{
					continue;
				}
				if ($group.DatabaseType -ne $context.DatabaseType -And $group.DatabaseType -ne [DeployDatabase]::None)
				{
					continue;
				}
				if ($group.InstallMode -ne $context.InstallMode -And $group.InstallMode -ne [InstallMode]::All)
				{
					continue;
				}
				if ($group.MinVersion -ge 0 -And $group.MinVersion -gt $context.Version)
				{
					continue;
				}
				if ($group.MaxVersion -ge 0 -And $group.MaxVersion -lt $context.Version)
				{
					continue;
				}
				if ([string]::IsNullOrWhitespace($group.Depends) -eq $false -And $context.Params[$group.Depends.Trim('$')] -ne $true)
				{
					continue
				}
				if ([string]::IsNullOrWhitespace($group.Message) -eq $false)
				{
					Write-Host " > $($context.ReplaceVariables($group.Message))"
				}
				$context.State.ResetAction();
				foreach($action in $group.Actions)
				{
					$context.State.IncreaseAction();
					$context.Debug("    - Executing action $($action.Name) Enabled: $($action.Enabled) SaveState: $($action.SaveState) Mode: $($action.DeployMode) DatabaseType: $($action.DatabaseType) Install: $($action.InstallMode) MinVersion: $($action.MinVersion) MaxVersion: $($action.MaxVersion) Depends: $($action.Depends)")
					if ($action.Enabled -eq $false)
					{
						continue;
					}
					if ($action.DeployMode -ne $context.DeployMode -And $action.DeployMode -ne [DeployMode]::All)
					{
						continue;
					}
					if ($action.DatabaseType -ne $context.DatabaseType -And $action.DatabaseType -ne [DeployDatabase]::None)
					{
						continue;
					}
					if ($action.InstallMode -ne $context.InstallMode -And $action.InstallMode -ne [InstallMode]::All)
					{
						continue;
					}
					if ($action.MinVersion -ge 0 -And $action.MinVersion -gt $context.Version)
					{
						continue;
					}
					if ($action.MaxVersion -ge 0 -And $action.MaxVersion -lt $context.Version)
					{
						continue;
					}
					if ([string]::IsNullOrWhitespace($action.Depends) -eq $false -And $context.Params[$action.Depends.Trim('$')] -ne $true)
					{
						continue;
					}
					if ($context.State.IsFallback())
					{
						continue;
					}
					if ([string]::IsNullOrWhitespace($action.Message) -eq $false)
					{
						Write-Host "    - $($context.ReplaceVariables($action.Message))"
					}
					if ($context.State.SkipAction() -eq $true)
					{
						continue;
					}
					Set-Location $context.SupplyPath
					$result = $this.Handle.Execute($context, $action);
					if ($result -eq $false)
					{
						if ([string]::IsNullOrEmpty($group.FallbackGroup) -eq $false -And $context.State.FiredFallbacks.Contains($group.FallbackGroup) -eq $false)
						{
							$context.Debug("    - Execution failed: fallback request")
							$context.State.SetFallback($true, $group.FallbackGroup)
							continue;
						}
						$context.Debug("    - Execution failed")
						return $false;
					}
					if ($block.SaveState -And $group.SaveState -And $action.SaveState)
					{
						$context.State.Save($context.Params);
					}
				}
				if ($context.State.IsFallback())
				{
					if ([string]::IsNullOrEmpty($group.FallbackGroupParam.Trim('$')) -eq $false)
					{
						$context.Params[$group.FallbackGroupParam.Trim('$')] = $true
					}
					$fallbackGroup = $context.State.GetFallbackName();
					$currentGroup = $context.State.GetFallbackIndex($fallbackGroup)
					$context.State.SetFallback($false, $null)
					$context.State.SetGroup($currentGroup);
					$context.State.ResetAction();
					$i = $currentGroup
				}
			}
		}
		if ($context.TadminSession -ne $null)
		{
			$this.Logout($context.TadminSession)
		}
		$context.State.Remove();
		$context.SaveActionState();
		return $true
	}
	
	[bool]DeployBinary([Context]$context, [BuildActionBlock]$block)
	{
		$context.State.ResetGroup();
		for ($i=0; $i -lt $block.Groups.Count; $i++)
		{
			$group = $block.Groups[$i]
			$context.State.IncreaseGroup($group.Name);
			$context.Debug(" > Executing group $($group.Name) Enabled: $($group.Enabled) SaveState: $($group.SaveState) Target: $($group.DeployTarget) Platform: $($group.DeployPlatform) Mode: $($group.DeployMode)  DatabaseType: $($group.DatabaseType) Install: $($group.InstallMode) MinVersion: $($group.MinVersion) MaxVersion: $($group.MaxVersion) Depends: $($group.Depends) FallbackGroup: $($group.FallbackGroup) FallbackGroupParam: $($group.FallbackGroupParam)")
			if ($group.Enabled -eq $false)
			{
				continue;
			}
			if ($group.DeployMode -ne $context.DeployMode -And $group.DeployMode -ne [DeployMode]::All)
			{
				continue;
			}
			if ($group.DatabaseType -ne $context.DatabaseType -And $group.DatabaseType -ne [DeployDatabase]::None)
			{
				continue;
			}
			if ($group.InstallMode -ne $context.InstallMode -And $group.InstallMode -ne [InstallMode]::All)
			{
				continue;
			}
			if ($group.MinVersion -ge 0 -And $group.MinVersion -gt $context.Version)
			{
				continue;
			}
			if ($group.MaxVersion -ge 0 -And $group.MaxVersion -lt $context.Version)
			{
				continue;
			}
			if ([string]::IsNullOrWhitespace($group.Depends) -eq $false -And $context.Params[$group.Depends.Trim('$')] -ne $true)
			{
				continue
			}
			$servers = $context.Settings.Servers | where { $_.Type -eq $group.DeployPlatform -And $_.Modules.Type -eq $group.DeployTarget -And $_.Modules.Enabled -eq $true }
			foreach($server in $servers)
			{
				$context.ServerContext = [ServerContext]::new($context, $server);
				$credential = $context.Settings.GetCredential($server.CredentialId);
				$context.ServerContext.Session = $this.Login($context, $server.Address, $credential)
				$modules = $server.Modules | where { $_.Type -eq $group.DeployTarget -And $_.Enabled -eq $true }
				foreach($module in $modules)
				{
					$context.ServerContext.Module = $module
					$context.ServerContext.InitParams();
					if ([string]::IsNullOrWhitespace($group.Message) -eq $false)
					{
						Write-Host " > $($context.ServerContext.ReplaceVariables($group.Message))"
					}
					$context.State.ResetAction();
					foreach($action in $group.Actions)
					{
						$context.State.IncreaseAction();
						$context.Debug("    - Executing action $($action.Name) Enabled: $($action.Enabled) SaveState: $($action.SaveState) Mode: $($action.DeployMode)  DatabaseType: $($action.DatabaseType) Install: $($action.InstallMode) MinVersion: $($action.MinVersion) MaxVersion: $($action.MaxVersion) Depends: $($action.Depends)")
						if ($action.Enabled -eq $false)
						{
							continue;
						}
						if ($action.DeployMode -ne $context.DeployMode -And $action.DeployMode -ne [DeployMode]::All)
						{
							continue;
						}
						if ($action.DatabaseType -ne $context.DatabaseType -And $action.DatabaseType -ne [DeployDatabase]::None)
						{
							continue;
						}
						if ($action.InstallMode -ne $context.InstallMode -And $action.InstallMode -ne [InstallMode]::All)
						{
							continue;
						}
						if ($action.MinVersion -ge 0 -And $action.MinVersion -gt $context.Version)
						{
							continue;
						}
						if ($action.MaxVersion -ge 0 -And $action.MaxVersion -lt $context.Version)
						{
							continue;
						}
						if ([string]::IsNullOrWhitespace($action.Depends) -eq $false -And $context.Params[$action.Depends.Trim('$')] -ne $true)
						{
							continue;
						}
						if ($context.State.IsFallback())
						{
							continue;
						}
						if ([string]::IsNullOrWhitespace($action.Message) -eq $false)
						{
							Write-Host "    - $($context.ServerContext.ReplaceVariables($action.Message))"
						}
						if ($context.State.SkipAction() -eq $true)
						{
							continue;
						}
						Set-Location $context.SupplyPath
						$result = $this.Handle.Execute($context, $action);
						if ($result -eq $false)
						{
							if ([string]::IsNullOrEmpty($group.FallbackGroup) -eq $false -And $context.State.FiredFallbacks.Contains($group.FallbackGroup) -eq $false)
							{
								$context.Debug("    - Execution failed: fallback request")
								$context.State.SetFallback($true, $group.FallbackGroup)
								continue;
							}
							$context.Debug("    - Execution failed")
							return $false;
						}
						if ($block.SaveState -And $group.SaveState -And $action.SaveState)
						{
							$context.State.Save($context.Params);
						}
					}
				}
				$this.Logout($context.ServerContext.Session)
			}
			if ($context.State.IsFallback())
			{
				if ([string]::IsNullOrEmpty($group.FallbackGroupParam.Trim('$')) -eq $false)
				{
					$context.Params[$group.FallbackGroupParam.Trim('$')] = $true
				}
				$fallbackGroup = $context.State.GetFallbackName();
				$currentGroup = $context.State.GetFallbackIndex($fallbackGroup)
				$context.State.SetFallback($false, $null)
				$context.State.SetGroup($currentGroup);
				$context.State.ResetAction();
				$i = $currentGroup
			}
		}
		$context.ServerContext = $null;
		return $true;
	}
	
	[string]BackupJson([string]$tools)
	{
		# Подготовка app.json для установки
		$toolsJson = [System.IO.Path]::Combine($tools, "app.json");
		$toolsJsonBak = [System.IO.Path]::Combine($tools, "app.json.bak");
		if ([System.IO.File]::Exists($toolsJsonBak))
		{
			del $toolsJson
			cp $toolsJsonBak $toolsJson
		}
		else
		{
			cp $toolsJson $toolsJsonBak
		}
		return $toolsJson
	}
	
	[System.Management.Automation.Runspaces.PSSession]Login([Context]$context, [string]$computerName, [Credential]$credential)
	{
		switch($credential.Type)
		{
			Login
			{
				$securePassword = ConvertTo-SecureString $credential.Params["Password"] -AsPlainText -Force
				$cred = [System.Management.Automation.PSCredential]::new($credential.Params["Login"], $securePassword)
				$session = New-PsSession -ComputerName $computerName -Credential $cred
				return $session;
			}
			Windows
			{
				$session = New-PsSession -ComputerName $computerName
				return $session;
			}
			Request
			{
				$cred = Get-Credential
				$session = New-PsSession -ComputerName $computerName -Credential $cred
				return $session;
			}
			Ssh
			{
				if ($context.UsePwsh -eq $false)
				{
					throw [System.Exception] "Use powershell 7 for ssh login!"
				}
				
				if ([string]::IsNullOrEmpty($credential.Params["KeyFilePath"]) -eq $false)
				{
					$session = New-PsSession -HostName $computerName -UserName $credential.Params["Login"] -KeyFilePath $credential.Params["KeyFilePath"]
				}
				else
				{
					$session = New-PsSession -HostName $computerName -UserName $credential.Params["Login"]
				}
				return $session;
			}
			default
			{
				throw [System.Exception] "Invalid login type $($credential.Type) !"
			}
		}
		throw [System.Exception] "Invalid login type $($credential.Type) !"
	}
	
	[void]Logout([System.Management.Automation.Runspaces.PSSession]$session)
	{
		Remove-PSSession -Session $session
	}
	
	[DeployDatabase]GetDatabaseType([Context]$context)
	{
		$this.Handle.Tadmin($context, "CheckDatabase", @("/dbms"), $false, $true, $false, $true, $false, $false)
		if ([string]::IsNullOrEmpty($context.Output) -eq $true)
		{
			return [DeployDatabase]::None
		}
		switch($context.Output)
		{
			"ms"
			{
				return [DeployDatabase]::MSSQL;
			}
			"pg"
			{
				return [DeployDatabase]::PostgreSQL;
			}
		}
		return [DeployDatabase]::None
	}
}

#-------------------------------------------------------------------------------

$encoding = [System.Text.Encoding]::GetEncoding("cp866")
[Console]::InputEncoding = $encoding
[Console]::OutputEncoding = $encoding

$PSNativeCommandArgumentPassing = "Legacy"
$startDate = [DateTime]::Now
Write-Host "Tessa Installation"
Write-Host

if ([Security.Principal.WindowsIdentity]::GetCurrent().Groups -contains 'S-1-5-32-544' -eq $false)
{
	$Host.UI.WriteErrorLine("Deploy must be executed as Administrator!")
	exit 1
}
if (![System.IO.File]::Exists($ConfigurationPath))
{
	$Host.UI.WriteErrorLine("File $ConfigurationPath not exists!")
	exit 1
}
if (![System.IO.Directory]::Exists($SupplyPath))
{
	$Host.UI.WriteErrorLine("Supply path $SupplyPath not exists!")
	exit 1
}

if ([string]::IsNullOrWhitespace($SupplyStatePath) -eq $true)
{
	$SupplyStatePath = [System.IO.Path]::Combine($SupplyPath, "state.json")
}

$json = Get-Content $ConfigurationPath
Validate-Json($json)

$settingsJson = $json | ConvertFrom-Json;
$settings = [Settings]::new($settingsJson, $RebuildIndexes, $LicensePath);
$manifestJson = Get-Content $ManifestPath | ConvertFrom-Json;
$manifest = [Manifest]::new($manifestJson);
$context = [Context]::new($settings, $manifest, [System.IO.Path]::GetFullPath($SupplyPath), [System.IO.Path]::GetFullPath($StateActionsPath), [System.IO.Path]::GetFullPath($AdditionalActionsPath), [System.IO.Path]::GetFullPath($SupplyStatePath), $FullInstall);

Write-Host "Installation started at $($startDate.ToString('HH:mm:ss'))" -ForegroundColor Yellow
Write-Host "Hash:" $context.State.Hash
Write-Host "Supply path: $([System.IO.Path]::GetFullPath($SupplyPath))"
Write-Host "Configuration path: $([System.IO.Path]::GetFullPath($ConfigurationPath))"
Write-Host "License path: $([System.IO.Path]::GetFullPath($LicensePath))"
Write-Host "State path: $([System.IO.Path]::GetFullPath($SupplyStatePath))"
if ([string]::IsNullOrWhitespace($AdditionalActionsPath) -eq $false -And [System.IO.File]::Exists($AdditionalActionsPath))
{
	Write-Host "Additional actions: $([System.IO.Path]::GetFullPath($AdditionalActionsPath))"
}
Write-Host "Rebuild indexes:" $RebuildIndexes
Write-Host "Install mode:" $context.DeployMode
Write-Host

if ([System.IO.File]::Exists($SupplyStatePath) -And $IgnoreState -eq $false)
{
	if ($SilentContinue -eq $true -Or (Read-Host "Detected uncompleted installation. Continue it? (Y/N)") -eq "Y")
	{
		$context.State.Load($context.Params)
	}
}

$location = Get-Location;
try
{
	$deployer = [Deployer]::new();
	$result = $deployer.Deploy($context);
	
	Write-Host
	Write-Host "Elapsed time: " $([DateTime]::Now.Subtract($startDate))
	if ($result -eq $true)
	{
		Write-Host "Tessa is installed." -ForegroundColor Green
		Write-Host "Press any key to close..." -ForegroundColor Green
	}
	else
	{
		Write-Host "Tessa not installed." -ForegroundColor Red
		Write-Host "Press any key to close..." -ForegroundColor Red
		exit 1
	}
}
catch [system.exception]
{
	Write-Host $_.Exception | Format-List -Force
	Write-Host "Tessa not installed." -ForegroundColor Red
	Write-Host "Press any key to close..." -ForegroundColor Red
	exit 1
}
finally
{
	Set-Location $location
}