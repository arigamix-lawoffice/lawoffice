DO $$
declare zoneID int = 0;
BEGIN
update "Roles"
set 
	"TimeZoneID" = t."ID",
	"TimeZoneShortName" = t."ShortName",
	"TimeZoneUtcOffsetMinutes" = t."UtcOffsetMinutes",
	"TimeZoneCodeName" = t."CodeName"
from 
	(select 
		"tz"."ID", 
		"tz"."ShortName",
		"tz"."UtcOffsetMinutes",
		"tz"."CodeName"
	from "TimeZones" "tz"
	where "tz"."ID" = zoneID
	union all
	select 
		"dz"."ZoneID" as "ID", 
		"dz"."ShortName",
		"dz"."UtcOffsetMinutes",
		"dz"."CodeName"
	from "DefaultTimeZone" "dz"
	where "dz"."ZoneID" = zoneID) "t"
	where "TypeID" = 5 or "TypeID" = 6;
END $$;