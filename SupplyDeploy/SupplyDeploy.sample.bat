@echo off
rem Use OEM-866 (Cyrillic) encoding
rem Пример батника для автоматической разворачиваемости поставок
rem Файл должен быть в кодировке UTF-8 (без BOM!)
rem Не редактировать блокнотом. Только notepad++!

powershell -command .\SupplyDeploy.ps1 ^
-SupplyPath 'C:\PROJECTS_Git\law-office\Supply\Test\' ^
-ConfigurationPath 'C:\PROJECTS_Git\law-office\SupplyConfiguration\configuration.xml' ^
-LicensePath 'C:\PROJECTS_Git\law-office\SupplyConfiguration\Syntellect.tlic' ^
-RebuildIndexes 1

::powershell -command .\SupplyBuild.ps1 ^
::-SupplyPath	'C:\Supply\Test\' ^
::-ConfigurationPath	'C:\Supply\configuration.xml' ^
::-LicensePath		'C:\Supply\Syntellect.tlic' ^
::-RebuildIndexes	1 ^

::SupplyPath		Путь к папке поставки тессы
::ConfigurationPath	Путь к файлу ответов (.xml)
::LicensePath		Путь к файлу лицензии
::RebuildIndexes	Пересчитывать ли индексы при установке

pause