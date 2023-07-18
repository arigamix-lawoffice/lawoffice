-- Удаление дубликатов в FmUserStat

IF EXISTS (SELECT * FROM sysobjects WHERE name='FmUserStat' AND xtype='U')
BEGIN
    EXEC sp_executesql N'WITH cte (duplicate)
    AS
    (
        SELECT ROW_NUMBER() OVER (
            PARTITION BY [TopicRowID], [UserID]
            ORDER BY [LastReadMessageTime] DESC
        )
        FROM [FmUserStat] WITH(NOLOCK)
    )

    DELETE cte
    WHERE duplicate > 1;';
END
GO

-- KrPermissionExtendedFileRules.CheckOwnFiles -> _KrPermissionExtendedFileRules.CheckOwnFiles

IF EXISTS (SELECT * FROM [sysobjects] WHERE [name]='_KrPermissionExtendedFileRules' AND [xtype]='U')
	DROP TABLE [_KrPermissionExtendedFileRules]
GO

CREATE TABLE [_KrPermissionExtendedFileRules]
(
	[RowID] uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	[CheckOwnFiles] bit NOT NULL
);
GO

INSERT INTO [_KrPermissionExtendedFileRules] ([RowID], [CheckOwnFiles])
SELECT [RowID], [CheckOwnFiles] FROM [KrPermissionExtendedFileRules] WITH (NOLOCK);
GO

-- LocalizationStrings LCID -> Culture name

IF EXISTS (
	SELECT 1
	FROM [sys].[columns]
	WHERE [name] = 'Culture'
	AND [object_id] = OBJECT_ID('[LocalizationStrings]')
	AND TYPE_NAME([system_type_id]) = 'int')
BEGIN

	-- меняем что-либо только если тип колонки int
	IF EXISTS (SELECT 1 FROM [sysobjects] WHERE [name]='_LocalizationStrings' AND [type]='U')
		DROP TABLE [_LocalizationStrings]
  
	CREATE TABLE [_LocalizationStrings]
	(
		[EntryID] uniqueidentifier NOT NULL,
		[Culture] nvarchar(8) NOT NULL,
		[Value] nvarchar(1024) NULL
	);

	INSERT INTO [_LocalizationStrings] ([EntryID], [Culture], [Value])
	SELECT 
		[EntryID],
		CASE [Culture]
			WHEN 9 THEN 'en'
			WHEN 25 THEN 'ru'
			ELSE ''
		END,
		[Value]
	FROM [LocalizationStrings] WITH (NOLOCK);

END
GO

-- Sessions need to be deleted
DELETE FROM [Sessions]
GO
