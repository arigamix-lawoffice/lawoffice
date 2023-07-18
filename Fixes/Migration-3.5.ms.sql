-- Скрипт требуется выполнить после обновления схемы на 3.5.0

-- Производим перенос данных о сателитах в новую таблицу Satellites

IF EXISTS (SELECT * FROM sysobjects WHERE name='_Satellites' and xtype='U')
BEGIN
	INSERT INTO Satellites (ID, MainCardID, TaskID, TypeID)
	SELECT s.ID, s.MainCardID, s.TaskID, s.TypeID
	FROM _Satellites s WITH(NOLOCK)
	LEFT JOIN Satellites s2 WITH(NOLOCK) ON s.ID = s2.ID
	WHERE s2.ID IS NULL

	DROP TABLE _Satellites
END
GO

-- Заполнение секции KrAdditionalApproval для старых заданий типа KrAdditionalApproval.

INSERT INTO [KrAdditionalApproval] ([ID], [TimeLimitation], [FirstIsResponsible])
SELECT 
	[t].[RowID],
	1 AS [TimeLimitation],
	0 AS [FirstIsResponsible]
FROM [Tasks] AS t WITH (NOLOCK)
WHERE [t].[TypeID] = 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c' -- KrAdditionalApproval
AND NOT EXISTS (
	SELECT 1
	FROM [KrAdditionalApproval] AS [kaa] WITH (NOLOCK)
	WHERE [kaa].[ID] = [t].[RowID]);
GO