-- Скрипт требуется выполнить перед обновлением схемы на 3.6.0

-- ActionHistory.Request -> _ActionHistory.Request

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_ActionHistory' AND [xtype]='U')
	DROP TABLE [_ActionHistory]
GO

CREATE TABLE [_ActionHistory]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Request] nvarchar(max) NULL
);
GO

INSERT INTO [_ActionHistory] ([RowID])
SELECT [RowID] FROM [ActionHistory] WITH (NOLOCK);
GO

-- BusinessProcessVersions.ProcessData -> _BusinessProcessVersions.ProcessData

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_BusinessProcessVersions' AND [xtype]='U')
	DROP TABLE [_BusinessProcessVersions]
GO

CREATE TABLE [_BusinessProcessVersions]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[ProcessData] nvarchar(max) NULL
);
GO

INSERT INTO [_BusinessProcessVersions] ([RowID])
SELECT [RowID] FROM [BusinessProcessVersions] WITH (NOLOCK);
GO

-- Deleted.Card -> _Deleted.Card

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Deleted' AND [xtype]='U')
	DROP TABLE [_Deleted]
GO

CREATE TABLE [_Deleted]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Card] nvarchar(max) NULL
);
GO

INSERT INTO [_Deleted] ([ID])
SELECT [ID] FROM [Deleted] WITH (NOLOCK);
GO

-- Errors.Request -> _Errors.Request

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Errors' AND [xtype]='U')
	DROP TABLE [_Errors]
GO

CREATE TABLE [_Errors]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Request] nvarchar(max) NULL
);
GO

INSERT INTO [_Errors] ([ID])
SELECT [ID] FROM [Errors] WITH (NOLOCK);
GO

-- FileConverterCache.ResponseInfo -> _FileConverterCache.ResponseInfo

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_FileConverterCache' AND [xtype]='U')
	DROP TABLE [_FileConverterCache]
GO

CREATE TABLE [_FileConverterCache]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[ResponseInfo] nvarchar(max) NULL
);
GO

INSERT INTO [_FileConverterCache] ([RowID])
SELECT [RowID] FROM [FileConverterCache] WITH (NOLOCK);
GO

-- Invalidate FileTemplates.PlaceholdersInfo

UPDATE [FileTemplates]
SET [PlaceholdersInfo] = NULL
GO

-- Operations.Request,Response -> _Operations.Request,Response

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Operations' AND [xtype]='U')
	DROP TABLE [_Operations]
GO

CREATE TABLE [_Operations]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Request] nvarchar(max) NULL,
	[Response] nvarchar(max) NULL
);
GO

INSERT INTO [_Operations] ([ID])
SELECT [ID] FROM [Operations] WITH (NOLOCK);
GO

-- Outbox.Info/InfoLegacy -> _Outbox.Info

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Outbox' AND [xtype]='U')
	DROP TABLE [_Outbox]
GO

CREATE TABLE [_Outbox]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Info] nvarchar(max) NULL
);
GO

IF EXISTS (SELECT 1 FROM [sys].[columns] WHERE [name]='InfoLegacy' AND [object_id] = OBJECT_ID(N'Outbox'))
BEGIN
    -- обновление с версии 3.5, где есть бинарная колонка InfoLegacy, которая удаляется
	EXEC sp_executesql N'INSERT INTO [_Outbox] ([ID])
		SELECT [ID] FROM [Outbox] WITH (NOLOCK)
		WHERE [InfoLegacy] IS NOT NULL';
END
ELSE
BEGIN
    -- обновление с версии 3.4 или раньше, где есть бинарная колонка Info, которая также удаляется
	INSERT INTO [_Outbox] ([ID])
	SELECT [ID] FROM [Outbox] WITH (NOLOCK)
	WHERE [Info] IS NOT NULL
END
GO

-- PersonalRoles.Security -> _PersonalRoles.Security

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_PersonalRoles' AND [xtype]='U')
	DROP TABLE [_PersonalRoles]
GO

CREATE TABLE [_PersonalRoles]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Security] nvarchar(max) NULL
);
GO

INSERT INTO [_PersonalRoles] ([ID])
SELECT [ID] FROM [PersonalRoles] WITH (NOLOCK);
GO

-- Templates.Card -> _Templates.Card

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Templates' AND [xtype]='U')
	DROP TABLE [_Templates]
GO

CREATE TABLE [_Templates]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Card] nvarchar(max) NULL
);
GO

INSERT INTO [_Templates] ([ID])
SELECT [ID] FROM [Templates] WITH (NOLOCK);
GO

-- WfSatellite.Data -> _WfSatellite.Data

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WfSatellite' AND [xtype]='U')
	DROP TABLE [_WfSatellite]
GO

CREATE TABLE [_WfSatellite]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Data] nvarchar(max) NULL
);
GO

INSERT INTO [_WfSatellite] ([ID])
SELECT [ID] FROM [WfSatellite] WITH (NOLOCK);
GO

-- WorkflowEngineErrors.ErrorData -> _WorkflowEngineErrors.ErrorData

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WorkflowEngineErrors' AND [xtype]='U')
	DROP TABLE [_WorkflowEngineErrors]
GO

CREATE TABLE [_WorkflowEngineErrors]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[ErrorData] nvarchar(max) NULL
);
GO

INSERT INTO [_WorkflowEngineErrors] ([RowID])
SELECT [RowID] FROM [WorkflowEngineErrors] WITH (NOLOCK);
GO

-- WorkflowEngineNodes.NodeData -> _WorkflowEngineNodes.NodeData

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WorkflowEngineNodes' AND [xtype]='U')
	DROP TABLE [_WorkflowEngineNodes]
GO

CREATE TABLE [_WorkflowEngineNodes]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[NodeData] nvarchar(max) NULL
);
GO

INSERT INTO [_WorkflowEngineNodes] ([RowID])
SELECT [RowID] FROM [WorkflowEngineNodes] WITH (NOLOCK);
GO

-- WorkflowEngineProcesses.ProcessData -> _WorkflowEngineProcesses.ProcessData

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WorkflowEngineProcesses' AND [xtype]='U')
	DROP TABLE [_WorkflowEngineProcesses]
GO

CREATE TABLE [_WorkflowEngineProcesses]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[ProcessData] nvarchar(max) NULL
);
GO

INSERT INTO [_WorkflowEngineProcesses] ([RowID])
SELECT [RowID] FROM [WorkflowEngineProcesses] WITH (NOLOCK);
GO

-- WorkflowProcesses.Params -> _WorkflowProcesses.Params

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WorkflowProcesses' AND [xtype]='U')
	DROP TABLE [_WorkflowProcesses]
GO

CREATE TABLE [_WorkflowProcesses]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Params] nvarchar(max) NULL
);
GO

INSERT INTO [_WorkflowProcesses] ([RowID])
SELECT [RowID] FROM [WorkflowProcesses] WITH (NOLOCK);
GO

-- WorkflowTasks.Params -> _WorkflowTasks.Params

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_WorkflowTasks' AND [xtype]='U')
	DROP TABLE [_WorkflowTasks]
GO

CREATE TABLE [_WorkflowTasks]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Params] nvarchar(max) NULL
);
GO

INSERT INTO [_WorkflowTasks] ([RowID])
SELECT [RowID] FROM [WorkflowTasks] WITH (NOLOCK);
GO

-- Types.Definition -> _Types.Metadata

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Types' AND [xtype]='U')
	DROP TABLE [_Types]
GO

CREATE TABLE [_Types]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Group] nvarchar(128) NULL,
	[InstanceTypeID] int NOT NULL,
	[Flags] bigint NOT NULL,
	[Metadata] nvarchar(max) NOT NULL,
	[Definition] xml NOT NULL
);
GO

INSERT INTO [_Types] ([ID], [Definition], [Metadata], [InstanceTypeID], [Flags], [Group])
SELECT [ID], [Definition], N'{}', 0, 0, NULL FROM [Types] WITH (NOLOCK);
GO

-- Workplaces.Metadata -> _Workplaces.Metadata

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_Workplaces' AND [xtype]='U')
	DROP TABLE [_Workplaces]
GO

CREATE TABLE [_Workplaces]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[MetadataLegacy] nvarchar(max) NULL,
	[Metadata] nvarchar(max) NULL
);
GO

INSERT INTO [_Workplaces] ([ID], [MetadataLegacy])
SELECT [ID], [Metadata] FROM [Workplaces] WITH (NOLOCK);
GO

UPDATE [Workplaces] SET [Metadata] = N'{}';
GO

-- SearchQueries.Metadata -> _SearchQueries.Metadata

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_SearchQueries' AND [xtype]='U')
	DROP TABLE [_SearchQueries]
GO

CREATE TABLE [_SearchQueries]
(
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[MetadataLegacy] [nvarchar](max) NOT NULL,
	[Metadata] [nvarchar](max) NULL,
	[ViewAlias] [nvarchar](128) NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[CreatedByUserID] [uniqueidentifier] NOT NULL,
	[TemplateCompositionID] [uniqueidentifier] NULL
);
GO

INSERT INTO [_SearchQueries] ([ID], [Name], [MetadataLegacy], [ViewAlias], [IsPublic], [LastModified], [CreatedByUserID], [TemplateCompositionID])
SELECT [ID], [Name], [Metadata], [ViewAlias], [IsPublic], [LastModified], [CreatedByUserID], [TemplateCompositionID] 
FROM [SearchQueries] WITH (NOLOCK);
GO

UPDATE [SearchQueries] SET [Metadata] = N'{}';
GO

-- PersonalRoleSatellite.Metadata -> _PersonalRoleSatellite.Metadata

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_PersonalRoleSatellite' AND [xtype]='U')
	DROP TABLE [_PersonalRoleSatellite]
GO

CREATE TABLE [_PersonalRoleSatellite]
(
	[ID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[WorkplaceExtensionsLegacy] nvarchar(max) NULL,
	[WorkplaceExtensions] nvarchar(max) NULL
);
GO

INSERT INTO [_PersonalRoleSatellite] ([ID], [WorkplaceExtensionsLegacy])
SELECT [ID], [WorkplaceExtensions] FROM [PersonalRoleSatellite] WITH (NOLOCK);
GO

UPDATE [PersonalRoleSatellite] SET [WorkplaceExtensions] = N'{}';
GO
