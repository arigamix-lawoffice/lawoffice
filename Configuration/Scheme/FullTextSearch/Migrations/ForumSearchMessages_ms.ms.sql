DECLARE @tablename nvarchar(255) = 'FmMessages'
DECLARE @colname nvarchar(255) = 'PlainText'
DECLARE @dbname nvarchar(255) = DB_NAME()

DECLARE @scr nvarchar(max) = 'SELECT TOP 1 @scr = [name] FROM [' + @dbname + '].[sys].[indexes] WHERE [object_id] = OBJECT_ID(''[' + @tablename + ']'') AND is_unique = 1'
DECLARE @EXEC nvarchar(max) = '[' + @dbname + N']..sp_executesql'

EXEC @EXEC @scr, N'@scr nvarchar(255) OUT', @scr OUT
EXEC ('USE [' + @dbname + ']; SELECT [name] INTO #null FROM [sys].[fulltext_catalogs] WHERE [name] = ''' + @tablename + '''')

IF (@@ROWCOUNT = 0) 
BEGIN
	EXEC ('USE [' + @dbname + ']; CREATE FULLTEXT CATALOG [' + @tablename + '] WITH accent_sensitivity = OFF AUTHORIZATION [dbo]')
END

EXEC ('USE [' + @dbname + ']; SELECT [object_id] INTO #null FROM [sys].[fulltext_indexes] WHERE [object_id] = object_id(''[' + @dbname + '].[dbo].[' + @tablename + ']'')')

IF (@@ROWCOUNT = 0) 
BEGIN	
	EXEC ('USE [' + @dbname + ']; CREATE FULLTEXT INDEX ON [dbo].[' + @tablename + '] (' + @colname + ') KEY INDEX ' + @scr + ' ON [' + @tablename + '] WITH STOPLIST = SYSTEM')
END

EXEC ('USE [' + @dbname + ']; SELECT [object_id] INTO #null FROM [sys].[fulltext_index_columns] WHERE COL_NAME([object_id], [column_id]) = ''' + @colname + '''')

IF (@@ROWCOUNT = 0)
BEGIN
	EXEC ('USE [' + @dbname + ']; ALTER FULLTEXT INDEX ON [dbo].[' + @tablename + '] ADD (' + @colname + ')')
END
GO