param(
	#Название файла поставки
	[Parameter(Mandatory=$true)][string]$ConfigurationPath
)

# add the required .NET assembly:
Add-Type -AssemblyName System.Xml.Linq;
Add-Type -AssemblyName System.Xml;

function GetOptionalValue($stringValue, $defaultValue = $true)
{
	$value = $null;
	if ([bool]::TryParse($stringValue, [ref]$value) -eq $true)
	{
		return $value;
	}
	return $defaultValue;
}

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

if (![System.IO.File]::Exists($ConfigurationPath))
{
	$Host.UI.WriteErrorLine("Configuration file not exists!")
	return;
}

#В xml не должно быть лишних символов, заменяйте:
#< = &lt;
#> = &gt;
#" = &quot;
#' = &apos;
#& = &amp;

try
{
	$settings = [System.Xml.Linq.XDocument]::Load($ConfigurationPath);
}
catch [system.exception]
{
	$Host.UI.WriteErrorLine("Failed to load $ConfigurationPath")
	return;
}

# Загрузка списка серверов для разворачивания
$services = $settings.Root.Element("Services");
$instanceAddress = $services.Attribute("url").Value;
$tessaLogin = $services.Attribute("login").Value;
$tessaPassword = $services.Attribute("password").Value;
$backup = [bool]::Parse($services.Attribute("backup").Value);
$key = $services.Attribute("key").Value;
$cipher = $services.Attribute("cipher").Value;
$fullTextSearch = GetOptionalValue $services.Attribute("fullTextSearch").Value $false;

$servers = [System.Linq.Enumerable]::ToList($services.Elements("service"));
$variables = [System.Collections.Generic.List[PsCustomObject]]::new();
$configVariables = [System.Linq.Enumerable]::ToList($settings.Root.Element("Variables").Elements("service"));
foreach ($config in $configVariables)
{
	$variable = [ordered]@{}
	$variable["Name"] = $config.Attribute("id").Value;
	$variable["Value"] = $config.Element("vname").Value;
	$variable["Comment"] = $config.Element("comment").Value;
	$variables.Add($variable)
}

$configServers = [System.Collections.Generic.List[PsCustomObject]]::new();
foreach ($server in $servers) 
{
	$address = $server.Element("address").Value;
	$login = $server.Attribute("login").Value;
	$password = $server.Attribute("password").Value;
	$web = $server.Element("web");
	
	$configServer = [ordered]@{}
	$configServer["Address"] = $address
	$configServer["CredentialId"] = 1
	$configServer["ServerType"] = "Windows";
	
	$modules = [System.Collections.Generic.List[PsCustomObject]]::new();
	if ([bool]::Parse($web.Value) -eq $true)
	{
		$poolName = $web.Attribute("name").Value;
		$useWinAuth = GetOptionalValue $web.Attribute("winAuth").Value;
		
		$params = [ordered]@{}
		$params["PoolName"] = $poolName
		$params["UseWinAuth"] = $useWinAuth
		$params["ChangeBinding"] = $false
		
		$serverConfig = [ordered]@{}
		$serverConfig["Enabled"] = $true
		$serverConfig["Type"] = "Server";
		$serverConfig["Params"] = $params;
		$modules.Add($serverConfig)
	}	
	
	$chronos = $server.Element("chronos");
	if ([bool]::Parse($chronos.Value) -eq $true)
	{
		$serviceName = $chronos.Attribute("name").Value;
		$instanceName = $chronos.Attribute("instanceName").Value;
		$startUp = GetOptionalValue $chronos.Attribute("startUp").Value;
		$useSystemDisk = GetOptionalValue $chronos.Attribute("useSystemDisk").Value;
		
		$params = [ordered]@{}
		$params["ServiceName"] = $serviceName
		$params["FolderName"] = $instanceName
		$params["StartUp"] = $startUp
		$params["UseSystemDisk"] = $useSystemDisk
		
		$chronosConfig = [ordered]@{}
		$chronosConfig["Enabled"] = $true
		$chronosConfig["Type"] = "Chronos";
		$chronosConfig["Params"] = $params;
		$modules.Add($chronosConfig)
	}
	$configServer["Modules"] = $modules
	$configServers.Add($configServer)
}

$credentials = [System.Collections.Generic.List[PsCustomObject]]::new();

$credentialParams = [ordered]@{}
$credentialParams["Login"] = $login
$credentialParams["Password"] = $password

$credential = [ordered]@{}
$credential["Id"] = 1
$credential["Type"] = "Login"
$credential["Params"] = $credentialParams
$credentials.Add($credential)

$setting = [ordered]@{}
$setting["Address"] = $instanceAddress
$setting["Login"] = $tessaLogin
$setting["Password"] = $tessaPassword
$setting["Backup"] = $backup
$setting["Debug"] = $false
$setting["SignatureKey"] = $key
$setting["ChipherKey"] = $cipher
$setting["UseFullTextSearch"] = $fullTextSearch
$setting["Credentials"] = $credentials

$info = [ordered]@{}
$info["Settings"] = $setting
$info["Servers"] = $configServers
$info["Variables"] = $variables
$info | ConvertTo-Json -Depth 100 | Format-Json | Out-File ($ConfigurationPath -Replace ".xml", ".json") -Encoding UTF8