CREATE FUNCTION [CalendarAddWorkingDaysToDateExact]
(
		@StartDateTime datetime,
		@Interval int,
		@CalendarID int
)
RETURNS datetime AS
BEGIN
	DECLARE @ResultDate datetime
		SELECT TOP 1 @ResultDate = t1.dt
		FROM
		(
			SELECT
			t.dt,
			row_number() OVER (ORDER BY t.dt) AS rn
			FROM
			(
				SELECT DISTINCT CONVERT(date, q.StartTime) AS dt
				FROM CalendarQuants q WITH(nolock)
				WHERE q.ID = @CalendarID
					AND q.StartTime BETWEEN @StartDateTime
					AND dateadd(day, @Interval * 3 + 14, @StartDateTime)
					AND q.Type = 0
			) t
		) t1
		WHERE t1.rn = @Interval + 1
	RETURN @ResultDate
END;
