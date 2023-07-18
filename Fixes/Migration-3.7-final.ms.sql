-- Скрипт требуется выполнить после импорта конфигурации 3.7.0

-- Calendar for Tasks

UPDATE [Tasks]
SET
	[CalendarID] = [si].[DefaultCalendarID],
	[CalendarName] = [si].[DefaultCalendarName]
FROM [ServerInstances] [si] WITH(NOLOCK)
WHERE [CalendarID] IS NULL
GO

-- Calendar for TaskHiostory

UPDATE [TaskHistory]
SET
	[CalendarID] = [si].[DefaultCalendarID],
	[CalendarName] = [si].[DefaultCalendarName]
FROM [ServerInstances] [si] WITH(NOLOCK)
WHERE [CalendarID] IS NULL
GO

-- Calendar for Roles

UPDATE [Roles]
SET
	[CalendarID] = [si].[DefaultCalendarID],
	[CalendarName] = [si].[DefaultCalendarName]
FROM [ServerInstances] [si] WITH(NOLOCK)
WHERE [CalendarID] IS NULL
AND [TypeID] IN (0,1,2,3,4,5,6)
GO

-- _CalendarExclusions -> CalendarExclusions

IF OBJECT_ID('_CalendarExclusions', 'U') IS NOT NULL
BEGIN

	INSERT INTO [CalendarExclusions]
	([ID], [RowID], [StartTime], [EndTime], [IsNotWorkingTime])
	SELECT 
		[si].[DefaultCalendarID] AS [ID], 
		[_ce].[RowID], 
		[_ce].[StartTime], 
		[_ce].[EndTime], 
		[_ce].[IsNotWorkingTime]
	FROM [_CalendarExclusions] [_ce]
	CROSS JOIN [ServerInstances] [si]

	DROP TABLE [_CalendarExclusions];

END
GO

-- _CalendarTypeWeekDays -> CalendarTypeWeekDays

IF OBJECT_ID('_CalendarTypeWeekDays', 'U') IS NOT NULL
BEGIN
	DELETE FROM [CalendarTypeWeekDays]

	INSERT INTO [CalendarTypeWeekDays]
	([ID], [RowID], [Number], [Name], [WorkingDayStart], [WorkingDayEnd], [LunchStart], [LunchEnd], [IsNotWorkingDay])
	SELECT 
		[cs].[CalendarTypeID] AS [ID],
		[_cwd].[RowID],
		[_cwd].[Number],
		[_cwd].[Name], 
		[_cwd].[WorkingDayStart],
		[_cwd].[WorkingDayEnd],
		[_cwd].[LunchStart],
		[_cwd].[LunchEnd],
		[_cwd].[IsNotWorkingDay]
	FROM [_CalendarTypeWeekDays] [_cwd]
	CROSS JOIN [ServerInstances] [si]
	INNER JOIN [CalendarSettings] [cs]
		ON [si].[DefaultCalendarID] = [cs].[ID]

	DROP TABLE [_CalendarTypeWeekDays];

END
GO

-- _CalendarSettings -> CalendarSettings

IF OBJECT_ID('_CalendarSettings', 'U') IS NOT NULL
BEGIN

	UPDATE [CalendarSettings]
	SET
		[CalendarStart] = [_cs].[CalendarStart],
		[CalendarEnd] = [_cs].[CalendarEnd] 
	FROM [_CalendarSettings] [_cs]
	CROSS JOIN [ServerInstances] [si]
	WHERE [CalendarSettings].[ID] = [si].[DefaultCalendarID]

	DROP TABLE [_CalendarSettings];

END
GO