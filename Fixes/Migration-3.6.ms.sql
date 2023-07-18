-- Скрипт требуется выполнить после обновления схемы на 3.6.0

-- _ActionHistory.Request -> ActionHistory.Request

IF OBJECT_ID('_ActionHistory', 'U') IS NOT NULL
BEGIN

	UPDATE [ActionHistory]
	SET [Request] = [t].[Request]
	FROM [_ActionHistory] AS [t]
	WHERE [ActionHistory].[RowID] = [t].[RowID];

	DROP TABLE [_ActionHistory];

END
GO

-- _BusinessProcessVersions.ProcessData -> BusinessProcessVersions.ProcessData

IF OBJECT_ID('_BusinessProcessVersions', 'U') IS NOT NULL
BEGIN

	UPDATE [BusinessProcessVersions]
	SET [ProcessData] = [t].[ProcessData]
	FROM [_BusinessProcessVersions] AS [t]
	WHERE [BusinessProcessVersions].[RowID] = [t].[RowID];

	DROP TABLE [_BusinessProcessVersions];

END
GO

-- _Deleted.Card -> Deleted.Card

IF OBJECT_ID('_Deleted', 'U') IS NOT NULL
BEGIN

	UPDATE [Deleted]
	SET [Card] = [t].[Card]
	FROM [_Deleted] AS [t]
	WHERE [Deleted].[ID] = [t].[ID];

	DROP TABLE [_Deleted];

END
GO

-- _Errors.Request -> Errors.Request

IF OBJECT_ID('_Errors', 'U') IS NOT NULL
BEGIN

	UPDATE [Errors]
	SET [Request] = [t].[Request]
	FROM [_Errors] AS [t]
	WHERE [Errors].[ID] = [t].[ID];

	DROP TABLE [_Errors];

END
GO

-- _FileConverterCache.ResponseInfo -> FileConverterCache.ResponseInfo

IF OBJECT_ID('_FileConverterCache', 'U') IS NOT NULL
BEGIN

	UPDATE [FileConverterCache]
	SET [ResponseInfo] = [t].[ResponseInfo]
	FROM [_FileConverterCache] AS [t]
	WHERE [FileConverterCache].[RowID] = [t].[RowID];

	DROP TABLE [_FileConverterCache];

END
GO

-- _Operations.Request,Response -> Operations.Request,Response

IF OBJECT_ID('_Operations', 'U') IS NOT NULL
BEGIN

	UPDATE [Operations]
	SET [Request] = [t].[Request], [Response] = [t].[Response]
	FROM [_Operations] AS [t]
	WHERE [Operations].[ID] = [t].[ID];

	DROP TABLE [_Operations];

END
GO

-- _Outbox.Info -> Outbox.Info

IF OBJECT_ID('_Outbox', 'U') IS NOT NULL
BEGIN

	UPDATE [Outbox]
	SET [Info] = [t].[Info]
	FROM [_Outbox] AS [t]
	WHERE [Outbox].[ID] = [t].[ID];

	DROP TABLE [_Outbox];

END
GO

-- _PersonalRoles.Security -> PersonalRoles.Security

IF OBJECT_ID('_PersonalRoles', 'U') IS NOT NULL
BEGIN

	UPDATE [PersonalRoles]
	SET [Security] = [t].[Security]
	FROM [_PersonalRoles] AS [t]
	WHERE [PersonalRoles].[ID] = [t].[ID];

	DROP TABLE [_PersonalRoles];

END
GO

-- _Templates.Card -> Templates.Card

IF OBJECT_ID('_Templates', 'U') IS NOT NULL
BEGIN

	UPDATE [Templates]
	SET [Card] = [t].[Card]
	FROM [_Templates] AS [t]
	WHERE [Templates].[ID] = [t].[ID];

	DROP TABLE [_Templates];

END
GO

-- _WfSatellite.Data -> WfSatellite.Data

IF OBJECT_ID('_WfSatellite', 'U') IS NOT NULL
BEGIN

	UPDATE [WfSatellite]
	SET [Data] = [t].[Data]
	FROM [_WfSatellite] AS [t]
	WHERE [WfSatellite].[ID] = [t].[ID];

	DROP TABLE [_WfSatellite];

END
GO

-- _WorkflowEngineErrors.ErrorData -> WorkflowEngineErrors.ErrorData

IF OBJECT_ID('_WorkflowEngineErrors', 'U') IS NOT NULL
BEGIN

	UPDATE [WorkflowEngineErrors]
	SET [ErrorData] = [t].[ErrorData]
	FROM [_WorkflowEngineErrors] AS [t]
	WHERE [WorkflowEngineErrors].[RowID] = [t].[RowID];

	DROP TABLE [_WorkflowEngineErrors];

END
GO

-- _WorkflowEngineNodes.NodeData -> WorkflowEngineNodes.NodeData

IF OBJECT_ID('_WorkflowEngineNodes', 'U') IS NOT NULL
BEGIN

	UPDATE [WorkflowEngineNodes]
	SET [NodeData] = [t].[NodeData]
	FROM [_WorkflowEngineNodes] AS [t]
	WHERE [WorkflowEngineNodes].[RowID] = [t].[RowID];

	DROP TABLE [_WorkflowEngineNodes];

END
GO

-- _WorkflowEngineProcesses.ProcessData -> WorkflowEngineProcesses.ProcessData

IF OBJECT_ID('_WorkflowEngineProcesses', 'U') IS NOT NULL
BEGIN

	UPDATE [WorkflowEngineProcesses]
	SET [ProcessData] = [t].[ProcessData]
	FROM [_WorkflowEngineProcesses] AS [t]
	WHERE [WorkflowEngineProcesses].[RowID] = [t].[RowID];

	DROP TABLE [_WorkflowEngineProcesses];

END
GO

-- _WorkflowProcesses.Params -> WorkflowProcesses.Params

IF OBJECT_ID('_WorkflowProcesses', 'U') IS NOT NULL
BEGIN

	UPDATE [WorkflowProcesses]
	SET [Params] = [t].[Params]
	FROM [_WorkflowProcesses] AS [t]
	WHERE [WorkflowProcesses].[RowID] = [t].[RowID];

	DROP TABLE [_WorkflowProcesses];

END
GO

-- _WorkflowTasks.Params -> WorkflowTasks.Params

IF OBJECT_ID('_WorkflowTasks', 'U') IS NOT NULL
BEGIN

	UPDATE [WorkflowTasks]
	SET [Params] = [t].[Params]
	FROM [_WorkflowTasks] AS [t]
	WHERE [WorkflowTasks].[RowID] = [t].[RowID];

	DROP TABLE [_WorkflowTasks];

END
GO

-- ServerInstance new tables: BackgroundColors, BlockColors, ForegroundColors

INSERT INTO [BackgroundColors] ([ID])
SELECT [ID]
FROM [ServerInstances]
WHERE NOT EXISTS (SELECT 1 FROM [BackgroundColors])
GO

INSERT INTO [BlockColors] ([ID])
SELECT [ID]
FROM [ServerInstances]
WHERE NOT EXISTS (SELECT 1 FROM [BlockColors])
GO

INSERT INTO [ForegroundColors] ([ID])
SELECT [ID]
FROM [ServerInstances]
WHERE NOT EXISTS (SELECT 1 FROM [ForegroundColors])
GO

-- _Types.Metadata -> Types.Metadata

IF OBJECT_ID('_Types', 'U') IS NOT NULL
BEGIN

	UPDATE [Types]
	SET [Metadata] = [t].[Metadata],
		[InstanceTypeID] = [t].[InstanceTypeID],
		[Flags] = [t].[Flags],
		[Group] = [t].[Group]
	FROM [_Types] AS [t]
	WHERE [Types].[ID] = [t].[ID];

	DROP TABLE [_Types];

END
GO

-- _Workplaces.Metadata -> Workplaces.Metadata

IF OBJECT_ID('_Workplaces', 'U') IS NOT NULL
BEGIN

	UPDATE [Workplaces]
	SET [Metadata] = [t].[Metadata]
	FROM [_Workplaces] AS [t]
	WHERE [Workplaces].[ID] = [t].[ID];

	DROP TABLE [_Workplaces];

END
GO

-- _SearchQueries.Metadata -> SearchQueries.Metadata

IF OBJECT_ID('_SearchQueries', 'U') IS NOT NULL
BEGIN

	UPDATE [SearchQueries]
	SET [Metadata] = [t].[Metadata]
	FROM [_SearchQueries] AS [t]
	WHERE [SearchQueries].[ID] = [t].[ID];

	DROP TABLE [_SearchQueries];

END
GO

-- _PersonalRoleSatellite.Metadata -> PersonalRoleSatellite.Metadata

IF OBJECT_ID('_PersonalRoleSatellite', 'U') IS NOT NULL
BEGIN

	UPDATE [PersonalRoleSatellite]
	SET [WorkplaceExtensions] = [t].[WorkplaceExtensions]
	FROM [_PersonalRoleSatellite] AS [t]
	WHERE [PersonalRoleSatellite].[ID] = [t].[ID];

	DROP TABLE [_PersonalRoleSatellite];

END
GO

-- Update name for "Check context role" workflow engine button extension.
UPDATE [BusinessProcessExtensions]
SET [ExtensionName] = N'$WorkflowEngine_TilePermission_CheckRolesForExecution'
WHERE [ExtensionID] = '358a033c-8b13-4236-8da5-0fef1b87def4'
GO

-- Insert entry sections for KrSigning tasks

INSERT INTO [KrSigningTaskOptions] ([ID], [AllowAdditionalApproval])
SELECT [RowID], 0
FROM [Tasks] AS [t]
WHERE [TypeID] = '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6'
  AND NOT EXISTS (
    SELECT NULL
    FROM [KrSigningTaskOptions] AS [ksto]
    WHERE [ksto].[ID] = [t].[RowID]);
GO

INSERT INTO [KrAdditionalApproval] ([ID])
SELECT [RowID]
FROM [Tasks] AS [t]
WHERE [TypeID] = '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6'
  AND NOT EXISTS (
    SELECT NULL
    FROM [KrAdditionalApproval] AS [kaa]
    WHERE [kaa].[ID] = [t].[RowID]);
GO
