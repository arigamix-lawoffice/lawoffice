-- Скрипт требуется выполнить перед обновлением схемы на 3.6.0

-- ActionHistory.Request -> _ActionHistory.Request

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_ActionHistory') THEN

	DROP TABLE "_ActionHistory";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_ActionHistory" (
	"RowID" uuid NOT NULL,
	"Request" jsonb NULL,
	CONSTRAINT "pk__ActionHistory" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_ActionHistory" ("RowID")
SELECT "RowID" FROM "ActionHistory";
GO

-- BusinessProcessVersions.ProcessData -> _BusinessProcessVersions.ProcessData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_BusinessProcessVersions') THEN

	DROP TABLE "_BusinessProcessVersions";

  END IF;
END;
$$ ;

CREATE TABLE "_BusinessProcessVersions" (
	"RowID" uuid NOT NULL,
	"ProcessData" jsonb NULL,
	CONSTRAINT "pk__BusinessProcessVersions" PRIMARY KEY ("RowID"));

INSERT INTO "_BusinessProcessVersions" ("RowID")
SELECT "RowID" FROM "BusinessProcessVersions";

-- Deleted.Card -> _Deleted.Card

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Deleted') THEN

	DROP TABLE "_Deleted";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Deleted" (
	"ID" uuid NOT NULL,
	"Card" jsonb NULL,
	CONSTRAINT "pk__Deleted" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Deleted" ("ID")
SELECT "ID" FROM "Deleted";
GO

-- Errors.Request -> _Errors.Request

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Errors') THEN

	DROP TABLE "_Errors";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Errors" (
	"ID" uuid NOT NULL,
	"Request" jsonb NULL,
	CONSTRAINT "pk__Errors" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Errors" ("ID")
SELECT "ID" FROM "Errors";
GO

-- FileConverterCache.ResponseInfo -> _FileConverterCache.ResponseInfo

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_FileConverterCache') THEN

	DROP TABLE "_FileConverterCache";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_FileConverterCache" (
	"RowID" uuid NOT NULL,
	"ResponseInfo" jsonb NULL,
	CONSTRAINT "pk__FileConverterCache" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_FileConverterCache" ("RowID")
SELECT "RowID" FROM "FileConverterCache";
GO

-- Invalidate FileTemplates.PlaceholdersInfo

UPDATE "FileTemplates" 
SET "PlaceholdersInfo" = null
GO

-- Operations.Request,Response -> _Operations.Request,Response

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Operations') THEN

	DROP TABLE "_Operations";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Operations" (
	"ID" uuid NOT NULL,
	"Request" jsonb NULL,
	"Response" jsonb NULL,
	CONSTRAINT "pk__Operations" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Operations" ("ID")
SELECT "ID" FROM "Operations";
GO

-- Outbox.Info/InfoLegacy -> _Outbox.Info

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Outbox') THEN

	DROP TABLE "_Outbox";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Outbox" (
	"ID" uuid NOT NULL,
	"Info" jsonb NULL,
	CONSTRAINT "pk__Outbox" PRIMARY KEY ("ID"));
GO

DO $$
BEGIN
  IF EXISTS (
    SELECT 1
	FROM "information_schema"."columns"
	WHERE "column_name"='InfoLegacy' AND "table_name"='Outbox') THEN

    -- обновление с версии 3.5, где есть бинарная колонка InfoLegacy, которая удаляется
	INSERT INTO "_Outbox" ("ID")
	SELECT "ID" FROM "Outbox"
	WHERE "InfoLegacy" IS NOT NULL;

  ELSE

    -- обновление с версии 3.4 или раньше, где есть бинарная колонка Info, которая также удаляется
	INSERT INTO "_Outbox" ("ID")
	SELECT "ID" FROM "Outbox"
	WHERE "Info" IS NOT NULL;

  END IF;
END;
$$ ;
GO

-- PersonalRoles.Security -> _PersonalRoles.Security

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_PersonalRoles') THEN

	DROP TABLE "_PersonalRoles";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_PersonalRoles" (
	"ID" uuid NOT NULL,
	"Security" jsonb NULL,
	CONSTRAINT "pk__PersonalRoles" PRIMARY KEY ("ID"));
GO

INSERT INTO "_PersonalRoles" ("ID")
SELECT "ID" FROM "PersonalRoles";
GO

-- Templates.Card -> _Templates.Card

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Templates') THEN

	DROP TABLE "_Templates";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Templates" (
	"ID" uuid NOT NULL,
	"Card" jsonb NULL,
	CONSTRAINT "pk__Templates" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Templates" ("ID")
SELECT "ID" FROM "Templates";
GO

-- WfSatellite.Data -> _WfSatellite.Data

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WfSatellite') THEN

	DROP TABLE "_WfSatellite";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WfSatellite" (
	"ID" uuid NOT NULL,
	"Data" jsonb NULL,
	CONSTRAINT "pk__WfSatellite" PRIMARY KEY ("ID"));
GO

INSERT INTO "_WfSatellite" ("ID")
SELECT "ID" FROM "WfSatellite";
GO

-- WorkflowEngineErrors.ErrorData -> _WorkflowEngineErrors.ErrorData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineErrors') THEN

	DROP TABLE "_WorkflowEngineErrors";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WorkflowEngineErrors" (
	"RowID" uuid NOT NULL,
	"ErrorData" jsonb NULL,
	CONSTRAINT "pk__WorkflowEngineErrors" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_WorkflowEngineErrors" ("RowID")
SELECT "RowID" FROM "WorkflowEngineErrors";
GO

-- WorkflowEngineNodes.NodeData -> _WorkflowEngineNodes.NodeData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineNodes') THEN

	DROP TABLE "_WorkflowEngineNodes";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WorkflowEngineNodes" (
	"RowID" uuid NOT NULL,
	"NodeData" jsonb NULL,
	CONSTRAINT "pk__WorkflowEngineNodes" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_WorkflowEngineNodes" ("RowID")
SELECT "RowID" FROM "WorkflowEngineNodes";
GO

-- WorkflowEngineProcesses.ProcessData -> _WorkflowEngineProcesses.ProcessData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineProcesses') THEN

	DROP TABLE "_WorkflowEngineProcesses";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WorkflowEngineProcesses" (
	"RowID" uuid NOT NULL,
	"ProcessData" jsonb NULL,
	CONSTRAINT "pk__WorkflowEngineProcesses" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_WorkflowEngineProcesses" ("RowID")
SELECT "RowID" FROM "WorkflowEngineProcesses";
GO

-- WorkflowProcesses.Params -> _WorkflowProcesses.Params

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowProcesses') THEN

	DROP TABLE "_WorkflowProcesses";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WorkflowProcesses" (
	"RowID" uuid NOT NULL,
	"Params" jsonb NULL,
	CONSTRAINT "pk__WorkflowProcesses" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_WorkflowProcesses" ("RowID")
SELECT "RowID" FROM "WorkflowProcesses";
GO

-- WorkflowTasks.Params -> _WorkflowTasks.Params

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowTasks') THEN

	DROP TABLE "_WorkflowTasks";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_WorkflowTasks" (
	"RowID" uuid NOT NULL,
	"Params" jsonb NULL,
	CONSTRAINT "pk__WorkflowTasks" PRIMARY KEY ("RowID"));
GO

INSERT INTO "_WorkflowTasks" ("RowID")
SELECT "RowID" FROM "WorkflowTasks";
GO

-- Types.Definition -> _Types.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Types') THEN
	DROP TABLE "_Types";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_Types" (
	"ID" uuid NOT NULL,
	"Group" text NULL,
	"InstanceTypeID" int NOT NULL,
	"Flags" bigint NOT NULL,
	"Metadata" jsonb NOT NULL,
	"Definition" xml NOT NULL,
	CONSTRAINT "pk__Types" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Types" ("ID", "Definition", "Metadata", "InstanceTypeID", "Flags", "Group")
SELECT "ID", "Definition", '{}', 0, 0::bigint, NULL FROM "Types";
GO

-- Workplaces.Metadata -> _Workplaces.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Workplaces') THEN
	DROP TABLE "_Workplaces";
  END IF;
END;
$$ ;
GO

CREATE TABLE "_Workplaces" (
	"ID" uuid NOT NULL,
	"MetadataLegacy" text NULL,
	"Metadata" jsonb NULL,
	CONSTRAINT "pk__Workplaces" PRIMARY KEY ("ID"));
GO

INSERT INTO "_Workplaces" ("ID", "MetadataLegacy")
SELECT "ID", "Metadata" FROM "Workplaces";
GO

UPDATE "Workplaces" SET "Metadata" = '{}';
GO

-- SearchQueries.Metadata -> _SearchQueries.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_SearchQueries') THEN
	DROP TABLE "_SearchQueries";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_SearchQueries" (
	"ID" uuid NOT NULL,
	"MetadataLegacy" text NULL,
	"Metadata" jsonb NULL,
	"Name" text,
	"ViewAlias" text,
	"IsPublic" boolean NOT NULL DEFAULT false,
	"LastModified" timestamp(6) with time zone NOT NULL,
	"CreatedByUserID" uuid NOT NULL,
	"TemplateCompositionID" uuid,
	CONSTRAINT "pk__SearchQueries" PRIMARY KEY ("ID"));
GO

INSERT INTO "_SearchQueries" ("ID", "MetadataLegacy", "Name", "ViewAlias", "IsPublic", "LastModified", "CreatedByUserID", "TemplateCompositionID")
SELECT "ID", "Metadata", "Name", "ViewAlias", "IsPublic", "LastModified", "CreatedByUserID", "TemplateCompositionID" FROM "SearchQueries";
GO

UPDATE "SearchQueries" SET "Metadata" = '{}';
GO

-- PersonalRoleSatellite.Metadata -> _PersonalRoleSatellite.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_PersonalRoleSatellite') THEN

	DROP TABLE "_PersonalRoleSatellite";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_PersonalRoleSatellite" (
	"ID" uuid NOT NULL,
	"WorkplaceExtensionsLegacy" text NULL,
	"WorkplaceExtensions" jsonb NULL,
	CONSTRAINT "pk__PersonalRoleSatellite" PRIMARY KEY ("ID"));
GO

INSERT INTO "_PersonalRoleSatellite" ("ID", "WorkplaceExtensionsLegacy")
SELECT "ID", "WorkplaceExtensions" FROM "PersonalRoleSatellite";
GO

UPDATE "PersonalRoleSatellite" SET "WorkplaceExtensions" = '{}';
GO
