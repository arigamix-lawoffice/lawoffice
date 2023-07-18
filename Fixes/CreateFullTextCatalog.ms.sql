/*
 * Запрос выполняется только на Microsoft SQL Server.
 * Хранение файлов в базе данных не поддерживается в PostgreSQL.
 * 
 * Скрипт добавляет полнотекстовый каталог для таблицы с контентом файлов FileContent.
 * База данных настраивается как хранилище для файлов в карточке "Настройки сервера"
 *
 * Список индексируемых расширений файлов:
 * select * from sys.fulltext_document_types
 *
 * Для поддержки офисных docx, xlsx и т.п. нужно установить MS Office 2010 Filter Pack SP2
 * Cсылки для скачивания:
 * * Office 2010 Filter Pack: https://www.microsoft.com/en-us/download/details.aspx?id=17062
 * * SP2: https://www.microsoft.com/en-us/download/details.aspx?id=39668
 *
 * Скрипт для перестроения полнотекстового каталога после установки фильтров: RebuildFullTextCatalog.sql
 *
 * Для удаления каталога используйте скрипт DropFullTextCatalog.sql
 */

/*
	Запрос, позволяющий искать слово по версиям файлов в таблице FileContent:

	select VersionRowID, Ext, (select Name from FileVersions v with(nolock) where v.RowID = VersionRowID) as FileName
	from FileContent with(nolock)
	where freetext(Content, N'искомая фраза');
	-- или where contains(Content, N'слово')
 */

CREATE FULLTEXT CATALOG ftFileContent AS DEFAULT;
GO

CREATE FULLTEXT INDEX ON FileContent(Content type column Ext) KEY INDEX pk_FileContent;
GO
