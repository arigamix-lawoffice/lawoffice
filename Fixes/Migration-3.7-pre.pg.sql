-- Скрипт требуется выполнить перед обновлением схемы на 3.7.0

-- Tasks -> _TaskAssignedRoles

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_TaskAssignedRoles') THEN

	DROP TABLE "_TaskAssignedRoles";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_TaskAssignedRoles"
(
	"ID" uuid NOT NULL,
	"RowID" uuid NOT NULL,
	"TaskRoleID" uuid NOT NULL,
	"RoleID" uuid NOT NULL,
	"RoleName" text NOT NULL,
	"RoleTypeID" uuid NOT NULL,
	"Position" text NULL,
	"ParentRowID" uuid NULL,
	"Master" boolean NOT NULL DEFAULT false,
	"ShowInTaskDetails" boolean NOT NULL DEFAULT false,
	CONSTRAINT "pk__TaskAssignedRoles" PRIMARY KEY ("RowID")
);
GO

-- Authors

INSERT INTO "_TaskAssignedRoles"
("ID", "RowID", "TaskRoleID", "RoleID", "RoleName", "RoleTypeID", "Position", "ParentRowID", "Master", "ShowInTaskDetails")
SELECT
	"t"."RowID"								AS "ID",
	gen_random_uuid()						AS "RowID",
	'6BC228A0-E5A2-4F15-BF6D-C8E744533241'	AS "TaskRoleID",
	"t"."AuthorID"							AS "RoleID",
	"t"."AuthorName"						AS "RoleName",
	'929AD23C-8A22-09AA-9000-398BF13979B2' 	AS "RoleTypeID",
	"t"."AuthorPosition"					AS "Position",
	NULL									AS "ParentRowID",
	false									AS "Master",
	false									AS "ShowInTaskDetails"
FROM "Tasks" "t"
WHERE NOT EXISTS ( 
	SELECT 1 
	FROM "_TaskAssignedRoles" "tar"
	WHERE "tar"."ID" = "t"."RowID"
	AND "tar"."TaskRoleID" = '6BC228A0-E5A2-4F15-BF6D-C8E744533241'
	LIMIT 1);
GO

-- Performers (контекстные роли переносятся как временные)

INSERT INTO "_TaskAssignedRoles"
("ID", "RowID", "TaskRoleID", "RoleID", "RoleName", "RoleTypeID", "Position", "ParentRowID", "Master", "ShowInTaskDetails")
SELECT
	"t"."RowID"								AS "ID",
	gen_random_uuid()						AS "RowID",
	'F726AB6C-A279-4D79-863A-47253E55CCC1'	AS "TaskRoleID",
	"t"."RoleID"							AS "RoleID",
	"t"."RoleName"							AS "RoleName",
	CASE "t"."RoleTypeID"
		WHEN 'B672E00C-0241-0485-9B07-4764BC96C9D3'  -- ID временных ролей
			THEN 'E97C253C-9102-0440-AC7E-4876E8F789DA' -- ID контекстных ролей
			ELSE "t"."RoleTypeID"
	END										AS "RoleTypeID",
	NULL									AS "Position",
	NULL									AS "ParentRowID",
	true									AS "Master",
	true									AS "ShowInTaskDetails"
FROM "Tasks" "t"
WHERE NOT EXISTS (
	SELECT 1 
	FROM "_TaskAssignedRoles" "tar"
	WHERE "tar"."ID" = "t"."RowID"
	AND "tar"."TaskRoleID" = 'F726AB6C-A279-4D79-863A-47253E55CCC1'
	LIMIT 1);
GO

-- TaskHistory -> _TaskHistory

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_TaskHistory') THEN

	DROP TABLE "_TaskHistory";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_TaskHistory"
(
	"RowID" uuid NOT NULL,
	"RoleName" text NOT NULL,
	"Settings" text NOT NULL,
	CONSTRAINT "pk__TaskHistory" PRIMARY KEY ("RowID")
);
GO

-- Sessions

DELETE FROM "Sessions"
GO

-- CalendarExclusions -> _CalendarExclusions

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_CalendarExclusions') THEN

	DROP TABLE "_CalendarExclusions";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_CalendarExclusions"
(
	"RowID" uuid NOT NULL,
	"StartTime" timestamp(6) with time zone NULL,
	"EndTime" timestamp(6) with time zone NULL,
	"IsNotWorkingTime" boolean NOT NULL DEFAULT true,
	CONSTRAINT "pk__CalendarExclusions" PRIMARY KEY ("RowID")
);
GO

INSERT INTO "_CalendarExclusions"
("RowID", "StartTime", "EndTime", "IsNotWorkingTime")
SELECT
	"ce"."RowID"							AS "RowID",
	"ce"."StartTime"						AS "StartTime",
	"ce"."EndTime"							AS "EndTime",
	"ce"."Type"								AS "IsNotWorkingTime"
FROM "CalendarExclusions" "ce"
WHERE NOT EXISTS (
	SELECT 1
	FROM "_CalendarExclusions" "_ce"
	WHERE "_ce"."RowID" = "ce"."RowID"
	LIMIT 1);
GO

-- CalendarSettings-> _CalendarTypeWeekDays

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_CalendarTypeWeekDays') THEN

	DROP TABLE "_CalendarTypeWeekDays";

  END IF;
END;
$$ ;
GO

CREATE TABLE "_CalendarTypeWeekDays"
(
	"RowID" uuid NOT NULL,
	"Number" smallint NOT NULL,
	"Name" text NOT NULL,
	"WorkingDayStart" time(6) with time zone NULL,
	"WorkingDayEnd" time(6) with time zone NULL,
	"LunchStart" time(6) with time zone NULL,
	"LunchEnd" time(6) with time zone NULL,
	"IsNotWorkingDay" boolean NOT NULL DEFAULT true,
	CONSTRAINT "pk__CalendarTypeWeekDays" PRIMARY KEY ("RowID")
);
GO

INSERT INTO "_CalendarTypeWeekDays"
("RowID", "Number", "Name", "WorkingDayStart", "WorkingDayEnd", "LunchStart", "LunchEnd", "IsNotWorkingDay")
SELECT
	gen_random_uuid()						AS "RowID",
	0										AS "Number",
	'$Calendar_Days_Monday'					AS "Name",
	"cs"."WorkDayStart"						AS "WorkingDayStart",
	"cs"."WorkDayEnd"						AS "WorkingDayEnd",
	"cs"."LunchStart"						AS "LunchStart",
	"cs"."LunchEnd"							AS "LunchEnd",
	false									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	1										AS "Number",
	'$Calendar_Days_Tuesday'				AS "Name",
	"cs"."WorkDayStart"						AS "WorkingDayStart",
	"cs"."WorkDayEnd"						AS "WorkingDayEnd",
	"cs"."LunchStart"						AS "LunchStart",
	"cs"."LunchEnd"							AS "LunchEnd",
	false									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	2										AS "Number",
	'$Calendar_Days_Wednesday'				AS "Name",
	"cs"."WorkDayStart"						AS "WorkingDayStart",
	"cs"."WorkDayEnd"						AS "WorkingDayEnd",
	"cs"."LunchStart"						AS "LunchStart",
	"cs"."LunchEnd"							AS "LunchEnd",
	false									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	3										AS "Number",
	'$Calendar_Days_Thursday'				AS "Name",
	"cs"."WorkDayStart"						AS "WorkingDayStart",
	"cs"."WorkDayEnd"						AS "WorkingDayEnd",
	"cs"."LunchStart"						AS "LunchStart",
	"cs"."LunchEnd"							AS "LunchEnd",
	false									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	4										AS "Number",
	'$Calendar_Days_Friday'					AS "Name",
	"cs"."WorkDayStart"						AS "WorkingDayStart",
	"cs"."WorkDayEnd"						AS "WorkingDayEnd",
	"cs"."LunchStart"						AS "LunchStart",
	"cs"."LunchEnd"							AS "LunchEnd",
	false									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	5										AS "Number",
	'$Calendar_Days_Saturday'				AS "Name",
	NULL									AS "WorkingDayStart",
	NULL									AS "WorkingDayEnd",
	NULL									AS "LunchStart",
	NULL									AS "LunchEnd",
	true									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
UNION ALL
SELECT
	gen_random_uuid()						AS "RowID",
	6										AS "Number",
	'$Calendar_Days_Sunday'					AS "Name",
	NULL									AS "WorkingDayStart",
	NULL									AS "WorkingDayEnd",
	NULL									AS "LunchStart",
	NULL									AS "LunchEnd",
	true									AS "IsNotWorkingDay"
FROM "CalendarSettings" "cs"
GO

-- CalendarSettings-> _CalendarSettings

DO $$
BEGIN
  IF EXISTS (
	SELECT 1
	FROM "information_schema"."tables"
	WHERE "table_name" = '_CalendarSettings') THEN

	DROP TABLE "_CalendarSettings";

  END IF;
END;
$$ ;
GO


CREATE TABLE "_CalendarSettings"
(
	"CalendarStart" timestamp(6) with time zone NULL,
	"CalendarEnd" timestamp(6) with time zone NULL
);
GO

INSERT INTO "_CalendarSettings"
("CalendarStart", "CalendarEnd")
SELECT	
	"cs"."CalendarStart"						AS "CalendarStart",
	"cs"."CalendarEnd"							AS "CalendarEnd"
FROM "CalendarSettings" "cs"
GO