CREATE FUNCTION [CalendarAddWorkingDaysToDate]
(
	@StartDateTime datetime,
	@Offset float,
	@CalendarID int
)
RETURNS datetime AS
BEGIN
	DECLARE @ResultDate datetime;
	DECLARE @QuantNumber int;

	DECLARE @QuantsInDay int
	SELECT @QuantsInDay = floor(ct.HoursInDay * 4)
	FROM CalendarSettings cs WITH(NOLOCK)
	INNER JOIN CalendarTypes ct with(nolock)
		ON ct.ID = cs.CalendarTypeID
	WHERE cs.CalendarID = @CalendarID;
	
	SELECT TOP 1 @QuantNumber = QuantNumber + floor(@Offset * @QuantsInDay) + Type
	FROM CalendarQuants WITH(NOLOCK)
	WHERE ID = @CalendarID
	AND StartTime <= @StartDateTime
	ORDER BY StartTime DESC;
	
	SELECT TOP 1 @ResultDate = StartTime
	FROM CalendarQuants WITH(NOLOCK)
	WHERE QuantNumber = @QuantNumber 
	AND ID = @CalendarID  
	AND Type = 0;

	RETURN @ResultDate;
END;
