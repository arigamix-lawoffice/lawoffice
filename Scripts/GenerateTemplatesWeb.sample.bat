@echo off
rem Use OEM-866 (Cyrillic) encoding
rem Пример батника генерации info файлов
rem Файл должен быть в кодировке UTF-8 (без BOM!)
rem Не редактировать блокнотом. Только notepad++!

:: Путь до корневой папки проекта
set ProjectPath=c:\law-office
:: Путь до папки, в которую должен генерироваться файл
set OutputFolder=WebClient SDK\src\law\info
:: Путь до папки с утилитой tadmin
set tadminPath=c:\law-office\Tools

cd %tadminPath%

echo;
echo ^> Generating templates

echo    - SchemeInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Scheme" /web /q /mode:Scheme /output:"%ProjectPath%\%OutputFolder%\schemeInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo    - TypeInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Types" /web /q /mode:Types /output:"%ProjectPath%\%OutputFolder%\typesInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo    - ViewInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Views" /web /q /mode:Views /output:"%ProjectPath%\%OutputFolder%\viewInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo    - WorkplaceInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Workplaces" /web /q /mode:Workplaces /output:"%ProjectPath%\%OutputFolder%\workplaceInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo    - RolesInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Cards\Roles" /web /q /mode:Roles /output:"%ProjectPath%\%OutputFolder%\rolesInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo    - LocalizationInfo
tadmin ConfigureGenerator "%ProjectPath%\Configuration\Localization" /web /q /mode:Localization /output:"%ProjectPath%\%OutputFolder%\localizationInfo.ts"
if not "%ErrorLevel%"=="0" goto :Fail

echo;
echo Press any key to close...
pause>nul
cls
goto :Finish

:Fail
echo Generation failed with error code: %ErrorLevel%
echo See the details in log file log.txt
echo;
echo Press any key to close...
pause>nul
cls
goto :Finish

:Finish
endlocal
goto :EOF