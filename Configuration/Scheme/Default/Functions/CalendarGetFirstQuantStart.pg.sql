CREATE FUNCTION "CalendarGetFirstQuantStart"
(
	date_time timestamptz,
	days_to_add int,
	calendar_id int
)
RETURNS timestamptz
AS $$
DECLARE
	first_quant_number bigint;
	first_quant_start timestamptz;
	quants_in_day int;
BEGIN
	SELECT floor("ct"."HoursInDay" * 4)
	INTO quants_in_day
	FROM "CalendarSettings" AS "cs" 
	INNER JOIN "CalendarTypes" AS "ct" 
		ON "ct"."ID" = "cs"."CalendarTypeID"
	WHERE "cs"."CalendarID" = calendar_id;

	SELECT "QuantNumber"
	INTO first_quant_number
	FROM "CalendarQuants"
	WHERE "ID" = calendar_id
		AND "StartTime" >= date_trunc('day', date_time)
		AND "Type" = false
	ORDER BY "StartTime"
	LIMIT 1;

	SELECT "StartTime"
	INTO first_quant_start
	FROM "CalendarQuants"
	WHERE "ID" = calendar_id
		AND "QuantNumber" >= first_quant_number + (quants_in_day * days_to_add)
		AND "Type" = false
	ORDER BY "QuantNumber"
	LIMIT 1;

	IF first_quant_start IS NULL THEN
		RETURN date_time;
	END IF;
	RETURN first_quant_start;
END; $$
LANGUAGE PLPGSQL;