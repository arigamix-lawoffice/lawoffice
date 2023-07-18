-- Скрипт требуется выполнить перед обновлением схемы на 3.5.0

-- Мигрируем данные сателлитов

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE "table_name" = '_Satellites') THEN
	
	DROP TABLE "_Satellites";
	
  END IF;
END;
$$ ;
GO


CREATE TABLE "_Satellites" (
	"ID" uuid NOT NULL,
	"MainCardID" uuid NOT NULL,
	"TaskID" uuid NULL,
	"TypeID" uuid NOT NULL);
GO



-- Перенос KrDialogSatellite
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'KrDialogSatellite') THEN
	
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", "TypeID"
	FROM "KrDialogSatellite";
	
  END IF;
END;
$$ ;
GO



-- Перенос WfTaskCards
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'WfTaskCards') THEN
	
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TaskID", "TypeID")
	SELECT "ID", "MainCardID", "TaskRowID", 'de75a343-8164-472d-a20e-4937819760ac'::uuid
	FROM "WfTaskCards";
	
  END IF;
END;
$$ ;
GO



-- Перенос FmForumSatellite
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'FmForumSatellite') THEN
	
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", '48e7c07a-295d-479a-9990-02a1f7a5f7db'::uuid
	FROM "FmForumSatellite";
	
  END IF;
END;
$$ ;
GO



-- Перенос PersonalRoleSatellite
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'PersonalRoleSatellite' AND "column_name" = 'MainCardID') THEN
		
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", 'f6c54fed-0bee-4d61-980a-8057179289ea'::uuid
	FROM "PersonalRoleSatellite";
	
  END IF;
END;
$$ ;
GO



-- Перенос WfSatellite
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'WfSatellite' AND "column_name" = 'MainCardID') THEN
		
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", 'a382ec40-6321-42e5-a9f9-c7b103feb38d'::uuid
	FROM "WfSatellite";
	
  END IF;
END;
$$ ;
GO



-- Перенос KrApprovalCommonInfo
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'KrApprovalCommonInfo' AND "column_name" = 'MainCardID') THEN
		
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", '4115f07e-0aaa-4563-a749-0450c1a850af'::uuid
	FROM "KrApprovalCommonInfo";
	
  END IF;
END;
$$ ;
GO



-- Перенос KrSecondaryProcessCommonInfo
DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.columns
	WHERE "table_name" = 'KrSecondaryProcessCommonInfo' AND "column_name" = 'MainCardID') THEN
		
	INSERT INTO "_Satellites" ("ID", "MainCardID", "TypeID")
	SELECT "ID", "MainCardID", '7593c144-31f7-43c2-9c4b-e3b776562f8f'::uuid
	FROM "KrSecondaryProcessCommonInfo";
	
  END IF;
END;
$$ ;
GO



-- Так как у топиков появились в 3.5.0 типы, у всех имеющихся заполним значение по умолчанию
-- поскольку колонка была в 3.4.0, то при обновлении с этой версии сначала происходит смена типа колонки на NOT NULL,
-- и уже потом применяется дефолтный констрейнт, поэтому, если колонка не создаётся с нуля (предыдущие сборки),
-- то надо заполнить значение у топиков
DO $$
BEGIN
	IF EXISTS (
		SELECT 1
		FROM information_schema.columns
		WHERE "table_name" = 'FmTopics' AND "column_name" = 'TypeID') THEN
	
		UPDATE "FmTopics" 
		SET "TypeID" = '680d0d81-d8f3-485e-9058-e17ab9e186e0';
  END IF;
END;
$$ ;
GO

-- В CompiledViews теперь хранится дата и время последней модификации метаинфы, чтобы исключить проблемы с кэшом в ситуациях, 
-- когда на одной ноде метаинфа уже обновлена, а другая нода еще хранит кэш старой и компилит вьюху по старой, после чего результат
-- будет использован всеми нодами как актуальный. Колонка помечена как NotNull, поэтому все старые записи нужно удалить.

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE "table_name" = 'CompiledViews') THEN
	
	DELETE FROM "CompiledViews";
	
  END IF;
END;
$$ ;
GO