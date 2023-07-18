-- Скрипт требуется выполнить после обновления схемы на 3.5.0

-- Производим перенос данных о сателитах в новую таблицу Satellites

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE "table_name" = '_Satellites') THEN
	
	INSERT INTO "Satellites" ("ID", "MainCardID", "TaskID", "TypeID")
	SELECT "s"."ID", "s"."MainCardID", "s"."TaskID", "s"."TypeID"
	FROM "_Satellites" "s"
	LEFT JOIN "Satellites" "s2" ON "s"."ID" = "s2"."ID"
	WHERE "s2"."ID" IS NULL;

	DROP TABLE "_Satellites";
	
  END IF;
END;
$$ ;
GO

-- Заполнение секции KrAdditionalApproval для старых заданий типа KrAdditionalApproval.

INSERT INTO "KrAdditionalApproval" ("ID", "TimeLimitation", "FirstIsResponsible")
SELECT 
	"t"."RowID",
	1 AS "TimeLimitation",
	false AS "FirstIsResponsible"
FROM "Tasks" AS "t"
WHERE "t"."TypeID" = 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c' -- KrAdditionalApproval
AND NOT EXISTS (
	SELECT 1
	FROM "KrAdditionalApproval" AS "kaa"
	WHERE "kaa"."ID" = "t"."RowID");
GO