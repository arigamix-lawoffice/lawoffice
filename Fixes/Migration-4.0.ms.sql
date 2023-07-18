-- Скрипт требуется выполнить после обновления схемы на 4.0.0

-- Копирование состояния документа из KrApprovalCommonInfo в DocumentCommonInfo.

UPDATE [DocumentCommonInfo]
SET [StateID] = [aci].[StateID],
    [StateName] = [aci].[StateName]
FROM [KrApprovalCommonInfo] AS [aci]
WHERE [DocumentCommonInfo].[ID] = [aci].[MainCardID];
GO

-- Очищаем пробелы из email-ов пользователей

UPDATE [PersonalRoles]
SET [Email] = LTRIM(RTRIM([Email]))
WHERE [Email] IS NOT NULL

-- _KrPermissionExtendedFileRules.CheckOwnFiles -> KrPermissionExtendedFileRules.CheckFileRule

IF OBJECT_ID('_KrPermissionExtendedFileRules', 'U') IS NOT NULL
BEGIN

	UPDATE [KrPermissionExtendedFileRules]
	SET [FileCheckRuleID] = CASE WHEN [t].[CheckOwnFiles] = 1 THEN 0 ELSE 1 END,
	    [FileCheckRuleName] = CASE WHEN [t].[CheckOwnFiles] = 1 THEN N'$KrPermissions_FileCheckRules_AllFiles' ELSE N'$KrPermissions_FileCheckRules_FilesOfOtherUsers' END
	FROM [_KrPermissionExtendedFileRules] AS [t]
	WHERE [KrPermissionExtendedFileRules].[RowID] = [t].[RowID];

	DROP TABLE [_KrPermissionExtendedFileRules];

END
GO

-- LocalizationStrings LCID -> Culture name

IF EXISTS (SELECT 1 FROM [sysobjects] WHERE [name]='_LocalizationStrings' AND [type]='U')
BEGIN
	TRUNCATE TABLE [LocalizationStrings];
	
	INSERT INTO [LocalizationStrings] ([EntryID], [Culture], [Value])
	SELECT [EntryID], [Culture], [Value]
	FROM [_LocalizationStrings] WITH (NOLOCK);
	
	DROP TABLE [_LocalizationStrings];
END
GO

-- Обновляем поля Created, CreatedByID/Name у удалённых карточек

UPDATE [Deleted]
SET [Created] = [i].[Created],
    [CreatedByID] = [i].[CreatedByID],
    [CreatedByName] = [i].[CreatedByName]
FROM [Instances] AS [i]
WHERE [Deleted].[ID] = [i].[ID];
GO
