-- Скрипт требуется выполнить перед обновлением схемы на 3.7.0

-- Tasks -> _TaskAssignedRoles

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_TaskAssignedRoles' AND [xtype]='U')
	DROP TABLE [_TaskAssignedRoles]
GO

CREATE TABLE [_TaskAssignedRoles]
(
	[ID] uniqueidentifier NOT NULL,
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[TaskRoleID] uniqueidentifier NOT NULL,
	[RoleID] uniqueidentifier NOT NULL,
	[RoleName] nvarchar(128) NOT NULL,
	[RoleTypeID] uniqueidentifier NOT NULL,
	[Position] nvarchar(256) NULL,
	[ParentRowID] uniqueidentifier NULL,
	[Master] bit NOT NULL DEFAULT(0),
	[ShowInTaskDetails] bit NOT NULL DEFAULT(0),
);
GO

-- Authors

INSERT INTO [_TaskAssignedRoles]
([ID], [RowID], [TaskRoleID], [RoleID], [RoleName], [RoleTypeID], [Position], [ParentRowID], [Master], [ShowInTaskDetails])
SELECT
	[t].[RowID]								AS [ID],
	NEWID()									AS [RowID],
	'6BC228A0-E5A2-4F15-BF6D-C8E744533241'	AS [TaskRoleID],
	[t].[AuthorID]							AS [RoleID],
	[t].[AuthorName]						AS [RoleName],
	'929AD23C-8A22-09AA-9000-398BF13979B2' 	AS [RoleTypeID],
	[t].[AuthorPosition]					AS [Position],
	NULL									AS [ParentRowID],
	0										AS [Master],
	0										AS [ShowInTaskDetails]
FROM [Tasks] [t] WITH(NOLOCK)
WHERE NOT EXISTS (
	SELECT TOP 1 1 
	FROM [_TaskAssignedRoles] [tar] WITH(NOLOCK)
	WHERE [tar].[ID] = [t].[RowID]
	AND [tar].[TaskRoleID] = '6BC228A0-E5A2-4F15-BF6D-C8E744533241');
GO

-- Performers (контекстные роли переносятся как временные)

INSERT INTO [_TaskAssignedRoles]
([ID], [RowID], [TaskRoleID], [RoleID], [RoleName], [RoleTypeID], [Position], [ParentRowID], [Master], [ShowInTaskDetails])
SELECT
	[t].[RowID]								AS [ID],
	NEWID()									AS [RowID],
	'F726AB6C-A279-4D79-863A-47253E55CCC1'	AS [TaskRoleID],
	[t].[RoleID]							AS [RoleID],
	[t].[RoleName]							AS [RoleName],
	CASE [t].[RoleTypeID]
		WHEN 'B672E00C-0241-0485-9B07-4764BC96C9D3' -- ID временных ролей
			THEN 'E97C253C-9102-0440-AC7E-4876E8F789DA' -- ID контекстных ролей
			ELSE [t].[RoleTypeID]
	END										AS [RoleTypeID],
	NULL									AS [Position],
	NULL									AS [ParentRowID],
	1										AS [Master],
	1										AS [ShowInTaskDetails]
FROM [Tasks] [t] WITH(NOLOCK)
WHERE NOT EXISTS (
	SELECT TOP 1 1
	FROM [_TaskAssignedRoles] [tar] WITH(NOLOCK)
	WHERE [tar].[ID] = [t].[RowID]
	AND [tar].[TaskRoleID] = 'F726AB6C-A279-4D79-863A-47253E55CCC1');
GO

-- TaskHistory -> _TaskHistory

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_TaskHistory' AND [xtype]='U')
	DROP TABLE [_TaskHistory]
GO

CREATE TABLE [_TaskHistory]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[RoleName] nvarchar(128) NOT NULL,
	[Settings] nvarchar(max) NOT NULL
);
GO

-- Sessions

DELETE FROM [Sessions]
GO

-- CalendarExclusions -> _CalendarExclusions

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_CalendarExclusions' AND [xtype]='U')
	DROP TABLE [_CalendarExclusions]
GO

CREATE TABLE [_CalendarExclusions]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[StartTime] DateTime NULL,
	[EndTime] DateTime NULL,
	[IsNotWorkingTime] bit NOT NULL DEFAULT(1)
);
GO

INSERT INTO [_CalendarExclusions]
([RowID], [StartTime], [EndTime], [IsNotWorkingTime])
SELECT
	[ce].[RowID]							AS [RowID],
	[ce].[StartTime]						AS [StartTime],
	[ce].[EndTime]							AS [EndTime],
	[ce].[Type]								AS [IsNotWorkingTime]
FROM [CalendarExclusions] [ce] WITH(NOLOCK)
WHERE NOT EXISTS (
	SELECT TOP 1 1
	FROM [_CalendarExclusions] [_ce] WITH(NOLOCK)
	WHERE [_ce].[RowID] = [ce].[RowID]);
GO

-- CalendarSettings-> _CalendarTypeWeekDays

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_CalendarTypeWeekDays' AND [xtype]='U')
	DROP TABLE [_CalendarTypeWeekDays]
GO

CREATE TABLE [_CalendarTypeWeekDays]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[Number] smallint NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[WorkingDayStart] time NULL,
	[WorkingDayEnd] time NULL,
	[LunchStart] time NULL,
	[LunchEnd] time NULL,
	[IsNotWorkingDay] bit NOT NULL DEFAULT(1)
);
GO

INSERT INTO [_CalendarTypeWeekDays]
([RowID], [Number], [Name], [WorkingDayStart], [WorkingDayEnd], [LunchStart], [LunchEnd], [IsNotWorkingDay])
SELECT
	NEWID()									AS [RowID],
	0										AS [Number],
	'$Calendar_Days_Monday'					AS [Name],
	[cs].[WorkDayStart]						AS [WorkingDayStart],
	[cs].[WorkDayEnd]						AS [WorkingDayEnd],
	[cs].[LunchStart]						AS [LunchStart],
	[cs].[LunchEnd]							AS [LunchEnd],
	0										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	1										AS [Number],
	'$Calendar_Days_Tuesday'				AS [Name],
	[cs].[WorkDayStart]						AS [WorkingDayStart],
	[cs].[WorkDayEnd]						AS [WorkingDayEnd],
	[cs].[LunchStart]						AS [LunchStart],
	[cs].[LunchEnd]							AS [LunchEnd],
	0										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	2										AS [Number],
	'$Calendar_Days_Wednesday'				AS [Name],
	[cs].[WorkDayStart]						AS [WorkingDayStart],
	[cs].[WorkDayEnd]						AS [WorkingDayEnd],
	[cs].[LunchStart]						AS [LunchStart],
	[cs].[LunchEnd]							AS [LunchEnd],
	0										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	3										AS [Number],
	'$Calendar_Days_Thursday'				AS [Name],
	[cs].[WorkDayStart]						AS [WorkingDayStart],
	[cs].[WorkDayEnd]						AS [WorkingDayEnd],
	[cs].[LunchStart]						AS [LunchStart],
	[cs].[LunchEnd]							AS [LunchEnd],
	0										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	4										AS [Number],
	'$Calendar_Days_Friday'					AS [Name],
	[cs].[WorkDayStart]						AS [WorkingDayStart],
	[cs].[WorkDayEnd]						AS [WorkingDayEnd],
	[cs].[LunchStart]						AS [LunchStart],
	[cs].[LunchEnd]							AS [LunchEnd],
	0										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	5										AS [Number],
	'$Calendar_Days_Saturday'				AS [Name],
	NULL									AS [WorkingDayStart],
	NULL									AS [WorkingDayEnd],
	NULL									AS [LunchStart],
	NULL									AS [LunchEnd],
	1										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
UNION ALL
SELECT
	NEWID()									AS [RowID],
	6										AS [Number],
	'$Calendar_Days_Sunday'					AS [Name],
	NULL									AS [WorkingDayStart],
	NULL									AS [WorkingDayEnd],
	NULL									AS [LunchStart],
	NULL									AS [LunchEnd],
	1										AS [IsNotWorkingDay]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
GO

-- CalendarSettings-> _CalendarSettings

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_CalendarSettings' AND [xtype]='U')
	DROP TABLE [_CalendarSettings]
GO

CREATE TABLE [_CalendarSettings]
(
	[CalendarStart] DateTime NULL,
	[CalendarEnd] DateTime NULL
);
GO

INSERT INTO [_CalendarSettings]
([CalendarStart], [CalendarEnd])
SELECT	
	[cs].[CalendarStart]						AS [CalendarStart],
	[cs].[CalendarEnd]							AS [CalendarEnd]
FROM [CalendarSettings] [cs] WITH(NOLOCK)
GO