CREATE FUNCTION "Localize"
(
	name text,
	culture varchar(8)
)
RETURNS text
RETURNS NULL ON NULL INPUT
AS $$
DECLARE
	result varchar;
BEGIN
	IF left(name, 1) <> '$' THEN
		RETURN name;
	END IF;

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
	WHERE "le"."Name" = right(name, length(name) - 1)
		AND "le"."Overridden" = false
	LIMIT 1;

	RETURN COALESCE(result, name);
END; $$
LANGUAGE PLPGSQL;