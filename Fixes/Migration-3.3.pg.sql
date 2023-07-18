-- Скрипт требуется выполнить после обновления схемы на 3.3

DO $$
  BEGIN
	IF EXISTS (
		SELECT 1
		FROM information_schema.tables
		WHERE "table_name" = 'temp_Kinds') THEN
		
		UPDATE "TaskHistory"
		SET "KindID" = "t"."KindID", "KindCaption" = "t"."KindCaption"
		FROM (
			SELECT "RowID", "KindID", "KindCaption"
			FROM "temp_Kinds"
		) "t"
		WHERE "TaskHistory"."RowID" = "t"."RowID";

		DROP TABLE "temp_Kinds";
	  
	END IF;
  END;
$$ ;
GO

DROP TABLE IF EXISTS "temp_SystemSatelliteID";

CREATE TEMPORARY TABLE "temp_SystemSatelliteID" ("ID" uuid NOT NULL);

INSERT INTO "temp_SystemSatelliteID"
SELECT "ID"
FROM "Satellites"
WHERE "MainCardID" = '11111111-1111-1111-1111-111111111111'
  AND "TypeID" = 'F6C54FED-0BEE-4D61-980A-8057179289EA';

DELETE FROM "PersonalRoleSatellite"
WHERE "ID" IN (SELECT "ID" FROM "temp_SystemSatelliteID");

DELETE FROM "Satellites"
WHERE "ID" IN (SELECT "ID" FROM "temp_SystemSatelliteID");

DELETE FROM "Instances"
WHERE "ID" IN (SELECT "ID" FROM "temp_SystemSatelliteID");

DROP TABLE "temp_SystemSatelliteID";
GO

