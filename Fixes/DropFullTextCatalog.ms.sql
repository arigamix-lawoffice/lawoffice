/*
 * Запрос выполняется только на Microsoft SQL Server.
 * Хранение файлов в базе данных не поддерживается в PostgreSQL.
 * 
 * Скрипт удаляет полнотекстовый каталог для таблицы с контентом файлов FileContent.
 *
 * Для создания каталога используйте скрипт CreateFullTextCatalog.sql
 */

DROP FULLTEXT INDEX ON FileContent
GO

DROP FULLTEXT CATALOG ftFileContent
GO
