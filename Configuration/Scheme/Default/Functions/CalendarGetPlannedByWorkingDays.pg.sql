CREATE FUNCTION "CalendarGetPlannedByWorkingDays"
(
	calendar_id int,
	hours_in_day float,
	date_time timestamptz,
	planned_working_days float
)
RETURNS timestamptz AS $$
DECLARE
	result_date timestamptz;
	quant_number int;
BEGIN
	SELECT "QuantNumber" + floor(hours_in_day * 4 * planned_working_days) + "Type"::int
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
