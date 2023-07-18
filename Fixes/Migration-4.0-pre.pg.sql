-- Удаление дубликатов в FmUserStat

DO $$
BEGIN
  IF EXISTS (
    SELECT 1
    FROM information_schema.columns
    WHERE "table_name" = 'FmUserStat') THEN
	
    DELETE FROM "FmUserStat"
    WHERE "RowID" IN
    (
        SELECT "RowID"
        FROM
        (
            SELECT
              "RowID",
              ROW_NUMBER() OVER (
                PARTITION BY "TopicRowID", "UserID"
                ORDER BY "LastReadMessageTime" DESC
            ) AS "rn"
            FROM "FmUserStat"
        ) AS "t"
        WHERE "t"."rn" > 1
    );
	
END IF;
END;
$$ ;
GO

CREATE TABLE "_KrPermissionExtendedFileRules" (
	"RowID" uuid NOT NULL,
	"CheckOwnFiles" boolean NOT NULL DEFAULT false,
	CONSTRAINT "pk__KrPermissionExtendedFileRules" PRIMARY KEY ("RowID", "CheckOwnFiles"));
GO

INSERT INTO "_KrPermissionExtendedFileRules" ("RowID", "CheckOwnFiles")
SELECT "RowID", "CheckOwnFiles" FROM "_KrPermissionExtendedFileRules";
GO

-- LocalizationStrings LCID -> Culture name

DO $$
BEGIN
	IF EXISTS (
		SELECT 1
		FROM "information_schema"."columns"
		WHERE "table_name" = 'LocalizationStrings'
		AND "column_name" = 'Culture'
		AND "data_type" = 'integer') THEN

		-- меняем что-либо только если тип колонки int
		IF EXISTS (
			SELECT 1
			FROM "information_schema"."tables"
			WHERE "table_name" = '_LocalizationStrings') THEN
			
			DROP TABLE "_LocalizationStrings";
		
		END IF;
		
		CREATE TABLE "_LocalizationStrings"
		(
			"EntryID" uuid NOT NULL,
			"Culture" varchar(8) NOT NULL,
			"Value" text NULL
		);
		
		INSERT INTO "_LocalizationStrings" ("EntryID", "Culture", "Value")
		SELECT
			"EntryID",
			CASE "Culture"
				WHEN 9 THEN 'en'
				WHEN 25 THEN 'ru'
				ELSE ''
			END,
			"Value"
		FROM "LocalizationStrings";
    
	END IF;
END;
$$ ;
GO

-- Sessions need to be deleted
DELETE FROM "Sessions"
GO
