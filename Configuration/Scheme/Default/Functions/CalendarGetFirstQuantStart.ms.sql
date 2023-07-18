CREATE FUNCTION [CalendarGetFirstQuantStart]
(
	@StartDateTime datetime,
	@Offset int,
	@CalendarID int
)
RETURNS datetime
AS
BEGIN
	DECLARE @QuantsInDay int
	SELECT @QuantsInDay = floor(ct.HoursInDay * 4)
	FROM CalendarSettings cs with(nolock)
	INNER JOIN CalendarTypes ct with(nolock)
		ON ct.ID = cs.CalendarTypeID
	WHERE cs.CalendarID = @CalendarID

	DECLARE @FirstQuantStart datetime
	DECLARE @DateFirstQuantNumber datetime
	SELECT TOP(1) @DateFirstQuantNumber = QuantNumber
	FROM [dbo].[CalendarQuants] WITH (NOLOCK)
	WHERE ID = @CalendarID
		AND StartTime >= CAST(CAST(@StartDateTime AS DATE) AS DATETIME)
		AND Type = 0
	ORDER BY StartTime

	SELECT TOP(1) @FirstQuantStart = StartTime
	FROM [dbo].[CalendarQuants] WITH (NOLOCK)
	WHERE ID = @CalendarID
		AND QuantNumber >= @DateFirstQuantNumber + (@QuantsInDay * @Offset )
		AND Type = 0
	ORDER BY QuantNumber

	IF @FirstQuantStart IS NULL
	BEGIN
		RETURN @StartDateTime 
	END
	RETURN @FirstQuantStart
END;