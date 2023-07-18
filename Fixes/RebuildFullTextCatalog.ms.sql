/*
 * Запрос выполняется только на Microsoft SQL Server.
 * Хранение файлов в базе данных не поддерживается в PostgreSQL.
 * 
 * Скрипт, выполняемый после установки полнотекстовых фильтров (например, Office 2010 Filter Pack)
 * для обновления системной информации и для перестройки полнотекстовых каталогов.
 *
 * Скрипт для создания полнотекстового каталога: CreateFullTextCatalog.sql
 */

EXEC sp_fulltext_service 'update_languages';
GO

EXEC sp_fulltext_service 'load_os_resources', 1;
GO

EXEC sp_fulltext_service 'restart_all_fdhosts';
GO

ALTER FULLTEXT CATALOG ftFileContent REBUILD
GO
