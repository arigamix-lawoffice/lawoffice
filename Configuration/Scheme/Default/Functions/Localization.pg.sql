CREATE FUNCTION "Localization"
(
	name text,
	culture varchar(8)
)
RETURNS TABLE
(
	"Value" text
)
AS $$
	SELECT COALESCE("ls"."Value", name) AS "Value"
	FROM (SELECT NULL AS "Temp") AS "t"
	LEFT JOIN LATERAL (
		SELECT "le"."ID"
		FROM "LocalizationEntries" AS "le"
		WHERE left(name, 1) = '$'
		    AND "le"."Name" = right(name, CASE WHEN length(name) > 0 THEN length(name) - 1 ELSE 0 END)
		    AND "le"."Overridden" = false
		LIMIT 1
	) AS "le" ON true
	LEFT JOIN LATERAL (
		SELECT "ls"."Value"
		FROM "LocalizationStrings" AS "ls"
		WHERE "ls"."EntryID" = "le"."ID"
			AND "ls"."Culture" = culture
		LIMIT 1
	) AS "ls" ON true
	LIMIT 1
$$
LANGUAGE SQL;