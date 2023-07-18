@echo off

setlocal EnableExtensions
setlocal EnableDelayedExpansion

set "Address=https://localhost/arigamix"
set "Login=admin"
set "Password=admin"
set "CheckTimeout=10"
set "Tessa=Arigamix"
set "TessaText=ARIGAMIX"

set "Database="
set "Connection=default"

set "CurrentDir=%~dp0"
if "%CurrentDir:~-1%" == "\" (
    set "CurrentDir=%CurrentDir:~0,-1%"
)

set "Configuration=%CurrentDir%\Configuration"
set "Tools=%CurrentDir%\tools"

cd /D "%Tools%"

:Start
cls
echo This script will export configuration for an existing %TessaText% installation
echo;
echo Please check connection string prior to installation in configuration file:
echo %Tools%\app-db.json
echo;
echo;
echo [Address] = %Address%
echo [Database] = %Database%
echo [Connection] = %Connection%
echo;
echo [Tools] = %Tools%
echo [Configuration] = %Configuration%
echo;
echo Press any key to begin the export...
pause>nul

cls
echo Exporting %TessaText% configuration
echo;

if "%Database%"=="" (
set DbParam=
set CheckDbParam="/db:"
) else (
set DbParam="/db:%Database%"
set CheckDbParam="/db:%Database%"
)

echo  ^> Checking connection to database
tadmin CheckDatabase /c "/cs:%Connection%" %CheckDbParam% /timeout:%CheckTimeout% /q
if not "%ErrorLevel%"=="0" goto :Fail

for /f "tokens=* usebackq" %%f in (`tadmin CheckDatabase "/cs:%Connection%" %CheckDbParam% /timeout:%CheckTimeout% /dbms`) do set dbms=%%f
echo    DBMS = %dbms%
if "%dbms%"=="" goto :Fail

echo;

echo  ^> Checking connection to web service
tadmin CheckService "/a:%Address%" "/u:%Login%" "/p:%Password%" /timeout:%CheckTimeout% /q
if not "%ErrorLevel%"=="0" goto :Fail

echo;

echo  ^> Exporting cards
rmdir "%Configuration%\Cards" /S /Q>nul 2>&1
rem types are exported in the same order they should be imported

echo    - Settings
rem CardTypeFlags: Hidden=false, Singleton=true, Administrative=true

echo SELECT i."ID", t."Caption" FROM "Types" t INNER JOIN "Instances" i ON i."TypeID"=t."ID" WHERE (t."Flags" ^^^& 400) = 384 ORDER BY t."Caption"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Settings" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Currencies
echo SELECT "ID", "Name" FROM "Currencies" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Currencies" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Roles: role generators
echo SELECT "ID", "Name" FROM "RoleGenerators" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Roles" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Roles: static roles, dynamic roles, context roles
echo SELECT "ID", "Name" FROM "Roles" WHERE "TypeID" IN (0, 3, 4) ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Roles" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Report permissions
echo SELECT "ID", "Caption" FROM "ReportRolesRules" ORDER BY "Caption"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Report permissions" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Document types
echo SELECT "ID", "Title" FROM "KrDocType" ORDER BY "Title"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Document types" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - KrProcess: stage templates
echo SELECT "ID", "Name" FROM "KrStageTemplates" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:KrProcess" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - KrProcess: stage groups
echo SELECT "ID", "Name" FROM "KrStageGroups" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:KrProcess" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - KrProcess: secondary processes
echo SELECT "ID", "Name" FROM "KrSecondaryProcesses" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:KrProcess" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Task history group types
echo SELECT "ID", "Caption" FROM "TaskHistoryGroupTypes" ORDER BY "Caption"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Task history group types" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - TaskKinds
echo SELECT "ID", "Caption" FROM "TaskKinds" ORDER BY "Caption"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:TaskKinds" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Notification types
echo SELECT "ID", "Name" FROM "NotificationTypes" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:NotificationTypes" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Notifications
echo SELECT "ID", "Name" FROM "Notifications" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Notifications" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - ConditionTypes
echo SELECT "ID", "Name" FROM "ConditionTypes" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:ConditionTypes" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - Access rules
echo SELECT "ID", "Caption" FROM "KrPermissions" ORDER BY "Caption"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\Platform.jcardlib" "/o:Access rules" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - File templates
echo SELECT "ID", "Name" FROM "FileTemplates" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\File templates.jcardlib" "/o:File templates" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo    - VirtualFiles
echo SELECT "ID", "Name" FROM "KrVirtualFiles" ORDER BY "Name"|tadmin Select %DbParam% "/cs:%Connection%" /q|tadmin ExportCards "/l:%Configuration%\Cards\File templates.jcardlib" "/o:VirtualFiles" "/a:%Address%" "/u:%Login%" "/p:%Password%" /localize:en /q
if not "%ErrorLevel%"=="0" goto :Fail

echo;

echo  ^> Exporting localization
tadmin ExportLocalization "/o:%Configuration%\Localization" "/a:%Address%" "/u:%Login%" "/p:%Password%" /c /q
if not "%ErrorLevel%"=="0" goto :Fail

echo  ^> Exporting scheme
tadmin ExportScheme "/o:%Configuration%\Scheme" "/a:%Address%" "/u:%Login%" "/p:%Password%" /q
if not "%ErrorLevel%"=="0" goto :Fail

echo  ^> Exporting types
tadmin ExportTypes "/o:%Configuration%\Types" "/a:%Address%" "/u:%Login%" "/p:%Password%" /s /c /q
if not "%ErrorLevel%"=="0" goto :Fail

echo  ^> Exporting views
tadmin ExportViews "/o:%Configuration%\Views" "/a:%Address%" "/u:%Login%" "/p:%Password%" /c /q
if not "%ErrorLevel%"=="0" goto :Fail

echo  ^> Exporting workplaces
tadmin ExportWorkplaces "/o:%Configuration%\Workplaces" "/a:%Address%" "/u:%Login%" "/p:%Password%" /c /q
if not "%ErrorLevel%"=="0" goto :Fail

echo;
echo Configuration is exported to "%Configuration%"
echo Press any key to close...
pause>nul
cls
goto :Finish

:Fail
echo;
echo Export failed with error code: %ErrorLevel%
echo See the details in log file: %Tools%\log.txt
echo;
echo Press any key to close...
pause>nul
cls
goto :Finish

:Finish
endlocal
goto :EOF
