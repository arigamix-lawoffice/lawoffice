CREATE FUNCTION "CalendarAddWorkQuants" 
(
	date_time timestamptz,
	quants_to_add bigint,
	calendar_id int
)
RETURNS timestamptz AS $$
DECLARE
	result_date timestamptz;
	quant_number int;
BEGIN
	SELECT "QuantNumber" + quants_to_add + "Type"::int
	INTO quant_number
	FROM "CalendarQuants"
	WHERE "ID" = calendar_id
	AND "StartTime" <= date_time
	ORDER BY "StartTime" DESC
	LIMIT 1;
		
	SELECT "StartTime"
	INTO result_date
	FROM "CalendarQuants"
	WHERE "QuantNumber" = quant_number
	AND "ID" = calendar_id
	AND "Type" = false
	LIMIT 1;

	RETURN result_date;
END; $$
LANGUAGE PLPGSQL;
