@echo off
rem Use OEM-866 (Cyrillic) encoding

setlocal EnableExtensions
setlocal EnableDelayedExpansion

set "CurrentDir=%~dp0"
if "%CurrentDir:~-1%" == "\" (
    set "CurrentDir=%CurrentDir:~0,-1%"
)

set "RootDir=%CurrentDir%\.."

:Args
if "%~1"=="" goto :Start
if "%~1"=="/configurationDir" goto :ConfigurationDirArg
if "%~1"=="/configurationTestDir" goto :ConfigurationTestDirArg
if "%~1"=="/resourcesDir" goto :ConfigurationResourcesDirArg
if "%~1"=="/tools" goto :ToolsArg
if "%~1"=="/batch" set Batch=1
shift
goto :Args

:ConfigurationDirArg
shift
set "ConfigurationDir=%~1"
shift
goto :Args

:ConfigurationResourcesDirArg
shift
set "ResourcesDir=%~1"
shift
goto :Args

:ConfigurationTestDirArg
shift
set "ConfigurationTestDir=%~1"
shift
goto :Args

:ToolsArg
shift
set "Tools=%~1"
shift
goto :Args

:Start
if "%ConfigurationDir%" == "" (
    set "ConfigurationDir=%CurrentDir%\..\Configuration"
)

if "%ConfigurationTestDir%" == "" (
    set "ConfigurationTestDir=%CurrentDir%"
)

if "%ResourcesDir%" == "" (
    set "ResourcesDir=%ConfigurationTestDir%\Resources"
)

if "%Tools%" == "" (
    set "Tools=%RootDir%\Tools"
)

cd /D "%Tools%"
if not "%ErrorLevel%" == "0" goto :Fail

mkdir "%ResourcesDir%\Sql">nul 2>&1
mkdir "%ResourcesDir%\Tsd">nul 2>&1

echo;
echo ^> Updating 'Scheme\Platform.tsd'
tadmin.exe SchemeUpdate "%ConfigurationDir%\Scheme" "/include:%ConfigurationTestDir%\Scheme" /q /nologo
if not "%ErrorLevel%" == "0" goto :Fail

echo;
echo ^> Building script 'Default_ms.sql'
set "RelationPath=\Sql\Default_ms.sql"
tadmin.exe SchemeScript "%ConfigurationDir%\Scheme" "/include:%ConfigurationTestDir%\Scheme" "/out:%ResourcesDir%%RelationPath%" /dbms:SqlServer /notran /q /nologo
if not "%ErrorLevel%" == "0" goto :Fail

echo;
echo ^> Building script 'Default_pg.sql'
set "RelationPath=\Sql\Default_pg.sql"
tadmin.exe SchemeScript "%ConfigurationDir%\Scheme" "/include:%ConfigurationTestDir%\Scheme" "/out:%ResourcesDir%%RelationPath%" /dbms:PostgreSQL /notran /q /nologo
if not "%ErrorLevel%" == "0" goto :Fail

echo;
echo ^> Building compact 'Default.tsd'
set "RelationPath=\Tsd\Default.tsd"
tadmin.exe SchemeCompact "%ConfigurationDir%\Scheme" "/include:%ConfigurationTestDir%\Scheme" "/out:%ResourcesDir%%RelationPath%" /q /nologo
if not "%ErrorLevel%" == "0" goto :Fail

if not "%Batch%" == "" goto :Finish
echo;

echo ^> Done. Please, rebuild your test projects.
echo;
echo Press any key to close...
pause>nul
cls
goto :Finish

:Fail
echo;
echo Updating test configuration failed with error code: %ErrorLevel%
echo See the details in log file (if applicable): %Tools%\log.txt
echo;
echo Press any key to close...
pause>nul
cls
goto :Finish

:Finish
endlocal
goto :EOF

:GetProjectName resultVar "path"
set "Local=%~dp2"
set "%1=%Local:~0,-1%"
goto :EOF