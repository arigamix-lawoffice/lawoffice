#!/bin/bash
Address="https://localhost"
Login="admin"
Password="admin"
CheckTimeout="10"
TESSA="Arigamix"

Database=""
Connection="default"

get_script_dir () {
     SOURCE="${BASH_SOURCE[0]}"
     # while $SOURCE is a symlink, resolve it
     while [ -h "$SOURCE" ]; do
          DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
          SOURCE="$( readlink "$SOURCE" )"
          # if $SOURCE was a relative symlink (so no -" as prefix, need to resolve it relative to the symlink base directory
          [[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE"
     done
     DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
     echo "$DIR"
}
CurrentDir="$(get_script_dir)"

Configuration="$CurrentDir/Configuration"
Tools="$CurrentDir/tools"

cd "$Tools"

exit_on_error () {
	ErrorLevel="$?"
	if [ "$ErrorLevel" != "0" ]; then
		echo
		echo "Export failed with error code: $ErrorLevel"
		echo "See the details in log file: $Tools/log.txt"
		exit 1
	fi
}

#Start
echo "This script will export configuration for an existing ${TESSA^^} installation"
echo
echo "Please check connection string prior to installation in configuration file:"
echo "$Tools/app-db.json"
echo
echo
echo "[Address] = $Address"
echo "[Database] = $Database"
echo "[Connection] = $Connection"
echo
echo "[Tools] = $Tools"
echo "[Configuration] = $Configuration"
echo
read -n1 -r -p "Press any key to begin the export or Ctrl+C to exit..." key

echo
echo "Exporting ${TESSA^^} configuration"
echo

if [ "$Database" == "" ]; then
	DbParam=""
	CheckDbParam="-db:"
else
	DbParam="-db:$Database"
	CheckDbParam="-db:$Database"
fi

echo " > Checking connection to database server"
./tadmin CheckDatabase -c "-cs:$Connection" $CheckDbParam "-timeout:$CheckTimeout" -q
exit_on_error

dbms="$(./tadmin CheckDatabase "-cs:$Connection" $CheckDbParam "-timeout:$CheckTimeout" -dbms)"
exit_on_error

echo "   DBMS = $dbms"
echo
if [ "$dbms" == "" ]; then
	exit 1
fi

echo " > Checking connection to web service"
./tadmin CheckService "-a:$Address" "-u:$Login" "-p:$Password" -timeout:$CheckTimeout -q
exit_on_error

echo

echo " > Exporting cards"
rm -rf "$Configuration/Cards/"
# types are exported in the same order they should be imported

echo "   - Settings"
# CardTypeFlags: Hidden=false, Singleton=true, Administrative=true

echo 'SELECT i."ID", t."Caption" FROM "Types" t INNER JOIN "Instances" i ON i."TypeID"=t."ID" WHERE (t."Flags" & 400) = 384 ORDER BY t."Caption"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Settings" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Currencies"
echo 'SELECT "ID", "Name" FROM "Currencies" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Currencies" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Roles: role generators"
echo 'SELECT "ID", "Name" FROM "RoleGenerators" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Roles" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Roles: static roles, dynamic roles, context roles"
echo 'SELECT "ID", "Name" FROM "Roles" WHERE "TypeID" IN (0, 3, 4) ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Roles" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Report permissions"
echo 'SELECT "ID", "Caption" FROM "ReportRolesRules" ORDER BY "Caption"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Report permissions" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Document types"
echo 'SELECT "ID", "Title" FROM "KrDocType" ORDER BY "Title"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Document types" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - KrProcess: stage templates"
echo 'SELECT "ID", "Name" FROM "KrStageTemplates" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:KrProcess" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - KrProcess: stage groups"
echo 'SELECT "ID", "Name" FROM "KrStageGroups" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:KrProcess" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - KrProcess: secondary processes"
echo 'SELECT "ID", "Name" FROM "KrSecondaryProcesses" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:KrProcess" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Task history group types"
echo 'SELECT "ID", "Caption" FROM "TaskHistoryGroupTypes" ORDER BY "Caption"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Task history group types" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - TaskKinds"
echo 'SELECT "ID", "Caption" FROM "TaskKinds" ORDER BY "Caption"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:TaskKinds" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Notification types"
echo 'SELECT "ID", "Name" FROM "NotificationTypes" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:NotificationTypes" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Notifications"
echo 'SELECT "ID", "Name" FROM "Notifications" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Notifications" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - ConditionTypes"
echo 'SELECT "ID", "Name" FROM "ConditionTypes" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:ConditionTypes" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - Access rules"
echo 'SELECT "ID", "Caption" FROM "KrPermissions" ORDER BY "Caption"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/Platform.jcardlib" "-o:Access rules" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - File templates"
echo 'SELECT "ID", "Name" FROM "FileTemplates" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/File templates.jcardlib" "-o:File templates" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo "   - VirtualFiles"
echo 'SELECT "ID", "Name" FROM "KrVirtualFiles" ORDER BY "Name"'|./tadmin Select $DbParam "-cs:$Connection" -q|./tadmin ExportCards "-l:$Configuration/Cards/File templates.jcardlib" "-o:VirtualFiles" "-a:$Address" "-u:$Login" "-p:$Password" -localize:en -q
exit_on_error

echo

echo " > Exporting localization"
./tadmin ExportLocalization "-o:$Configuration/Localization" "-a:$Address" "-u:$Login" "-p:$Password" -c -q
exit_on_error

echo " > Exporting scheme"
./tadmin ExportScheme "-o:$Configuration/Scheme" "-a:$Address" "-u:$Login" "-p:$Password" -q
exit_on_error

echo " > Exporting types"
./tadmin ExportTypes "-o:$Configuration/Types" "-a:$Address" "-u:$Login" "-p:$Password" -s -c -q
exit_on_error

echo " > Exporting views"
./tadmin ExportViews "-o:$Configuration/Views" "-a:$Address" "-u:$Login" "-p:$Password" -c -q
exit_on_error

echo " > Exporting workplaces"
./tadmin ExportWorkplaces "-o:$Configuration/Workplaces" "-a:$Address" "-u:$Login" "-p:$Password" -c -q
exit_on_error

echo
echo "${TESSA^^} is exported to $Configuration"
echo "Press any key to close..."
exit 0
