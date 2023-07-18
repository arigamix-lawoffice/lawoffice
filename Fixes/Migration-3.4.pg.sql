-- Скрипт требуется выполнить после обновления схемы на 3.4

UPDATE "PersonalRoleSatellite"
SET "WebTheme" = 'Cold'
WHERE "PersonalRoleSatellite"."WebTheme" = 'cold-theme';
GO

UPDATE "PersonalRoleSatellite"
SET "WebTheme" = 'Warm'
WHERE "PersonalRoleSatellite"."WebTheme" = 'warm-theme';
GO

-- Добавляем новую секцию в задания 
INSERT INTO "TaskCommonInfo" ("ID")
SELECT "task"."RowID"
FROM "Tasks" AS "task"
LEFT JOIN "TaskCommonInfo" AS "tci" ON "tci"."ID" = "task"."RowID"
WHERE "task"."TypeID" IN (
    'e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c', -- Approve
    '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6', -- Signing
    '09fdd6a3-3946-4f30-9ef9-f533fad3a4a2', -- Registration
    '9c6d9824-41d7-41e6-99f1-e19ea9e576c5', -- KrUniversalTask
    'e19ca9b5-48be-4fdf-8dc5-78534b4767de', -- KrEdit
    'c9b93ae3-9b7b-4431-a306-aace4aea8732' -- KrEditInterject
) AND "tci"."ID" IS NULL;
GO

-- Заполняем FmMessagesPluginTable минимальным значением
INSERT INTO "FmMessagesPluginTable" 
VALUES (current_timestamp AT TIME ZONE 'UTC');
GO