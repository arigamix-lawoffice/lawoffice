CREATE FUNCTION [CalendarGetPlannedByWorkingDays]
(
	@CalendarID int,
	@HoursInDay float,
	@StartDate datetime,
	@PlannedWorkingDays float
)
RETURNS datetime AS
BEGIN
	DECLARE @ResultDate datetime;
	DECLARE @QuantNumber int;

	SELECT TOP 1 @QuantNumber = QuantNumber + floor(@HoursInDay * 4 * @PlannedWorkingDays) + Type
	FROM CalendarQuants WITH(NOLOCK)
	WHERE ID = @CalendarID 
	AND StartTime <= @StartDate
	ORDER BY StartTime DESC;
	
	SELECT TOP 1 @ResultDate = StartTime
	FROM CalendarQuants WITH(NOLOCK)
	WHERE QuantNumber = @QuantNumber 
	AND ID = @CalendarID  
	AND Type = 0;

	RETURN @ResultDate;
END; 