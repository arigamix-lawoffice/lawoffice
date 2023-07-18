declare @offsetMinutes int;
set @offsetMinutes = 180;

update [CalendarSettings]
set 
    [LunchEnd] = DATEADD(minute, @offsetMinutes, [LunchEnd]),
    [LunchStart] = DATEADD(minute, @offsetMinutes, [LunchStart]),
    [WorkDayEnd] = DATEADD(minute, @offsetMinutes, [WorkDayEnd]),
    [WorkDayStart] = DATEADD(minute, @offsetMinutes, [WorkDayStart])

update CalendarExclusions
set 
	[EndTime] = DATEADD(minute, @offsetMinutes, [EndTime]),
	[StartTime] = DATEADD(minute, @offsetMinutes, [StartTime])