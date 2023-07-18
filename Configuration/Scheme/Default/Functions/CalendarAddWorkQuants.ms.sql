CREATE FUNCTION [CalendarAddWorkQuants]
(
	@StartDate datetime,
	@Quants int,
	@CalendarID int
)
RETURNS datetime AS
BEGIN
	DECLARE @ResultDate datetime;
	DECLARE @QuantNumber int;
	
	SELECT TOP 1 @QuantNumber = QuantNumber + @Quants + Type
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
END
