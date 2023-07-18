@echo off

setlocal EnableExtensions
setlocal EnableDelayedExpansion

:Args
if "%~1"=="" goto :Start
if "%~1"=="/batch" set batch=1
shift
goto :Args

:Start

echo Searching for absent app-local-*.json files

for %%a in (
    "app-local-client.json",
    "app-local-server.json"
) do (
    if not exist "%%a" (
        echo    ^> Creating "%%~a" from "%%~a.sample"
        echo F|xcopy "%%~a.sample" "%%~a" /Q /R /Y>nul || goto :Error
    )
)

if not "%batch%"=="" goto :Finish

echo;
echo  ^> Done. Press any key to quit...

pause>nul
cls

:Finish
endlocal
goto :EOF

:Error
echo;
echo  ^> Error occured.
echo;
if "%batch%"=="" (
    echo  ^> Press any key to quit...
    pause>nul
)
exit 1
goto :Finish
