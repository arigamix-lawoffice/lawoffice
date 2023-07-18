CREATE FUNCTION "DropFunction"
(
	name text
)
RETURNS int AS $$
DECLARE
	query text;
	result int;
BEGIN
	SELECT count(*)::int, string_agg('DROP FUNCTION ' || oid::regprocedure::text, ';')
	FROM pg_proc
	WHERE proname = name AND pg_function_is_visible(oid)
	INTO result, query;

	IF result > 0 THEN
		EXECUTE query;
	END IF;

	RETURN result;
END; $$
LANGUAGE PLPGSQL;