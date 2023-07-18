-- Скрипт требуется выполнить перед обновлением схемы на 3.5.0

-- Мигрируем данные сателлитов

IF EXISTS (SELECT * FROM sysobjects WHERE name='_Satellites' AND xtype='U')
	DROP TABLE _Satellites
GO

CREATE TABLE _Satellites
(
	ID uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	MainCardID uniqueidentifier NOT NULL, -- ID основной карточки
	TypeID uniqueidentifier NOT NULL, -- ID типа сателлита
	TaskID uniqueidentifier NULL, -- ID задания (если сателлит относится к заданию)
)
GO


-- Перенос KrDialogSatellite
IF EXISTS (SELECT * FROM sysobjects WHERE name='KrDialogSatellite' AND xtype='U')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, TypeID FROM KrDialogSatellite WITH(NOLOCK)'
END
GO


-- Перенос WfTaskCards
IF EXISTS (SELECT * FROM sysobjects WHERE name='WfTaskCards' AND xtype='U')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TaskID, TypeID)
	SELECT ID, MainCardID, TaskRowID, ''de75a343-8164-472d-a20e-4937819760ac'' FROM WfTaskCards WITH(NOLOCK)'
END
GO


-- Перенос FmForumSatellite
IF EXISTS (SELECT * FROM sysobjects WHERE name='FmForumSatellite' AND xtype='U')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, ''48e7c07a-295d-479a-9990-02a1f7a5f7db'' FROM FmForumSatellite WITH(NOLOCK)'
END
GO


-- Перенос PersonalRoleSatellite
IF EXISTS (
	SELECT 1
	FROM [sys].[columns] 
	WHERE [object_id] = OBJECT_ID(N'dbo.PersonalRoleSatellite') AND [name] = N'MainCardID')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, ''f6c54fed-0bee-4d61-980a-8057179289ea'' FROM PersonalRoleSatellite WITH(NOLOCK)'
END
GO


-- Перенос WfSatellite
IF EXISTS (
	SELECT 1
	FROM [sys].[columns] 
	WHERE [object_id] = OBJECT_ID(N'dbo.WfSatellite') AND [name] = N'MainCardID')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, ''a382ec40-6321-42e5-a9f9-c7b103feb38d'' FROM WfSatellite WITH(NOLOCK)'
END
GO


-- Перенос KrApprovalCommonInfo
IF EXISTS (
	SELECT 1
	FROM [sys].[columns] 
	WHERE [object_id] = OBJECT_ID(N'dbo.KrApprovalCommonInfo') AND [name] = N'MainCardID')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, ''4115f07e-0aaa-4563-a749-0450c1a850af'' FROM KrApprovalCommonInfo WITH(NOLOCK)'
END
GO


-- Перенос KrSecondaryProcessCommonInfo
IF EXISTS (
	SELECT 1
	FROM [sys].[columns] 
	WHERE [object_id] = OBJECT_ID(N'dbo.KrSecondaryProcessCommonInfo') AND [name] = N'MainCardID')
BEGIN
	EXEC sp_executesql N'INSERT INTO _Satellites (ID, MainCardID, TypeID)
	SELECT ID, MainCardID, ''7593c144-31f7-43c2-9c4b-e3b776562f8f'' FROM KrSecondaryProcessCommonInfo WITH(NOLOCK)'
END
GO

-- Так как у топиков появились в 3.5.0 типы, у всех имеющихся заполним значение по умолчанию
-- поскольку колонка была в 3.4.0, то при обновлении с этой версии сначала происходит смена типа колонки на NOT NULL,
-- и уже потом применяется дефолтный констрейнт, поэтому, если колонка не создаётся с нуля (предыдущие сборки),
-- то надо заполнить значение у топиков
IF EXISTS (
	SELECT 1
	FROM [sys].[columns] 
	WHERE [object_id] = OBJECT_ID(N'FmTopics') AND [name] = N'TypeID')
BEGIN
	UPDATE "FmTopics" 
	SET "TypeID" = '680d0d81-d8f3-485e-9058-e17ab9e186e0'
END
GO
