CREATE FUNCTION "CalendarIsWorkTime"
(
	date_time timestamptz,
	calendar_id int
)
RETURNS bool
AS $$
DECLARE
	quant_type bool;
BEGIN
	SELECT "Type"
	INTO quant_type
	FROM "CalendarQuants"
	WHERE "ID" = calendar_id
		AND "StartTime" <= date_time
	ORDER BY "StartTime" DESC
	LIMIT 1;
	
	IF quant_type = false THEN
		RETURN true;
	END IF;
	
	-- true or NULL
	RETURN false;
END; $$
LANGUAGE PLPGSQL;