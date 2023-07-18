CREATE FUNCTION "CalendarGetDayOfWeek"
(
	date_time timestamptz
)
RETURNS int
AS $$
	SELECT extract(isodow FROM date_time)::int;
$$
LANGUAGE SQL;