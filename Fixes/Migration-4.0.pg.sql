-- Скрипт требуется выполнить после обновления схемы на 4.0.0

-- Копирование состояния документа из KrApprovalCommonInfo в DocumentCommonInfo.

UPDATE "DocumentCommonInfo"
SET "StateID" = "aci"."StateID",
    "StateName" = "aci"."StateName"
FROM "KrApprovalCommonInfo" AS "aci"
WHERE "DocumentCommonInfo"."ID" = "aci"."MainCardID";
GO

-- Очищаем пробелы из email-ов пользователей

UPDATE "PersonalRoles"
SET "Email" = TRIM("Email")
WHERE "Email" IS NOT NULL
GO

-- _KrPermissionExtendedFileRules.CheckOwnFiles -> KrPermissionExtendedFileRules.CheckFileRule

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_KrPermissionExtendedFileRules') THEN

	UPDATE "KrPermissionExtendedFileRules"
	SET "FileCheckRuleID" = CASE WHEN "t"."CheckOwnFiles" THEN 0 ELSE 1 END,
		"FileCheckRuleName" = CASE WHEN "t"."CheckOwnFiles" THEN '$KrPermissions_FileCheckRules_AllFiles' ELSE '$KrPermissions_FileCheckRules_FilesOfOtherUsers' END
	FROM "_KrPermissionExtendedFileRules" AS "t"
	WHERE "KrPermissionExtendedFileRules"."RowID" = "t"."RowID";
	
	DROP TABLE "_KrPermissionExtendedFileRules";
	
  END IF;
END;
$$ ;
GO

-- LocalizationStrings LCID -> Culture name

DO $$
BEGIN
	IF EXISTS (
		SELECT 1
		FROM "information_schema"."tables"
		WHERE "table_name" = '_LocalizationStrings') THEN
		
		TRUNCATE TABLE "LocalizationStrings";
		
		INSERT INTO "LocalizationStrings" ("EntryID", "Culture", "Value")
		SELECT "EntryID", "Culture", "Value"
		FROM "_LocalizationStrings";
		
		DROP TABLE "_LocalizationStrings";
		
	END IF;
END;
$$ ;
GO

-- Обновляем поля Created, CreatedByID/Name у удалённых карточек

UPDATE "Deleted"
SET "Created" = "i"."Created",
    "CreatedByID" = "i"."CreatedByID",
    "CreatedByName" = "i"."CreatedByName"
FROM "Instances" AS "i"
WHERE "Deleted"."ID" = "i"."ID";
GO
