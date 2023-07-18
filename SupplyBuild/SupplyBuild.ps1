param(
	#Путь к папке поставки тессы
	[string]$TessaFullPath = $null,
	#Название файла поставки
	[Parameter(Mandatory=$true)][AllowEmptyString()][string]$SupplyName = $null,
	#Нужно ли собирать полную поставку
	[System.Nullable[bool]]$IsFullSupply = $null,
	#Нужно ли включать карточки в сборку
	[System.Nullable[bool]]$WithCards = $null,
	#Архивировать ли сборку
	[System.Nullable[bool]]$ArchiveBuild = $null,
	#Включить файлы из каталога
	[string]$IncludeDirectory = $null,
	#Путь к репозиторию (по умолчанию на уровень выше относительно каталога с этим скриптом)
	[string]$RepoDir = "$($PSScriptRoot)\..",
	#Название файла манифеста
	[string]$ManifestPath = "manifest.json",
	#Название файла дополнительных действий
	[string]$AdditionalActionsPath = "actions.json",
	#Путь к Nuget
	[string]$NugetUrl = $null,
	#Загрузка артефакта
	[System.Nullable[bool]]$UploadArtifact = $null
)

#Падать при любой ошибке
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

enum BuildMode
{
	Diff
	Full
	All
}

enum ApplyMode
{
	Add
	Remove
	Replace
}

class Context
{
	[string]$RepoDir;
	[string]$NugetUrl;
	[string]$BuildFolder;
	[Settings]$Settings;
	[BuildModules]$Modules;
	[System.Collections.Generic.Dictionary[string,object]]$Variables;
	
	Context([string]$repo, [string]$nugetUrl, [Manifest]$manifest)
	{
		$this.RepoDir = $repo;
		$this.NugetUrl = $nugetUrl;
		$this.Settings = $manifest.Settings;
		$this.Modules = $manifest.Modules;
		$this.Variables = $manifest.Variables;
	}
	
	[void]Debug([string]$message)
	{
		if ($this.Settings.Debug)
		{
			Write-Host $message
		}
	}
}

class Manifest
{
	[Settings]$Settings;
	[BuildModules]$Modules;
	[System.Collections.Generic.Dictionary[string,object]]$Variables;
	
	Manifest([PSCustomObject]$manifest, [string]$additionalActions)
	{
		$this.Settings = [Settings]::new($manifest.Settings);
		$this.Modules = [BuildModules]::new($manifest.Modules);
		$this.Variables = [System.Collections.Generic.Dictionary[string,object]]::new();
		foreach ($property in $manifest.Variables.PSObject.Properties)
		{
			$this.Variables[$property.Name] = $property.Value
		}
		if ([System.IO.File]::Exists($additionalActions))
		{
			$actions = Get-Content $additionalActions | ConvertFrom-Json;
			foreach ($group in $actions.Actions)
			{
				$grp = [BuildActionGroup]::new($group)
				switch($grp.ApplyMode)
				{
					Add
					{
						$g = $this.Modules.Groups | Where { $_.Name -eq $grp.Name } | Select -index 0
						if ($g -ne $null)
						{
							foreach ($action in $grp.Actions)
							{
								if ([string]::IsNullOrWhitespace($action.ApendTo) -eq $false)
								{
									for ($i=0; $i -lt $g.Actions.Count; $i++)
									{
										if ($g.Actions[$i].Name -eq $action.ApendTo)
										{
											switch($action.ApplyMode)
											{
												Add
												{
													$g.Actions.Insert($i+1, $action)
												}
												Remove
												{
													$g.Actions.RemoveAt($i)
												}
												Replace
												{
													$g.Actions.Insert($i+1, $action)
													$g.Actions.RemoveAt($i)
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
									$g.Actions.Add($action)
								}
							}
						}
						else
						{
							if ([string]::IsNullOrWhitespace($grp.ApendTo) -eq $false)
							{
								for ($i=0; $i -lt $this.Modules.Groups.Count; $i++)
								{
									if ($this.Modules.Groups[$i].Name -eq $grp.ApendTo)
									{
										$this.Modules.Groups.Insert($i+1, $grp)
										break;
									}
								}
							}
							else
							{
								$this.Modules.Groups.Add($grp)
							}
						}
					}
					Remove
					{
						$g = $this.Modules.Groups | Where { $_.Name -eq $grp.Name } | Select -index 0
						if ($g -ne $null)
						{
							$this.Modules.Groups.Remove($g)
						}
					}
					Replace
					{
						$g = $this.Modules.Groups | Where { $_.Name -eq $grp.Name } | Select -index 0
						$g.Actions.Clear();
						foreach ($action in $grp.Actions)
						{
							$g.Actions.Add($action)
						}
						$g.Message = $grp.Message;
						$g.Enabled = $grp.Enabled;
					}
					default
					{
						throw [System.Exception] "Invalid apply mode $($grp.ApplyMode) !"
					}
				}
			}
		}
	}
}

class Settings
{
	[string]$TessaFullPath
	[string]$SupplyName
	[bool]$FullSupply
	[bool]$AddCards
	[bool]$ArchiveBuild
	[bool]$UploadArtifact
	[string]$IncludeDirectory
	[bool]$Debug
	
	Settings([PSCustomObject]$settings)
	{
		$this.TessaFullPath = $settings.TessaFullPath
		$this.SupplyName = $settings.SupplyName
		$this.FullSupply = [bool]::Parse($settings.FullSupply)
		$this.AddCards = [bool]::Parse($settings.AddCards)
		$this.ArchiveBuild = [bool]::Parse($settings.ArchiveBuild)
		$this.IncludeDirectory = $settings.IncludeDirectory
		$this.Debug = [bool]::Parse($settings.Debug)
	}
	
	static [object]ReadValue([object]$jsonValue, [object]$paramValue)
	{
		if ([string]::IsNullOrWhitespace($paramValue) -eq $false)
		{
			return $paramValue;
		}
		return $jsonValue;
	}
}

class BuildModules
{
	[Collections.Generic.List[BuildActionGroup]]$Groups;
	
	BuildModules([PSCustomObject]$groups)
	{
		$this.Groups = [Collections.Generic.List[BuildActionGroup]]::new()
		foreach ($group in $groups)
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
	[BuildMode]$BuildMode = [BuildMode]::All;
	[ApplyMode]$ApplyMode = [ApplyMode]::Add;
	[string]$Depends;
	[string]$ApendTo;
	[System.Collections.Generic.List[BuildAction]]$Actions;
	
	BuildActionGroup([PSCustomObject]$group)
	{
		$this.Name = $group.Name;
		$this.Message = $group.Message;
		$this.Enabled = [bool]::Parse($group.Enabled);
		if ($group.BuildMode -ne $null)
		{
			$this.BuildMode = [BuildMode]$group.BuildMode
		}
		if ($group.ApplyMode -ne $null)
		{
			$this.ApplyMode = [ApplyMode]$group.ApplyMode
		}
		$this.Depends = $group.Depends
		$this.ApendTo = $group.ApendTo;
		$this.Actions = [Collections.Generic.List[BuildAction]]::new()
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
	[string]$Type;
	[BuildMode]$BuildMode = [BuildMode]::All;
	[ApplyMode]$ApplyMode = [ApplyMode]::Add;
	[string]$Depends;
	[string]$ApendTo;
	[System.Collections.Generic.Dictionary[string,object]]$Params;
	
	BuildAction([PSCustomObject]$action)
	{
		$this.Name = $action.Name;
		$this.Message = $action.Message;
		$this.Enabled = [bool]::Parse($action.Enabled);
		$this.Type = $action.Type;
		if ($action.BuildMode -ne $null)
		{
			$this.BuildMode = [BuildMode]$action.BuildMode
		}
		if ($action.ApplyMode -ne $null)
		{
			$this.ApplyMode = [ApplyMode]$action.ApplyMode
		}
		$this.Depends = $action.Depends
		$this.ApendTo = $action.ApendTo;
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
		$this.Register("MSBuild", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildMsBuild($context, $this.ReadValue($param, "MSBuild"), $this.ReadValue($param, "Path"), $this.ReadOptionalValue($param, "Mode", "Release"))})
		$this.Register("Dotnet", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildDotnet($context, $this.ReadValue($param, "Path"), $this.ReadOptionalValue($param, "Mode", "Release"))})
		$this.Register("Publish", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.PublishDotnet($context, $this.ReadValue($param, "Project"), $this.ReadValue($param, "Path"), $this.ReadOptionalValue($param, "Mode", "Release"))})
		$this.Register("Npm", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildNpmApp($context, $this.ReadValue($param, "Path"))})
		$this.Register("Android", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildAndoidApp($context, $this.ReadValue($param, "Path"))})
		$this.Register("Java", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildJavaApp($context, $this.ReadValue($param, "Path"))})
		$this.Register("Copy", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CopyRepoItem($context, $this.ReadValue($param, "Source"), $this.ReadValue($param, "Destination"), $this.ReadValue($param, "Recurse"))})
		$this.Register("Remove", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.RemoveItem($context, $this.ReadValue($param, "Path"), $this.ReadValue($param, "Recurse"))})
		$this.Register("DistCopy", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.CopyDistItem($context, $this.ReadValue($param, "Source"), $this.ReadValue($param, "Destination"), $this.ReadValue($param, "Recurse"))})
		$this.Register("BuildInfo", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.BuildInfo($context, $this.ReadValue($param, "Destination"))})
		$this.Register("RNMerge", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ReleaseNotesMerge($context)})
		$this.Register("RNBuild", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.ReleaseNotesBuild($context, $this.ReadValue($param, "Destination"))})
		$this.Register("Script", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.Script($context, $this.ReadValue($param, "File"))})
		$this.Register("UploadArtifact", [System.Func[Context, [System.Collections.Generic.Dictionary[string,object]], bool]]{param($context, $param) return $this.UploadArtifact($context)})
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
			Write-Error "Unknown action type $($action.Type)!"
			return $false;
		}
		$context.Debug("Executing $($action.Name)")
		$result = $func.Invoke($context, $action.Params);
		if ($result -eq $false)
		{
			return $false;
		}
		return $true;
	}
	
	[object]ReadValue([System.Collections.Generic.Dictionary[string,object]]$param, [string]$key)
	{
		if ($param.ContainsKey($key))
		{
			return $param[$key];
		}
		throw [System.Exception] "Key $key not found"
	}
	
	[object]ReadOptionalValue([System.Collections.Generic.Dictionary[string,object]]$param, [string]$key, [object]$defaultValue = $null)
	{
		if ($param.ContainsKey($key))
		{
			return $param[$key];
		}
		return $defaultValue;
	}
	
	[string]GetFilePath([Context]$context, [string]$path)
	{
		foreach ($var in $context.Variables.Keys)
		{
			$path = $path.Replace("`$$var", $context.Variables[$var])
		}
		return $path
	}
	
	[bool]BuildMsBuild([Context]$context, [string]$msBuildPath, [string]$path, [string]$mode)
	{
		$path = Join-Path $context.RepoDir $path
		$context.Debug("Building solution file $($path)")
		if ([string]::IsNullOrWhitespace($context.NugetUrl) -eq $false)
		{
			& dotnet restore $path --source $context.NugetUrl --force | Out-Null
		}
		else
		{
			& dotnet restore $path | Out-Null
		}
		$output = [string](& $msBuildPath $path /t:Clean,Build /property:Configuration=$mode /nologo /clp:ErrorsOnly) | Out-String
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			return $false
		}
		return $true
	}
	
	[bool]BuildDotnet([Context]$context, [string]$path, [string]$mode)
	{
		$path = Join-Path $context.RepoDir $path
		$context.Debug("Build $($path)")
		if ([string]::IsNullOrWhitespace($context.NugetUrl) -eq $false)
		{
			$output = & dotnet build $path -c $mode --source $context.NugetUrl --force | Out-String
		}
		else
		{
			$output = & dotnet build $path -c $mode | Out-String
		}
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			return $false
		}
		return $true
	}
	
	[bool]PublishDotnet([Context]$context, [string]$project, [string]$path, [string]$mode)
	{
		$project = Join-Path $context.RepoDir $project
		$path = Join-Path $context.RepoDir $path
		if ([System.IO.Directory]::Exists($path))
		{
			$context.Debug("Cleanup path $($path)")
			del $path -Force -Recurse
		}
		$context.Debug("Publish solution file $($project)")
		if ([string]::IsNullOrWhitespace($context.NugetUrl) -eq $false)
		{
			$output = & dotnet publish --source $context.NugetUrl --force -c $mode -r win-x64 -o $path $project | Out-String
		}
		else
		{
			$output = & dotnet publish -c $mode -r win-x64 -o $path $project | Out-String
		}
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			return $false
		}
		Get-ChildItem -Path $path -Include cs, de, es, fr, it, ja, ko, pl, pt-BR, ru, tr, zh-Hans, zh-Hant, *.Development.json -Recurse | Remove-Item -Force -Recurse #remove garbage from iframe
		return $true
	}
	
	[bool]BuildNpmApp([Context]$context, [string]$path)
	{
		$path = Join-Path $context.RepoDir $path
		$loc = Get-Location
		Set-Location $path
		
		if ([System.IO.Directory]::Exists("$path\node_modules"))
		{
			del "$path\node_modules" -Force -Recurse
		}
		
		$output = Invoke-Command -Script { $ErrorActionPreference="SilentlyContinue"; & npm install --silent --no-progress 2>&1 | Out-String }
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		$output = Invoke-Command -Script { $ErrorActionPreference="SilentlyContinue"; & npm run build --silent --no-progress 2>&1 | Out-String }
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		Set-Location $loc
		return $true
	}
	
	[bool]BuildAndoidApp([Context]$context, [string]$path)
	{
		$path = Join-Path $context.RepoDir $path
		$loc = Get-Location
		Set-Location $path
		
		if ([System.IO.Directory]::Exists("$path\node_modules"))
		{
			del "$path\node_modules" -Force -Recurse
		}
		
		$output = Invoke-Command -Script { $ErrorActionPreference="SilentlyContinue"; & npm install --silent --no-progress 2>&1 | Out-String }
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		
		if ([System.IO.Directory]::Exists("$path\android\app\build"))
		{
			del "$path\android\app\build" -Force -Recurse
		}
		
		$androidPath = [System.IO.Path]::Combine($path, 'android')
		Set-Location $androidPath
		
		$output = Invoke-Command -Script { $ErrorActionPreference="SilentlyContinue"; & .\gradlew assembleRelease --no-daemon 2>&1 |% ToString }
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		Set-Location $loc
		return $true
	}
	
	[bool]BuildJavaApp([Context]$context, [string]$path)
	{
		$path = Join-Path $context.RepoDir $path
		$loc = Get-Location
		Set-Location $path
		
		$output = & mvn clean | Out-String
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		
		$output = & mvn package | Out-String
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			Set-Location $loc
			return $false
		}
		Set-Location $loc
		return $true
	}
	
	[bool]Archive([Context]$context, [string]$path)
	{
		if ($context.Settings.ArchiveBuild -eq $false)
		{
			return $false;
		}
		
		if ([string]::IsNullOrWhitespace("$env:7ZipInstall") -eq $false)
		{
			set-alias sz "$env:7ZipInstall\7z.exe" 
		}
		elseif ((test-path "$env:ProgramFiles(x86)\7-Zip\7z.exe") -eq $true)
		{
			set-alias sz "$env:ProgramFiles(x86)\7-Zip\7z.exe" 
		}
		elseif ((test-path "$env:ProgramFiles\7-Zip\7z.exe") -eq $true)
		{
			set-alias sz "$env:ProgramFiles\7-Zip\7z.exe" 
		}
		else
		{
			Write-Host "    - 7z not found" -ForegroundColor Yellow
			return $false;
		}
		
		Write-Host ' > Archiving build'
		$buildFiles = [string]::Format("{0}\*", $path)
		$file = [string]::Format("{0}.7z", $path)
		sz a -t7z -mqs -mx=9 -m0=IA64 -m1=LZMA2:d=32m -aoa -sdel $file $buildFiles
		del $path
		$hash = Get-FileHash $file -Algorithm MD5
		Write-Host "    - MD5 hash: $($hash.Hash)"
		return $true;
	}
	
	[void]Manifest([Context]$context, [string]$buildFolder)
	{
		#hg identify --num
		cd $context.RepoDir
		$node = ((git branch --show-current) | Out-String).Trim()
		if ([string]::IsNullOrWhitespace($node))
		{
			$node = ((git show -s --pretty=%D) | Out-String).Trim()
			if ($node -match 'origin/(?<node>.*?),')
			{
				$node = $Matches.node
			}
		}
		$revision = ((git log -1 --pretty=format:'%H') | Out-String).Trim()
		$revisionDate = [DateTime]((git show -s --format='%ci') | Out-String).Trim()
		$user = ((git config user.name) | Out-String).Trim()
		$token = ((git config remote.origin.url) | Out-String).Trim()
		if ($token.Contains("://gitlab-ci-token:"))
		{
			$author = "CI/CD module build"
		}
		elseif ([string]::IsNullOrWhitespace($user) -eq $false)
		{
			$email = ((git config user.email) | Out-String).Trim()
			$author = "$user ($email)"
		}
		else
		{
			$author = "unknown"
		}
		
		$path = [System.IO.Path]::Combine($buildFolder, "manifest.json");
		
		$info = [ordered]@{}
		$info["Author"] = $author
		$info["Branch"] = $node
		$info["Revision"] = $revision
		$info["RevisionDate"] = $revisionDate.ToString("dd.MM.yyyy HH:mm:ss")
		$info["IsFullSupply"] = $context.Settings.FullSupply
		$info["SupplyDate"] = [DateTime]::Now.ToString("dd.MM.yyyy HH:mm:ss")
		$info | ConvertTo-Json -Depth 100 | Format-Json | Out-File $path -Encoding UTF8
	}
	
	[bool]BuildInfo([Context]$context, [string]$path)
	{
		$dest = Join-Path $context.BuildFolder $path
		#hg identify --num
		cd $context.RepoDir
		$node = ((git branch --show-current) | Out-String).Trim()
		if ([string]::IsNullOrWhitespace($node))
		{
			$node = ((git show -s --pretty=%D) | Out-String).Trim()
			if ($node -match 'origin/(?<node>.*?),')
			{
				$node = $Matches.node
			}
		}
		$revision = ((git log -1 --pretty=format:'%h') | Out-String).Trim()
		$revisionDate = ((git show -s --date=format:'%Y-%m-%d' --format='%cd') | Out-String).Trim()
		$user = ((git config user.name) | Out-String).Trim()
		$token = ((git config remote.origin.url) | Out-String).Trim()
		if ($token.Contains("://gitlab-ci-token:"))
		{
			$author = "CI/CD module build"
		}
		elseif ([string]::IsNullOrWhitespace($user))
		{
			$email = ((git config user.email) | Out-String).Trim()
			$author = "$user ($email)"
		}
		else
		{
			$author = "unknown"
		}
		
		$path = [System.IO.Path]::Combine($dest, "patch-$($revision).json");
		$patch = [ordered]@{}
		$patch["Name"] = $revision
		$patch["Date"] = $revisionDate
		
		$patches = [System.Collections.Generic.List[PsCustomObject]]::new();
		$patches.Add($patch)
		
		$settings = [ordered]@{}
		$settings["Patch"] = $patches
		
		$info = [ordered]@{}
		$info["Settings"] = $settings
		$info | ConvertTo-Json -Depth 100 | Format-Json | Out-File $path -Encoding UTF8
		return $true;
	}
	
	[bool]ReleaseNotesMerge([Context]$context)
	{
		cd $context.RepoDir
		& dotnet 'ReleaseNotes\MergeReleaseNotes.dll' 'ReleaseNotes\rn-*.txt' | Out-Null
		return $true
	}
	
	[bool]ReleaseNotesBuild([Context]$context, [string]$path)
	{
		$dest = Join-Path $context.BuildFolder $path
		cd $context.RepoDir
		Set-Location $context.RepoDir
		$bat = "$($context.RepoDir)\Docs\doc-build-releasenotes.bat"
		$output = Invoke-Command -Script { $ErrorActionPreference="SilentlyContinue"; & $bat /batch /out $dest | Out-String }
		if ($LASTEXITCODE -ne 0)
		{
			(Get-Host).UI.WriteErrorLine($output)
			return $false
		}
		return $true
	}
	
	[bool]CopyItem([Context]$context, [string]$source, [string]$dest, [bool]$isRecurse = $true)
	{
		$context.Debug("Coping from $($source) to $($dest) Recurse=$($isRecurse)")
		if ((Test-Path $source) -eq $false)
		{
			return $true;
		}
		[System.IO.Directory]::CreateDirectory($dest)
		if ($isRecurse)
		{
			Copy-Item $source $dest -Force -Recurse
		}
		else
		{
			Copy-Item $source $dest -Force
		}
		return $true;
	}
	
	[bool]RemoveItem([Context]$context, [string]$path, [bool]$isRecurse = $true)
	{
		$path = Join-Path $context.BuildFolder $path
		$context.Debug("Remove items at path $($path) Recurse=$($isRecurse)")
		if ((Test-Path $path) -eq $false)
		{
			return $true;
		}
		if ($isRecurse)
		{
			Remove-Item $path -Force -Recurse
		}
		else
		{
			Remove-Item $path -Force
		}
		return $true;
	}
	
	[bool]CopyRepoItem([Context]$context, [string]$source, [string]$dest, [bool]$isRecurse = $true)
	{
		$source = Join-Path $context.RepoDir $source;
		$dest = Join-Path $context.BuildFolder $dest
		$dest = $this.GetFilePath($context, $dest)
		return $this.CopyItem($context, $source, $dest, $isRecurse);
	}
	
	[bool]CopyDistItem([Context]$context, [string]$source, [string]$dest, [bool]$isRecurse = $true)
	{
		$source = Join-Path $context.Settings.TessaFullPath $source;
		$dest = Join-Path $context.BuildFolder $dest
		return $this.CopyItem($context, $source, $dest, $isRecurse);
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
		Write-Host "Build failed with error code: $LASTEXITCODE" -ForegroundColor Red
	}
	
	[bool]Script([Context]$context, $path)
	{
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
			$this.ReportError($context,  $_.Exception.Message);
			return $false;
		}
	}
	
	[bool]UploadArtifact([Context]$context)
	{
		if ($context.Settings.UploadArtifact -eq $false)
		{
			return $true;
		}
		
		$supplyFolder = [System.IO.Path]::Combine($context.RepoDir, 'Supply\')
		$archiveName = [System.IO.Directory]::GetFiles($supplyFolder, "*.7z")[0];
		$name = [System.IO.Path]::GetFileName($archiveName);
		
		Write-Host ' > Uploading build'
		
		if ([string]::IsNullOrWhitespace($env:JFROG_URL))
		{
			Write-Host "    - JFROG_URL is empty"
			return $false;
		}
		if ([string]::IsNullOrWhitespace($env:PROJECT_NAME))
		{
			Write-Host "    - PROJECT_NAME is empty"
			return $false;
		}
		
		$this.RemoveArtifact($context, $name);
		
		$url = "$($env:JFROG_URL)/artifactory/tessa/$($env:PROJECT_NAME)/$($name)"
		$context.Debug("Uploading to '$url'")
		
		$basic = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes($env:JFROG_USER + ":" + $env:JFROG_PASSWORD));
		$client = [System.Net.WebClient]::new()
		$client.Headers["Authorization"] = "Basic $basic"
		$client.Headers["Content-Type"] = "application/json"
		$client.UploadFile($url, "PUT", $archiveName)
		return $true
	}
	
	[void]RemoveArtifact([Context]$context, [string]$name)
	{
		$requestUrl = "$($env:JFROG_URL)/ui/api/v1/ui/search/artifact?name=$($env:CI_COMMIT_BRANCH)&repos=tessa"
		
		$context.Debug("Requesting for artifacts '$requestUrl'")
		
		$basic = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes($env:JFROG_USER + ":" + $env:JFROG_PASSWORD));
		$client = [System.Net.WebClient]::new()
		$client.Headers["Authorization"] = "Basic $basic"
		$client.Headers["Content-Type"] = "application/json"
		
		try
		{
			$artifacts = $client.DownloadString($requestUrl) | ConvertFrom-Json
			
			foreach ($artifact in $artifacts.results.uri)
			{
				try
				{
					$removeUrl = $artifact -replace '/api/storage/','/'
					
					if ($removeUrl.Contains("-$($env:CI_COMMIT_BRANCH)_") -eq $false -And $removeUrl.Contains("/$($env:CI_COMMIT_BRANCH)_") -eq $false)
					{
						continue
					}
					
					$context.Debug("Removing artifact '$removeUrl'")
					$client.UploadValues($removeUrl, "DELETE", [System.Collections.Specialized.NameValueCollection]::new());
				}
				catch [System.SystemException]
				{
					#ignored
				}
			}
		}
		catch [System.SystemException]
		{
			#ignored
		}
	}
}

class Builder
{
	[Handlers]$Handle = [Handlers]::new();
	
	[bool]Build([Context]$context)
	{
		if ([System.IO.Directory]::Exists($context.Settings.TessaFullPath) -eq $false)
		{
			Write-Host "Tessa Full Path is not exists!" -ForegroundColor Yellow
			return $false;
		}
		$supplyFolder = [System.IO.Path]::Combine($context.RepoDir, 'Supply\')
		$context.Debug("Constructing build with name $($context.Settings.SupplyName)")
		$buildStartDate = [DateTime]::Now
		
		$context.BuildFolder = [System.IO.Path]::Combine($supplyFolder, $context.Settings.SupplyName)
		if ([System.IO.Directory]::Exists($context.BuildFolder))
		{
			$context.Debug("Removing previous build folder $($context.BuildFolder)")
			del $context.BuildFolder -Force -Recurse
		}
		$context.Debug("Build folder $($context.BuildFolder)")
		
		$archive = [string]::Format("{0}.7z", $context.BuildFolder)
		if ([System.IO.File]::Exists($archive))
		{
			if ((Read-Host "File $($archive) already exists. Delete it? (Y/N)") -eq "Y")
			{
				$context.Debug('Removing previous build')
				del $archive -Force -Recurse
			}
			else
			{
				return $false;
			}
		}
		
		mkdir $context.BuildFolder | Out-Null
		foreach($group in $context.Modules.Groups)
		{
			$context.Debug(" > Executing group $($group.Name) Enabled: $($group.Enabled) Mode: $($group.BuildMode) Depends: $($group.Depends)")
			if ($group.Enabled -eq $false)
			{
				continue;
			}
			if ($group.BuildMode -eq [BuildMode]::Diff -And $context.Settings.FullSupply -eq $true)
			{
				continue;
			}
			if ($group.BuildMode -eq [BuildMode]::Full -And $context.Settings.FullSupply -eq $false)
			{
				continue;
			}
			if ([string]::IsNullOrWhitespace($group.Depends) -eq $false -And $context.Variables[$group.Depends.Trim('$')] -ne $true)
			{
				continue
			}
			if ([string]::IsNullOrWhitespace($group.Message) -eq $false)
			{
				Write-Host " > $($group.Message)"
			}
			foreach($action in $group.Actions)
			{
				$context.Debug("    - Executing action $($action.Name) Enabled: $($action.Enabled) Mode: $($action.BuildMode) Depends: $($action.Depends)")
				if ($action.Enabled -eq $false)
				{
					continue;
				}
				if ($action.BuildMode -eq [BuildMode]::Diff -And $context.Settings.FullSupply -eq $true)
				{
					continue;
				}
				if ($action.BuildMode -eq [BuildMode]::Full -And $context.Settings.FullSupply -eq $false)
				{
					continue;
				}
				if ([string]::IsNullOrWhitespace($action.Depends) -eq $false -And $context.Variables[$action.Depends.Trim('$')] -ne $true)
				{
					continue
				}
				if ([string]::IsNullOrWhitespace($action.Message) -eq $false)
				{
					Write-Host "    - $($action.Message)"
				}
				$result = $this.Handle.Execute($context, $action);
				if ($result -eq $false)
				{
					$context.Debug("    - Execution failed")
					return $false;
				}
			}
		}
		
		if ($context.Settings.IncludeDirectory -eq $true)
		{
			Write-Host ' > Copying additional files from include directory'
			$SupplyBuildDirectory = [System.IO.Path]::Combine($context.RepoDir, "SupplyBuild")
			cp ([System.IO.Path]::Combine($SupplyBuildDirectory, $context.Settings.IncludeDirectory, "*")) $context.BuildFolder -Force -Recurse
		}
		
		Write-Host ' > Removing garbage'
		Get-ChildItem $context.BuildFolder -include *.bak -recurse | foreach ($_) { Remove-Item $_.fullname }
		Get-ChildItem $context.BuildFolder -include *.orig -recurse | foreach ($_) { Remove-Item $_.fullname }
		
		if ($context.Settings.AddCards -eq $false)
		{
			Write-Host ' > Removing Cards'
			Remove-Item -Path "$($context.BuildFolder)\Configuration\Cards" -Recurse
		}
		
		Write-Host ' > Generating manifest'
		$this.Handle.Manifest($context, $context.BuildFolder)
		
		$result = $this.Handle.Archive($context, $context.BuildFolder)
		if ($result -eq $false)
		{
			$archive = $context.BuildFolder
		}
		
		$result = $this.Handle.UploadArtifact($context);
		if ($result -eq $false)
		{
			return $false;
		}
		
		Write-Host
		Write-Host "Elapsed time:" $([DateTime]::Now.Subtract($buildStartDate))
		Write-Host 'Build path: '
		Write-Host $archive -ForegroundColor Yellow
		Write-Host 'Build completed.' -ForegroundColor Green
		return $true
	}
}

# -=-=-=-=- Script start -=-=-=-=-

$json = Get-Content $ManifestPath | ConvertFrom-Json;
$manifest = [Manifest]::new($json, $AdditionalActionsPath)
$manifest.Settings.TessaFullPath = [Settings]::ReadValue($manifest.Settings.TessaFullPath, $TessaFullPath)
$manifest.Settings.SupplyName = [Settings]::ReadValue($manifest.Settings.SupplyName, $SupplyName)
$manifest.Settings.FullSupply = [Settings]::ReadValue($manifest.Settings.FullSupply, $IsFullSupply)
$manifest.Settings.AddCards = [Settings]::ReadValue($manifest.Settings.AddCards, $WithCards)
$manifest.Settings.ArchiveBuild = [Settings]::ReadValue($manifest.Settings.ArchiveBuild, $ArchiveBuild)
$manifest.Settings.UploadArtifact = [Settings]::ReadValue($manifest.Settings.UploadArtifact, $UploadArtifact)
$manifest.Settings.IncludeDirectory = [Settings]::ReadValue($manifest.Settings.IncludeDirectory, $IncludeDirectory)
$manifest.Settings.Debug = [bool]::Parse($manifest.Settings.Debug)

Write-Host "Tessa Packaging"
Write-Host
Write-Host "Packaging started at $([DateTime]::Now.ToString('HH:mm:ss'))" -ForegroundColor Yellow
Write-Host "Supply name is $($manifest.Settings.SupplyName)"
if ([string]::IsNullOrWhitespace($AdditionalActionsPath) -eq $false -And [System.IO.File]::Exists($AdditionalActionsPath))
{
	Write-Host "Additional actions: $AdditionalActionsPath"
}
Write-Host "Build is full: $($manifest.Settings.FullSupply)"
Write-Host "WithCards: $($manifest.Settings.AddCards)"
Write-Host "ArchiveBuid: $($manifest.Settings.ArchiveBuild)"
Write-Host "UploadArtifact: $($manifest.Settings.UploadArtifact)"
Write-Host
Write-Host "--------------------------------------------------------------------------------"

$builder = [Builder]::new();
$context = [Context]::new($RepoDir, $NugetUrl, $manifest)
$result = $builder.Build($context);
if ($result -eq $false)
{
	Write-Host "Build failed." -ForegroundColor Red
	[Environment]::Exit(1)
}
[Environment]::Exit(0)

Write-Host "--------------------------------------------------------------------------------"

return $Result