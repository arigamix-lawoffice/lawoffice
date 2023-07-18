-- Скрипт требуется выполнить после обновления схемы на 3.6.0

-- _ActionHistory.Request -> ActionHistory.Request

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_ActionHistory') THEN

	UPDATE "ActionHistory"
	SET "Request" = "t"."Request"
	FROM "_ActionHistory" AS "t"
	WHERE "ActionHistory"."RowID" = "t"."RowID";
	
	DROP TABLE "_ActionHistory";
	
  END IF;
END;
$$ ;
GO

-- _BusinessProcessVersions.ProcessData -> BusinessProcessVersions.ProcessData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_BusinessProcessVersions') THEN

	UPDATE "BusinessProcessVersions"
	SET "ProcessData" = "t"."ProcessData"
	FROM "_BusinessProcessVersions" AS "t"
	WHERE "BusinessProcessVersions"."RowID" = "t"."RowID";
	
	DROP TABLE "_BusinessProcessVersions";
	
  END IF;
END;
$$ ;
GO

-- _Deleted.Card -> Deleted.Card

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Deleted') THEN

	UPDATE "Deleted"
	SET "Card" = "t"."Card"
	FROM "_Deleted" AS "t"
	WHERE "Deleted"."ID" = "t"."ID";
	
	DROP TABLE "_Deleted";
	
  END IF;
END;
$$ ;
GO

-- _Errors.Request -> Errors.Request

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Errors') THEN

	UPDATE "Errors"
	SET "Request" = "t"."Request"
	FROM "_Errors" AS "t"
	WHERE "Errors"."ID" = "t"."ID";
	
	DROP TABLE "_Errors";
	
  END IF;
END;
$$ ;
GO

-- _FileConverterCache.ResponseInfo -> FileConverterCache.ResponseInfo

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_FileConverterCache') THEN

	UPDATE "FileConverterCache"
	SET "ResponseInfo" = "t"."ResponseInfo"
	FROM "_FileConverterCache" AS "t"
	WHERE "FileConverterCache"."RowID" = "t"."RowID";
	
	DROP TABLE "_FileConverterCache";
	
  END IF;
END;
$$ ;
GO

-- _Operations.Request,Response -> Operations.Request,Response

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Operations') THEN

	UPDATE "Operations"
	SET "Request" = "t"."Request", "Response" = "t"."Response"
	FROM "_Operations" AS "t"
	WHERE "Operations"."ID" = "t"."ID";
	
	DROP TABLE "_Operations";
	
  END IF;
END;
$$ ;
GO

-- _Outbox.Info -> Outbox.Info

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Outbox') THEN

	UPDATE "Outbox"
	SET "Info" = "t"."Info"
	FROM "_Outbox" AS "t"
	WHERE "Outbox"."ID" = "t"."ID";
	
	DROP TABLE "_Outbox";
	
  END IF;
END;
$$ ;
GO

-- _PersonalRoles.Security -> PersonalRoles.Security

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_PersonalRoles') THEN

	UPDATE "PersonalRoles"
	SET "Security" = "t"."Security"
	FROM "_PersonalRoles" AS "t"
	WHERE "PersonalRoles"."ID" = "t"."ID";
	
	DROP TABLE "_PersonalRoles";
	
  END IF;
END;
$$ ;
GO

-- _Templates.Card -> Templates.Card

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Templates') THEN

	UPDATE "Templates"
	SET "Card" = "t"."Card"
	FROM "_Templates" AS "t"
	WHERE "Templates"."ID" = "t"."ID";
	
	DROP TABLE "_Templates";
	
  END IF;
END;
$$ ;
GO

-- _WfSatellite.Data -> WfSatellite.Data

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WfSatellite') THEN

	UPDATE "WfSatellite"
	SET "Data" = "t"."Data"
	FROM "_WfSatellite" AS "t"
	WHERE "WfSatellite"."ID" = "t"."ID";
	
	DROP TABLE "_WfSatellite";
	
  END IF;
END;
$$ ;
GO

-- _WorkflowEngineErrors.ErrorData -> WorkflowEngineErrors.ErrorData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineErrors') THEN

	UPDATE "WorkflowEngineErrors"
	SET "ErrorData" = "t"."ErrorData"
	FROM "_WorkflowEngineErrors" AS "t"
	WHERE "WorkflowEngineErrors"."RowID" = "t"."RowID";
	
	DROP TABLE "_WorkflowEngineErrors";
	
  END IF;
END;
$$ ;
GO

-- _WorkflowEngineNodes.NodeData -> WorkflowEngineNodes.NodeData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineNodes') THEN

	UPDATE "WorkflowEngineNodes"
	SET "NodeData" = "t"."NodeData"
	FROM "_WorkflowEngineNodes" AS "t"
	WHERE "WorkflowEngineNodes"."RowID" = "t"."RowID";
	
	DROP TABLE "_WorkflowEngineNodes";
	
  END IF;
END;
$$ ;
GO

-- _WorkflowEngineProcesses.ProcessData -> WorkflowEngineProcesses.ProcessData

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowEngineProcesses') THEN

	UPDATE "WorkflowEngineProcesses"
	SET "ProcessData" = "t"."ProcessData"
	FROM "_WorkflowEngineProcesses" AS "t"
	WHERE "WorkflowEngineProcesses"."RowID" = "t"."RowID";
	
	DROP TABLE "_WorkflowEngineProcesses";
	
  END IF;
END;
$$ ;
GO

-- _WorkflowProcesses.Params -> WorkflowProcesses.Params

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowProcesses') THEN

	UPDATE "WorkflowProcesses"
	SET "Params" = "t"."Params"
	FROM "_WorkflowProcesses" AS "t"
	WHERE "WorkflowProcesses"."RowID" = "t"."RowID";
	
	DROP TABLE "_WorkflowProcesses";
	
  END IF;
END;
$$ ;
GO

-- _WorkflowTasks.Params -> WorkflowTasks.Params

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_WorkflowTasks') THEN

	UPDATE "WorkflowTasks"
	SET "Params" = "t"."Params"
	FROM "_WorkflowTasks" AS "t"
	WHERE "WorkflowTasks"."RowID" = "t"."RowID";
	
	DROP TABLE "_WorkflowTasks";
	
  END IF;
END;
$$ ;
GO

-- ServerInstance new tables: BackgroundColors, BlockColors, ForegroundColors

INSERT INTO "BackgroundColors" ("ID")
SELECT "ID"
FROM "ServerInstances"
WHERE NOT EXISTS (SELECT 1 FROM "BackgroundColors");
GO

INSERT INTO "BlockColors" ("ID")
SELECT "ID"
FROM "ServerInstances"
WHERE NOT EXISTS (SELECT 1 FROM "BlockColors");
GO

INSERT INTO "ForegroundColors" ("ID")
SELECT "ID"
FROM "ServerInstances"
WHERE NOT EXISTS (SELECT 1 FROM "ForegroundColors");
GO

-- _Types.Metadata -> Types.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Types') THEN

	UPDATE "Types"
	SET "Metadata" = "t"."Metadata",
		"InstanceTypeID" = "t"."InstanceTypeID",
		"Flags" = "t"."Flags",
		"Group" = "t"."Group"
	FROM "_Types" AS "t"
	WHERE "Types"."ID" = "t"."ID";
	
	DROP TABLE "_Types";
	
  END IF;
END;
$$ ;
GO

-- _Workplaces.Metadata -> Workplaces.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_Workplaces') THEN

	UPDATE "Workplaces"
	SET "Metadata" = "t"."Metadata"
	FROM "_Workplaces" AS "t"
	WHERE "Workplaces"."ID" = "t"."ID";
	
	DROP TABLE "_Workplaces";
	
  END IF;
END;
$$ ;
GO

-- _SearchQueries.Metadata -> SearchQueries.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_SearchQueries') THEN

	UPDATE "SearchQueries"
	SET "Metadata" = "t"."Metadata"
	FROM "_SearchQueries" AS "t"
	WHERE "SearchQueries"."ID" = "t"."ID";

	DROP TABLE "_SearchQueries";
	
  END IF;
END;
$$ ;
GO

-- _PersonalRoleSatellite.Metadata -> PersonalRoleSatellite.Metadata

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_PersonalRoleSatellite') THEN

	UPDATE "PersonalRoleSatellite"
	SET "WorkplaceExtensions" = "t"."WorkplaceExtensions"
	FROM "_PersonalRoleSatellite" AS "t"
	WHERE "PersonalRoleSatellite"."ID" = "t"."ID";
	
	DROP TABLE "_PersonalRoleSatellite";
	
  END IF;
END;
$$ ;
GO

-- Update name for "Check context role" workflow engine button extension.
UPDATE "BusinessProcessExtensions"
SET "ExtensionName" = '$WorkflowEngine_TilePermission_CheckRolesForExecution'
WHERE "ExtensionID" = '358a033c-8b13-4236-8da5-0fef1b87def4';
GO

-- Insert entry sections for KrSigning tasks

INSERT INTO "KrSigningTaskOptions" ("ID", "AllowAdditionalApproval")
SELECT "RowID", false
FROM "Tasks" AS "t"
WHERE "TypeID" = '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6'
  AND NOT EXISTS (
    SELECT NULL
    FROM "KrSigningTaskOptions" AS "ksto"
    WHERE "ksto"."ID" = "t"."RowID");
GO

INSERT INTO "KrAdditionalApproval" ("ID")
SELECT "RowID"
FROM "Tasks" AS "t"
WHERE "TypeID" = '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6'
  AND NOT EXISTS (
    SELECT NULL
    FROM "KrAdditionalApproval" AS "kaa"
    WHERE "kaa"."ID" = "t"."RowID");
GO
