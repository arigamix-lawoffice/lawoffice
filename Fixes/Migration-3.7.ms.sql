-- Скрипт требуется выполнить после обновления схемы на 3.7.0

-- _TaskAssignedRoles -> TaskAssignedRoles

IF OBJECT_ID('_TaskAssignedRoles', 'U') IS NOT NULL
BEGIN

	INSERT INTO [TaskAssignedRoles]
	([ID], [RowID], [TaskRoleID], [RoleID], [RoleName], [RoleTypeID], [Position], [ParentRowID], [Master], [ShowInTaskDetails])
	SELECT 
		[ID], [RowID], [TaskRoleID], [RoleID], [RoleName], [RoleTypeID], [Position], [ParentRowID], [Master], [ShowInTaskDetails]
	FROM [_TaskAssignedRoles]

	DROP TABLE [_TaskAssignedRoles];

END
GO

-- _TaskHistory -> TaskHistory

IF OBJECT_ID('_TaskHistory', 'U') IS NOT NULL
BEGIN

	UPDATE [TaskHistory]
	SET
		[CompletedByRole] = CASE WHEN [CompletedByID] IS NOT NULL
			THEN [ths].[RoleName]
			ELSE NULL
		END, 
		[AssignedOnRole] = [ths].[RoleName],
		[Settings] = [ths].[Settings]
	FROM [_TaskHistory] [ths] WITH (NOLOCK)
	WHERE [TaskHistory].[RowID] = [ths].[RowID]

	DROP TABLE [_TaskHistory];

END
GO

-- Calendar ID

UPDATE [CalendarQuants]
SET
	[ID] = 0
WHERE [ID] IS NULL
GO

-- DefaultCalendar ID

UPDATE [ServerInstances]
SET 
	[DefaultCalendarID] = [cs].[ID],
	[DefaultCalendarName] = N'$Calendars_DefaultCalendar'
FROM [CalendarSettings] [cs] WITH(NOLOCK)
WHERE [DefaultCalendarID] IS NULL
GO