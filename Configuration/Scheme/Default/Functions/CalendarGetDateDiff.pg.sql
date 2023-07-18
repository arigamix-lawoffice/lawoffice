CREATE FUNCTION "CalendarGetDateDiff"
(
	first_date timestamptz,
	second_date timestamptz,
	calendar_id int
)
RETURNS bigint
AS $$
DECLARE
	first_quant bigint;
	second_quant bigint;
BEGIN
	SELECT "QuantNumber"
	INTO first_quant
	FROM "CalendarQuants"
	WHERE  "ID" = calendar_id
		AND "StartTime" <= first_date
	ORDER BY "StartTime" DESC
	LIMIT 1;
	
	SELECT "QuantNumber"
	INTO second_quant
	FROM "CalendarQuants"
	WHERE "ID" = calendar_id
		AND "StartTime" <= second_date
	ORDER BY "StartTime" DESC
	LIMIT 1;
	
	IF first_quant IS NULL OR second_quant IS NULL THEN
		RETURN 0;
	END IF;
	RETURN second_quant - first_quant;
END; $$
LANGUAGE PLPGSQL;