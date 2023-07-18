CREATE FUNCTION "CalendarAddWorkingDaysToDate"
(
	date_time timestamptz,
	days_to_add float,
	calendar_id int
)
RETURNS timestamptz AS $$
DECLARE
	result_date timestamptz;
	quant_number int;
	quants_in_day int;
BEGIN
	SELECT floor("ct"."HoursInDay" * 4)
	INTO quants_in_day
	FROM "CalendarSettings" AS "cs"
	INNER JOIN "CalendarTypes" AS "ct" 
		ON "ct"."ID" = "cs"."CalendarTypeID"
	WHERE "cs"."CalendarID" = calendar_id;

	SELECT "QuantNumber" + floor(days_to_add * quants_in_day) + "Type"::int
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
