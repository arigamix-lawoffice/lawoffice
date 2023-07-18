CREATE FUNCTION "GetAggregateRoleUsers" ()
RETURNS TABLE
(
	"RoleID" uuid,
	"RoleName" text,
	"UserID" uuid,
	"UserName" text,
	"CalendarID" uuid,
	"TimeZoneID" smallint	
)
AS $$
    WITH RECURSIVE "RoleUsersCTE" ("ID", "RoleID", "RoleName", "CalendarID", "TimeZoneID") AS (
	 -- выбираем статические роли и департаменты,
        -- которые либо не являются листовыми,
        -- либо для которых уже была создана метароль генератором агрегатных ролей
        SELECT  "r"."ID",
                "r"."ID",
                "r"."Name" || ' (все)',
                "r"."CalendarID",
                "r"."TimeZoneID"               
        FROM "Roles" AS "r"
        LEFT JOIN "MetaRoles" AS "mr"
            ON "mr"."GeneratorID" = '3E832ED8-B3D6-0ACD-9524-6322A7231FEF'::uuid
            AND "mr"."IDGuid" = "r"."ID"
        WHERE "r"."TypeID" IN (0, 2)
            AND "r"."Hidden" = false
            AND ("mr"."GeneratorID" IS NOT NULL OR EXISTS (SELECT 1 FROM "Roles" AS "r2" WHERE "r2"."ParentID" = "r"."ID"  AND "r2"."TypeID" <> 8 ))
        UNION ALL
        SELECT  "r"."ID",
                "t"."RoleID",
                "t"."RoleName",
                "t"."CalendarID",
                "t"."TimeZoneID"                
        FROM "Roles" AS "r"
        INNER JOIN "RoleUsersCTE" AS "t"
			ON "r"."ParentID" = "t"."ID"
        WHERE "r"."TypeID" <> 8
    )
    SELECT DISTINCT
        "r"."RoleID",
        "r"."RoleName",
        "ru"."UserID",
        "ru"."UserName",       
        "r"."CalendarID",
         "r"."TimeZoneID"
    FROM "RoleUsersCTE" AS "r"
    LEFT JOIN "RoleUsers" AS "ru"
        ON "ru"."ID" = "r"."ID";
$$
LANGUAGE SQL;