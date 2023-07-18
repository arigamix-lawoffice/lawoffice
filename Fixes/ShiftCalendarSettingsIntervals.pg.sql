DO $$
declare offsetMinutes int = 180;
BEGIN
update "CalendarSettings"
set 
    "LunchEnd" = "LunchEnd" + offsetMinutes * interval '1 minute',
    "LunchStart" = "LunchStart" + offsetMinutes * interval '1 minute',
    "WorkDayEnd" = "WorkDayEnd" + offsetMinutes * interval '1 minute',
    "WorkDayStart" = "WorkDayStart" + offsetMinutes * interval '1 minute';

update "CalendarExclusions"
set 
	"EndTime" = "EndTime" + offsetMinutes * interval '1 minute',
	"StartTime" = "StartTime" + offsetMinutes * interval '1 minute';
END $$;