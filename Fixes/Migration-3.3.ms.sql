-- Скрипт требуется выполнить после обновления схемы на 3.3

IF OBJECT_ID('temp_Kinds', 'U') IS NOT NULL 
BEGIN

	UPDATE [th]
	SET [KindID] = [t].[KindID], [KindCaption] = [t].[KindCaption]
	FROM [temp_Kinds] AS [t]
	INNER JOIN [TaskHistory] AS [th]
		ON [t].[RowID] = [th].[RowID];

	DROP TABLE [temp_Kinds];

END;
GO

BEGIN TRAN

DECLARE @SystemSatelliteID uniqueidentifier;
SELECT @SystemSatelliteID = [ID]
FROM [Satellites] WITH (NOLOCK)
WHERE [MainCardID] = '11111111-1111-1111-1111-111111111111'
  AND [TypeID] = 'F6C54FED-0BEE-4D61-980A-8057179289EA';

IF (@SystemSatelliteID IS NOT NULL)
BEGIN
	DELETE FROM [PersonalRoleSatellite]
	WHERE [ID] = @SystemSatelliteID;
	
	DELETE FROM [Satellites]
	WHERE [ID] = @SystemSatelliteID;
	
	DELETE FROM [Instances]
	WHERE [ID] = @SystemSatelliteID;
END;

COMMIT
GO
