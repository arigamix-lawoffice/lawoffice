#!/bin/bash

POSITIONAL=()
while [[ $# -gt 0 ]]
do
key="$1"

case $key in
    --configurationDir)
    CONFIGURATION_DIR="$2"
    shift # past argument
    shift # past value
    ;;
    --configurationTestDir)
    CONFIGURATION_TEST_DIR="$2"
    shift # past argument
    shift # past value
    ;;
    --resourcesDir)
    RESOURCES_DIR="$2"
    shift # past argument
    shift # past value
    ;;
    --tools)
    TOOLS_DIR="$2"
    shift # past argument
    shift # past value
    ;;
    *)    # unknown option
    POSITIONAL+=("$1") # save it in an array for later
    shift # past argument
    ;;
esac
done

mkdir -p "${RESOURCES_DIR}/Sql"
mkdir -p "${RESOURCES_DIR}/Tsd"

echo "Updating Configuration.Test/Scheme/Default from Configuration/Scheme"

echo "Updating Scheme\Tessa.tsd"
dotnet ${TOOLS_DIR}/tadmin.dll  SchemeUpdate "${CONFIGURATION_DIR}/Scheme" "-include:${CONFIGURATION_TEST_DIR}/Scheme" -q -nologo

echo "Building script Default_ms.sql"
dotnet ${TOOLS_DIR}/tadmin.dll  SchemeScript "${CONFIGURATION_DIR}/Scheme" "-include:${CONFIGURATION_TEST_DIR}/Scheme" "-out:${RESOURCES_DIR}/Sql/Default_ms.sql" -dbms:SqlServer -notran -q -nologo

echo "Building script Default_pg.sql"
dotnet ${TOOLS_DIR}/tadmin.dll  SchemeScript "${CONFIGURATION_DIR}/Scheme" "-include:${CONFIGURATION_TEST_DIR}/Scheme" "-out:${RESOURCES_DIR}/Sql/Default_pg.sql" -dbms:PostgreSQL -notran -q -nologo

echo "Building compact Default.tsd"
dotnet ${TOOLS_DIR}/tadmin.dll  SchemeCompact "${CONFIGURATION_DIR}/Scheme" "-include:${CONFIGURATION_TEST_DIR}/Scheme" "-out:${RESOURCES_DIR}/Tsd/Default.tsd" -q -nologo