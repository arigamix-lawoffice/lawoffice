CREATE FUNCTION [CalendarGetLastQuantEnd]
(
	@StartDateTime datetime,
	@Offset int,
	@CalendarID int
)
RETURNS datetime
AS
BEGIN
	DECLARE @FirstQuantStart datetime
	SELECT @FirstQuantStart = [dbo].[CalendarGetFirstQuantStart](@StartDateTime , @Offset, @CalendarID)
	DECLARE @NextDay datetime
	SELECT @NextDay = DATEADD(Day, 1,CAST(CAST(@FirstQuantStart AS DATE) AS DATETIME))

    	DECLARE @LastQuantEnd datetime
	SELECT TOP(1) @LastQuantEnd = EndTime
	FROM [dbo].[CalendarQuants] WITH (NOLOCK)
	WHERE ID = @CalendarID
		AND EndTime >= @FirstQuantStart
		AND EndTime < @NextDay
		AND Type = 0
	ORDER BY EndTime DESC

	IF @LastQuantEnd IS NULL
	BEGIN
		RETURN @StartDateTime 
	END
	RETURN @LastQuantEnd 
END;