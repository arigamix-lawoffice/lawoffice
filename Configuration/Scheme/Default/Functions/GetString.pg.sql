CREATE FUNCTION "GetString"
(
	name text,
	culture varchar(8)
)
RETURNS text
RETURNS NULL ON NULL INPUT
STABLE
AS $$
DECLARE
	result text;
BEGIN
	SELECT "ls"."Value"
	INTO result
	FROM "LocalizationEntries" AS "le"
	INNER JOIN LATERAL (
		SELECT "ls"."Value"
		FROM "LocalizationStrings" AS "ls"
		WHERE "ls"."EntryID" = "le"."ID"
			AND "ls"."Culture" = culture
		LIMIT 1
	) AS "ls" ON true
	WHERE "le"."Name" = name
		AND "le"."Overridden" = false
	LIMIT 1;

	return COALESCE(result, '$' || name);
END; $$
LANGUAGE PLPGSQL;