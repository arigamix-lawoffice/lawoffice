declare @ZoneID int;
set @ZoneID = 0;

update [Roles]
set 
	[TimeZoneID] = t.[ID],
	[TimeZoneShortName] = t.[ShortName],
	[TimeZoneUtcOffsetMinutes] = t.[UtcOffsetMinutes],
	[TimeZoneCodeName] = t.[CodeName]
from 
	(select 
		tz.[ID], 
		tz.[ShortName],
		tz.[UtcOffsetMinutes],
		tz.[CodeName]
	from [TimeZones] tz with(nolock)
	where tz.[ID] = @ZoneID
	union all
	select 
		dz.[ZoneID] as [ID], 
		dz.[ShortName],
		dz.[UtcOffsetMinutes],
		dz.[CodeName]
	from [DefaultTimeZone] dz with(nolock)
	where dz.[ZoneID] = @ZoneID) t
where TypeID = 5 or TypeID = 6
