-- Скрипт требуется выполнить после обновления схемы на 3.7.0

-- _TaskAssignedRoles -> TaskAssignedRoles

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_TaskAssignedRoles') THEN

	INSERT INTO "TaskAssignedRoles"
	("ID", "RowID", "TaskRoleID", "RoleID", "RoleName", "RoleTypeID", "Position", "ParentRowID", "Master", "ShowInTaskDetails")
	SELECT 
		"ID", "RowID", "TaskRoleID", "RoleID", "RoleName", "RoleTypeID", "Position", "ParentRowID", "Master", "ShowInTaskDetails"
	from "_TaskAssignedRoles";

	DROP TABLE "_TaskAssignedRoles";

  END IF;
END
$$ ;
GO

-- _TaskHistory -> TaskHistory

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_TaskHistory') THEN
	
	UPDATE "_TaskHistory"
	SET
		"CompletedByRole" = CASE WHEN "CompletedByID" IS NOT NULL
			THEN "ths"."RoleName"
			ELSE NULL
		END,
		"AssignedOnRole" = "ths"."RoleName",
		"Settings" = "ths"."Settings"
	FROM "_TaskHistory" "ths"
	WHERE "TaskHistory"."RowID" = "ths"."RowID"

	DROP TABLE "_TaskHistory";

  END IF;
END;
$$ ;
GO

-- Calendar ID

UPDATE "CalendarQuants"
SET
	"ID" = 0
WHERE "ID" IS NULL
GO

-- DefaultCalendar ID

UPDATE "ServerInstances"
SET 
	"DefaultCalendarID" = "cs"."ID",
	"DefaultCalendarName" = '$Calendars_DefaultCalendar'
FROM "CalendarSettings" "cs"
WHERE "DefaultCalendarID" IS NULL
GO