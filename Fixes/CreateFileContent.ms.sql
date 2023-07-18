/*
 * Запрос выполняется только на Microsoft SQL Server.
 * Хранение файлов в базе данных не поддерживается в PostgreSQL.
 * 
 * Скрипт подготавливает базу данных, в которой не установлена Tessa, для того,
 * чтобы в этой базе данных мог храниться контент файлов Tessa.
 *
 * База данных настраивается как хранилище для файлов в карточке "Настройки сервера"
 */

 create table FileContent (
	VersionRowID uniqueidentifier not null,
	Content varbinary(max) null,
	Ext nvarchar(100) null,
	constraint pk_FileContent primary key clustered (VersionRowID asc)
 )
 go
