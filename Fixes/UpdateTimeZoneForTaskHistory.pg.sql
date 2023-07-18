DO $$
declare zoneID int = 0;
BEGIN
update "TaskHistory"
set 
	"TimeZoneID" = "t"."ID",
	"TimeZoneUtcOffsetMinutes" = "t"."UtcOffsetMinutes"
from 
	(select 
		"tz"."ID", 
		tz."UtcOffsetMinutes"
	from "TimeZones" "tz"
	where "tz"."ID" = zoneID
	union all
	select 
		dz."ZoneID" as "ID", 
		dz."UtcOffsetMinutes"
	from "DefaultTimeZone" "dz"
	where "dz"."ZoneID" = zoneID) "t";
END $$;