CREATE FUNCTION "CalendarAddWorkingDaysToDateExact"
(
	date_time timestamptz,
	days_to_add int,
	calendar_id int
)
RETURNS timestamptz AS $$
DECLARE
	result_date timestamptz;
BEGIN
	SELECT "t1"."dt"
	INTO result_date
	FROM (
		SELECT "t"."dt", row_number() OVER (ORDER BY "t"."dt") AS "rn"
		FROM (
			SELECT DISTINCT date_trunc('day', "q"."StartTime") AS "dt"
			FROM "CalendarQuants" AS "q"
			WHERE  "q"."ID" = calendar_id
				AND "q"."StartTime" BETWEEN date_time AND date_time + make_interval(days => days_to_add * 3 + 14)
				AND "q"."Type" = FALSE) AS "t"
		) AS "t1"
	WHERE "t1"."rn" = days_to_add + 1
	LIMIT 1;
	RETURN result_date;
END; $$
LANGUAGE PLPGSQL;
