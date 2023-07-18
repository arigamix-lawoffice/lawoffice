-- Скрипт требуется выполнить после импорта конфигурации на 3.3

UPDATE "Notifications"
SET "NotificationTypeID" = 'c5a765f4-bd96-44c3-8c5f-5cf5fe43c521',
	"NotificationTypeName" = '$Notifications_Other'
WHERE "NotificationTypeID" IS NULL;
GO
