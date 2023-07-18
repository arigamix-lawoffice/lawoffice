CREATE FUNCTION [CalendarGetDayOfWeek]
(
	@StartDateTime datetime
)
RETURNS INT
AS
BEGIN
	RETURN (((DATEPART(dw, @StartDateTime) + @@DATEFIRST - 2) % 7) + 1)
END