@echo off
rem Use OEM-866 (Cyrillic) encoding
rem Пример батника для автоматической разворачиваемости поставок
rem Файл должен быть в кодировке UTF-8 (без BOM!)
rem Не редактировать блокнотом. Только notepad++!

powershell -command .\SupplyBuild.ps1 ^
-TessaFullPath 'C:\TessaArchive\arigamix-4.0.0\' ^
-IsFullSupply 1 ^
-WithCards 1 ^
-ArchiveBuild 1 ^
-IncludeDirectory ''

::powershell -command .\SupplyBuild.ps1 ^
::-TessaFullPath	'D:\Tessa\arigamix-4.0.0\' ^
::-SupplyName		'OS-12345' ^
::-IsFullSupply		1 ^
::-WithCards		1 ^
::-IncludeDirectory	'Include' ^
::-BranchName		'default'

::TessaFullPath		Путь к папке поставки arigamix
::SupplyName		Название файла поставки
::IsFullSupply		Нужно ли собирать полную поставку
::WithCards			Нужно ли включать карточки в сборку
::IncludeDirectory	Каталог с дополнительными файлами
::BranchName		Название ветки в репозитории (если не задана то переход не производится)

pause